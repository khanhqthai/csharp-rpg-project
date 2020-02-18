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
        public int ItemTypeID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="itemTypeID">Item ID.</param>
        /// <param name="name">name of item.</param>
        /// <param name="price">item price.</param>
        public Item(int itemTypeID, string name, int price)
        {
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
        }

        /// <summary>
        /// Copy/clone an item
        /// </summary>
        /// <returns> 
        /// Item object
        /// </returns>
        public Item Clone() 
        {
            return new Item(ItemTypeID, Name, Price);
        }
    }
}
