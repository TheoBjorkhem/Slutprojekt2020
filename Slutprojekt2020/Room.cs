using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt2020
{
    class Room
    {
        //Variabler
        //Insåg att man tydligen inte kan komma åt variabler i ärvda klasser på något lätt sätt så jag la dom här istället.
        public int enemyAmount = 0;
        public int goldAmount = 0;
        public bool hasBought = false;
        public bool hasHealed = false;
        static public Random generator = new Random();

        //Listan med alla rum.
        static public List<Room> roomList = new List<Room>();

        //Metoder
        //Allting blev mycket svårare när jag försökte lägga metoderna i klasserna så det mesta ligger i Main.
        //Vet inte riktigt vad för metoder som är användbara att ha i klasserna nu.
        public Room ()
        {
            //Insåg att det var lättare att lägga till den i en lista i main istället för här.
            //roomList.Add(this);
            enemyAmount = generator.Next(1,6);
            goldAmount = generator.Next(0,11);
        }
    }
}
