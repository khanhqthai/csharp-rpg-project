using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// Base class for game items such as weapons,scrolls,armor
    /// </summary>
    public class Item
    {   
        public int ItemTypeID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        // public constructor
        public Item(int itemTypeID, string name, int price)
        {
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
        }

        // return new Item
        public Item Clone() 
        {
            return new Item(ItemTypeID, Name, Price);
        }
    }
}
