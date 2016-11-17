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

        public BJLoop BJState
        {
            get { return _active_state; }
            private set { }
        }

        public GameState GameState
        {
            get { return _state; }
            private set { ; }
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
            if (BJLogicHelper.round_complete(context.GameState))
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

            if (BJLogicHelper.round_complete(context.GameState))
                context.BJLoop = new BJDealerTurn();
            else {

                BJLogicHelper.debug_state(context.GameState);

                if (BJLogicHelper.cards_value(context.GameState.Player.Hand) >= BJLoop.BLACKJACK)
                {
                    if (context.GameState.Player.Split)
                        context.BJLoop = new BJPlayerSplitTurn();
                    else
                    {
                        context.GameState.Current_Player += 1;
                        context.BJLoop = new BJPlayerTurn();
                    }
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
            context.GameState.Player.action(context);
        }

    }

    class BJPlayerSplitTurn : BJLoop
    {
        public BJPlayerSplitTurn()
        {}

        public override void action(BJLoopContext context)
        {

            BJLogicHelper.debug_state(context.GameState);

            if (BJLogicHelper.cards_value(context.GameState.Player.Split_Hand) >= BJLoop.BLACKJACK)
                {
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
            int dealer_hand = BJLogicHelper.cards_value(context.GameState.Dealer.Hand);

            while (dealer_hand < 17)
            {
                context.GameState.Deck.draw(context.GameState.Dealer.Hand);
                dealer_hand = BJLogicHelper.cards_value(context.GameState.Dealer.Hand);
            }

            if (dealer_hand == BLACKJACK)
                context.BJLoop = new BJDealerWins();
            else
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
            // wanted to do a for each in loop but it wouldnt let me
            if (BJLogicHelper.round_complete(context.GameState))
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
        {}

        public override void action(BJLoopContext context)
        {
            BJLogicHelper.calc_results(context.GameState);
            context.BJLoop = new BJEnd();
        }
    }

    class BJEnd : BJLoop
    {
        public BJEnd()
        {}

        public override void action(BJLoopContext context)
        {
            BJLogicHelper.clean_up(context.GameState);
            context.BJLoop = new BJStart();
        }

    }

}
