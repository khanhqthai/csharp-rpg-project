using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// Merhcant class - Sells and buys game item
    /// </summary>

    public class Merchant : BaseNotificationClass
    {
        public string Name { get; set; }
        public ObservableCollection<Item> Inventory { get; set; }
        public Merchant(string name) 
        {
            Name = name;
            Inventory = new ObservableCollection<Item>();
        }
        public void AddItemToInventory(Item item) 
        {
            Inventory.Add(item);
        }
        public void RemoveItemFromInventory(Item item) 
        {
            Inventory.Remove(item);
        }

    }
}
