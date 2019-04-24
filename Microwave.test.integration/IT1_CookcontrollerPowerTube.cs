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

        [TestCase(50)]
        [TestCase(700)]
        [TestCase(350)]
        public void startCooking_cookWithPowerAndTime_OutputContainsCorrectPower(int s1)
        {
            UUTcookController_.StartCooking(s1,30);

            output_.Received(1).OutputLine(Arg.Is<string>(s => s.Contains(Convert.ToString(s1))));

            //Assert.That(output_.rec, Is.EqualTo("PowerTube works with 50 %"));
            // ovenstående hvor metoden recieved benyttes, svarer til at lave en assert
        }

        [TestCase(49)]
        [TestCase(701)]
        public void startCooking_cookWithPowerAndTimeOutOfRange_ThrowsException(int s1)
        {
            
            Assert.That(() => UUTcookController_.StartCooking(s1, 30), Throws.TypeOf<ArgumentOutOfRangeException>());
            
        }

        [Test]
        public void startCooking_IsOnAlready_ThrowsException()
        {
            UUTcookController_.StartCooking(60,30);

            Assert.That(() => UUTcookController_.StartCooking(200, 30), Throws.TypeOf<ApplicationException>());
        }

        [Test]
        public void StopCooking_TurnOff()
        {
            UUTcookController_.StartCooking(350, 30);

            UUTcookController_.Stop();
            output_.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("PowerTube turned off")));
           
        }

    }
}
