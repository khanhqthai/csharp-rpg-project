using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// Base monster class
    /// Name - Monster's name
    /// ImageName - monster's image
    /// MaxHitPoints - Monster health points
    /// RewardExpPoints - Experience rewarded for killing monster
    /// RewardGold - Gold reward for killing monster
    /// Inventory - Monster's inventory
    /// </summary>
    public class Monster : BaseNotificationClass
    {
        private _hitPoints;
        
        public string Name { get; set; }
        public string ImageName { get; set; }
        public int MaxHitPoints { get; set; }
        public int RewardExpoints { get; set; }
        public int RewardGold { get; set; }
        public ObservableCollection<Item> Inventory { get; set; } =  new ObservableCollection<Item>();
    }
}
