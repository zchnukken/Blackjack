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
    /*run this in program mainloop*/
    class BlackJack
    {
        /**
         * holds players, dealer, deck EVERYTHING
         */
        private GameState state = new GameState(2/*numplayers*/); // WARNING! WARNING! WTF IS HAPPENING!?
        private BJLoopContext _game_loop_context;

        /*
         * rules, functions
         * run_helpers
         * token passers that is, turn control
         * has_token checks
         * catch events/actions?
         * timers? 
         */

        public BlackJack()
        {
            _BJHit = new BJHit(this);
            _BJStand = new BJStand(this);
            _BJBet = new BJBet(this);
            _BJSplit = new BJSplit(this);
            _game_loop_context = new BJLoopContext(ref state, new BJInit());
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

                if (temp.GetType() == typeof(BJPlayerAction) || temp.GetType() == typeof(BJPlayerSplit) || temp.GetType() == typeof(BJPlayerSplitTurn))
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

                if (temp.GetType() == typeof(BJPlayerSplit))
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
            Console.WriteLine("############ DRAW CARD ########");
            BJActions.hit(_game_loop_context);
            
        }

        public void stand()
        {
            Console.WriteLine("############### STANDING ############");
            BJActions.stand(_game_loop_context);
        }

        public void bet()
        {
            Console.WriteLine("############### BETTING ############");
            BJActions.bet(_game_loop_context, 100);
        }

        public void split()
        {
            Console.WriteLine("############### SPLITTING ###############");
            BJActions.split(_game_loop_context);
        }

        private void UpdateGUI()
        {
          //  update_hand();
        }
        /*
        private void update_hand()
        {
            //PlayerHand.Children.Clear();

            foreach (Card c in state.get_player().get_hand().get_hand())
            {
                BitmapImage moo = new BitmapImage();
                moo.BeginInit();
                moo.UriSource = CardImageLoader.card_to_uri(c);
                moo.EndInit();

                Image img = new Image();
                img.Height = 150;
                img.Width = 100;
                img.Margin = new System.Windows.Thickness(-50, 0, 0, 0);
                img.Source = moo;

               // PlayerHand.Children.Add(img);
            }
        }
         * */
    }
}
