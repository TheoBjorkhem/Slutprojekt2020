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
            int winRoom = 52;
            int outputInt = 0;
            int currentRoom = 0;
            string output = " ";
            Random generator = new Random();
            int currentP = 0;

            //Start
            Intro();
            //Skapar spelarn. Kanske inte behövde varit en class men det blir enklare om man vill ha flera spelare sen.
            Player p1 = new Player();
            FirstRoom();

            //Spelloppen. Åker runt så länge man inte har besegrat tillräkligt många rum och så länge man inte får under 1 HP.
            while (currentRoom != winRoom && p1.hp > 0)
            {
                //Skickar en input till en metod.
                output = Convert(Console.ReadLine());
                //Skickar resultatet till en metod.
                outputInt = Check(output);
                //Allting buggade sönder tills jag insåg att listan börjar på noll och gjorde då en index int som väljer Room i listan.
                index = currentRoom - 1;

                //PlayerInfo
                if (outputInt == 1)
                {
                    //Kollar upp klass instansens HP.
                    Player.playerList[currentP].PlayerInfo();
                }
                //Go North
                else if (outputInt == 2)
                {
                    //Kollar om man är i det senaste rummet som har skapats.
                    if (Room.roomList.Count == currentRoom)
                    {
                        //Kollar att det inte finns några fiender i det rummet.
                        if (Room.roomList[index].enemyAmount < 1)
                        {
                            //Slumpar vilket rum som ska skapas.
                            int roomDecider = generator.Next(0, 100);
                            if (roomDecider <= 59)
                            {
                                Room.roomList.Add(new Room());
                            }
                            else if (roomDecider >= 60 && roomDecider <= 69)
                            {
                                Room.roomList.Add(new SafeRoom());
                            }
                            else if (roomDecider >= 70 && roomDecider <= 79)
                            {
                                Room.roomList.Add(new DangerRoom());
                            }
                            else if (roomDecider >= 80 && roomDecider <= 89)
                            {
                                Room.roomList.Add(new LootRoom());
                            }
                            else if (roomDecider >= 90 && roomDecider <= 99)
                            {
                                Room.roomList.Add(new ShopRoom());
                            }
                            //Ger en guldet i rummet om det inte finns några fiender. Lite klumpigt gjort att ha den koden på..
                            //...2 ställen men jag kom inte på att man behövde kunna ta upp guld även utan fiender förens mot...
                            //...slutet av projektet.
                            if (Room.roomList[index].GetType().Name != "Room" || Room.roomList[index].GetType().Name != "DangerRoom")
                            {
                                //Ger en rummets guld.
                                Player.playerList[0].gold = Player.playerList[0].gold + Room.roomList[index].goldAmount;
                            }
                            //Fixar indexeringen när ett nytt Room har skapats.
                            currentRoom++;
                            index = currentRoom - 1;
                            //Kallar på Info metoden i classen.
                            Room.roomList[index].Info();

                        }
                        //Man måste besegra fienderna först.
                        else if (Room.roomList[index].enemyAmount > 0)
                        {
                            Console.WriteLine("You can't go to the next room before defeating the enemies in this room." );
                        }                        
                    }
                    //Går bara framåt ett steg om man inte är i det senaste rummet som har skapats.
                    else
                    {
                        currentRoom++;
                        index = currentRoom - 1;
                        Room.roomList[index].Info();
                    }
                }
                //Go South
                else if (outputInt == 3)
                {
                    if (currentRoom == 1)
                    {
                        Console.WriteLine("You can't go south in this room.");
                    }
                    //Backar bara ett rum och fixar indexeringen.
                    if (currentRoom > 1)
                    {
                        currentRoom--;
                        index = currentRoom - 1;
                        Room.roomList[index].Info();
                    }
                }
                //Attack
                else if (outputInt == 4)
                {
                    //Kallar på metoden i instansen av spelar classen.
                    Player.playerList[currentP].Attack(Room.roomList[index]);
                }
                //Heal
                else if (outputInt == 5)
                {
                    //Jag har fortfarande en kontroll a vilket rum det är i Main eftersom...
                    //...det blev smidigare än att ha 3 eller 4 sådana kontroller i classen.
                    if ((Room.roomList[index].GetType().Name != "SafeRoom" && Room.roomList[index].GetType().Name != "ShopRoom"))
                    {
                        Console.WriteLine("You can only heal in a Shop or a Safe Room.");
                    }
                    else
                    {
                        Room.roomList[index].Heal(Player.playerList[currentP]);
                    }
                }
                //Buy
                else if (outputInt == 6)
                {
                    //Samma här som med Heal.
                    if ((Room.roomList[index].GetType().Name != "ShopRoom"))
                    {
                        Console.WriteLine("You can only buy in a Shop.");
                    }
                    else
                    {
                        Room.roomList[index].Buy(Player.playerList[currentP]);
                    }
                }
                //Help
                else if (outputInt == 7)
                {
                    //Säger bara introt igen för att påminna en.
                    Intro();
                }
            }

            //Avslutar programmet efter metoden.
            End();
            Console.ReadLine();

            //Metoder
            //Ger bara info till spelaren. Kallas på i början och när man skriver "Help".
            void Intro ()
            {
                Console.WriteLine("You are stuck in this dungeon. " +
                    "\nFight your way through to get out. " +
                    "\nType 'Info' to see your Health, Damage and Gold." +
                    "\nType 'GoN' to go North." +
                    "\nType 'GoS' to go South." +
                    "\nType 'Attack' to attack the enemies in the room." +
                    "\nType 'Heal' to heal if you are in a Safe Room or a Shop." +
                    "\nType 'Buy' to buy a DMG upgrade to your Sword." +
                    "\nYou will pick up the gold automatically after defeating the enemies and immedietly if there are no enemies." +
                    "\nType 'Help' to get a reminder of this." +
                    "\nGood Luck");
            }
            //Sätter inställningarna på rummet man spawnar i rätt och sätter currenRoom till 1 då ett Room nu finns.
            void FirstRoom ()
            {
                Room.roomList.Add(new SafeRoom());
                Room.roomList[0].goldAmount = 0;
                Room.roomList[0].enemyAmount = 0;
                Console.WriteLine("You are currently in a Safe Room with only a path North.");
                currentRoom = 1;
            }
            //Gör så att stringen får små bokstäver.
            string Convert (string inp)
            {
                string inputLower = " ";
                inputLower = inp.ToLower();
                return inputLower;
            }
            //Konverterar inputen till en int som sedan avgör vad spelaren ville göra.
            int Check (string inp)
            {
                int outP = 0;
                if (inp == "info")
                {
                    outP = 1; 
                }
                else if (inp == "gon")
                {
                    outP = 2;
                }
                else if (inp == "gos")
                {
                    outP = 3;
                }
                else if (inp == "attack")
                {
                    outP = 4;
                }
                else if (inp == "heal")
                {
                    outP = 5;
                }
                else if (inp == "buy")
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
            //Kollar bara om man vann eller inte och ger ett lämpligt meddelande.
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
