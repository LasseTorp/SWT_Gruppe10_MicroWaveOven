using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit;
using MicrowaveOvenClasses;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;

namespace Microwave.test.integration
{
    [TestFixture]
    class IT1_CookcontrollerPowerTube
    {
        private CookController UUTcookController_;
        private IUserInterface userInterface_;
        private IDisplay display_;
        private IPowerTube powerTube_;
        private ITimer timer_;

        private IOutput output_;

        [SetUp]
        public void Setup()
        {
            userInterface_ = Substitute.For<IUserInterface>();
            timer_ = Substitute.For<ITimer>();

            output_ = new Output();

            powerTube_ = new PowerTube(output_);

            UUTcookController_ = new CookController(timer_, display_, powerTube_);
            UUTcookController_.UI = userInterface_;
        }
    }
}
