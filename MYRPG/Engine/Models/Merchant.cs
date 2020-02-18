using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// Merchant class - class to create all our merchants in our game
    /// </summary>
    /// <remarks>
    /// Players can buy or sell item from a merchant
    /// </remarks>

    public class Merchant : BaseNotificationClass
    {
        public string Name { get; set; }
        public ObservableCollection<Item> Inventory { get; set; }

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="name">Name of merchant.</param>
        public Merchant(string name) 
        {
            Name = name;
            Inventory = new ObservableCollection<Item>();
        }

        /// <summary>
        /// Adds item to merchant inventory.
        /// </summary>
        /// <param name="item">item to add.</param>
        public void AddItemToInventory(Item item) 
        {
            Inventory.Add(item);
        }

        /// <summary>
        /// Removes item from merchant inventory.
        /// </summary>
        /// <param name="item">item to remove.</param>
        public void RemoveItemFromInventory(Item item) 
        {
            Inventory.Remove(item);
        }

    }
}
