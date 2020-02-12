using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models; // import Player.cs
using Engine.Factories;

namespace Engine.ViewModels
{
    public class GameSession
    {
        public Player currentPlayer { get; set; } // will hold current player object/instance
        public Location currentLocation { get; set; } // will hold players location
        public World currentWorld { get; set; } // will contain information about the game world
        public GameSession()
        {
            currentPlayer = new Player();
            currentPlayer.name = "Lord Khanh";
            currentPlayer.characterClass = "Warlord";
            currentPlayer.hitPoints = 10;
            currentPlayer.expPoints = 0;
            currentPlayer.level = 1;
            currentPlayer.gold = 10000;


            // Our game has a lot of things to instantiate. For this we introduce factory design pattern.
            // We let the factory handle for creations of objects without exposing logic to the client.
            WorldFactory worldFactory = new WorldFactory();
            currentWorld = worldFactory.CreateWorld();

            // 
            currentLocation = currentWorld.LocationAt(0,0);
        }

    }
}