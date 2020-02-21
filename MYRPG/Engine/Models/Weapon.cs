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
        public int MinDmg { get;  }
        public int MaxDmg { get;  }

        /// <summary>
        /// Class to create Weapon items
        /// </summary>
        /// <param name="itemTypeID"> Unique weapon ID</param>
        /// <param name="name"> Weapon name</param>
        /// <param name="price">Price</param>
        /// <param name="minDmg">minimum weapon damage</param>
        /// <param name="maxDmg">maximum weapon damage</param>
        /// <remarks>
        /// by default all weapons willb unique, players will be able to modify their weapons with gems, poison, fire...etc
        /// </remarks>
        public Weapon(int itemTypeID, string name, int price, int minDmg, int maxDmg) 
            : base(itemTypeID, name, price, true)
        {
            MinDmg = minDmg;
            MaxDmg = maxDmg;
        }

        /* we use "new" because, Clone() is defined in Item already, and Weapon  inherits that function 
         * So we use "new" to overide the Clone() and define it here  */
        public new Weapon Clone() 
        {
            return new Weapon(ItemTypeID, Name, Price, MinDmg, MaxDmg);
        }
    }
}
