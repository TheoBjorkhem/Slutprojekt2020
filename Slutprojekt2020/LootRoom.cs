using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt2020
{
    class LootRoom : Room
    {
        //Variabler

        //Metoder
        public LootRoom()
        {
            //roomList.Add(this);
            enemyAmount = 0;
            goldAmount = generator.Next(5, 16);
        }


    }
}
