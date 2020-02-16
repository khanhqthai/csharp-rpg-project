using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models; // import Models
using Engine.Factories; // import Factories
using Engine.EventArgs; // import EventArgs; 
using System.Diagnostics;

namespace Engine.ViewModels
{
    /// <summary>
    /// Our main class, it is the middle man between, View and Model.  It would be C in MVC.
    /// Handles all game world logic, creates and runs the game world.
    /// </summary>
    public class GameSession : BaseNotificationClass
    {
        public event EventHandler<GameMessageEventArgs> OnMessagedRaised;

        private Location _currentLocation;
        private Monster _currentMonster;

        public Player CurrentPlayer { get; set; } 
        public World CurrentWorld { get; set; }
        public Weapon CurrentWeapon { get; set; }

        public Location CurrentLocation  
        {
            get { return _currentLocation; }
            set 
            {
                _currentLocation = value;
                /*  
                 *  instead of  OnPropertyChanged("CurrentLocation"); we use nameof();
                    because if for any reason, if we change the CurrrentLocation to CurrentLocation2, visual studio will make the update
                    to our entire code, but if we use   OnPropertyChanged("CurrentLocation"); 
                    we have to mannually update that "CurrentLocation" to "CurrentLocation2" ourselves    
                 */

                //invoke OnPropertyChange function everytime _currentLocation value is set/updated
                OnPropertyChanged(nameof(CurrentLocation));

                // for the HasLocation bools, there is not setter(we can't call OnPropertyChange to update the xaml)
                // but we can do it here.  When the location is update/set.  We want to notify/update the changes to xaml
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToSouth));

                // Every time a location is set(moved to) 
                // we want to automatically give the player the quests available at the location
                GivePlayerQuestAtLocation();

                // Every time a location is set(moved to) 
                // we automatically get the monster for the player to fight(if location has a monster)
                GetMonsterAtLocation();


