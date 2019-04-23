using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    class IT4_CookcontrollerTimer
    {
        private CookController UUTCookcontroller_;
        private IDisplay display_;
        private IPowerTube powertube_;
        private ITimer timer_;
        private IUserInterface userInterface_;

        private IOutput output_;

        [SetUp]
        public void setup()
        {
            userInterface_ = Substitute.For<IUserInterface>();

            output_ = new Output();
            display_ = new Display(output_);
            powertube_ = new PowerTube(output_);
            timer_ = new Timer();

            UUTCookcontroller_ = new CookController(timer_, display_, powertube_);
            UUTCookcontroller_.UI = userInterface_; 

        }

    }
}
