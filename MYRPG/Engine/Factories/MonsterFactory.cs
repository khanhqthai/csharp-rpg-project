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

        // Creates a monster  using switch case logic, and returns a monster object accordingly
        public static Monster GetMonster(int monsterID) 
        {
            switch (monsterID) 
            {
                /*  Monster(
                 *  string name, 
                 *  string imageName, 
                 *  int maxHitPoints, 
                 *  int hitPoints, 
                 *  int minDamage, 
                 *  int maxDamage,
                 *  int rewardExpPoints, 
                 *  int rewardGold) 
                 */
                case 1:
                    Monster skeleton = new Monster("Skeleton", 1, 5, 1, 2, 3, 2, "skeleton.png");
                    // add loot to monster's inventory base on chance percentage
                    AddLootItem(skeleton, 2004, 50); // 50% chance to add skeleton bones
                    AddLootItem(skeleton, 2005, 25); // 25% chance to add skeleton shield
                    return skeleton;

                case 2:
                    Monster redEyeMushroom = new Monster("Red Eye Mushroom", 1,  3, 1, 2, 10, 1, "red-eye-mushroom.png");
                    AddLootItem(redEyeMushroom, 2003, 100);
                    return redEyeMushroom;

                case 3:
                    Monster oneEyeBat = new Monster("One Eye Bat", 1,  4, 1, 2, 2, 1, "one-eye-bat.png");
                    AddLootItem(oneEyeBat, 2000, 100);
                    return oneEyeBat;

                case 4:
                    Monster greenGoblin = new Monster("Green Goblin", 1,5, 1, 2, 3, 2, "green-goblin.png");
                    AddLootItem(greenGoblin, 2001, 100);
                    AddLootItem(greenGoblin, 2002, 25);
                    return greenGoblin;

                default:
                    // throw exception if monsterID does not correspond to one of our cases
                    throw new ArgumentException(string.Format("Monster type ID {0} does not exist", monsterID));
            }

        }

        /* AddLootItem function will randomly add an item to the monster inventory base on percentage
           itemID - is the ID of the item we want to add 
           percentage - is the chance of the item being added(up to 100%)*/
        private static void AddLootItem(Monster monster, int itemID, int percentage) 
        {
            if (RandomNumberGenerator.NumberBetween(1,100) <= percentage) 
            {
                monster.AddItemToInventory(ItemFactory.CreateItem(itemID));
            }
        }
    }
}
