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
using NUnit.Framework.Internal;

namespace Microwave.test.integration
{

    [TestFixture]
    class IT8_DoorUserInterface
    {
        private Door uutDoor_;
        private ILight light_;
        private IDisplay display_;
        private IPowerTube powerTube_;
        private ITimer timer_;
        private ICookController cookController_;
        private IUserInterface userInterface_;
        private IButton StartCancelButton_;
        private IButton PowerButton_;
        private IButton TimeButton_;

        private IOutput output_;

        //knapperne skal oprettes for at kunne oprette userinterface rigtigt længere nede???

        [SetUp]
        public void setup()
        {
            timer_ = Substitute.For<ITimer>();
            output_ = Substitute.For<IOutput>();
            display_ = Substitute.For<IDisplay>();
            powerTube_ = Substitute.For<IPowerTube>();
            StartCancelButton_ = Substitute.For<IButton>();
            PowerButton_ = Substitute.For<IButton>();
            TimeButton_ = Substitute.For<IButton>();

            light_ = new Light(output_);
           
            uutDoor_ = new Door();

            cookController_ = new CookController(timer_,display_,powerTube_,userInterface_);

            userInterface_ = new UserInterface(PowerButton_,TimeButton_,StartCancelButton_,uutDoor_,display_,light_,cookController_);

        }

        [Test]
        public void opensDoor_LightOn_LoglineRecieved()
        {
            uutDoor_.Open();

            output_.Received(1).OutputLine(Arg.Is<String>(s => s.Contains("on")));
            
        }

        [Test]
        public void CloseDoor_LightsOff_LoglineRecieved()
        {
            uutDoor_.Open();
            uutDoor_.Close();

            output_.Received(1).OutputLine(Arg.Is<String>(s => s.Contains("off")));

        }


    }
}
