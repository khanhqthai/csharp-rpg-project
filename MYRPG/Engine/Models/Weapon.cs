using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// Class for our weapons in the game
    /// </summary>
    public class Weapon : Item
    {
        public int MinDmg { get; set; }
        public int MaxDmg { get; set; }
        public Weapon(int itemTypeID, string name, int price, int minDmg, int maxDmg) : base(itemTypeID, name, price)
        {
            MinDmg = minDmg;
            MaxDmg = maxDmg;
        }

        /* we use "new" because, Clone() is defined in Item already, and Weapon  inherits that function 
         * So we use "new" to overide the Clone() and define it here
         */
        public new Weapon Clone() 
        {
            return new Weapon(ItemTypeID, Name, Price, MinDmg, MaxDmg);
        }
    }
}
