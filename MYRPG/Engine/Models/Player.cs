using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // included this because we want to ObservableCollection
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{

    /// <summary>
    /// Class for player character in the game
    /// </summary>
    /*  Note: INotifyPropertyChanged is an interface that we will use to update the changes to our View(MainWindow.xaml)
        When we change hitPoints, expPoints..etc.  The changes in are not reflected in our view.  This is where INotifyPropertyChanged comes in
        It will notify the view that these values have changed. we will implement  this is our BaseNotifications  class and make available for any class to use.
    */
    public class Player : BaseNotificationClass
    {
        #region properties
        private String _name;
        private String _characterClass;
        private int _hitPoints;
        private int _expPoints;
        private int _level;
        private int _gold;



        public String Name
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

        public String CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }
        public int HitPoints
        {
            get { return _hitPoints; }
            set
            {
                _hitPoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }
        }
        public int ExpPoints
        {
            get { return _expPoints; }
            set
            {
                _expPoints = value;
                OnPropertyChanged(nameof(ExpPoints));
            }
        }
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
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

        /* We will be using ObservableCollection because it can make notification to the xaml.
         * Any changes in these collections(Lists) will be updated to the UI
         * You may ask why don't we just use INotifyPropertyChanged interface instead to do the notify.
         * ObservableCollection is made available from System.Collections.ObjectModel.
         * Might as well use it.
        */
        public ObservableCollection<Item> Inventory  { get; set; }
        public ObservableCollection<QuestStatus> Quests { get; set; }

        /* List of all weapons in they player's inventory
         * We are not going to use a getter or setter here, but instead use LINQ  get the values
         * We also need to notify the UI of any changes to our weapons list.
         * We will do it AddItemToInventory()
         * Note: when using LINQ the queries, it does not execuate till it is needed(Deferred Execution).
         *       "Inventory.Where(i => i is Weapon)" will not be ran till it is being use.
         *       So we call ToList() to force that execution and materialize the results.
         *       Why deferred execution? it's for speed 
         */
        public List<Item> Weapons => Inventory.Where(i => i is Weapon).ToList();
        #endregion

        public Player() 
        {
            Inventory = new ObservableCollection<Item>();
            Quests = new ObservableCollection<QuestStatus>();
        }

        // Any time item is added to the inventory
        // we notify the UI of the changes
        public void AddItemToInventory(Item item) 
        {
            Inventory.Add(item);
            OnPropertyChanged(nameof(Weapons));
        }
        // Removes item from inventory 
        public void RemoveItemFromInventory(Item item) 
        {
            // when we remove item, it will notify the changes because Item is an ObservableCollection
            Inventory.Remove(item);
            // some the items are weapons, we need to notify the changes..since Weapon is not an ObservableCollection
            OnPropertyChanged(nameof(Weapons)); 
        }

        
        public bool HasAllTheseItems(List<ItemQuantity> items) 
        {
            // We pass in List<ItemQuantity> - which is the items and quantity needed
            foreach (ItemQuantity item in items) 
            {
                // We check items,quantity needed against what's in Player's inventory
                if (Inventory.Count(i => i.ItemTypeID == item.ItemID) < item.Quantity) 
                {
                    // return false if missing any item.
                    return false;
                }   
            }
            // if we go through the list and none of them return false, this means the player has all the items
            return true;
        }
    }
}
