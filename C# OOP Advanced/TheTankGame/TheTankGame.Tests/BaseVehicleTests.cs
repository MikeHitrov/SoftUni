using System;
using TheTankGame.Entities.Miscellaneous;
using TheTankGame.Entities.Miscellaneous.Contracts;
using TheTankGame.Entities.Parts;
using TheTankGame.Entities.Parts.Contracts;
using TheTankGame.Entities.Vehicles;
using TheTankGame.Entities.Vehicles.Contracts;

namespace TheTankGame.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class BaseVehicleTests
    {
        [Test]
        public void TrueTests()
        {
            IVehicle vehicle = new Vanguard("Tank", 100, 100m, 5, 5, 5, new VehicleAssembler());

            Assert.AreEqual("Tank", vehicle.Model);
            Assert.AreEqual(100, vehicle.Weight);
            Assert.AreEqual(100m, vehicle.Price);
            Assert.AreEqual(5, vehicle.Attack);
            Assert.AreEqual(5, vehicle.Defense);
            Assert.AreEqual(5, vehicle.HitPoints);

            var arsenal = new ArsenalPart("asd", 5, 5m, 5);
            vehicle.AddArsenalPart(arsenal);
            Assert.That(vehicle.Parts, Has.Member(arsenal));

            var shell = new ShellPart("asd", 5, 5m, 5);
            vehicle.AddShellPart(shell);
            Assert.That(vehicle.Parts, Has.Member(shell));

            var edurance = new EndurancePart("asd", 5, 5m, 5);
            vehicle.AddEndurancePart(edurance);
            Assert.That(vehicle.Parts, Has.Member(edurance));
        }

        [Test]
        public void ExceptionTests1()
        {
            IVehicle vehicle;

            Assert.That(() => vehicle = new Vanguard("", 100, 100m, 5, 5, 5, new VehicleAssembler()), Throws.ArgumentException.With.Message.EqualTo("Model cannot be null or white space!"));
            Assert.That(() => vehicle = new Vanguard(null, 100, 100m, 5, 5, 5, new VehicleAssembler()), Throws.ArgumentException.With.Message.EqualTo("Model cannot be null or white space!"));
            Assert.That(() => vehicle = new Vanguard("asd", -1, 100m, 5, 5, 5, new VehicleAssembler()), Throws.ArgumentException.With.Message.EqualTo("Weight cannot be less or equal to zero!"));
            Assert.That(() => vehicle = new Vanguard("asd", 100, -100m, 5, 5, 5, new VehicleAssembler()), Throws.ArgumentException.With.Message.EqualTo("Price cannot be less or equal to zero!"));
            Assert.That(() => vehicle = new Vanguard("asd", 1, 100m, -5, 5, 5, new VehicleAssembler()), Throws.ArgumentException.With.Message.EqualTo("Attack cannot be less than zero!"));
            Assert.That(() => vehicle = new Vanguard("asd", 1, 100m, 5, -5, 5, new VehicleAssembler()), Throws.ArgumentException.With.Message.EqualTo("Defense cannot be less than zero!"));
            Assert.That(() => vehicle = new Vanguard("asd", 1, 100m, 5, 5, -5, new VehicleAssembler()), Throws.ArgumentException.With.Message.EqualTo("HitPoints cannot be less than zero!"));

        }

        //[Test]
        //public void TotalWeight()
        //{
        //    IAssembler vehicleAssembler = new VehicleAssembler();
        //    IPart part = new ShellPart("asd", 50, 5m, 5);

        //    vehicleAssembler.AddShellPart(part);

        //    IVehicle vehicle = new Vanguard("Tank", 100, 100m, 5, 5, 5, vehicleAssembler);

        //    Assert.AreEqual(10, vehicle.TotalAttack);
        //}

        [Test]
        public void TotlalAtack()
        {
            IAssembler vehicleAssembler = new VehicleAssembler();
            IPart arsenal = new ArsenalPart("b", 1.0, 1.0m, 5);

            vehicleAssembler.AddArsenalPart(arsenal);

            IVehicle vehicle = new Vanguard("Tank", 100, 100m, 5, 5, 5, vehicleAssembler);

            Assert.AreEqual(10, vehicle.TotalAttack);
        }
    }
}