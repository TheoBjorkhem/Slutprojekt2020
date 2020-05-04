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
            //roomList.Add(this);
            enemyAmount = generator.Next(5, 16);
            goldAmount = generator.Next(10, 21);
        }
        public override void Info()
        {
            Console.WriteLine("This is a Danger Room. There are usually more enemies in Danger Rooms." +
                "\nThere are " + this.enemyAmount + " enemys in this room," +
                "\nand" +
                "\n" + this.goldAmount + " gold coins.");
        }
    }
}
