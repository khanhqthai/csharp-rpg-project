using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// This is our item quanity class.
    /// The quest.cs class needs a way to track and manage amount of items
    /// some quests require x amount of item, or x amount of kills to complete a quests
    /// this class will help with that.
    /// Or we need to reward a player with 2 potion
    /// This class can help with that.
    /// we can look up the ItemID and Quantity amount.
    /// </summary>
    /// <param name="ItemID">ID of item</param>
    /// <param name="Quantity">Amount of the item</param>
    class ItemQuantity
    {
        public int ItemID { get; set; }
        public int Quantity { get; set; }

        public ItemQuantity(int itemID, int quantity ) 
        {
            ItemID = itemID;
            Quantity = quantity;
        }
    }
}
