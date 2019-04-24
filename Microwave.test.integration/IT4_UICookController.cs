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
    class IT4_UICookController
    {
        private ILight light_;
        private IDisplay display_;
        private IOutput output_;
        private ITimer timer_;
        private IButton powerButton_;
        private IButton timeButton_;
        private IButton startCancelButton_;
        private IDoor door_;

        private PowerTube powerTube_;
        private CookController UUTcookController_;
        private UserInterface UUTUserInterface_;

        

        [SetUp]
        public void setUp()
        {
            timer_ = Substitute.For<ITimer>();
            output_ = Substitute.For<IOutput>();
            light_ = Substitute.For<ILight>();
            display_ = Substitute.For<IDisplay>();
            powerButton_ = Substitute.For<IButton>();
            timeButton_ = Substitute.For<IButton>();
            startCancelButton_ = Substitute.For<IButton>();
            door_ = Substitute.For<IDoor>();


            powerTube_ = new PowerTube(output_);
            UUTcookController_ = new CookController(timer_, display_, powerTube_);
            UUTUserInterface_ = new UserInterface(powerButton_, timeButton_, startCancelButton_, door_, display_, light_, UUTcookController_);

            UUTcookController_.UI = UUTUserInterface_;

        }

        [Test]
        public void powerIsSet_PowerTimeAndStartButtonPressed_()
        {
            powerButton_.Pressed += Raise.Event();
            powerButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            startCancelButton_.Pressed += Raise.Event();

            output_.Received().OutputLine(Arg.Any<string>());
            
        }

        [Test]
        public void testmemore()
        {
            powerButton_.Pressed += Raise.Event();
            powerButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            startCancelButton_.Pressed += Raise.Event();

            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains(Convert.ToString(100))));

        }


        //output_.Received().OutputLine(Arg.Is<string>(s => s.Contains("100")));

    }
}
