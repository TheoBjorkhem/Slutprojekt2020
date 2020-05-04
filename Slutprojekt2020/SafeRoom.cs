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
        public override void Heal(Player p1)
        {
            //Kollar att man är i en affär eller ett SafeRoom och att man inte redan har healat.
            if (this.hasHealed == true)
            {
                Console.WriteLine("You can only heal once in every shop or Safe Room.");
            }
            //Samma sak fast här är om man har lyckasts.
            else if (this.hasHealed == false)
            {
                Console.WriteLine("You healed back to full health.");
                //Borde egentligen göra en int för spelarens max HP men jag hann aldrig gör HP uppgraderingar ändå.
                p1.hp = 100;
                this.hasHealed = true;
            }
        }
        public override void Info()
        {
            Console.WriteLine("This is a Safe Room. You can Heal here by typing 'Heal'." +
                "\nThere are " + this.goldAmount + " gold coins in this room.");
        }
    }
}
