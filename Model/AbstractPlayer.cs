using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Model
{
    abstract class AbstractPlayer
    {
        protected Hand[] hands = new Hand[2];
        protected int current_hand = 0;
        protected Wallet wallet = new Wallet();

        protected Boolean split;
        //private IIntelligence brains = null; AI or Human ? should AI be a player?

        /*
         * bankroll etc?
         * no need for action methods here..
         * display name
         * need to interface the hand methods... which is kinda stoopid
         * what is a player?
         * could be only the AI / Human case switch
         * /

        /* pass AI reference ?*/
        protected AbstractPlayer(/*IIntelligence brains*/)
        {
            hands[0] = new Hand();
            hands[1] = null;
            /* this.AI = brains */
        }

        public Hand Hand
        {
            get
            {
                if (split)
                    return hands[current_hand];
                else
                    return hands[0];
            }
            private set { }
        }

        public int Current_Hand
        {
            get { return current_hand; }
            set
            {
                if (value <= 1)
                    current_hand = value;
            }
        }
        public Wallet Wallet
        {
            get { return this.wallet; }
            private set { }
        }

        public Boolean Split
        {
            get { return split; }
            set
            {
                split = value;
                if (split)
                {
                    hands[1] = new Hand();
                    hands[0].draw(hands[1]);
                }
                else
                    hands[1] = null;
            }
        }

        public Hand Split_Hand
        {
            get
            {
                if (hands[1] != null)
                    return hands[1];
                else
                    return new Hand();
            }
            private set { }
        }
        public Boolean can_split()
        {
            if (!split && Hand.get_hand().Count == 2 && Wallet.Bet * 2 <= Wallet.Balance)
                return Hand.get_hand()[0].Value == Hand.get_hand()[1].Value;
            return false;
        }

        public abstract void action(BJLoopContext context);
    }
}
