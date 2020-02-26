using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// Class for game item in our game world
    /// </summary> 
    public class Item
    {   
        public enum ItemCategory 
        { 
            Miscellanous,
            Consumeable,
            Weapon,
        }

        public ItemCategory Category { get; }
        public int ItemTypeID { get; }
        public string Name { get;  }
        public int Price { get;  }
        public bool IsUnique { get;  }
        public int MinDamage { get; }
        public int MaxDamage { get; }

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="itemTypeID">Item ID.</param>
        /// <param name="name">name of item.</param>
        /// <param name="price">item price.</param>
        /// <param name="isUnique">default is false</param>
        /// <remarks>
        /// We want the player to be able to modify their items.  e.i. add gems, add poison to their Weapons.
        /// Some items we will need them to be unique inorder to do this
        /// </remarks>
        public Item(ItemCategory category, int itemTypeID, string name, int price, int minDamage = 0, int maxDamage = 0, bool isUnique = false)
        {
            Category = category;
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }

        /// <summary>
        /// Copy/clone an item
        /// </summary>
        /// <returns> 
        /// Item object
        /// </returns>
        public Item Clone() 
        {
            return new Item(Category, ItemTypeID, Name, Price, MinDamage, MaxDamage, IsUnique);
        }
    }
}
