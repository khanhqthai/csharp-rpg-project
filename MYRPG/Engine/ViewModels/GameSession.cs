using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models; // import Player.cs

namespace Engine.ViewModels
{
    public class GameSession
    {
        public Player currentPlayer { get; set; } // will hold current player object/instance
        public Location currentLocation { get; set; } // will hold players location
        public GameSession()
        {
            currentPlayer = new Player();
            currentPlayer.name = "Lord Khanh";
            currentPlayer.characterClass = "Warlord";
            currentPlayer.hitPoints = 10;
            currentPlayer.expPoints = 0;
            currentPlayer.level = 1;
            currentPlayer.gold = 10000;

            currentLocation = new Location();
            currentLocation.name = "Home";
            currentLocation.xCordindate = 0;
            currentLocation.yCordindate = -1;
            currentLocation.imageName = "pack://application:,,,/Engine;component/Images/Locations/Home.png";
            currentLocation.description = "A little broken down, But it's home";
        }

    }
}