using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.ViewModel
{
    using Commands;
    public class BlackJackMenu
    {
        private BJStartGame _start_game;
        private MainWindow _win;

        public BlackJackMenu(MainWindow window)
        {
            _win = window;
            _start_game = new BJStartGame(this);
        }

        public BJStartGame _StartGame
        {
            get { return _start_game; }
            private set { ;}
        }

        public Boolean can_start
        {
            get { return true; }
            private set { ;}
        }

        public void startGame()
        {
            _win.DataContext = new BlackJack();
        }
    }
}
