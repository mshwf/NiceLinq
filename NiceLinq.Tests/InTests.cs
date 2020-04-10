using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mshwf.NiceLinq;
using System.Linq;

namespace NiceLinq.Tests
{
    [TestClass]
    public class InTests
    {
        [TestMethod]
        public void InTest()
        {
            var fleet = Models.FleetFactory.CreateFleet();
            var someCars = Mshwf.NiceLinq.Enumerable.In(fleet.Cars, x => x.Id, 1, 2, 3).ToList();

            Assert.AreEqual(someCars[0].Id, 1);
            Assert.AreEqual(someCars[1].Id, 2);
            Assert.AreEqual(someCars[2].Id, 3);
            Assert.AreEqual(someCars.Count, 3);
        }
        [TestMethod]
        public void NotInTest()
        {
            var fleet = Models.FleetFactory.CreateFleet();
            var someCars = Mshwf.NiceLinq.Enumerable.NotIn(fleet.Cars, x => x.Id, 1, 2, 3).ToList();

            Assert.IsFalse(someCars.Any(x => x.Id == 1));
            Assert.IsFalse(someCars.Any(x => x.Id == 2));
            Assert.IsFalse(someCars.Any(x => x.Id == 3));
        }
    }
}
