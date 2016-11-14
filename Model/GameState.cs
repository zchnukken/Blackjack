using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Model
{
    //active player
    class GameState
    {
        private Deck _deck = new Deck(Deck.add_deck());
        private BJDealer _dealer = new BJDealer();
 

        private Player[] _players; // loool

        private int _current_player = 0;
        private int _number_of_players;


        public GameState(int number_of_players)
        {
            _number_of_players = number_of_players;
            //initialize array of players
            _players = new Player[_number_of_players];

            for (int i = 0; i < _number_of_players; ++i)
                _players[i] = new Player();
        }

        public int Current_Player
        {
            get { return _current_player; }
            set 
            {
                if (value <= Number_Of_Players)
                    _current_player = value; 
            }
        }

        public Boolean Round_Complete
        {
            get
            {
                Boolean res = _number_of_players == _current_player;
                if ( res)
                    Current_Player = 0;
                return res;
            }
            private set{;}
        }

        public int Number_Of_Players
        {
            get { return _number_of_players; }
            private set {;}
        }

        public Deck Deck
        {
            get { return _deck; }
            private set { }
        }

        public Player Player
        {
            
            get {
                return _players.ElementAt(Current_Player); }
            private set {}
        }

        public Player[] Players
        {
            get { return _players; }
            private set { ;} 
        }

        public BJDealer Dealer
        {
            get { return _dealer; }
            private set { }
        }

        public void debug_state()
        {
            Console.WriteLine("Current player: " + Current_Player);
            Console.WriteLine("Player has: " + Player.Wallet.Balance);
            Console.WriteLine("Player bet: " + Player.Wallet.Bet);
            Console.WriteLine("Player split: " + Player.Split);
            Console.WriteLine("Player hand: \n{\n" + Player.Hand.ToString() +"}");
            Console.WriteLine("Player split hand: \n{\n" + Player.Split_Hand.ToString()+"}");
            Console.WriteLine("Dealer hand: \n{\n" + Dealer.Hand.ToString()+"}");

        }


    }
}
