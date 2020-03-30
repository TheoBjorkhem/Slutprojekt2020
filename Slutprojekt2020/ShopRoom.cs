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

        //Metoder
        public Room()
        {
            roomList.Add(this);
            enemyAmount = 0;
            goldAmount = generator.Next(0, 4);
        }
        public void Buy ()
        {

        }

    }
}
