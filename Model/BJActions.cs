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

    static class BJActions
    {
        //private GameState _state;
        public static void hit(BJLoopContext context)
        {

            Console.WriteLine("Action: hit");
            context.GameState.Deck.draw(context.GameState.Player.Hand);
            context.BJLoop = new BJPlayerTurn();
        }

        public static void stand(BJLoopContext context)
        {
            Console.WriteLine("Action: stand");
            context.GameState.Current_Player += 1;
            context.BJLoop = new BJPlayerTurn();
        }

        /*
        * dealer deals 2 card from dekk
        */
        public static void deal(BJLoopContext context)
        {
            while (!context.GameState.Round_Complete)
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
            context.BJLoop = new BJPlayerSplitTurn();
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
