using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class World
    {
        private List<Location> _locations = new List<Location>();
        internal void AddLocation(int xCordinate, int yCordinate, string name, string description, string imageName) 
        {
            Location loc = new Location();
            loc.XCordindate = xCordinate;
            loc.YCordindate = yCordinate;
            loc.Name = name;
            loc.Description = description;
            loc.ImageName = imageName;
            _locations.Add(loc);
        }

        // returns a location object, if it finds it inside the _location list, else return null.
        public Location LocationAt(int xCordinate, int yCordinate) 
        {
            // loop through our _location list, if the cordinate matche return the location, else return null
            foreach (Location loc in _locations) 
            {
                if (loc.XCordindate == xCordinate && loc.YCordindate == yCordinate) {
                    return loc;
                }
                                
            }
            return null;
        }
    }
}
