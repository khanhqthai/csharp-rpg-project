using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models; // import Player.cs
using Engine.Factories;
using System.ComponentModel; // import so we can use  INotifyPropertyChanged

namespace Engine.ViewModels
{
    public class GameSession : INotifyPropertyChanged
    {

        private Location _currentLocation;
        public Player CurrentPlayer { get; set; } // will hold current player info
        
        public Location CurrentLocation  // will hold current location in game
        {
            get { return _currentLocation; }
            set 
            {
                _currentLocation = value;
                // invoke OnPropertyChange function everytime _currentLocation value is set/updated
                OnPropertyChanged("CurrentLocation");
                
                // for the HasLocation bools, there is not setter(we can't call OnPropertyChange to update the xaml)
                // but we can do it here.  When the location is update/set.  We want to notify/update the changes to xaml
                OnPropertyChanged("HasLocationToNorth");
                OnPropertyChanged("HasLocationToWest");
                OnPropertyChanged("HasLocationToEast");
                OnPropertyChanged("HasLocationToSouth");
            }
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




        public World CurrentWorld { get; set; } // will contain information about the game world
        public GameSession()
        {
            Player player = new Player();
            CurrentPlayer = player;
            CurrentPlayer.Name = "Lord Khanh";
            CurrentPlayer.CharacterClass = "Warlord";
            CurrentPlayer.HitPoints = 10;
            CurrentPlayer.ExpPoints = 0;
            CurrentPlayer.Level = 1;
            CurrentPlayer.Gold = 10000;


            // Our game has a lot of things to instantiate. For this we introduce factory design pattern.
            // We let the factory handle for creations of objects without exposing logic to the client.
            WorldFactory worldFactory = new WorldFactory();
            CurrentWorld = worldFactory.CreateWorld();

            // 
            CurrentLocation = CurrentWorld.LocationAt(0,-1);
        }

        // Define our movement functions below. 
        // The function updates current location
        public void MoveNorth() 
        {
            // added one to yCordinate, to move north
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCordindate, CurrentLocation.YCordindate + 1);
        }
        public void MoveWest()
        {
            // subtract one to from the xCordinate to move west
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCordindate - 1, CurrentLocation.YCordindate);
        }
        public void MoveEast()
        {
            // added one to xCordinate to move East
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCordindate + 1, CurrentLocation.YCordindate);
        }
        public void MoveSouth()
        {
            // subtract one from yCordinate to move south
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCordindate, CurrentLocation.YCordindate - 1);
        }
        
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            // if anybody is listening to PropertyChanged, notify them that the property has changed
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}