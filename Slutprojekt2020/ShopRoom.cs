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
        private int itemPrice = 50;
        private bool hasBought = false;

        //Metoder
        public ShopRoom()
        {
            //roomList.Add(this);
            enemyAmount = 0;
            goldAmount = generator.Next(0, 4);
        }
        public override void Buy(Player p1)
        {
            //Kollar att man inte redan har köpt här, att man har guld nog, och att man är i en affär.
            if (hasBought == false && (Player.playerList[Player.GetCurrentP()].GetGold() >= itemPrice))
            {
                //Ger en mer DMG. Ser till att man inte kan köpa i den affären igen och tar guldet från en.
                Player.playerList[Player.GetCurrentP()].AddDMG(1);
                Console.WriteLine("You bought the upgrade an now have " + Player.playerList[Player.GetCurrentP()].GetDMG() + " DMG.");
                hasBought = true;
                Player.playerList[Player.GetCurrentP()].AddGold(-itemPrice);
            }
            //Om man redan har köpt här.
            else if (hasBought == true)
            {
                Console.WriteLine("You have already bought the upgrade in this shop. Look for a new one.");
            }
            //Om man inte har guld nog.
            else if (hasBought == false && (Player.playerList[Player.GetCurrentP()].GetGold() < itemPrice))
            {
                Console.WriteLine("You do not have enough gold coins. You need " + itemPrice + ".");
            }
        }
        public override void Info()
        {
            Console.WriteLine("This is a Shop. You can buy a DMG upgrade for " + itemPrice + " gold by typing 'Buy'." +
                "\nIt will give you 1 more DMG." +
                "\nThere are " + this.goldAmount + " gold coins in this room.");
        }

    }
}
