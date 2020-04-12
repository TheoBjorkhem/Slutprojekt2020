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
            int index = 0;
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
                //Kontroller under kodandet.
                //Console.WriteLine("RoomAmount " + Room.roomList.Count);
                //Console.WriteLine("CurentRoom " + currentRoom);
                //Console.WriteLine("Index " + index);

                output = Convert(Console.ReadLine());
                outputInt = Check(output);
                index = currentRoom - 1;

                //Check HP
                if (outputInt == 1)
                {
                    Console.WriteLine("You have " + p1.hp + " HP.");
                }
                //Check DMG
                else if (outputInt == 2)
                {
                    Console.WriteLine("You have " + p1.dmg + " DMG.");
                }
                //Go North
                else if (outputInt == 3)
                {
                    if (Room.roomList.Count == currentRoom)
                    {
                        if (Room.roomList[index].enemyAmount < 1)
                        {
                            int roomDecider = generator.Next(0, 100);
                            if (roomDecider <= 59)
                            {
                                Room.roomList.Add(new Room());
                                //Info();
                            }
                            else if (roomDecider >= 60 && roomDecider <= 69)
                            {
                                Room.roomList.Add(new SafeRoom());
                                //Info();
                            }
                            else if (roomDecider >= 70 && roomDecider <= 79)
                            {
                                Room.roomList.Add(new DangerRoom());
                                //Info();
                            }
                            else if (roomDecider >= 80 && roomDecider <= 89)
                            {
                                Room.roomList.Add(new LootRoom());
                                //Info();
                            }
                            else if (roomDecider >= 90 && roomDecider <= 99)
                            {
                                Room.roomList.Add(new ShopRoom());
                                //Info();
                            }
                            currentRoom++;
                            index = currentRoom - 1;
                            Info();
                        }
                        else if (Room.roomList[index].enemyAmount > 0)
                        {
                            Console.WriteLine("You can't go to the next room efore defeating the enemies in this room." );
                        }                        
                    }
                    else
                    {
                        currentRoom++;
                        index = currentRoom - 1;
                        Info();
                    }
                }
                //Go South
                else if (outputInt == 4)
                {
                    if (currentRoom == 1)
                    {
                        Console.WriteLine("You can't go south in this room.");
                    }
                    if (currentRoom > 1)
                    {
                        currentRoom--;
                        index = currentRoom - 1;
                        Info();
                    }
                }
                //Attack
                else if (outputInt == 5)
                {
                    if (Room.roomList[index].enemyAmount >= 1)
                    {
                        p1.hp = p1.hp - (Room.roomList[index].enemyAmount / p1.dmg);
                        if (p1.hp > 0)
                        {
                            p1.gold = p1.gold + Room.roomList[index].goldAmount;
                            Console.WriteLine("You defeated all " + Room.roomList[index].enemyAmount + " enemies and took the " +
                                Room.roomList[index].goldAmount + " gold coin(s)." +
                                "\nYou now have " + p1.gold + " gold coins.");
                            Room.roomList[index].enemyAmount = 0;
                            Room.roomList[index].goldAmount = 0;
                        }
                        else if (p1.hp < 1)
                        {
                            Console.WriteLine("You failed to defeat all the enemies without dying.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no enemies in this room.");
                    }

                }
                //Heal
                else if (outputInt == 6)
                {
                    if ((Room.roomList[index].GetType().Name == "SafeRoom" ||
                        Room.roomList[index].GetType().Name == "ShopRoom") &&
                        Room.roomList[index].hasHealed == true)
                    {
                        Console.WriteLine("You can only heal once in every shop or Safe Room.");
                    }
                    else if ((Room.roomList[index].GetType().Name != "SafeRoom" &&
                        Room.roomList[index].GetType().Name != "ShopRoom"))
                    {
                        Console.WriteLine("You can only heal in a Shop or a Safe Room.");
                    }
                    else if ((Room.roomList[index].GetType().Name == "SafeRoom" ||
                        Room.roomList[index].GetType().Name == "ShopRoom") &&
                        Room.roomList[index].hasHealed == false)
                    {
                        Console.WriteLine("You healed back to full health.");
                        p1.hp = 100;
                        Room.roomList[index].hasHealed = true;
                    }
                }
                //Buy
                else if (outputInt == 7)
                {
                    if (Room.roomList[index].hasBought == false &&
                        (Room.roomList[index].GetType().Name == "ShopRoom") &&
                        (Player.playerList[1].gold >= 10))
                    {
                        Player.playerList[1].dmg++;
                        Console.WriteLine("You bought the upgrade an now have " + Player.playerList[1].dmg + " DMG.");
                        Room.roomList[currentRoom].hasBought = true;
                    }
                    else if (Room.roomList[index].hasBought == true &&
                        Room.roomList[index].GetType().Name == "ShopRoom")
                    {
                        Console.WriteLine("You have already bought the upgrade in this shop. Look for a new one.");
                    }
                    else if (Room.roomList[index].hasBought == false &&
                        (Room.roomList[index].GetType().Name == "ShopRoom") &&
                        (Player.playerList[1].gold < 10))
                    {
                        Console.WriteLine("You do not have enough gold coins. You need 10.");
                    }
                    else if (Room.roomList[index].GetType().Name != "ShopRoom")
                    {
                        Console.WriteLine("You can only buy in a Shop.");
                    }
                }
                //Help
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
                Room.roomList.Add(new SafeRoom());
                //Room.roomList[0].isSouth = false;
                //Room.roomList[0].isNorth = true;
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

                //Kontroller under kodandet.
                //Console.WriteLine("RoomAmount " + Room.roomList.Count);
                //Console.WriteLine("CurentRoom " + currentRoom);
                //Console.WriteLine("Index " + index);

                if (Room.roomList[index].GetType().Name == "Room")
                {
                    Console.WriteLine("This is a Standard Room.");
                }
                else if (Room.roomList[index].GetType().Name == "SafeRoom")
                {
                    Console.WriteLine("This is a Safe Room. You can Heal here by typing 'Heal'.");
                }
                else if (Room.roomList[index].GetType().Name == "DangerRoom")
                {
                    Console.WriteLine("This is a Danger Room. There are usually more enemies in Danger Rooms.");
                }
                else if (Room.roomList[index].GetType().Name == "LootRoom")
                {
                    Console.WriteLine("This is a Loot Room. Here be extra loot and no enemies.");
                }
                else if (Room.roomList[index].GetType().Name == "ShopRoom")
                {
                    Console.WriteLine("This is a Shop. You can buy a DMG upgrade for 50 gold by typing 'Buy'." +
                        "\nIt will give you 1 more DMG.");
                }

                Console.WriteLine("There are " + Room.roomList[index].enemyAmount + " enemys in this room," +
                    "\nand" +
                    "\n" + Room.roomList[index].goldAmount + " gold coins.");
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
