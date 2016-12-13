using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Model
{
    public static class BJLogicHelper
    {
        /*transform a hand value into a blackjack hand value*/
        public static int cards_value(Hand hand)
        {
            int sum = 0;
            int num_aces = 0;

            foreach (Card c in hand.get_hand())
            {
                int val = (int)c.Value;
                if (val == 1)
                {
                    sum += 11;
                    ++num_aces;
                }
                else if (val >= 11)
                    sum += 10;
                else
                    sum += val;
            }

            while (sum > 21 && num_aces-- != 0)
            {
                sum -= 10;
            }

            return sum;
        }


        /*check if a round is complete and set counter to 0 when it's done*/
        public static Boolean round_complete(GameState state)
        {
                Boolean res = state.Number_Of_Players == state.Current_Player;
                if (res)
                    state.Current_Player = 0;
                return res;
        }

        /*clean up all players' hands and the dealer's hand and shuffle it back into the deck*/
        public static void clean_up(GameState state)
        {

            foreach (Player p in state.Players)
            {
                p.Hand.discard_all();
                p.Split_Hand.discard_all();
                p.Split = false;
            }

            state.Dealer.Hand.discard_all();
            state.Deck.Trashpile.shuffle_back(state.Deck);
            state.Deck.shuffle();
        }


        public static void calc_results(GameState state)
        {
            int dealer_hand = cards_value(state.Dealer.Hand);
            foreach (Player p in state.Players)
            {
                int player_hand = cards_value(p.Hand);
                int split_hand = cards_value(p.Split_Hand);

                if (dealer_hand < 21) // dealer did not bust
                {
                    if (p.Split)
                    {
                        //win split hand == half of the doubled hand
                        if (split_hand <= 21 && split_hand > dealer_hand)
                            p.Wallet.Balance += p.Wallet.Bet;

                        //win regular hand == half of the doubled hand
                        if (player_hand <= 21 && player_hand > dealer_hand)
                            p.Wallet.Balance += p.Wallet.Bet;

                    }
                    else if (player_hand <= 21 && player_hand > dealer_hand)
                        p.Wallet.Balance += p.Wallet.Bet * 2;
                    p.Wallet.Bet = 0;
                }
                else // dealer busted
                {
                    if (p.Split)
                    {
                        //win split hand == half of the doubled hand
                        if (split_hand <= 21 )
                            p.Wallet.Balance += p.Wallet.Bet;

                        //win regular hand == half of the doubled hand
                        if (player_hand <= 21)
                            p.Wallet.Balance += p.Wallet.Bet;

                    }
                    else if (player_hand <= 21)
                        p.Wallet.Balance += p.Wallet.Bet * 2;
                    p.Wallet.Bet = 0;
                }
                debug_state(state);
            }


        }

        public static void debug_state(GameState state)
        {
            Console.WriteLine("###################################");
            int i = 0;

            Console.WriteLine("Current player: " + state.Current_Player);
            foreach (Player p in state.Players)
            {
                Console.WriteLine("Player " + i + " has: " + p.Wallet.Balance);
                Console.WriteLine("Player " + i + " bet: " + p.Wallet.Bet);
                Console.WriteLine("Player " + i + " split: " + p.Split);
                Console.WriteLine("Player " + i + " hand: " + p.Hand.ToString() + "(" + cards_value(p.Hand) + ")");
                if (p.Split)
                    Console.WriteLine("Player " + i + " split hand: " + p.Split_Hand.ToString() + "(" +cards_value(p.Split_Hand) + ")");
                ++i;
            }
            Console.WriteLine("Dealer hand: " + state.Dealer.Hand.ToString() + "(" + cards_value(state.Dealer.Hand) + ")");
        }
    }
}
