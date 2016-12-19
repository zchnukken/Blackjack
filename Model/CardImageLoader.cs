using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Model
{
    public class CardImageLoader
    {
        private static String imageDir = "C:\\Users\\Anton\\Documents\\Visual Studio 2015\\Projects\\Blackjack\\blackjack\\Images\\";
        public static Uri card_to_uri(Card card)
        {
            String str = card.ToString().ToLower().Replace(' ','_');
            str = imageDir + str + ".png";
            //exists? exceptions yada yada
            try
            {
                Uri tmp = new Uri(str);
                //Console.WriteLine(tmp.ToString());
                return tmp;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            
        }
    }
}
