using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models; 
using Engine.Factories; 
using Engine.EventArgs; 
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
        private Merchant _currentMerchant;
        private Player _currentPlayer;

        public string GameIconImageSprite { get; set; }
        public Player CurrentPlayer
        {
            get { return _currentPlayer; }

            set
            {
                if (_currentPlayer != null) 
                {
                    // unsubscribe from previous CurrentPlayer
                    _currentPlayer.OnKilled -= OnCurrentPlayerKilled;
                    _currentPlayer.OnLeveledUp -= OnCurrentPlayerLeveledUp;
                }
                
                // set new current player value, then subcribe to it
                _currentPlayer = value;
                if (_currentPlayer!=null) 
                {
                    _currentPlayer.OnKilled += OnCurrentPlayerKilled;
                    _currentPlayer.OnLeveledUp += OnCurrentPlayerLeveledUp;
                }
            }
        }



        public World CurrentWorld { get;}
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

                // for the HasLocation bools, there is no setter(we can't call OnPropertyChange to update the xaml)
                // but we can do it here.  When the location is update/set.  We want to notify/update the changes to xaml
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToSouth));

                // Every time the player move to a location 
                // we want to automatically give the player the quests available at the location
                GivePlayerQuestAtLocation();

                // Every time the player move to a location 
                // we automatically get the monster for the player to fight(if location has a monster)
                GetMonsterAtLocation();

                // Every time the player move to a location 
                // We check if the player has the required items to complete the quest at the location.
                QuestCompleteAtLocation();

                if (CurrentMonster != null) 
                {
                    // If there is a monster at the location, trigger event to display message on xaml
                    RaiseMessage("****");
                    RaiseMessage($"You have encounter a {CurrentMonster.Name}!" );
                }
                // Everytime player moves to a location, set the current merchant for the location
                CurrentMerchant = CurrentLocation.MerchantHere;
            }
        }
        // we are using back property because, we want to use the notification on it
        // need to update monster info(mainly to display HP) to the UI
        public Monster CurrentMonster
        {
            get { return _currentMonster; }
            set 
            {
                if (_currentMonster !=null ) 
                {
                    _currentMonster.OnKilled -= OnCurrentMonsterKilled;
                }

                _currentMonster = value;
                if (_currentMonster != null) 
                {
                    _currentMonster.OnKilled += OnCurrentMonsterKilled;
                }
                // Notify UI, CurrentMonster, HasMonster value has changed
                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));
            }
        }


        public Merchant CurrentMerchant 
        {
            get { return _currentMerchant; }
            set 
            {
                _currentMerchant = value;
                OnPropertyChanged(nameof(CurrentMerchant));
                OnPropertyChanged(nameof(HasMerchant)); // notify UI to hide/show 'Shop' button      
            }
        }

        // adds available quests at location to Player, if player does not already have it.
    
        // Before the player moves North/South/East/West, we want to check if the location exists in our Location List.
        // We will have 4 boolean that tests each direction. 
        // Our View(MainWindow.xaml) will make use of these boolean.
        // If true we will we display the. (N/S/E/W) button for the player to click
        public bool HasLocationToNorth 
        {   //return True if not null
            get { return CurrentWorld.LocationAt(CurrentLocation.XCordindate, CurrentLocation.YCordindate + 1) != null ; }
        }

        // we can write the above function using lambda expression.  lambda expression were introduced in C#3.
        // just makes our code cleaner.
        public bool HasLocationToWest => 
            CurrentWorld.LocationAt(CurrentLocation.XCordindate - 1, CurrentLocation.YCordindate) != null;
        public bool HasLocationToEast => 
            CurrentWorld.LocationAt(CurrentLocation.XCordindate + 1, CurrentLocation.YCordindate) != null;
        public bool HasLocationToSouth => 
            CurrentWorld.LocationAt(CurrentLocation.XCordindate, CurrentLocation.YCordindate - 1) != null;

        // true if CurrentMonster is not null
        public bool HasMonster => CurrentMonster != null;

        // true if CurrentMerchant is not null
        // we'll use this bool value to hide or show our merchant 'Shop' button
        public bool HasMerchant => CurrentMerchant != null; 


        public GameSession()
        {
             // load game icons sprite
             GameIconImageSprite = "pack://application:,,,/Engine;component/Images/game-item-icons.png";

            // using Named parameter method to create Player Object
            // because Visual Studio will provide intellisense for the parameters, when using it.
            CurrentPlayer = new Player("Lord Khanh",0,0,10,10,10000 ) { 
                CharacterClass = "Warlord", 
            };

            // Add item to player's inventory(starting item)
            CurrentPlayer.Inventory.Add(ItemFactory.CreateItem(0)); // pendant
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1001)); // wooden stick

            // Our game has a lot of things to instantiate. For this we introduce factory design pattern.
            // We let the factory handle for creations of objects without exposing logic to the client.
            CurrentWorld = WorldFactory.CreateWorld();
            CurrentLocation = CurrentWorld.LocationAt(0,-1); // set intitial game location
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
                RaiseMessage("");
                RaiseMessage("****");
                RaiseMessage("Select a weapon to attack with.");
                RaiseMessage("****");
                RaiseMessage("");
                // exit our function
                return;
            }

            // determine damage to monster(In most rpg board games, this would be the dice roll)
            int damageToMonster = RandomNumberGenerator.NumberBetween(CurrentWeapon.MinDmg, CurrentWeapon.MaxDmg);

            if (damageToMonster == 0)
            {
                RaiseMessage("");
                RaiseMessage("****");
                RaiseMessage($"Your attack missed the {CurrentMonster.Name}.");
                RaiseMessage("****");
                RaiseMessage("");
            }
            else 
            {
                
                RaiseMessage("");
                RaiseMessage("****");
                RaiseMessage($"Your attack does {damageToMonster} damage to the {CurrentMonster.Name}");
                RaiseMessage("****");
                RaiseMessage("");
                CurrentMonster.TakeDamage(damageToMonster);
            }

            // If monster is killed 
            if (CurrentMonster.IsDead)
            {
                // get another monster to fight
                GetMonsterAtLocation();
            }
            else
            {
                // if monster still alive, monster fights back
                int damageToPlayer = RandomNumberGenerator.NumberBetween(CurrentMonster.MinDamage, CurrentMonster.MaxDamage);

                if (damageToPlayer == 0)
                {
                    RaiseMessage("");
                    RaiseMessage("****");
                    RaiseMessage($"{CurrentMonster.Name}'s attack miss");
                    RaiseMessage("****");
                    RaiseMessage("");
                }
                else 
                {
                    RaiseMessage("");
                    RaiseMessage("****");
                    RaiseMessage($"{CurrentMonster.Name}'s attack does {damageToPlayer} damage!");
                    CurrentPlayer.TakeDamage(damageToPlayer);

                    RaiseMessage("****");
                    RaiseMessage("");
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
        private void OnCurrentPlayerKilled(object sender, System.EventArgs e)
        {
            RaiseMessage("");
            RaiseMessage("****");
            RaiseMessage($"You have been killed by {CurrentMonster.Name}!");
            RaiseMessage("****");
            RaiseMessage("");

            CurrentLocation = CurrentWorld.LocationAt(0, -1);
            CurrentPlayer.FullHeal();
        }

        // give experience, gold, and item to Player 
        private void OnCurrentMonsterKilled(object sender, System.EventArgs e)
        {
            RaiseMessage("");
            RaiseMessage($"You have slained {CurrentMonster.Name}!");
            CurrentPlayer.AddExpPoints(CurrentMonster.RewardExpPoints); //  give experience
            RaiseMessage($"You have gained {CurrentMonster.RewardExpPoints} experience points!"); // raise message to display to xaml

            CurrentPlayer.ReceiveGold(CurrentMonster.Gold); // give gold
            RaiseMessage($"You picked up {CurrentMonster.Gold} gold!");

            // loot monster's inventory
            foreach (Item item in CurrentMonster.Inventory)
            {
                CurrentPlayer.AddItemToInventory(item);
                RaiseMessage($"You looted one {item.Name} from {CurrentMonster.Name}");
            }


        }
        private void GivePlayerQuestAtLocation()
        {
            // We check the List of QuestAvailableHere at the location against the Player's List of Quests
            // We add any quest from the QuestAvailable list that is not already in the Player's List of Quests
            foreach (Quest quest in CurrentLocation.QuestAvailableHere)
            {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
                    RaiseMessage("****");
                    RaiseMessage($"You recieve the '{quest.Name}' quest!");
                    RaiseMessage($" {quest.Description}");

                    RaiseMessage("This quest requires: ");
                    foreach (ItemQuantity itemQuantity in quest.RequiredQuestItems)
                    {
                        RaiseMessage($" {itemQuantity.Quantity} {ItemFactory.CreateItem(itemQuantity.ItemID).Name}");
                    }
                    RaiseMessage(" to complete. ");
                    RaiseMessage("Your reward will be: ");
                    RaiseMessage($" {quest.RewardExpPoints} experience points.");
                    RaiseMessage($" {quest.RewardGold} gold.");
                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        RaiseMessage($" {itemQuantity.Quantity} {ItemFactory.CreateItem(itemQuantity.ItemID).Name}");
                    }

                }
            }
        }

        // get monster at location
        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }
        /// <summary>
        /// 1. Check if player meets quest requirement items, if so give player the quest's reward
        /// 2. Removes  quest requirement items from player's inventory. 
        /// </summary>
        private void QuestCompleteAtLocation()
        {

            // loop through each quest at location
            foreach (Quest quest in CurrentLocation.QuestAvailableHere)
            {
                // Check for  incompleted quests
                QuestStatus questToComplete =
                    CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID && !q.IsCompleted);

                if (questToComplete != null)
                {

                    if (CurrentPlayer.HasAllTheseItems(quest.RequiredQuestItems))
                    {
                        // remove the required quest items from player's inventory
                        foreach (ItemQuantity itemQuantity in quest.RequiredQuestItems)
                        {
                            // we'll need to loop the total amount and remove it.
                            for (int i = 0; i < itemQuantity.Quantity; i++)
                            {
                                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(item => item.ItemTypeID == itemQuantity.ItemID));
                            }

                        }
                        RaiseMessage("****");
                        RaiseMessage($"You completed the '{quest.Name}' quest!");

                        // give player the rewards for completing the quest
                        RaiseMessage($"You gained {quest.RewardExpPoints} experience points!");
                        CurrentPlayer.AddExpPoints(quest.RewardExpPoints);

                        RaiseMessage($"You recieved {quest.RewardGold} gold!");
                        CurrentPlayer.ReceiveGold(quest.RewardGold);
;
                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            for (int i = 0; i < itemQuantity.Quantity; i++)
                            {
                                Item rewardItem = ItemFactory.CreateItem(itemQuantity.ItemID);
                                RaiseMessage($"You recieved a {rewardItem.Name}!");
                                CurrentPlayer.AddItemToInventory(rewardItem);
                            }

                        }

                        // Mark the Quest completed
                        questToComplete.IsCompleted = true;
                      
                    }


                }


            }
        }

        private void OnCurrentPlayerLeveledUp(object sender, System.EventArgs e) 
        {
            RaiseMessage("");
            RaiseMessage($"Congratulations! You are now level {CurrentPlayer.Level}".ToUpper());
        }
    }
}