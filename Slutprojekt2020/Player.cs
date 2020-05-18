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
        private int hp = 0;
        private int dmg = 0;
        private int gold = 0;
        static private int currentP = 0;
        static private Random generator = new Random();

        //Även om jag inte ska ha fler spelare så la jag den ändå i en list så att jag skulle kunna komma åt...
        //...instansen av den här klassen utan att göra alla variabler statiska. Jag tyckte det var en bättre lösning.
        static public List<Player> playerList = new List<Player>();

        //Metoder
        public Player()
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
            if (room.GetEnemyAmount() > 0)
            {
                //Spelaren tar skada beroende på hur många fiender det är och hur mycket DMG spelaren har.
                int initialAmount = room.GetEnemyAmount();
                for (int i = room.GetEnemyAmount(); room.GetEnemyAmount() > 0;)
                {
                    room.SetEnemyAmount(room.GetEnemyAmount() - this.dmg);
                    this.hp -= 1;
                }

                //this.hp = this.hp - (room.enemyAmount / this.dmg);
                //Kollar om man lever.
                if (this.hp > 0)
                {
                    //Ger en rummets guld.
                    this.gold = this.gold + room.GetGoldAmount();
                    Console.WriteLine("You defeated all " + initialAmount + " enemies and took the " +
                        room.GetGoldAmount() + " gold coin(s)." +
                        "\nYou now have " + this.gold + " gold coins.");
                    //Fixar värdena på rummet.
                    room.SetEnemyAmount(0);
                    room.SetGoldAmount(0);

                    int i = generator.Next(0, 100);
                    if (i > 0 && i < 11)
                    {
                        Console.WriteLine("You found a mysterious potion. Type 'Drink' to drink it.");
                        i = generator.Next(0, 2);
                        if (i == 0)
                        {
                            Potion.potionStack.Push(new HealthPot());
                        }
                        else
                        {
                            Potion.potionStack.Push(new OuchPot());
                        }
                    }


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

        //Metoder som handlar variabler med främmande länder.
        public void SetHp(int newHp)
        {
            hp = hp + newHp;
        }
        public int GetHp()
        {
            return hp;
        }
        public void AddDMG(int addDMG)
        {
            dmg = dmg + addDMG;
        }
        public int GetDMG()
        {
            return dmg;
        }
        public void AddGold(int addGold)
        {
            gold = gold + addGold;
        }
        public int GetGold()
        {
            return gold;
        }
        static public int GetCurrentP()
        {
            return currentP;
        }
        /*
        static public void SetCurrentP(int newCurrentP)
        {
            currentP = newCurrentP;
        }
        */
    }
}
