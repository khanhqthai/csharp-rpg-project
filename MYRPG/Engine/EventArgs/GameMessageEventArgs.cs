using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.EventArgs
{
    /// <summary>
    /// GameMessageEventArgs class - create game message events
    /// GameMessageEventArgs - inherits from System.EventArgs(stanard system events args used with raising a message)
    ///                        we will use it to make our  custom GameMessageEventArgs class
    ///  Getter - public because we want Message value to be read from any part of our program.  In our case GameSession.cs
    ///  Setter - private because we only want our class to be able to set it.  
    /// </summary>
    public class GameMessageEventArgs : System.EventArgs
    {
        public string Message { get; private set; }
        public GameMessageEventArgs(string message) 
        {
            Message = message;
        }
    }
}
