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

        private IOutput output_;

        //knapperne skal oprettes for at kunne oprette userinterface rigtigt længere nede???

        [SetUp]
        public void setup()
        {
            timer_ = Substitute.For<ITimer>();

            output_ = new Output();

            light_ = new Light(output_);
            display_ = new Display(output_);
            powerTube_ = new PowerTube(output_);

            cookController_ = new CookController(timer_,display_,powerTube_);
            cookController_.UI = userInterface_;

            userInterface_ = new UserInterface();//mangler sine parametre 
        }

        [Test]
        public void ddd()
        {
            //ffff
        }
    }
}
