using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Model
{

    class BJLoopContext {

        private GameState _state;
        private BJLoop _active_state;

        public BJLoopContext(ref GameState state, BJLoop active_state) {
            _active_state = active_state;
            _state = state;
            _active_state.action(this);
        }

        public void request()
        {
            _active_state.action(this);
        }

        public BJLoop BJLoop
        {
            get { return _active_state; }
            set 
            { 
                _active_state = value;

                Console.WriteLine("state changed to: " + _active_state.GetType().Name);
                _active_state.action(this);
            }
        }

        public int card_value(Hand hand)
        {
            List<Card> cards = hand.get_hand();
            cards.OrderByDescending(Card => Card.Value);
            int sum = 0;
            int num_aces = 0;

            foreach (Card c in hand.get_hand())
            {
                int val = (int)c.Value;
                if (val == 1)
                {
                    sum += 11;
                    ++num_aces;
                }
                else if (val >= 11)
                    sum += 10;
                else
                    sum += val;
            }

            // ugly aces
            while (sum > 21 && num_aces-- != 0)
            {
                sum -= 10;
            }

            return sum;
        }

        public BJLoop BJState
        {
            get { return _active_state; }
            private set { }
        }

        public GameState GameState
        {
            get { return _state; }
            private set { }
        }
    }

    abstract class BJLoop
    {
        public static int BLACKJACK = 21;
        public BJLoop() { }

        abstract public void action(BJLoopContext context);

    }

    class BJInit : BJLoop
    {
        public BJInit()
        {
        }

        public override void action(BJLoopContext context)
        {
            // Deal 2 cards to all players and dealer
            context.BJLoop = new BJStart();
        }

    }
    class BJStart : BJLoop
    {
        public BJStart()
        {
        }

        public override void action(BJLoopContext context)
        {
            context.GameState.Deck.shuffle();
            context.BJLoop = new BJPlayerBet();
            //two cards lul
            BJActions.deal(context);
            BJActions.deal(context);
        }

    }
    class BJPlayerBet : BJLoop
    {
        public BJPlayerBet()
        { 
        }

        public override void action(BJLoopContext context)
        {
            // wait for all ppls to bet
            if (context.GameState.Round_Complete)
                context.BJLoop = new BJPlayerTurn();
        }
    }

    //checks game logic for a current player
    class BJPlayerTurn : BJLoop
    {
        public BJPlayerTurn()
        {
        }

        public override void action(BJLoopContext context)
        {

            if (context.GameState.Round_Complete)
                context.BJLoop = new BJDealerTurn();
            else {

                // debug
                context.GameState.debug_state();

                if(context.GameState.Player.can_split())
                {
                    context.BJLoop = new BJPlayerSplit();
                }
                else if (context.card_value(context.GameState.Player.Hand) >= BJLoop.BLACKJACK)
                {
                    if (!context.GameState.Player.Split)
                    {
                        context.GameState.Current_Player += 1;
                        context.BJLoop = new BJPlayerTurn();
                    }
                    else
                        context.BJLoop = new BJPlayerSplitTurn();
            
                }
                else
                {
                    context.BJLoop = new BJPlayerAction();
                }
            }
        }

    }

    // doesnt do anything really
    class BJPlayerAction : BJLoop
    {

        public BJPlayerAction()
        {
        }

        public override void action(BJLoopContext context)
        {
            Console.WriteLine("User action required....");
            context.GameState.Player.action(context);

        }

        //not used
        /*
        public void action(BJLoop context, BJLoop loop)
        {
            //goto state = loop;
        }*/

    }

    class BJPlayerSplit : BJLoop
    {
        public BJPlayerSplit()
        {}

        public override void action(BJLoopContext context)
        {
            context.GameState.Player.action(context);
        }
    }

    class BJPlayerSplitTurn : BJLoop
    {
        public BJPlayerSplitTurn()
        {}

        public override void action(BJLoopContext context)
        {
            context.GameState.Player.Current_Hand = 1;

            if (context.card_value(context.GameState.Player.Hand) >= BJLoop.BLACKJACK)
                {
                    context.GameState.Player.Current_Hand = 0;
                    context.GameState.Current_Player += 1;
                    context.BJLoop = new BJPlayerTurn();
                    
                }
        }
    }

    class BJDealerTurn : BJLoop
    {
        public BJDealerTurn()
        {
        }
        public override void action(BJLoopContext context)
        {
            //super smart dealer
            //int player_hand = context.card_value(context.GameState.Player.Hand);
            int dealer_hand = context.card_value(context.GameState.Dealer.Hand);

            while (dealer_hand < 17)
            {
                context.GameState.Deck.draw(context.GameState.Dealer.Hand);
                dealer_hand = context.card_value(context.GameState.Dealer.Hand);
               
                if (dealer_hand == BLACKJACK)
                {
                    Console.WriteLine("dealer wins");
                    context.BJLoop = new BJDealerWins();
                    return; //IDK
                }
            }

            context.BJLoop = new BJCalculateResults();
        }
    }

    class BJDealerWins : BJLoop
    {
        public BJDealerWins()
        {
           
        }
        public override void action(BJLoopContext context)
        {
            //wanted to do a for each in loop but it wouldnt let me
            if (context.GameState.Round_Complete)
                context.BJLoop = new BJEnd();
            else
            {
                context.GameState.Player.Wallet.Bet = 0;

                context.GameState.Current_Player += 1;
                context.BJLoop = new BJDealerWins();
            }        
        }
    }

    class BJCalculateResults : BJLoop
    {
        public BJCalculateResults()
        {

        }
        public override void action(BJLoopContext context)
        {
            // TODO
            // Make sure split hands are calculated correctly (bets)
            //

            if (context.GameState.Round_Complete)
                context.BJLoop = new BJEnd();
            else
            {
                int player_hand = context.card_value(context.GameState.Player.Hand);
                int dealer_hand = context.card_value(context.GameState.Dealer.Hand);
                // each player under 21 wins
                if (dealer_hand > 21)
                {
                    if (player_hand < 21)
                        context.GameState.Player.Wallet.Balance += context.GameState.Player.Wallet.Bet * 2;
                    else
                        context.GameState.Player.Wallet.Bet = 0;

                    if (context.GameState.Player.Split && (context.GameState.Player.Current_Hand != 1))
                        context.GameState.Player.Current_Hand += 1;
                    else
                    {
                        Console.WriteLine("Player has " + context.GameState.Player.Wallet.Balance);
                        context.GameState.Current_Player += 1;
                    }
                    context.BJLoop = new BJCalculateResults();
                }
                else
                {
                    // each player above dealer wins
                    if (player_hand > dealer_hand && player_hand <= 21)
                        context.GameState.Player.Wallet.Balance += context.GameState.Player.Wallet.Bet * 2;
                    else
                        context.GameState.Player.Wallet.Bet = 0;

                    if (context.GameState.Player.Split && (context.GameState.Player.Current_Hand != 1))
                        context.GameState.Player.Current_Hand += 1;
                    else
                    {
                        Console.WriteLine("Player has " + context.GameState.Player.Wallet.Balance);
                        context.GameState.Current_Player += 1;
                    }
                    context.BJLoop = new BJCalculateResults();
                }
            }
        }
    }

    class BJEnd : BJLoop
    {
        public BJEnd()
        {
        }

        public override void action(BJLoopContext context)
        {
            if (context.GameState.Round_Complete)
            {
                context.GameState.Dealer.Hand.discard_all();
                context.GameState.Deck.Trashpile.shuffle_back(context.GameState.Deck);
                context.GameState.Deck.shuffle();
                context.BJLoop = new BJStart();
            }
            else
            {
                context.GameState.Player.Current_Hand = 0;
                if(context.GameState.Player.Split)
                    context.GameState.Player.Split_Hand.discard_all();
                context.GameState.Player.Split = false;
                context.GameState.Player.Hand.discard_all();
                context.GameState.Current_Player += 1;
                context.BJLoop = new BJEnd();
            }
        }

    }

}
