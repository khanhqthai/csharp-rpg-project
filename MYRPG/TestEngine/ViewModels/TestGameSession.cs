using System;
using System.Linq;
using Engine.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TestEngine.ViewModels
{
    [TestClass] // atrribute to declare this to be a unit test class
    public class TestGameSession
    {
        [TestMethod] // attribute to declare this is a unit test function
        public void TestCreateGameSession()
        {
            GameSession gameSession = new GameSession();
            
            // Player should not be null.
            Assert.IsNotNull(gameSession.CurrentPlayer); 
            
            // Player should have starting item Fine Wooden Stick in inventory
            Assert.AreEqual("Fine Wooden Stick", gameSession.CurrentPlayer.Inventory.First(item => item.Name == "Fine Wooden Stick").Name);

            // Checks the starting location for player is his home.
            Assert.AreEqual("Home", gameSession.CurrentLocation.Name); 
            
        }
    }
}
