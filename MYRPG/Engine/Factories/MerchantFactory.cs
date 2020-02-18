using System;
using System.Collections.Generic;
using Engine.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    /// <summary>
    /// MerchantFactory class to create all the Merchant in our game world.  
    /// </summary>
    public static class MerchantFactory
    {
        private static readonly List<Merchant> _merchants = new List<Merchant>();

        static MerchantFactory()
        {
            Merchant tim = new Merchant("Tim 'Last King'");
            tim.AddItemToInventory(ItemFactory.CreateItem(0));
            tim.AddItemToInventory(ItemFactory.CreateItem(1000));
            tim.AddItemToInventory(ItemFactory.CreateItem(1001));
            tim.AddItemToInventory(ItemFactory.CreateItem(1002));
            tim.AddItemToInventory(ItemFactory.CreateItem(1003));
            Merchant herbalistAdria = new Merchant("Herbalist Adria");
            herbalistAdria.AddItemToInventory(ItemFactory.CreateItem(0));


            AddToMerchantList(tim);
            AddToMerchantList(herbalistAdria);

        }

        // return Merchant if found in list _merchants else return null
        public static Merchant getMerchantByName(string name) 
        {
            return _merchants.FirstOrDefault(m => m.Name == name);
        }

        // adds Merchant to _merchant List
        private static void AddToMerchantList(Merchant merchant)
        {
            if (_merchants.Any(m => m.Name == merchant.Name )) 
            {
                throw new ArgumentException($"There is already a merchant named {merchant.Name} in the List");
            }
            _merchants.Add(merchant);
        }
    }
}
