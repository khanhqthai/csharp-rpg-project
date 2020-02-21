using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// Monster class
    /// Name - Monster's name
    /// ImageName - monster's image to display on UI
    /// _hitPoints - monster's current hit points. we need to update the changes  hit points to the UI.
    /// MaxHitPoints - Monster  max health point
    /// RewardExpPoints - Experience rewarded for killing monster
    /// RewardGold - Gold reward for killing monster
    /// Inventory - Monster's inventory
    /// 
    /// Monster class will inherits base class BaseNotificationClass because we want to use OnPropertyChanged to notify the UI
    /// </summary>
    public class Monster : LivingEntity
    {


        public int MinDamage { get; }
        public int MaxDamage { get;  }
        public string ImageName { get; }
        public int RewardExpPoints { get; }


        // Monster constructor
        public Monster(string name, int level,  int maxHitPoints, int minDamage, int maxDamage,
            int rewardExpPoints, int rewardGold, string imageName) : base(name, maxHitPoints, maxHitPoints, rewardGold, level)
        {
            ImageName = $"pack://application:,,,/Engine;component/Images/Monsters/{imageName}";
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            RewardExpPoints = rewardExpPoints;
        }
    }
}
