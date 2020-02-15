using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// MonsterEncounter class - handles chance encounters at location
    /// MonsterID - is the ID of the monster we want
    /// ChanceOfEncountering - is percantage that we will encounter a monster at said location.
    /// </summary>
    public class MonsterEncounter
    {
        public int MonsterID { get; set; }
        public int ChanceOfEncountering { get; set; }

        public MonsterEncounter(int monsterID, int chanceOfEncountering) 
        {
            MonsterID = monsterID;
            ChanceOfEncountering = chanceOfEncountering;
        }
    }
}
