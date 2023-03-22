using ElevatorConsole.Interfaces;
using IElevatorConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElevatorsApplication.Tests
{
    [TestClass]
    public class BuildingTests
    {
        [TestMethod]
        public static void TestCanCreateSystem()
        {
            IControlSystem system = new ControlSystem(3);

           Assert.IsNotNull(system);
        }
    }
    
}
