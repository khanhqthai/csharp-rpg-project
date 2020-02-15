﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class Monster : BaseNotificationClass
    {
        private int _hitPoints;
        
        public int HitPoints 
        {
            get { return _hitPoints; }
            private set 
            {

                _hitPoints = value;
                // when hits points change
                OnPropertyChanged(nameof(HitPoints));
            }
        }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public int MaxHitPoints { get; set; }
        public int RewardExpPoints { get; set; }
        public int RewardGold { get; set; }
        public ObservableCollection<ItemQuantity> Inventory { get; set; }

        // Monster constructor
        public Monster(string name, string imageName, int maxHitPoints, int hitPoints,
            int rewardExpPoints, int rewardGold) 
        {
            Name = name;
            ImageName = string.Format("pack://application:,,,/Engine;component/Images/Monsters/{0}", imageName);
            MaxHitPoints = maxHitPoints;
            HitPoints = hitPoints;
            RewardExpPoints = rewardExpPoints;
            RewardGold = rewardGold;
            Inventory = new ObservableCollection<ItemQuantity>(); // assign empty List so we can add items to it.
        }
    }
}
