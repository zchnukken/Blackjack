using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Model
{
    public class Wallet : INotifyPropertyChanged
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
            set { _balance = value;
                NotifyPropertyChanged();
            }
        }

        public int Bet
        {
            get { return _bet; }
            set {
                    if (value <= _balance)
                    {
                        Balance -= value;
                        _bet = value;
                        NotifyPropertyChanged();
                    }
                }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
