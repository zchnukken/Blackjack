using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Model
{
    /*public enum UserActions
    {
        bet,
        draw,
        stand,
        save,
        quit,
        emote
    };*/

    public static class BJActions
    {
        //private GameState _state;
        public static void hit(BJLoopContext context)
        {

            Console.WriteLine("Action: hit");
            if (context.BJState.GetType() == typeof(BJPlayerSplitTurn))
            {
                context.GameState.Deck.draw(context.GameState.Player.Split_Hand);
                context.BJLoop = new BJPlayerSplitTurn();
            }
            else
            {
                context.GameState.Deck.draw(context.GameState.Player.Hand);
                context.BJLoop = new BJPlayerTurn();
            }
            BJLogicHelper.debug_state(context.GameState);
        }

        public static void stand(BJLoopContext context)
        {
            Console.WriteLine("Action: stand");
            // if split is false OR we come from a split turn 
            if (!context.GameState.Player.Split || context.BJLoop.GetType() == typeof(BJPlayerSplitTurn))
            {
                context.GameState.Current_Player += 1;
                context.BJLoop = new BJPlayerTurn();
            }
            else if (context.GameState.Player.Split) // should only be else
                context.BJLoop = new BJPlayerSplitTurn();
        }

        /*
        * dealer deals 2 card from dekk
        */
        public static void deal(BJLoopContext context)
        {
            while (!BJLogicHelper.round_complete(context.GameState))
            {
                context.GameState.Deck.draw(context.GameState.Player.Hand);
                context.GameState.Current_Player += 1;
            } 

            context.GameState.Deck.draw(context.GameState.Dealer.Hand);
        }

        public static void split(BJLoopContext context)
        {
            Console.WriteLine("Action: split");
            context.GameState.Player.Split = true;
            context.GameState.Player.Wallet.Bet += context.GameState.Player.Wallet.Bet;
            context.GameState.Deck.draw(context.GameState.Player.Hand);
            context.GameState.Deck.draw(context.GameState.Player.Split_Hand);
            context.BJLoop = new BJPlayerTurn();
        }

        public static void bet(BJLoopContext context, int coin = 100)
        {
            //if player can bet
            if (context.GameState.Player.Wallet.Balance >= 100)
                context.GameState.Player.Wallet.Bet = coin;
            else
                ;//kick player from current game
            context.GameState.Current_Player += 1;
            context.BJLoop = new BJPlayerBet();
        }
    }
}
