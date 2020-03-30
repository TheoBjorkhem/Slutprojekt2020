using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt2020
{
    class DangerRoom : Room
    {
        //Variabler

        //Metoder
        public DangerRoom()
        {
            roomList.Add(this);
            enemyAmount = generator.Next(5, 16);
            goldAmount = generator.Next(10, 21);
        }

    }
}
