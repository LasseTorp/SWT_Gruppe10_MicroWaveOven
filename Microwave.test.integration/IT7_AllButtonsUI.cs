using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.test.integration
{
    class IT7_AllButtonsUI
    {
        private Button uutStartCancelButton_;
        private Button uutPowerButton_;
        private Button uutTimeButton_;

        private IOutput output_;
        private ILight light_; 
        private IDisplay display_;
        private IPowerTube powertube_;
        private Timer timer_;
        private ICookController cookController_;
        private UserInterface uutUserInterface_;
        private IDoor door_; 

        [SetUp]
        public void setup()
        {
            output_ = Substitute.For<IOutput>();
            light_ = new Light(output_);
            display_ = new Display(output_);
            powertube_ = new PowerTube(output_);
            timer_ = new Timer();
            cookController_ = new CookController(timer_, display_, powertube_, uutUserInterface_);
            door_ = Substitute.For<IDoor>();

            uutStartCancelButton_ = new Button();
            uutPowerButton_ = new Button();
            uutTimeButton_ = new Button();

            uutUserInterface_ = new UserInterface(uutPowerButton_, uutTimeButton_, uutStartCancelButton_, door_, display_, light_, cookController_);
        }

        [Test]
        public void ButtonsPressed_Started_LightsTurnedOn()
        {
            uutPowerButton_.Press();
            uutTimeButton_.Press();
            uutStartCancelButton_.Press();
            output_.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("on")));

        }

        [Test]
        public void ButtonsPressed_Canceled_LightsAndPowerTubesTurnedOff()
        {
            uutPowerButton_.Press();
            uutTimeButton_.Press();
            uutStartCancelButton_.Press();
            uutStartCancelButton_.Press();
            output_.Received(2).OutputLine(Arg.Is<string>(s => s.Contains("off")));

        }

        [Test]
        public void test2()
        {
            door_.Opened += Raise.Event();
            door_.Closed += Raise.Event();
            output_.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("off")));
        }

        [TestCase(1, 50)]
        [TestCase(5, 250)]
        [TestCase(10, 500)]
        [TestCase(14, 700)]
        [TestCase(15, 50)]
        public void powerButtonIsPressed_PowerSet_outputRecievesStringContainsCorrectWatt(int s1, int expectedWatt)
        {
            for (int i = 0; i < s1; i++)
            {
                uutPowerButton_.Press();
            }

            uutTimeButton_.Press();
            uutStartCancelButton_.Press();

            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains(Convert.ToString(expectedWatt))));

        }

        [TestCase(2, 1100, 1, 59)]
        [TestCase(2, 5400, 1, 55)]
        [TestCase(12, 1100, 11, 59)]
        [TestCase(4, 4200, 3, 56)]
        [TestCase(1, 1100, 0, 59)]
        public void timeButtonIsPressed_TimeSet_displayRecivescorrectMinutesAndSeconds(int s1, int wait, int expectedMinutes, int expectedSeconds)
        {
            uutPowerButton_.Press();

            for (int i = 0; i < s1; i++)
            {
                uutTimeButton_.Press();
            }

            uutStartCancelButton_.Press();
            
            ManualResetEvent pause = new ManualResetEvent(false);
            pause.WaitOne(wait);

            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains(Convert.ToString(expectedMinutes)+":"+Convert.ToString(expectedSeconds))));
        }

        [Test]
        public void turnOffMicroOven_turnOnButtonAndTurnOfButton_outputRecievesOff()
        {
            uutPowerButton_.Press();
            uutTimeButton_.Press();
            uutStartCancelButton_.Press();
            uutStartCancelButton_.Press();
           
            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains("off")));

        }
    }
}
