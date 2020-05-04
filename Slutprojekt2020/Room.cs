﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt2020
{
    public class Room
    {
        //Variabler
        public int goldAmount = 0;
        public int enemyAmount = 0;
        public bool hasHealed = false;
        static public Random generator = new Random();

        //Listan med alla rum.
        static public List<Room> roomList = new List<Room>();

        //Metoder
        public Room ()
        {
            //Insåg att det var lättare att lägga till den i en lista i main istället för här.
            //roomList.Add(this);
            enemyAmount = generator.Next(1,6);
            goldAmount = generator.Next(0,11);
        }
        public virtual void Heal(Player p1)
        {
        }
        public virtual void Buy(Player p1)
        {
        }
        public virtual void Info()
        {
            Console.WriteLine("This is a Standard Room." +
                "\nThere are " + this.enemyAmount + " enemys in this room," +
                "\nand" +
                "\n" + this.goldAmount + " gold coins.");
        }
    }
}
