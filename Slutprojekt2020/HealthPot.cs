using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt2020
{
    class HealthPot : Potion
    {
        private int healtBonus = 25;

        public override void Drink()
        {
            Player.playerList[Player.GetCurrentP()].SetHp(healtBonus);
            Console.WriteLine("Glugg Gugg! The potion healed you for " + healtBonus + " HP.");
            potionStack.Pop();
        }

    }
}
