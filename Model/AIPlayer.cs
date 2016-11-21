using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Model
{
    class AIPlayer : Player
    {
        public override void action(BJLoopContext context)
        {
            Type temp = context.BJLoop.GetType();

            if (temp == typeof(BJPlayerBet))
                AIOracle.bet(context);

            if (context.GameState.Player.can_split())
                AIOracle.split(context);
            else if (temp == typeof(BJPlayerAction) || temp == typeof(BJPlayerSplitTurn))
                AIOracle.hitstand(context);
        }
    }
}
