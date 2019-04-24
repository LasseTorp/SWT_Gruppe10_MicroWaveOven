using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.test.integration
{
    class IT4_UICookController
    {
        private ILight light_;
        private IDisplay display_;
        private IOutput output_;
        private IButton powerButton_;
        private IButton timeButton_;
        private IButton startCancelButton_;
        private IDoor door_;
        private ITimer fakeTimer_;

        private Timer timer_;
        private PowerTube powerTube_;
        private CookController UUTcookController_;
        private UserInterface UUTUserInterface_;

        

        [SetUp]
        public void setUp()
        {
            output_ = Substitute.For<IOutput>();
            light_ = Substitute.For<ILight>();
            display_ = Substitute.For<IDisplay>();
            powerButton_ = Substitute.For<IButton>();
            timeButton_ = Substitute.For<IButton>();
            startCancelButton_ = Substitute.For<IButton>();
            door_ = Substitute.For<IDoor>();
            
            timer_ = new Timer();
            powerTube_ = new PowerTube(output_);
            UUTcookController_ = new CookController(timer_, display_, powerTube_);
            UUTUserInterface_ = new UserInterface(powerButton_, timeButton_, startCancelButton_, door_, display_, light_, UUTcookController_);

            UUTcookController_.UI = UUTUserInterface_;

        }

        [Test]
        public void powerIsSet_PowerTimeAndStartButtonPressed_outputRecievedString()
        {
            powerButton_.Pressed += Raise.Event();
            powerButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            startCancelButton_.Pressed += Raise.Event();

            output_.Received().OutputLine(Arg.Any<string>());
            
        }

        [TestCase(1,50)]
        [TestCase(5, 250)]
        [TestCase(10, 500)]
        [TestCase(14, 700)]
        [TestCase(15, 50)]
        public void powerIsSet_ClicksOnPowerButton_outputRecievesStringContainsCorrectWatt(int s1, int expectedWatt)
        {
            for (int i = 0; i < s1; i++)
            {
                powerButton_.Pressed += Raise.Event();
            }
            
            timeButton_.Pressed += Raise.Event();
            startCancelButton_.Pressed += Raise.Event();

            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains(Convert.ToString(expectedWatt))));

        }

        [TestCase(2, 1100, 1, 59)]
        [TestCase(2, 5400, 1, 55)]
        [TestCase(12, 1100, 11, 59)]
        [TestCase(4, 4200, 3, 56)]
        [TestCase(1, 1100, 0, 59)]
        public void timeIsSet_DifferentMinutesIn_displayRecivescorrectMinutesAndSeconds(int s1, int wait, int expectedMinutes, int expectedSeconds)
        {
            powerButton_.Pressed += Raise.Event();

            for (int i = 0; i < s1; i++)
            {
                timeButton_.Pressed += Raise.Event();
            }
            
            startCancelButton_.Pressed += Raise.Event();

            ManualResetEvent pause = new ManualResetEvent(false);
            pause.WaitOne(wait);

            display_.Received().ShowTime(expectedMinutes, expectedSeconds);
        }

        [Test]
        public void turnOffMicroOven_TurnOnMicroovenAndTurnOff_outputRecievesOff()
        {
            powerButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            startCancelButton_.Pressed += Raise.Event();
            startCancelButton_.Pressed += Raise.Event();

            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains("off")));

        }

        [Test]
        public void cookingIsDone_cookingIsDone_displayClear()
        {
            powerButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            startCancelButton_.Pressed += Raise.Event();
            UUTUserInterface_.CookingIsDone();

            display_.Received().Clear();
        }

        [Test]
        public void cookingIsDone_cookingIsDone_LightTurnOff()
        {
            powerButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            startCancelButton_.Pressed += Raise.Event();
            UUTUserInterface_.CookingIsDone();

            light_.Received().TurnOff();
        }

    }
}
