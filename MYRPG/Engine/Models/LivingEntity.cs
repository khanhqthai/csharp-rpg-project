using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// LivingEntity class - base class for entities alive in our game world(Monsters, Merchants, Player)
    /// </summary>
    /// <remarks>
    /// inherits BaseNotificationClass because, we need to update property changes to the XAML
    /// </remarks>
    public abstract class LivingEntity : BaseNotificationClass
    {
        private string _name;
        private int _currentHitPoints;
        private int _maxHitPoints;
        private int _gold;
        public string ImageName { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                // everytime  we set/update our value for name
                // we call OnPropertyChanged to notify anything using the property that it has been changed
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int CurrentHitPoints
        {
            get { return _currentHitPoints; }
            set
            {
                _currentHitPoints = value;
                OnPropertyChanged(nameof(_currentHitPoints));
            }
        }
        public int MaxHitPoints 
        {
           get { return _maxHitPoints;  }
            set 
            {
                _maxHitPoints = value;
                OnPropertyChanged(nameof(_maxHitPoints));
            }
        }
        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }

        /// <summary>
        /// List container to hold game items
        /// </summary>
        /// <remarks>
        /// We will be using ObservableCollection because it can make notification to the XAML.
        /// Any changes in these collections(Lists) will be updated to the UI
        /// So when a player removes/add an item from his inventory the changes are reflected in the XAML
        /// A little nice feature if we are using collections
        /// </remarks>
        public ObservableCollection<Item> Inventory { get; set; }

        /// <summary>
        /// List of all weapons in they player's inventory
        /// </summary>
        /// <remarks>
        /// We are not going to use a getter or setter here, but instead use LINQ  get the values
        /// We also need to notify the UI of any changes to our weapons list.
        /// We will do it AddItemToInventory()
        /// Note: when using LINQ the queries, it does not execuate till it is needed(Deferred Execution).
        ///     "Inventory.Where(i => i is Weapon)" will not be ran till it is being use.
        ///     So we call ToList() to force that execution and materialize the results.
        ///     Why deferred execution? it's for speed 
        /// </remarks>
        public List<Item> Weapons => Inventory.Where(i => i is Weapon).ToList();

        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; set; }


        // protected - because we only want children of the LivingEntity have access to it. 
        protected LivingEntity()
        {
            // we will need to refactor this in the future, we have 2 inventory, it is duplication
            Inventory = new ObservableCollection<Item>();
            GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
        }

        /// <summary>
        /// Adds an item from Inventory and GroupedInventory
        /// </summary>
        /// <param name="item"></param>
        public void AddItemToInventory(Item item)
        {
            Inventory.Add(item); // add item to main Inventory

            // if item is unique we add new item to GroupInventoryItem, it is always new new Object everytime
            if (item.IsUnique)
            {
                GroupedInventory.Add(new GroupedInventoryItem(item, 1));
            }
            else 
            {
                // not unique item, we only create a new one once and increment quantity
                // add item to group inventory if does not exists, set intitial quantity 0
                if (!GroupedInventory.Any(gi => gi.Item.ItemTypeID == item.ItemTypeID)) 
                {
                    GroupedInventory.Add(new GroupedInventoryItem(item,0));
                }

                // increment quantity 
                GroupedInventory.First(gi => gi.Item.ItemTypeID == item.ItemTypeID).Quantity++;
            }

            OnPropertyChanged(nameof(Weapons));
        }

        /// <summary>
        /// Removes an item from Inventory and GroupedInventory
        /// </summary>
        public void RemoveItemFromInventory(Item item)
        {
            Inventory.Remove(item);
            GroupedInventoryItem groupedInventoryItemToRemove = GroupedInventory.FirstOrDefault(gi =>gi.Item == item);
            if (groupedInventoryItemToRemove != null) 
            {
                if (groupedInventoryItemToRemove.Quantity == 1)
                {
                    GroupedInventory.Remove(groupedInventoryItemToRemove);
                }
                else 
                {
                    groupedInventoryItemToRemove.Quantity--;
                }
            }
            // some the items are weapons, we need to notify the changes since Weapon is not an ObservableCollection type
            OnPropertyChanged(nameof(Weapons));
        }
    }
}
