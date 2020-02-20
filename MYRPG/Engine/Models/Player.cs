using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // included this because we want to ObservableCollection
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{

    /// <summary>
    /// Defines our player character in the game
    /// </summary>
    public class Player : LivingEntity
    {

        private String _characterClass;
        private int _expPoints;
        private int _level;

        public String CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
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

        public ObservableCollection<QuestStatus> Quests { get; set; }


        public Player(string name, int currentHitPoints, int maxHitPoints, int gold) : base(name, currentHitPoints, maxHitPoints, gold)
        {
            Quests = new ObservableCollection<QuestStatus>();
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
