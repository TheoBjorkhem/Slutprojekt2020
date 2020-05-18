using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt2020
{
    class OuchPot : Potion
    {
        private int healtOuch = -25;

        public override void Drink()
        {
            Player.playerList[Player.GetCurrentP()].SetHp(healtOuch);
            Console.WriteLine("Glugg Gugg! The potion hurt you for " + healtOuch + " HP.");
            potionStack.Pop();
        }
    }
}
