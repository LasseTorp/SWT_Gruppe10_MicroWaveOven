using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.test.integration
{
    [TestFixture]
    class IT6_UserInterfaceLight
    {
        private UserInterface uutUserInterface_;
        private Light uutLight_;
        private IDisplay display_;
        private IPowerTube powerTube_;
        private ITimer timer_;
        private ICookController cookController_;
        private IButton powerButton_;
        private IButton timeButton_;
        private IButton startCancelButton_;
        private IDoor door_;

        private IOutput output_;

        [SetUp]
        public void Setup()
        {
            output_ = Substitute.For<IOutput>();
            display_ = new Display(output_);
            powerTube_ = new PowerTube(output_);
            timer_ = Substitute.For<ITimer>();
            powerButton_ = Substitute.For<IButton>();
            timeButton_ = Substitute.For<IButton>();
            startCancelButton_ = Substitute.For<IButton>();
            door_ = Substitute.For<IDoor>();
            uutLight_ = new Light(output_);
            cookController_ = new CookController(timer_,display_,powerTube_);
            uutUserInterface_ = new UserInterface(powerButton_,timeButton_,startCancelButton_,door_,display_,uutLight_, cookController_);
        }

        [Test]
        public void DoorOpen_LightsOn()
        {
            door_.Opened += Raise.Event();
            output_.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("on")));

        }

        [Test]
        public void DoorClosed_LightsOff()
        {
            door_.Opened += Raise.Event();
            door_.Closed += Raise.Event();
            output_.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("off")));
        }


    }
}
