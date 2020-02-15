using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    /// <summary>
    /// Factory class to create our quests in the game
    /// internal - because we only want this to be used in Engine.Factories namespace only
    /// static - because we are not going to instantiate it, but just use the functions in it.
    /// Similiar to WorldFactory class.
    /// </summary>
    internal static class QuestFactory
    {
        // will contain all our quests for the game
        private static readonly List<Quest> _quest = new List<Quest>();

        /* static class do not have constructors because it never is instantiated, 
         * there no object created, so no constructor called.
         * but it does however for the first time any one runs anything in the the class, a function is called
         * we will use that to create our items and populate our List _quest
        */

        // QuestFactory() function gets called the first time anything is used inside the QuestFactory class.
        static QuestFactory() 
        {
           
            // Declare  list of items required to complete quest, and  rewards
            List<ItemQuantity> requiredQuestItems = new List<ItemQuantity>();
            List<ItemQuantity> rewardItems = new List<ItemQuantity>();

        
            requiredQuestItems.Add(new ItemQuantity(2003, 4)); // mushroom stems, quantity 4
            rewardItems.Add(new ItemQuantity(1001,1)); // Wooden Stick, quantity 1
            
            // create Quests 
            _quest.Add(new Quest(0, 
                "Mushroom Problems",
                "Get rid the pesky mushrooms in the farm  field", 
                requiredQuestItems, 1, 1, rewardItems));

            _quest.Add(new Quest(1,
                "Stop the encroachment",
                "Towns peoples would like you remove the goblins from the forest",
                 new List<ItemQuantity> { new ItemQuantity(2001, 5) }, 
                 1, 1,
                 new List<ItemQuantity> { new ItemQuantity(1002, 1) })); // Quest item: 5 Goblin Ear, Reward: 1 Wooden Sword
        }

        internal static Quest GetQuestByID(int id) 
        {
            /* instead of using a ForEach and loop through our List and find the item.
             * makes our code cleaner, easier to maintain.
             * It's functional program...always great for large projects.
             * We can use LINQ: Langauge-Intergrated Query to retrieve our item from the List _quest
             * Our query will return the first item in the list where Quest.ID is equal  id we passed in.
             * If it does not find any thing, the default value is null */
            return _quest.FirstOrDefault(quest => quest.ID == id);
        }
    }
}
