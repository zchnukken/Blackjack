﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Model
{
    class CardImageLoader
    {
        private static String imageDir = "C:\\Users\\Sneeky\\Documents\\Visual Studio 2013\\Projects\\maraTest\\Images\\";
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