                if (CurrentMonster != null) 
                {
                    // If there is a monster at the location, trigger event to display message on xaml
                    RaiseMessage($"You have encounter a {CurrentMonster.Name}!" );
                }
            }
        }
        // we are using back property because, we want to use the notification on it
        // need to update monster info(mainly to display HP) to the UI
        public Monster CurrentMonster
        {
            get { return _currentMonster; }
            set 
            {
                _currentMonster = value;
                
                // Notify UI, CurrentMonster, HasMonster value has changed
                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));
            }
        }

        // adds available quests at location to Player, if player does not already have it.
        private void GivePlayerQuestAtLocation()
        {
            // We check the List of QuestAvailableHere at the location against the Player's List of Quests
            // We add any quest from the QuestAvailable list that is not already in the Player's List of Quests
            foreach (Quest quest in CurrentLocation.QuestAvailableHere)
            {
                if (!CurrentPlayer.Quests.Any(q=> q.PlayerQuest.ID == quest.ID)) 
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
                }
            }
        }

        // get monster at location
        private void GetMonsterAtLocation() 
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }

        // Before the player moves North/South/East/West, we want to check if the location exists in our Location List.
        // We will have 4 boolean that tests each direction. 
        // Our View(MainWindow.xaml) will make use of these boolean.
        // If true we will we display the. (N/S/E/W) button for the player to click
        public bool HasLocationToNorth 
        {   //return True if not null
            get { return CurrentWorld.LocationAt(CurrentLocation.XCordindate, CurrentLocation.YCordindate + 1) != null ; }
        }
        public bool HasLocationToWest
        {   //return True if not null
            get { return CurrentWorld.LocationAt(CurrentLocation.XCordindate - 1, CurrentLocation.YCordindate) != null; }
        }
        public bool HasLocationToEast
        {   //return True if not null
            get { return CurrentWorld.LocationAt(CurrentLocation.XCordindate + 1, CurrentLocation.YCordindate) != null; }
        }
        public bool HasLocationToSouth
        {   //return True if not null
            get { return CurrentWorld.LocationAt(CurrentLocation.XCordindate, CurrentLocation.YCordindate - 1) != null; }
        }

        // returns true, when location has CurrentMonster set
        public bool HasMonster => CurrentMonster != null;

        
        
        public GameSession()
        {

            /* 
             * Old way of creating Player object
                CurrentPlayer = new Player();
                CurrentPlayer.Name = "Lord Khanh";
                CurrentPlayer.CharacterClass = "Warlord";
                CurrentPlayer.HitPoints = 10;
                CurrentPlayer.ExpPoints = 0;
                CurrentPlayer.Level = 1;
                CurrentPlayer.Gold = 10000;
             * We will replace this with named parameter method, see below
            */

            // using Named parameter method to create Player Object
            // because Visual Studio will provide intellisense for the parameters, when using it.
            CurrentPlayer = new Player { Name = "Lord Khanh", CharacterClass = "Warlord", HitPoints = 10, ExpPoints = 0,
                Level = 1,
                Gold = 10000
            };

            // Add item to player's inventory(starting item)
            CurrentPlayer.Inventory.Add(ItemFactory.CreateItem(0)); // pendant
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1001)); // wooden stick
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1002)); // wooden sword

            // Our game has a lot of things to instantiate. For this we introduce factory design pattern.
            // We let the factory handle for creations of objects without exposing logic to the client.
            CurrentWorld = WorldFactory.CreateWorld();

            // set current location
            CurrentLocation = CurrentWorld.LocationAt(0,-1);
            

        }

        // Define our movement functions below. 
        // The function updates current location
        public void MoveNorth() 
        {
            // if true, then move north
            if (HasLocationToNorth) 
            { 
                // added one to yCordinate, to move north
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCordindate, CurrentLocation.YCordindate + 1);
            }
        }
        public void MoveWest()
        {
            if (HasLocationToWest) 
            { 
                // subtract one to from the xCordinate to move west
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCordindate - 1, CurrentLocation.YCordindate);
            }
        }
        public void MoveEast()
        {
            if (HasLocationToEast) 
            {
                // added one to xCordinate to move East
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCordindate + 1, CurrentLocation.YCordindate);
            }
        }
        public void MoveSouth()
        {
            if (HasLocationToSouth) 
            { 
                // subtract one from yCordinate to move south
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCordindate, CurrentLocation.YCordindate - 1);
            }
        }

        // logic for our game combat.
        public void AttackCurrentMonster() 
        {
            // Guard clause - check the value needed exists before proceeding
            // In this case, we need CurrentWeapon to be set.
            if (CurrentWeapon == null) 
            {
                RaiseMessage("Select a weapon to attack with.");
                // exit our function
                return;
            }

            // determine damage to monster(In most rpg board games, this would be the dice roll)
            int damageToMonster = RandomNumberGenerator.NumberBetween(CurrentWeapon.MinDmg, CurrentWeapon.MaxDmg);

            if (damageToMonster == 0)
            {
                RaiseMessage($"Your attack missed the {CurrentMonster.Name}.");
            }
            else 
            {
                CurrentMonster.HitPoints -= damageToMonster;
                RaiseMessage($"Your attack does {damageToMonster} damage to the {CurrentMonster.Name}");
            }

            // If monster is killed, collect loot and experience
            if (CurrentMonster.HitPoints <= 0)
            {
                RaiseMessage($"You have slained {CurrentMonster.Name}!");
                CurrentPlayer.ExpPoints += CurrentMonster.RewardExpPoints; // Get experience
                RaiseMessage($"You have gained {CurrentMonster.RewardExpPoints} experience points!"); // raise message to display to xaml

                CurrentPlayer.Gold += CurrentMonster.RewardGold; // Get gold
                RaiseMessage($"You picked up {CurrentMonster.RewardGold} gold!");

                // loot monster's inventory
                foreach (ItemQuantity itemQuantity in CurrentMonster.Inventory)
                {
                    Item item = ItemFactory.CreateItem(itemQuantity.ItemID);
                    CurrentPlayer.AddItemToInventory(item);
                    RaiseMessage($"You looted {itemQuantity.Quantity} {item.Name} from {CurrentMonster.Name}");
                }

                // Spawn another monster to fight
                GetMonsterAtLocation();
            }
            else
            {
                // if monster still alive, monster fights back
                int damageToPlayer = RandomNumberGenerator.NumberBetween(CurrentMonster.MinDamage, CurrentMonster.MaxDamage);

                if (damageToPlayer == 0)
                {
                    RaiseMessage($"{CurrentMonster.Name}'s attack miss");
                }
                else 
                {
                    CurrentPlayer.HitPoints -= damageToPlayer;
                    RaiseMessage($"{CurrentMonster.Name}'s attack does {damageToPlayer} damage!");
                }

                // if player is killed, spawn player at Home location and restore Hit Points
                if (CurrentPlayer.HitPoints <=0) 
                {
                    RaiseMessage($"You have been killed by {CurrentMonster.Name}!");

                    CurrentLocation = CurrentWorld.LocationAt(0, -1);
                    CurrentPlayer.HitPoints = CurrentPlayer.Level * 10;
                }

            }

        }
        

        public void RaiseMessage(string messsage) 
        {
            // if there are any subscriber to OnMessagedRaised, run the function
            // In our case it will be the MainWindow.xaml.OnGameMessagedRaised() function
            // but any funciton can be subscribed(added to) to event OnMessagedRaised 
            OnMessagedRaised?.Invoke(this, new GameMessageEventArgs(messsage));
        }
    }
}