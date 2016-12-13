/*
 * Main game ViewModel
 * Holds state information and game logic objects
 * Also contains all commands while in an active Blackjack session
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Blackjack.ViewModel
{
    using Commands;
    using Model;
    using View;
    /*run this in program mainloop*/
    public class BlackJack
    {
        /**
         * holds players, dealer, deck EVERYTHING
         */
        private GameState _state = new GameState(2/*numplayers*/); // WARNING! WARNING! WTF IS HAPPENING!?
        private BJLoopContext _game_loop_context;

        /*
         * rules, functions
         * run_helpers
         * token passers that is, turn control
         * has_token checks
         * catch events/actions?
         * timers? 
         */

        private HandView _hand1 = null;
        private HandView _hand2 = null;
        public BlackJack()
        {
            _BJHit = new BJHit(this);
            _BJStand = new BJStand(this);
            _BJBet = new BJBet(this);
            _BJSplit = new BJSplit(this);
            _game_loop_context = new BJLoopContext(ref _state, new BJInit());

            _hand1 = new HandView(ref GameState.Players[0]);
            _hand2 = new HandView(ref GameState.Players[1]);
        }

        public GameState GameState
        {
            get { return _state; }
            private set {; }
        }

        public HandView Hand1
        {
            get { return _hand1; }
        }

        public HandView Hand2
        {
            get { return _hand2; }
        }

        public BJHit _BJHit
        {
            get;
            private set;
        }

        public BJStand _BJStand
        {
            get;
            private set;
        }

        public BJSplit _BJSplit
        {
            get;
            private set;
        }

        public BJBet _BJBet
        {
            get;
            private set;
        }

        public Boolean can_action
        {
            get
            {
                BJLoop temp = _game_loop_context.BJLoop;

                if (temp.GetType() == typeof(BJPlayerAction) || temp.GetType() == typeof(BJPlayerSplitTurn))
                    return true;

                return false;
            }
            private set
            {
                can_action = value;
            }

        }

        public Boolean can_split
        {
            get
            {
                BJLoop temp = _game_loop_context.BJLoop;

                if (_game_loop_context.GameState.Player.can_split() && temp.GetType() == typeof(BJPlayerAction))
                    return true;

                return false;
            }
            private set
            {
                can_action = value;
            }
        }

        public Boolean can_bet
        {
            /*
             * ask gameloop object
             * possibly use polymorphism switch-case on BJCOMMANDS if
             * we can make it polymorphic
             */
            get
            {
                BJLoop temp = _game_loop_context.BJLoop;

                if (temp.GetType() == typeof(BJPlayerBet))
                    return true;

                return false;
            }
            private set
            {
                can_bet = value;
            }

        }
        
        public void hit()
        {
            BJActions.hit(_game_loop_context);
        }

        public void stand()
        {
            BJActions.stand(_game_loop_context);
        }

        public void bet()
        {
            BJActions.bet(_game_loop_context, 100);
        }

        public void split()
        {
            BJActions.split(_game_loop_context);
        }

        private void UpdateGUI()
        {
          //  update_hand();
        }
    }
}
