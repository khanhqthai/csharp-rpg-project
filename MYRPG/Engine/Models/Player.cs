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
            private set // private - only allow player to set experience points.
            {
                _expPoints = value;
                OnPropertyChanged(nameof(ExpPoints));
                SetLevelAndMaxHitPoints(); // check if level up requirement are met every time, players gains experience points
            }
        }

        public event EventHandler OnLeveledUp; 

        public ObservableCollection<QuestStatus> Quests { get; set; }



        public Player(string name, int exPoints, int currentHitPoints, int maxHitPoints, int gold, int level)
            : base(name, currentHitPoints, maxHitPoints, gold, level)
        {
            ExpPoints = exPoints;
            Quests = new ObservableCollection<QuestStatus>();
        }

        protected virtual void RaiseOnLeveledUp()
        {
            // if not null, notify subscribers of OnLeveledUp
            OnLeveledUp?.Invoke(this, new System.EventArgs());
        }

        public void AddExpPoints(int expPoints) 
        {
            ExpPoints += expPoints;
        }

        /// <summary>
        /// Sets new level, max hit points
        /// </summary>
        /// <remarks>
        /// This function will called everytime the ExpPoints property is set/update.
        /// Checks if the player ExpPoints is high enough to reward a new level.
        /// Our formula for leveling up is (Expoints/100) + 1...so every 100 experience points
        /// level 1 = 100 exp, level 2 = 200 exp  
        /// We essentially check if the new , level 3 = 300 exp and so on
        /// </remarks>
        private void SetLevelAndMaxHitPoints()
        {
            int originalLevel = Level;
            Level = (ExpPoints / 100) + 1;
            if (Level != originalLevel) 
            {
                // set new max hits points for leveling up, and full heal 
                MaxHitPoints = Level * 10;
                FullHeal();

                // raise event to let subscribers know that player has leveled up.
                // maybe have the xaml can do some sort of flashy graphics congratulating the player.
                RaiseOnLeveledUp(); 
            }
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
