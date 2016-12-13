using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Model
{
    public static class AIOracle
    {
        public static void hitstand(BJLoopContext context)
        {
            int player_hand = BJLogicHelper.cards_value(context.GameState.Player.Hand);

            if(context.BJLoop.GetType() == typeof(BJPlayerSplitTurn))
                player_hand = BJLogicHelper.cards_value(context.GameState.Player.Split_Hand);

            if (player_hand < 17)
                BJActions.hit(context);
            else
                BJActions.stand(context);
        }

        public static void bet(BJLoopContext context)
        {
            BJActions.bet(context);
        }

        public static void split(BJLoopContext context)
        {
            BJActions.split(context);
        }
    }
}
