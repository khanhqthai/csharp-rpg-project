using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    // This class is not public, we only want to use this inside the Engine project(namespace)
    // we are going to make WorldFactory class internal, by default class are internal but it is good practice to declare it.
    // **internal limits	access exclusively to classes defined within the current project assembly
    internal class WorldFactory
    {
        internal World CreateWorld() 
        {
           
            World newWorld = new World();

            // Create locations for the game world
            newWorld.AddLocation(0,0,"Town Square",
                "Town square...It used to be a lot more livelier.",
                "pack://application:,,,/Engine;component/Images/Locations/town-square-new.png");

            newWorld.AddLocation(0,-1,"Home", 
                "A little broken down, But it's home", 
                "pack://application:,,,/Engine;component/Images/Locations/home.png");

            newWorld.AddLocation(-1,-1,"Farm House", 
                "The back bone of this town, farmers.", 
                "pack://application:,,,/Engine;component/Images/Locations/farm-house.png");
            
            newWorld.AddLocation(-2, -1, "Farm Field",
                "Looks like the seasons have been well for farming",
                "pack://application:,,,/Engine;component/Images/Locations/farm-field.png");

            newWorld.AddLocation(-1, 0, "Trade Shop",
                "One stop shop for all things",
                "pack://application:,,,/Engine;component/Images/Locations/trader.png");

            newWorld.AddLocation(0, 1, "Herbal Hut",
                "Old school smoke shop",
                "pack://application:,,,/Engine;component/Images/Locations/herbalist-hut.png");

            newWorld.AddLocation(0, 2, "Herb Garden",
                           "A garden full of the lastest herbs",
                           "pack://application:,,,/Engine;component/Images/Locations/herbalists-garden.png");

            newWorld.AddLocation(0, 3, "Hidden Path",
               "Hmm, where does this lead to",
               "pack://application:,,,/Engine;component/Images/Locations/herbalist-garden.png");

            newWorld.AddLocation(1, 0, "Town Gate",
               "Two big wooden doors",
               "pack://application:,,,/Engine;component/Images/Locations/town-gate.png");

            newWorld.AddLocation(2, 0, "Forest",
                       "Spiders roam these woods",
                       "pack://application:,,,/Engine;component/Images/Locations/forest.png");

            newWorld.AddLocation(3, 0, "Mountain Foothill",
               "Proceed with caution",
               "pack://application:,,,/Engine;component/Images/Locations/foothill.png");


            return newWorld;
        }
    }
}
