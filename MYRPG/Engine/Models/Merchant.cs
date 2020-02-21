using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// Merchant class - class to create all our merchants in our game
    /// </summary>
    /// <remarks>
    /// Players can buy or sell item from a merchant
    /// </remarks>

    public class Merchant : LivingEntity
    {

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="name">Name of merchant.</param>
        public Merchant(string name) : base(name,9999,9999,9999,9999)
        {
     
        }

    }
}
