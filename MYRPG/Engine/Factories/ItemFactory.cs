using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models; // import so we can Item class(Item.cs)

namespace Engine.Factories
{
    /// <summary>
    /// ItemFactory class - this class will create all the items in our game world.  
    /// static class - because we are not going to instantiate it, but just use the functions in it.
    ///                Similiar to WorldFactory class.
    /// internal - limits access exclusively to classes defined within the current project assembly
    ///  A List container will hold all generated items
    /// </summary>
    internal static class ItemFactory
    {
        private static List<Item> _standardItems;

        /* static class do not have constructors because it is never instantiated, 
         * there no objects created, so no constructor called.
         * but it does however have a feature where, 
         * the first time anything is ran in the the class, a function is called
         * we will use that feature to create our items and populate our _standardItems*/

        // ItemFactory() function gets called the first time anything is used inside the ItemFactory class.
        // This is where we will generate our items for our game world.
        static ItemFactory() 
        {
            _standardItems = new List<Item>();
            _standardItems.Add(new Weapon(1000,"Wooden Stick",1,1,1));
            _standardItems.Add(new Weapon(1001, "Fine Wooden Stick", 2, 1, 2));
            _standardItems.Add(new Weapon(1002, "Wooden Sword", 3, 1, 3));
            _standardItems.Add(new Weapon(1003, "Goblin Sword", 3, 1, 3));
            _standardItems.Add(new Item(0,"Jade Pendant",5));
            _standardItems.Add(new Item(2000, "One Eye Bat's Eyes", 1));
            _standardItems.Add(new Item(2001, "Goblin Ear", 1));
            _standardItems.Add(new Item(2002, "Wooden Shield", 1));
            _standardItems.Add(new Item(2003, "Mushroom Stem", 1));
            _standardItems.Add(new Item(2004, "Skeleton Bone", 1));
            _standardItems.Add(new Item(2005, "Skeleton Shield", 1));

        }

        // Good through our list of available game items, Copy/Clones it and returns that item.
        public static Item CreateItem(int itemTypeID) 
        {
            /* instead of using a ForEach and loop through our List and find the item.
             * We can use LINQ: Langauge-Intergrated Query to retrieve our item from the list(_standardItems)
             * Our query will return the first item in the list where ItemTypeID is equal the item type id we passed in.
             * If it does not find any thing, the default value is null  */
            Item standardItem = _standardItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID);

            if (standardItem != null) 
            {

                /* normally we would return the item, but we want to be able to modify this item in the future.
                 * like add a gem to the item, or upgrade it, trade the item.  We want  Player to have it's own unique item.
                 * We can do this by making a copy of it, or often called cloning.
                 */
                if (standardItem is Weapon) {
                    // cast stanardItem as Weapon so we can use Weapon.Clone() function instead of Item.Clone();
                    return (standardItem as Weapon).Clone();
                }
                return standardItem.Clone();
            
            }

            // returns null if item is not found, We need to address  this case later
            return null;
            
        }
    }
}
