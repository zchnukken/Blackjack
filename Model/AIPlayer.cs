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
            AIOracle.prophesise(context);
        }
    }
}
