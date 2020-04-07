using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt2020
{
    class ShopRoom : SafeRoom
    {
        //Variabler
        public bool hasBought = false;

        //Metoder
        public ShopRoom()
        {
            //roomList.Add(this);
            enemyAmount = 0;
            goldAmount = generator.Next(0, 4);
        }
        public void Buy()
        {
            if (Player.playerList[1].gold >= 5)
            {
                Player.playerList[1].dmg++;
            }
            else
            {
                Console.WriteLine("You do not have enough gold to buy this.");
            }
        }

    }
}
