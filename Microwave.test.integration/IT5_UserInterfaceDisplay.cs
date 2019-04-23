using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.test.integration
{
    class IT5_UserInterfaceDisplay
    {
        private UserInterface uutUserInterface_;
        private ICookController cookController_;
        private ITimer timer_;
        private IPowerTube powerTube_;
        private IDisplay display_;
        private ILight light_;

        private IOutput output_;

        [SetUp]
        public void setup()
        {
            timer_ = Substitute.For<ITimer>();
            light_ = Substitute.For<ILight>();

            //igen oprettet fordi den er nødvendig for powertube og display. 
            output_ = new Output();

            powerTube_ = new PowerTube(output_);
            display_ = new Display(output_);

            //kan ikke hente UI da interfacet Icookcontroller ikke har den. 
            cookController_ = new CookController(timer_, display_, powerTube_);
            cookController_.UI = uutUserInterface_;
        }

    }
}
