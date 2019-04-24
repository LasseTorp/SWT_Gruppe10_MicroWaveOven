using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute; 

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
        private ITimer timer_;
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
            timer_ = Substitute.For<ITimer>();
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
    }
}
