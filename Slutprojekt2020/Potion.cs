using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt2020
{
    class Potion
    {
        static public Stack<Potion> potionStack = new Stack<Potion>();

        public Potion ()
        {
            //potionQue.Push(this);
        }
        public virtual void Drink()
        {
        }

    }
}
