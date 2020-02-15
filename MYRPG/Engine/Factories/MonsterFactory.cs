using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    /// <summary>
    /// MonsterFactory class - class generate/create monsters in our game
    /// internal - because we only want this to be used in Engine.Factories namespace only
    /// static - because we are not going to instantiate it, but just use the functions in it.
    ///          similiar to WorldFactory class.
    /// 
    /// </summary>
    internal static class MonsterFactory
    {

        // Creates a monster base on switch case logic, and returns a monster object accordingly
        public static Monster GetMonster(int monsterID) 
        {
            switch (monsterID) 
            { 

                case 1:
                    Monster skeleton = new Monster("Skeleton", "skeleton.png", 5, 5, 3, 2);
                    // add loot to monster's inventory base on chance percentage
                    AddLootItem(skeleton, 2004, 50); // 50% chance to add skeleton bones
                    AddLootItem(skeleton, 2005, 25); // 25% chance to add skeleton shield
                    return skeleton;

                case 2:
                    Monster redEyeMushroom = new Monster("Red Eye Mushroom", "red-eye-mushroom.png", 3, 3, 1, 1);
                    AddLootItem(redEyeMushroom, 2003, 100);
                    return redEyeMushroom;

                case 3:
                    Monster oneEyeBat = new Monster("One Eye Bat", "one-eye-bat.png", 4, 4, 2, 1);
                    AddLootItem(oneEyeBat, 2000, 100);
                    return oneEyeBat;

                case 4:
                    Monster greenGoblin = new Monster("Green Goblin", "green-goblin.png", 5, 5, 3, 2);
                    AddLootItem(greenGoblin, 2001, 100);
                    AddLootItem(greenGoblin, 2002, 25);
                    return greenGoblin;

                default:
                    // throw exception if monsterID does not correspond to one of our cases
                    throw new ArgumentException(string.Format("Monster type ID {0} does not exist", monsterID));
            }

        }

        /* AddLootItem function will randomly add an item to the monster inventory base on percentage
           itemID is the ID of the item we want to add 
           percentage, is the chance of the item being add(up to 100%)*/
        private static void AddLootItem(Monster monster, int itemID, int percentage) 
        {
            if (RandomNumberGenerator.NumberBetween(1,100) <= percentage) 
            {
                monster.Inventory.Add(new ItemQuantity(itemID, 1));
            }
        }
    }
}
