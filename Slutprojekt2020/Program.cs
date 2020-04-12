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
            int itemPrice = 50;
            int currentRoom = 0;
            string output = " ";
            Random generator = new Random();

            //Start
            Intro();
            //Skapar spelarn. Kanske inte behövde varit en class men det blir enklare om man vill ha flera spelare sen.
            Player p1 = new Player();
            FirstRoom();

            //Spelloppen. Åker runt så länge man inte har besegrat tillräkligt många rum och så länge man inte får under 1 HP.
            while (currentRoom != winRoom && p1.hp > 0)
            {
                //Kontroller under kodandet.
                //Console.WriteLine("RoomAmount " + Room.roomList.Count);
                //Console.WriteLine("CurentRoom " + currentRoom);
                //Console.WriteLine("Index " + index);

                //Skickar en input till en metod.
                output = Convert(Console.ReadLine());
                //Skickar resultatet till en metod.
                outputInt = Check(output);
                //Allting buggade sönder tills jag insåg att listan börjar på noll och gjorde då en index int som väljer Room i listan.
                index = currentRoom - 1;

                //Check HP
                if (outputInt == 1)
                {
                    //Kollar upp klass instansens HP.
                    //Det kanske vore bättre att kolla upp spelaren i listan istället men jag ska bara ha en spelare ändå.
                    Console.WriteLine("You have " + p1.hp + " HP.");
                }
                //Check DMG
                else if (outputInt == 2)
                {
                    //Samma som HP fast DMG.
                    Console.WriteLine("You have " + p1.dmg + " DMG.");
                }
                //Go North
                else if (outputInt == 3)
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
                            //Kallar på Info metoden.
                            Info();

                        }
                        //Man måste besegra fienderna först.
                        else if (Room.roomList[index].enemyAmount > 0)
                        {
                            Console.WriteLine("You can't go to the next room efore defeating the enemies in this room." );
                        }                        
                    }
                    //Går bara framåt ett steg om man inte är i det senaste rummet som har skapats.
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
                    //Backar bara ett rum och fixar indexeringen.
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
                    //Kollar om det finns fiender i rummet.
                    if (Room.roomList[index].enemyAmount >= 1)
                    {
                        //Spelaren tar skada beroende på hur många fiender det är och hur mycket DMG spelaren har.
                        p1.hp = p1.hp - (Room.roomList[index].enemyAmount / p1.dmg);
                        //Kollar om man lever.
                        if (p1.hp > 0)
                        {
                            //Ger en rummets guld.
                            p1.gold = p1.gold + Room.roomList[index].goldAmount;
                            Console.WriteLine("You defeated all " + Room.roomList[index].enemyAmount + " enemies and took the " +
                                Room.roomList[index].goldAmount + " gold coin(s)." +
                                "\nYou now have " + p1.gold + " gold coins.");
                            //Fixar värdena på rummet.
                            Room.roomList[index].enemyAmount = 0;
                            Room.roomList[index].goldAmount = 0;
                        }
                        //Du dog.
                        else if (p1.hp < 1)
                        {
                            Console.WriteLine("You failed to defeat all the enemies without dying.");
                        }
                    }
                    //Om inga fiender.
                    else
                    {
                        Console.WriteLine("There are no enemies in this room.");
                    }

                }
                //Heal
                else if (outputInt == 6)
                {
                    //Kollar att man är i en affär eller ett SafeRoom och att man inte redan har healat.
                    if ((Room.roomList[index].GetType().Name == "SafeRoom" ||
                        Room.roomList[index].GetType().Name == "ShopRoom") &&
                        Room.roomList[index].hasHealed == true)
                    {
                        Console.WriteLine("You can only heal once in every shop or Safe Room.");
                    }
                    //Samma sak fast annat meddelande.
                    else if ((Room.roomList[index].GetType().Name != "SafeRoom" &&
                        Room.roomList[index].GetType().Name != "ShopRoom"))
                    {
                        Console.WriteLine("You can only heal in a Shop or a Safe Room.");
                    }
                    //Samma sak fast här är om man har lyckasts.
                    else if ((Room.roomList[index].GetType().Name == "SafeRoom" ||
                        Room.roomList[index].GetType().Name == "ShopRoom") &&
                        Room.roomList[index].hasHealed == false)
                    {
                        Console.WriteLine("You healed back to full health.");
                        //Borde egentligen göra en int för spelarens max HP men jag hann aldrig gör HP uppgraderingar ändå.
                        p1.hp = 100;
                        Room.roomList[index].hasHealed = true;
                    }
                }
                //Buy
                else if (outputInt == 7)
                {
                    //Kollar att man inte redan har köpt här, att man har guld nog, och att man är i en affär.
                    if (Room.roomList[index].hasBought == false &&
                        (Room.roomList[index].GetType().Name == "ShopRoom") &&
                        (Player.playerList[0].gold >= itemPrice))
                    {
                        //Ger en mer DMG. Ser till att man inte kan köpa i den affären igen och tar guldet från en.
                        Player.playerList[0].dmg++;
                        Console.WriteLine("You bought the upgrade an now have " + Player.playerList[0].dmg + " DMG.");
                        Room.roomList[index].hasBought = true;
                        Player.playerList[0].gold = Player.playerList[0].gold - itemPrice;
                    }
                    //Om man redan har köpt här.
                    else if (Room.roomList[index].hasBought == true &&
                        Room.roomList[index].GetType().Name == "ShopRoom")
                    {
                        Console.WriteLine("You have already bought the upgrade in this shop. Look for a new one.");
                    }
                    //Om man inte har guld nog.
                    else if (Room.roomList[index].hasBought == false &&
                        (Room.roomList[index].GetType().Name == "ShopRoom") &&
                        (Player.playerList[0].gold < itemPrice))
                    {
                        Console.WriteLine("You do not have enough gold coins. You need " + itemPrice + ".");
                    }
                    //Om man inte är i en affär.
                    else if (Room.roomList[index].GetType().Name != "ShopRoom")
                    {
                        Console.WriteLine("You can only buy in a Shop.");
                    }
                }
                //Help
                else if (outputInt == 8)
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
                    "\nType 'HP?' to see your Health." +
                    "\nType 'DMG?' to see your damage output." +
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
            //Informerar spelaren om vilken typ av rum den är i och hur mycket pengar och iender det finns.
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
                    Console.WriteLine("This is a Shop. You can buy a DMG upgrade for " + itemPrice + " gold by typing 'Buy'." +
                        "\nIt will give you 1 more DMG.");
                }

                Console.WriteLine("There are " + Room.roomList[index].enemyAmount + " enemys in this room," +
                    "\nand" +
                    "\n" + Room.roomList[index].goldAmount + " gold coins.");
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
