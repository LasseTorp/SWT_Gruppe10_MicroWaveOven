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

    [TestFixture]
    class IT3_CookcontrollerDisplay
    {
        private CookController UUTcookController_;
        private IUserInterface userInterface_;
        private IDisplay display_;
        private IPowerTube powerTube_;
        private ITimer timer_;

        private IOutput output_;

        [SetUp]
        public void setup()
        {
            userInterface_ = Substitute.For<IUserInterface>();
            powerTube_ = Substitute.For<IPowerTube>();
            timer_ = Substitute.For<ITimer>();

            //denne oprettede vi fordi den var nødvendigfor oprettelsen af display.
            output_ = new Output();

            display_ = new Display(output_);

            UUTcookController_ = new CookController(timer_,display_,powerTube_);
            UUTcookController_.UI = userInterface_;

        }


        //ud fra sekvensdiagrammet kan vi se vi skal teste showtime() metoden for at teste forbindelsen imellem display og cookcontroller. 
        // vi ved ikke helt hvordan vi skal asserte? 
        [Test]
        public void ddd()
        {

        }


    }
}
