using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt2020
{
    class SafeRoom : Room
    {
        //Variabler

        //Metoder
        public SafeRoom()
        {
            //roomList.Add(this);
            enemyAmount = 0;
            goldAmount = generator.Next(0, 6);
            hasHealed = false;
        }
    }
}
