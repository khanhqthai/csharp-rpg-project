using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// class  to manage inventory items.
    /// </summary>
    /// <remarks>
    /// We need a way to manage Player's inventory items.
    /// Class is very simliar to ItemQuanity class
    /// </remarks>
    public class GroupedInventoryItem : BaseNotificationClass
    {
        private Item _item;
        private int _quantity;

        public Item Item 
        {
            get { return _item; }
            set 
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        public int Quantity 
        {
            get { return _quantity; }
            set 
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        /// <summary>
        /// The class constructor
        /// </summary>
        /// <param name="item">game Item</param>
        /// <param name="quantity">how many game Item</param>
        public GroupedInventoryItem(Item item, int quantity) 
        {
            Item = item;
            Quantity = quantity;
        }

    }
}
