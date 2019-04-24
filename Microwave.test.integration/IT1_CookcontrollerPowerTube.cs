using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit;
using MicrowaveOvenClasses;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace Microwave.test.integration
{
    [TestFixture]
    class IT1_CookcontrollerPowerTube
    {
        private CookController UUTcookController_;
        private IUserInterface userInterface_;
        private IDisplay display_;
        private PowerTube UUTpowerTube_;
        private ITimer timer_;

        private IOutput output_;

        [SetUp]
        public void Setup()
        {
            userInterface_ = Substitute.For<IUserInterface>();
            timer_ = Substitute.For<ITimer>();
            display_ = Substitute.For<IDisplay>();
            output_ = Substitute.For<IOutput>();

            UUTpowerTube_ = new PowerTube(output_);
            UUTcookController_ = new CookController(timer_, display_, UUTpowerTube_);

            UUTcookController_.UI = userInterface_;
        }

        [TestCase(50, "50")]
        [TestCase(100, "100")]
        [TestCase(1, "1")]
        public void startCooking_cookWithPowerAndTime_OutputContainsCorrectPower(int s1, string expectedResult)
        {
            UUTcookController_.StartCooking(s1,30);

            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains(expectedResult)));

            //Assert.That(output_.rec, Is.EqualTo("PowerTube works with 50 %"));
            // ovenstående hvor metoden recieved benyttes, svarer til at lave en assert
        }

        [TestCase(101, "101")]
        [TestCase(0, "0")]
        public void startCooking_cookWithPowerAndTime_OutputDontContainsCorrectPower(int s1, string expectedResult)
        {
            UUTcookController_.StartCooking(s1, 30);

            output_.DidNotReceive().OutputLine(Arg.Is<string>(s => s.Contains(expectedResult)));

          
        }
    }
}
