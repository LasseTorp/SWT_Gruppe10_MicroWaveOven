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
    class IT4b_UICookController
    {
        // Denne klasse er oprettet for at udføre nogle test med en fake timer, for at kunne teste
        // om metoden "stop" kaldes i timeren, når mikroovnen standses

        private ILight light_;
        private IDisplay display_;
        private IOutput output_;
        private IButton powerButton_;
        private IButton timeButton_;
        private IButton startCancelButton_;
        private IDoor door_;
        private ITimer timer_;
        
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
            timer_ = Substitute.For<ITimer>();
            
            powerTube_ = new PowerTube(output_);
            UUTcookController_ = new CookController(timer_, display_, powerTube_);
            UUTUserInterface_ = new UserInterface(powerButton_, timeButton_, startCancelButton_, door_, display_, light_, UUTcookController_);

            UUTcookController_.UI = UUTUserInterface_;

        }

        [Test]
        public void turnOffMicroOven_TurnOnMicroovenAndTurnOff_timerRecievesStop()
        {

            powerButton_.Pressed += Raise.Event();
            timeButton_.Pressed += Raise.Event();
            startCancelButton_.Pressed += Raise.Event();
            startCancelButton_.Pressed += Raise.Event();

            timer_.Received().Stop();

        }

        
    }
}
