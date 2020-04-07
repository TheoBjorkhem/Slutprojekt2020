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
        public int currentRoom = 0;
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
            gold = 0;
        }
        
    }
}
