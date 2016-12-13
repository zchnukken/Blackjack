using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Model
{
    public abstract class AbstractPlayer
    {
 
        protected Hand _hand = null;
        protected Hand _split_hand = null;
  
        protected Wallet wallet = new Wallet();

        protected Boolean _split;
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
            _hand = new Hand();
            /* this.AI = brains */
        }

        public Hand Hand
        {
            get
            { return _hand; }
            private set { }
        }

        public Wallet Wallet
        {
            get { return this.wallet; }
            private set { }
        }

        public Boolean Split
        {
            get { return _split; }
            set
            {
                _split = value;
                if (_split)
                {
                    _split_hand = new Hand();
                    _hand.draw(_split_hand);
                }
                else
                    _split_hand = null;
            }
        }

        public Hand Split_Hand
        {
            get
            {
                if (_split_hand != null)
                    return _split_hand;
                else
                    return new Hand();
            }
            private set { }
        }

        public Boolean can_split()
        {
            if (!_split && Hand.get_hand().Count == 2 && Wallet.Bet * 2 <= Wallet.Balance)
                return Hand.get_hand()[0].Value == Hand.get_hand()[1].Value;
            return false;
        }

        public abstract void action(BJLoopContext context);
    }
}
