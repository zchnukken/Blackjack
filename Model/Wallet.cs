using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Model
{
    class Wallet
    {
        private int _balance;
        private int _bet;
        public Wallet(int balance = 1000)
        {
            _balance = balance;
            _bet = 0;
        }

        public int Balance
        {
            get { return _balance;}
            set { _balance = value; }
        }

        public int Bet
        {
            get { return _bet; }
            set {
                    if (value <= _balance)
                    {
                        _balance -= value;
                        _bet = value;
                    }
                }
        }

    }
}
