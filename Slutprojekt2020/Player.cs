using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt2020
{
    public class Player
    {
        //Variabler
        public int hp = 0;
        public int dmg = 0;
        public int gold = 0;
        Random generator = new Random();

        //Även om jag inte ska ha fler spelare så la jag den ändå i en list så att jag skulle kunna komma åt...
        //...instansen av den här klassen utan att göra alla variabler statiska. Jag tyckte det var en bättre lösning.
        static public List<Player> playerList = new List<Player>();

        //Metoder
        public Player ()
        {
            playerList.Add(this);
            hp = 100;
            dmg = 1;
            gold = 1;
        }
        public virtual void PlayerInfo()
        {
            Console.WriteLine("You have:" +
                "\n" + hp + " HP," +
                "\n" + dmg + " DMG," +
                "\n" + gold + " gold.");
        }
        public virtual void Attack(Room room)
        {
            //Kollar om det finns fiender i rummet.
            if (room.enemyAmount >= 1)
            {
                //Spelaren tar skada beroende på hur många fiender det är och hur mycket DMG spelaren har.
                this.hp = this.hp - (room.enemyAmount / this.dmg);
                //Kollar om man lever.
                if (this.hp > 0)
                {
                    //Ger en rummets guld.
                    this.gold = this.gold + room.goldAmount;
                    Console.WriteLine("You defeated all " + room.enemyAmount + " enemies and took the " +
                        room.goldAmount + " gold coin(s)." +
                        "\nYou now have " + this.gold + " gold coins.");
                    //Fixar värdena på rummet.
                    room.enemyAmount = 0;
                    room.goldAmount = 0;
                }
                //Du dog.
                else if (this.hp < 1)
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
    }
}
