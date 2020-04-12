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
        public int enemyAmount = 0;
        public int goldAmount = 0;
        public bool hasBought = false;
        public bool hasHealed = false;
        public bool canAttack = false;
        public bool isNorth = false;
        public bool isSouth = false;

        static public List<Room> roomList = new List<Room>();
        static public Random generator = new Random();

        //Metoder
        public Room ()
        {
            //roomList.Add(this);
            enemyAmount = generator.Next(1,6);
            goldAmount = generator.Next(0,11);

        }
        public int GetRoom ()
        {
            int roomNumb = 0;


            return roomNumb;
        }


    }
}
