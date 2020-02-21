using System;
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

            Assert.IsNotNull(gameSession.CurrentPlayer);
            Assert.AreEqual("Home", gameSession.CurrentLocation.Name);
           

        }
    }
}
