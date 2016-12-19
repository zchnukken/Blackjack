using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Model
{
    using System.Windows.Media.Imaging;
    //using Cards = Stack<Card>;
    using Cards = ObservableCollection<Card>;

    public enum Value 
    {
        Ace = 1
        ,Two 
        ,Three
        ,Four
        ,Five
        ,Six
        ,Seven
        ,Eight
        ,Nine
        ,Ten
        ,Knight
        ,Queen
        ,King
    };

    public enum Suite
    {
        Heart
        ,Diamond
        ,Club
        ,Spade
    };

    /* Generic card class */
    public class Card
    {
        private Value value   = 0;
        private Suite suite = 0;

        public Card(Value val, Suite suite)
        {
            this.value = val;
            this.suite = suite;
        }

        public Value Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public Suite Suite
        {
            get { return this.suite; }
            set { this.suite = value; }
        }

        public String Name
        {
            get { return ToString(); }
        }

        public BitmapImage Image
        {
            get
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = CardImageLoader.card_to_uri(this);
                bmp.EndInit();
                return bmp;
            }
        }


        public override string ToString()
        { return value.ToString() + " of " + suite.ToString() + "s"; }


       public static bool operator <(Card lcard, Card rcard)
       {

           if (lcard.Suite == rcard.Suite)
           {
               return lcard.Value < rcard.Value;
           }
           else
               return lcard.Suite < rcard.Suite;
       }

       public static bool operator >(Card lcard, Card rcard)
       {

           if (lcard.Suite == rcard.Suite)
           {
               return lcard.Value > rcard.Value;
           }
           else
               return lcard.Suite > rcard.Suite;
       }
    
    }

    public abstract class CardContainer
    {
        protected Cards cont = new Cards();
        protected Trash trashpile = Trash.Instance();

        public Cards Cards
        {
            get { return this.cont; }
            private set {; }
        }

        public void draw(CardContainer container, int i = 0)
        {
            if (container == null)
                container = trashpile;

            if (this.cont.Count > 0 && i < this.cont.Count)
            {
                Card tmp = this.cont[i];
                container.put(tmp);
                this.cont.RemoveAt(i);
            }
         }

        private void put(Card card)
        {
            this.cont.Add(card);
        }

        public int Value
        {
            get { return this.cont.Sum(Card => (int)Card.Value); }
            private set { }
        }
        public override String ToString()
        {
            String cardinhand = "";
            foreach (Card card in cont)
            {
                cardinhand += card.ToString() + ", ";
            }
            return cardinhand;
        }

        public void discard(int index = 0)
        {
            draw(null, index); //discards to trashpile? hopefully
        }

        public void discard_all()
        {
            int size = this.cont.Count;
            for (int i = 0; i < size; ++i)
                discard();
        }

        public Trash Trashpile
        {
            get { return trashpile; }
            private set {; }
        }
    }

    public class Hand : CardContainer
    {

        public Hand()
        { }


        /* perhaps not nescessary */
        public Cards get_hand()
        { return this.cont; }

        public void clear()
        {
            while(cont.Count > 0)
                discard();
        }

    }

    public class Deck : CardContainer
    {
        public Deck(Cards cards)
        {
            this.cont= cards;
        }

        /* Create and return an all permutation un-shuffled stack of cards */
        public static Cards add_deck()
        {
           Cards temp = new Cards();

           foreach(Suite suite in Enum.GetValues(typeof(Suite)) )
           {
                foreach(Value val in Enum.GetValues(typeof(Value)) )
                {
                    temp.Add(new Card(val,suite));
                   // Console.WriteLine(new Card(val, suite).ToString());
                }
           }
        
            return temp;
        }


        /* really ghetto random shuffle */
        public void shuffle()
        {
            Random randy = new Random();
            
            int size = cont.Count-1;

            for (int i = 0; i < size; ++i )
            {
                int rand = randy.Next() % size;
                Card tmp = this.cont[i];
                this.cont[i] = this.cont[rand];
                this.cont[rand] = tmp;
            }
        }

    }

    public class Trash : CardContainer
    {
        static public Trash trashPile = new Trash();
        
        public static Trash Instance() 
        {
            return trashPile;
        }

        public void shuffle_back(CardContainer cont)
        {
            //shuffle trashpile
            for (int i = 0; i < this.cont.Count; ++i)
                draw(cont);
        }
    }

}
