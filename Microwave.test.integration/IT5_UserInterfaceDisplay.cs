using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Smtp;
using MicrowaveOvenClasses;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;

namespace Microwave.test.integration
{

    [TestFixture]
    class IT5_UserInterfaceDisplay
    {
        private UserInterface uutUserInterface_;
        private ICookController cookController_;
        private IPowerTube powerTube_;
        private IDisplay UUTdisplay_;

        private ITimer timer_;
        private ILight light_;
        private IOutput output_;
        private IButton startCancelButton_;
        private IButton timeButton_;
        private IButton powerButton_;
        private IDoor door_; 

        [SetUp]
        public void setup()
        {
            timer_ = Substitute.For<ITimer>();
            light_ = Substitute.For<ILight>();
            output_ = Substitute.For<IOutput>();
            startCancelButton_ = Substitute.For<IButton>();
            powerButton_ = Substitute.For<IButton>();
            timeButton_ = Substitute.For<IButton>();
            door_ = Substitute.For<IDoor>();

            powerTube_ = new PowerTube(output_);
            UUTdisplay_ = new Display(output_);
            cookController_ = new CookController(timer_, UUTdisplay_, powerTube_);
            uutUserInterface_ = new UserInterface(powerButton_, timeButton_, startCancelButton_, door_, UUTdisplay_, light_, cookController_);

        }

        [TestCase(50)]
        [TestCase(250)]
        [TestCase(700)]
        public void showPower_showWithPower_outputContainsCorrectPower(int power)
        {
            for (int i = 0; i < power/50; i++)
            {
                powerButton_.Pressed += Raise.Event();
            }

            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains(Convert.ToString(power))));
        }

        [Test]
        public void clear_clears_outputIsEmpty()
        {
            powerButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            startCancelButton_.Pressed += Raise.Event();

            door_.Opened += Raise.Event(); 

            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains("Display cleared")));
        }

        [TestCase(1,0)]
        public void showTime_showWithMinAndSec_outputContainsCorrectMinAndSec(int expectedmin, int expectedsec)
        {
            powerButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            startCancelButton_.Pressed += Raise.Event();

            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains(Convert.ToString(expectedmin)) && s.Contains(Convert.ToString(expectedsec))));
        }

        [TestCase(10, 0)]
        public void showTime_showWithMinAndSec_outputContainsWrongMinAndSEC(int expectedmin, int expectedsec)
        {
            powerButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            startCancelButton_.Pressed += Raise.Event();

            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains(Convert.ToString(expectedmin)) && s.Contains(Convert.ToString(expectedsec))));
        }

    }
}
