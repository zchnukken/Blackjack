using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Model
{
    public class BJDealer
    {
            /*
             * what is a dealer?
             * Hand() hand;
             * do I hold de deck.... neeh ?=
             * 
             * 
             */
        private Hand _dealer_hand = new Hand();
        public Hand Hand
        {
            get { return _dealer_hand; }
            private set { }
        }
    }
}
