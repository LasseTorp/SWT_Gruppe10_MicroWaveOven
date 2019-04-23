using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;

namespace Microwave.test.integration
{
    [TestFixture]
    class IT6_UserInterfaceLight
    {
        private UserInterface uutUserInterface_;
        private ILight light_;
        private IDisplay display_;
        private IPowerTube powerTube_;
        private ITimer timer_;
        private ICookController cookController_;

        private IOutput output_;

        [SetUp]
        public void Setup()
        {
            output_ = new Output();
            light_ = new Light(output_);
            display_ = new Display(output_);
            powerTube_ = new PowerTube(output_);
            timer_ = NSubstitute.Substitute.For<ITimer>();
            cookController_ = new CookController(timer_, display_, powerTube_);


            //SKAL BUTTONS MED I TESTEN, MULIGVIS FAKES?
            uutUserInterface_ = new UserInterface();
        }

    }
}
