using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt2020
{
    class Player
    {
        //Variabler
        public int hp = 0;
        public int dmg = 0;

        Random generator = new Random();

        //Metoder
        public Player ()
        {
            hp = 100;
            dmg = 1;
        }

    }
}
