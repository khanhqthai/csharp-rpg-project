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
    /// internal - because we only want this to be used in Engine namespace only
    /// static - because we are just going to use the class, we don't need instances of it.
    /// there should only be one factory
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
           
            // Declare items required to complete quest, and  rewards
            List<ItemQuantity> requiredQuestItems = new List<ItemQuantity>();
            List<ItemQuantity> rewardItems = new List<ItemQuantity>();

            // add item requirements - itemID = 1 for snake skin, quantity = 2
            requiredQuestItems.Add(new ItemQuantity(1,2));
            // add reward item - itemID=1001 for fine wooden stick, quantity = 1
            rewardItems.Add(new ItemQuantity(1001,1));

            // create Quests and added it to our list of quests in the game
            _quest.Add(new Quest(0, 
                "Snake Problem",
                "Get rid the pesky snakes in the farm  field", 
                requiredQuestItems, 1, 1, rewardItems));
            _quest.Add(new Quest(1,
                "Silk Dreams",
                "Town merchants love spider silk.",
                 new List<ItemQuantity> { new ItemQuantity(2, 5) }, 
                 1, 1,
                 new List<ItemQuantity> { new ItemQuantity(0, 1) }));
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
