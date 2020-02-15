using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Factories;

namespace Engine.Models
{
    /// <summary>
    /// Class describing our locations in the game
    /// X,Y cordinate describe where each location is located on the map.
    /// Name: name of the location
    /// Description: describes the location
    /// ImageName: contain link to the image of the location
    /// List<Quest>: contains quests available at location.
    /// List<MonsterEncounter>: contains a list of monsters(to fight/encounter) at location.
    /// </summary>
    public class Location
    {
        public int XCordindate { get; set; }
        public int YCordindate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public List<Quest> QuestAvailableHere { get; set; } = new List<Quest>();
        public List<MonsterEncounter> MonstersHere { get; set; } = new List<MonsterEncounter>();

        // function to add monster encounter to MonstersAvailableHere
        public void AddMonster(int monsterID, int chanceOfEncountering) 
        {
            // check if monster is already in the list(MonsterHere) 
            // if so replace the ChanceOfEncountering value with new chanceOfEncountering value
            if (MonstersHere.Exists(m => m.MonsterID == monsterID))
            {
                MonstersHere.First(m => m.MonsterID == monsterID).ChanceOfEncountering = chanceOfEncountering;
            }
            else 
            {
                // add new mounter encounter to the list(MonsterHere)
                MonstersHere.Add(new MonsterEncounter(monsterID, chanceOfEncountering));
            }
        }

        public  Monster GetMonster() 
        {
            // if MonstersHere list is empty, return null
            if (!MonstersHere.Any()) 
            {
                return null;
            }

            //total percentages of all monsters at this location
            int totalChances = MonstersHere.Sum(m => m.ChanceOfEncountering);
            // Select a random number between 1 and the total (in case the total chances is not 100).
            int randomNumber = RandomNumberGenerator.NumberBetween(1, totalChances);

            /* Loop through the monster list, 
               adding the monster's percentage chance of appearing to the runningTotal variable.
               When the random number is lower than the runningTotal,
               that is the monster to return. */
            int runningTotal = 0;

            foreach (MonsterEncounter monsterEncounter in MonstersHere)
            {
                runningTotal += monsterEncounter.ChanceOfEncountering;

                if (randomNumber <= runningTotal)
                {
                    return MonsterFactory.GetMonster(monsterEncounter.MonsterID);
                }
            }

            // return the last monster in the list if the above code failed
            return MonsterFactory.GetMonster(MonstersHere.Last().MonsterID);

        }

    }
}
