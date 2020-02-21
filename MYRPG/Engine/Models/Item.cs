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
        public int ItemTypeID { get; }
        public string Name { get;  }
        public int Price { get;  }
        public bool IsUnique { get;  }

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
        public Item(int itemTypeID, string name, int price, bool isUnique = false)
        {
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
        }

        /// <summary>
        /// Copy/clone an item
        /// </summary>
        /// <returns> 
        /// Item object
        /// </returns>
        public Item Clone() 
        {
            return new Item(ItemTypeID, Name, Price, IsUnique);
        }
    }
}
