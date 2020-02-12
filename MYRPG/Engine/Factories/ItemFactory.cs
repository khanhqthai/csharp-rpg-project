using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models; // import so we can Item class(Item.cs)

namespace Engine.Factories
{
    // this is a static class, because we are just going to use the class and do not instances of it. Similiar to WorldFactory class.
    // this factory class will create all the items in our game world.  
    // we use a List to hold thesse items
    public static class ItemFactory
    {
        private static List<Item> _standardItems;

        /* static class do not constructors because it never instantiate, there no object created, so no constructor called.
         * but it does have a functions, the first time any one runs anything in the the class call this function.
         * we will use that to create our items and populate our List(_standardItems)
        */

        // ItemFactory() gets called the first time anything is used inside the ItemFactory class.
        static ItemFactory() 
        {
            _standardItems = new List<Item>();
            _standardItems.Add(new Weapon(1000,"Wooden Stick",1,1,1));
            _standardItems.Add(new Weapon(1001, "Fine Wooden Stick", 2, 1, 2));
            _standardItems.Add(new Weapon(1002, "Wooden Sword", 3, 1, 3));
            _standardItems.Add(new Item(0,"Jade Pendant",5));

        }

        public static Item CreateItem(int itemTypeID) 
        {
            /* instead of using a ForEach and loop through our List and find the item.
             * We can use LINQ: Langauge-Intergrated Query to retrieve our item from the list(_standardItems)
             * Our query will return the first item in the list where ItemTypeID is equal the item type id we passed in.
             * If it does not find any thing, the default value is null
            */
            Item standardItem = _standardItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID);

            if (standardItem != null) 
            {
                /* normally we would return the item, but we want to be able to modify this item in the future.
                 * like add a gem to the item, or upgrade it, trade the item.  We want each Player to have it's own unique item.
                 * We can do this by making a copy of it or often called cloning.
                 * We will modify the Item class to add a cloning function
                 */
                return standardItem.Clone();
            
            }

            // returns null if item is not found, We need to address  this case later
            return null;
            
        }
    }
}
