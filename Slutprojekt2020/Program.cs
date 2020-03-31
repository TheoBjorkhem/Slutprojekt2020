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
            int currentRoom = 0;
            int roomsCleared = 0;
            int outputInt = 0;
            string output = " ";


            //Spelloop
            Intro();
            Player p1 = new Player();
            FirstRoom();
            currentRoom++;

            while (roomsCleared < 10)
            {
                output = Convert(Console.ReadLine());
                outputInt = Check(output);

                if (outputInt == 1)
                {
                    Console.WriteLine("You have " + p1.hp + " HP.");
                }
                else if (outputInt == 2)
                {
                    Console.WriteLine("You have " + p1.dmg + " DMG.");
                }
                else if (outputInt == 3)
                {
                    if (Room.roomList.Count == currentRoom)
                    {
                        Room.roomList.Add(new Room());
                        Info();
                    }
                    currentRoom++;
                    //Console.WriteLine(Room.roomList.Count);
                }
                else if (outputInt == 4)
                {
                    if (currentRoom < 1)
                    {
                        Console.WriteLine("You can't go south in this room.");
                    }
                    if (currentRoom > 0)
                    {
                        currentRoom--;
                    }
                }
                else if (outputInt == 5)
                {
                    Intro();
                }


                //Console.WriteLine(output);
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
                    "\nType 'Help' to get a reminder of this." +
                    "\nGood Luck");
            }
            void FirstRoom ()
            {
                Room.roomList.Add(new Room());
                Room.roomList[0].canAttack = false;
                Room.roomList[0].isSouth = false;
                Room.roomList[0].isNorth = true;
                Room.roomList[0].goldAmount = 0;
                Room.roomList[0].enemyAmount = 0;
                Console.WriteLine("You are currently in a Safe Room with only a path North.");
            }
            void Info ()
            {
                Console.WriteLine("There are " + Room.roomList[currentRoom].enemyAmount + " enemys in this room." +
                    "\n");
            }
            string Convert (string inp)
            {
                string inputLower = " ";
                inputLower = inp.ToLower();
                return inputLower;
            }
            int Check (string inp)
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
                else if (inp == "gon")
                {
                    outP = 3;
                }
                else if (inp == "gos")
                {
                    outP = 4;
                }
                else if (inp == "help")
                {
                    outP = 5;
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
