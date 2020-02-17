using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// This ItemQuantity class.
    /// Use to manage game Item quantity in Player's inventory and Quest requirements
    /// </summary>
    /// <remarks>
    /// The quest.cs class needs a way to track and manage amount of items
    /// some quests require x amount of item, or x amount of kills to complete a quests
    /// this class will help with that.
    /// Or if we need to reward a player with 2 potion
    /// This class can help with that.
    /// we can look up the ItemID and Quantity amount.
    /// </remarks>
    public class ItemQuantity
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
