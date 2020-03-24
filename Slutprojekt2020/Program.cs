using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt2020
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variabler
            int roomsCleared = 0;
            int outputInt = 0;
            string output = " ";

            //Spelloop
            Intro();
            while (roomsCleared < 10)
            {
                bool correct = false;
                output = Check(Console.ReadLine());
                outputInt = Convert(output);

                while (correct == false)
                {
                    if (output != "q")
                    {

                    }
                }


                Console.WriteLine(output);
            }


            Console.ReadLine();

            //Metoder
            void Intro ()
            {
                Console.WriteLine("You are stuck in this dungeon. " +
                    "\nFight your way through to get out. " +
                    "\nYou can buy better weapons and armor with the gold you find." +
                    "\nIn the safe rooms you can heal yourself." +
                    "\nType 'HP?' to see your Health." +
                    "\nType 'DMG?' to see your damage output." +
                    "\nType 'GoS' to go South." +
                    "\nType 'GoN' to go North." +
                    "\nType 'GoW' to go West." +
                    "\nType 'GoE' to go East." +
                    "\nType 'Help' to get a reminder of this." +
                    "\nYou are currently in a room with only a path North." +
                    "\nGood Luck");
            }
            string Check (string inp)
            {
                string inputLower = " ";
                inputLower = inp.ToLower();
                return inputLower;
            }
            int Convert (string inp)
            {
                int outP = 0;
                if (inp == "hp?")
                {
                    outP = 1; 
                }
                else if (inp == "dmg?")
                {
                    outP = 2;
                }
                else if (inp == "gos")
                {
                    outP = 3;
                }
                else if (inp == "gon")
                {
                    outP = 4;
                }
                else if (inp == "gow")
                {
                    outP = 5;
                }
                else if (inp == "goe")
                {
                    outP = 6;
                }
                else if (inp == "help")
                {
                    outP = 7;
                }
                else
                {
                    outP = 0;
                    Console.WriteLine("That's not a valid command." +
                        "\nType 'Help' to get a reminder of the commands.");
                }
                return outP;
            }

        }
    }
}
