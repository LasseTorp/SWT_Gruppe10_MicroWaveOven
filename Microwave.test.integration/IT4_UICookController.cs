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
        private IPowerTube powerTube_;
        private ITimer timer_;
        private ICookController cookController_;
        private UserInterface UUTUserInterface_;
        private IOutput output_;

        [SetUp]
        public void setUp()
        {
            output_ = Substitute.For<IOutput>();
            light_ = Substitute.For<ILight>();
            display_ = Substitute.For<IDisplay>();
            timer_ = Substitute.For<ITimer>();
            powerTube_ = new PowerTube(output_);

            cookController_ = new CookController(timer_, display_, powerTube_);

            //UUTUserInterface_ = new UserInterface();

        }

    }
}
