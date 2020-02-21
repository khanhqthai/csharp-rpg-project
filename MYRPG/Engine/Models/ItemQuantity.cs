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
    /// The Quest class needs a way to track and manage amount of items
    /// some quests require X amount of item, or X amount of kills to complete a quests
    /// this class will help with that.
    /// Or if we need to reward a player with 2 potion
    /// This class can help with that.
    /// we can look up the ItemID and Quantity amount.
    /// </remarks>
    public class ItemQuantity
    {
        public int ItemID { get;  }
        public int Quantity { get;   }

        public ItemQuantity(int itemID, int quantity ) 
        {
            ItemID = itemID;
            Quantity = quantity;
        }
    }
}
