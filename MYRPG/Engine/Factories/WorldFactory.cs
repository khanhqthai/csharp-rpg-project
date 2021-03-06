﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    /// <summary>
    /// WorldFactory Class - builds locations, quests, monsters, merchants
    /// </summary>
    /// <remarks>
    /// This class is not public, we only want to use this inside the Engine project(namespace)
    /// we are going to make WorldFactory class internal, by default class are internal but it is good practice to declare it.
    /// internal - limits access exclusively to classes defined within the current project assembly
    /// We also are going make this class static, because in our RPG, we will only need one game world. 
    /// We do not need multiple instances of the World. By default all classes are instance classes
    /// unlike the Player class, where there can be  more than one player in the world. 
    /// static class can only contain static data members, static methods, and a static constructor
    /// It is not allowed to create objects of the static class.
    /// Static classes are sealed, means you cannot inherit a static class from another class.
    /// static class can only contain static data members and static methods
    /// </remarks>
    internal static class WorldFactory
    {
        internal static World CreateWorld() 
        {
           
            World newWorld = new World();

            // Create locations for the game world
            newWorld.AddLocation(0,0,"Town Square",
                "Town square...It used to be a lot more livelier.",
                "town-square-new.png");

            newWorld.AddLocation(0,-1,"Home", 
                "A little broken down, But it's home", 
                "home.png");
            newWorld.LocationAt(0, -1).QuestAvailableHere.Add(QuestFactory.GetQuestByID(1));

            newWorld.AddLocation(-1,-1,"Farm House", 
                "The back bone of this town, farmers.", 
                "farm-house.png");
            /* add quest to Farm House location. 
             * Note: We are able to use "." QuestAvailableHere() method, 
             * because newWorld.LocationAt() returns a location object
             * we can use the property on the object, similiar to "." chaining in JavaScript
             * 
             * We can also add quest the following way:
             *  Location foo = newWorld.location(-1,-1)
             *  foo.QuestAvailableHere.Add(QuestFactory.GetQuestByID(0));
             * but we would create the temporary foo variable */
            newWorld.LocationAt(-1, -1).QuestAvailableHere.Add(QuestFactory.GetQuestByID(0));


            newWorld.AddLocation(-2, -1, "Farm Field",
                "Looks like the seasons have been well for farming",
                "farm-field.png");
            newWorld.LocationAt(-2, -1).AddMonster(2,100); // add mushroom monster to Farmer's Field
            newWorld.AddLocation(-1, 0, "Merchant Shop",
                "One stop shop for all things",
                "trader.png");
            // add merchant to location
            newWorld.LocationAt(-1, 0).MerchantHere = MerchantFactory.getMerchantByName("Tim 'Last King'");

            newWorld.AddLocation(0, 1, "Herbal Hut",
                "Old school smoke shop",
                "herbalist-hut.png");
            newWorld.LocationAt(0, 1).MerchantHere = MerchantFactory.getMerchantByName("Herbalist Adria");

            newWorld.AddLocation(0, 2, "Herb Garden",
                           "A garden full of the lastest herbs",
                           "herbalists-garden.png");

            newWorld.AddLocation(0, 3, "Hidden Path",
               "Hmm, where does this lead to",
               "herbalists-garden.png");

            newWorld.AddLocation(1, 0, "Town Gate",
               "Two big wooden doors",
               "town-gate.png");

            newWorld.AddLocation(2, 0, "Forest",
                       "Goblins roam these woods",
                       "forest.png");
            newWorld.LocationAt(2, 0).AddMonster(4,100); // add green gobin to forest location

            newWorld.AddLocation(3, 0, "Mountain Foothill",
               "Proceed with caution",
               "foothill.png");

            return newWorld;
        }
    }
}
