using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{   
    /// <summary>
    /// Class describing our locations in the game
    /// X,Y cordinate describe where each location is located on the map.
    /// Name: name of the location
    /// Description: describes the location
    /// ImageName: contain link to the image of the location
    /// List<Quest>: contains a list of quests, available at location.
    /// </summary>
    public class Location
    {
        public int XCordindate { get; set; }
        public int YCordindate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public List<Quest> QuestAvailableHere { get; set; } = new List<Quest>(); 

    }
}
