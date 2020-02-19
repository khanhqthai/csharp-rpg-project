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


        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int RewardExpPoints { get; private set; }


        // Monster constructor
        public Monster(string name, string imageName, int maxHitPoints, int hitPoints, int minDamage, int maxDamage,
            int rewardExpPoints, int rewardGold) 
        {
            Name = name;
            ImageName = $"pack://application:,,,/Engine;component/Images/Monsters/{imageName}";
            MaxHitPoints = maxHitPoints;
            CurrentHitPoints = hitPoints;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            RewardExpPoints = rewardExpPoints;
            Gold = rewardGold;

        }
    }
}
