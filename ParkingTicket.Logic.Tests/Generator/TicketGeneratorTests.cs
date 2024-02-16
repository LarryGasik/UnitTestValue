using System;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ParkingTicketLogic.Generators;


namespace ParkingTicketLogic.Test.Generator
{
    [TestFixture]
    public class TicketGeneratorTests
    {
        private TicketGenerator _sut;
        [SetUp]
        public void SetUp()
        {
            _sut = new TicketGenerator();
        }

        [Test]
        public void NothingReturnedWhenThereIsNoTicketAndNoTicketIssued()
        {
            string result = _sut.InstructionGenerator(false, false);
            ClassicAssert.AreEqual(string.Empty, result);
        }

        [Test]
        public void TowReturnedWhenTowFlagged()
        {
            string result = _sut.InstructionGenerator(true, false);
            ClassicAssert.AreEqual("Tow", result);
        }

        [Test]
        public void TicketReturnedWhenTicketFlagged()
        {
            string result = _sut.InstructionGenerator(false, true);
            ClassicAssert.AreEqual("here's your ticket", result);

            //Note: Normally you want to not have multiple asserts in a test, however, the set up is the same, and this
            //      will reduce maintenance, even if when the test fails, multiple things could have failed.
            ClassicAssert.IsFalse(result.Contains("Tow"));
        }

        [Test]
        public void DirectionsShouldBeOnTwoLinesWhenTowingAndIssuingTicket()
        {
            string result = _sut.InstructionGenerator(true, true);
            ClassicAssert.IsTrue(result.Contains(Environment.NewLine));
        }
    }
}
