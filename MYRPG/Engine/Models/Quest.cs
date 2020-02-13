using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// This is our  class for quests in the game
    /// Quests have conditions to complete: kill x number of monster or bring back x number of stuff..etc
    /// Quests have rewards: can be gold,items,experience...etc
    /// </summary>
    class Quest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ItemQuantity> RequiredQuestItems { get; set; }

        public int RewardExpPoints { get; set; }
        public int RewardGold { get; set; }
        public List<ItemQuantity> RewardItems { get; set; }
        public Quest(int id, string name, string description, List<ItemQuantity> requiredQuestItems, 
            int rewardExpPoints, int rewardGold, List<ItemQuantity> rewardItems ) 
        {
            ID = id;
            Name = name;
            Description = description;
            RequiredQuestItems = requiredQuestItems;
            RewardExpPoints = rewardExpPoints;
            RewardGold = rewardGold;
            RewardItems = rewardItems;
        }

    }
}
