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
            int outputInt = 0;
            string output = " ";
            Random generator = new Random();

            //Spelloop
            Intro();
            Player p1 = new Player();
            FirstRoom();
            currentRoom = 1;

            while (currentRoom < 10 && p1.hp > 0)
            {
                //Console.WriteLine(Room.roomList.Count);
                //Console.WriteLine(currentRoom);

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
                        int roomDecider = generator.Next(0, 100);
                        if (roomDecider <= 59)
                        {
                            Room.roomList.Add(new Room());
                            Info();
                        }
                        else if (roomDecider >= 60 && roomDecider <= 69)
                        {
                            Room.roomList.Add(new SafeRoom());
                            Info();
                        }
                        else if (roomDecider >= 70 && roomDecider <= 79)
                        {
                            Room.roomList.Add(new DangerRoom());
                            Info();
                        }
                        else if (roomDecider >= 80 && roomDecider <= 89)
                        {
                            Room.roomList.Add(new LootRoom());
                            Info();
                        }
                        else if (roomDecider >= 90 && roomDecider <= 99)
                        {
                            Room.roomList.Add(new ShopRoom());
                            Info();
                        }
                        currentRoom++;
                    }
                    else
                    {
                        Info();
                    }
                }
                else if (outputInt == 4)
                {
                    if (currentRoom == 1)
                    {
                        Console.WriteLine("You can't go south in this room.");
                    }
                    if (currentRoom > 1)
                    {
                        currentRoom--;
                    }
                }
                else if (outputInt == 5)
                {
                    if (Room.roomList[currentRoom].enemyAmount > 0)
                    {
                        p1.hp = p1.hp - (Room.roomList[currentRoom].enemyAmount / p1.dmg);
                        if (p1.hp > 0)
                        {
                            Console.WriteLine("You defeated all " + Room.roomList[currentRoom].enemyAmount + " enemies and took the " +
                                Room.roomList[currentRoom].goldAmount + " gold coins." +
                                "\nYou now have " + p1.gold + " gold coins.");
                            Room.roomList[currentRoom].enemyAmount = 0;
                        }
                        else if (p1.hp < 1)
                        {
                            Console.WriteLine("You failed to defeat all the enemies without dying.");
                        }
                    }

                }
                else if (outputInt == 6)
                {
                }
                else if (outputInt == 7)
                {
                }
                else if (outputInt == 8)
                {
                    Intro();
                }


                //Console.WriteLine(output);
            }

            End();
            Console.ReadLine();

            //Metoder
            void Intro ()
            {
                Console.WriteLine("You are stuck in this dungeon. " +
                    "\nFight your way through to get out. " +
                    "\nType 'HP?' to see your Health." +
                    "\nType 'DMG?' to see your damage output." +
                    "\nType 'GoN' to go North." +
                    "\nType 'GoS' to go South." +
                    "\nType 'Attack' to attack the enemies in the room." +
                    "\nType 'Heal' to heal if you are in a Safe Room or a Shop." +
                    "\nType 'Buy' to buy a DMG upgrade to your Sword." +
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
                //Tykte inte det här blev snyggt eftersom man inte kan ha mellanrum i klassnamn.
                //Jag kanske kunde ha gjort namnet till en string och delat upp den vid den... 
                //...första stora bokstaven men jag vet inte hur man gör det.
                //Console.WriteLine("This is a " + Room.roomList[currentRoom].GetType().Name + ".");

                if (Room.roomList[currentRoom].GetType().Name == "Room")
                {
                    Console.WriteLine("This is a Standard Room.");
                }
                else if (Room.roomList[currentRoom].GetType().Name == "SafeRoom")
                {
                    Console.WriteLine("This is a Safe Room. You can Heal here by typing 'Heal'.");
                }
                else if (Room.roomList[currentRoom].GetType().Name == "DangerRoom")
                {
                    Console.WriteLine("This is a Danger Room. There are usually more enemies in Danger Rooms.");
                }
                else if (Room.roomList[currentRoom].GetType().Name == "LootRoom")
                {
                    Console.WriteLine("This is a Loot Room. Here be extra loot and no enemies.");
                }
                else if (Room.roomList[currentRoom].GetType().Name == "ShopRoom")
                {
                    Console.WriteLine("This is a Shop. You can buy a DMG upgrade for 50 gold by typing 'Buy'." +
                        "\nIt will give you 1 more DMG.");
                }

                Console.WriteLine("There are " + Room.roomList[currentRoom].enemyAmount + " enemys in this room," +
                    "\nand" +
                    "\n" + Room.roomList[currentRoom].goldAmount + " gold coins.");
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
                else if (inp == "attack")
                {
                    outP = 5;
                }
                else if (inp == "heal")
                {
                    outP = 6;
                }
                else if (inp == "buy")
                {
                    outP = 7;
                }
                else if (inp == "help")
                {
                    outP = 8;
                }
                else
                {
                    outP = 0;
                    Console.WriteLine("That's not a valid command." +
                        "\nType 'Help' to get a reminder of the commands.");
                }
                return outP;
            }
            void End ()
            {
                if (p1.hp > 0)
                {
                    Console.WriteLine("You escaped the dungeon and got out with " + p1.gold + " gold coins." +
                        "\nGood job.");
                }
                else
                {
                    Console.WriteLine("You perished in the dungeon." +
                        "\nBetter luck next time. Literally.");
                }
            }
        }
    }
}
