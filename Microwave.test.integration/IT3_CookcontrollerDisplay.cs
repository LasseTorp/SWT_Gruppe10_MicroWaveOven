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
        private IDisplay UUTdisplay_;
        private IPowerTube powerTube_;
        private ITimer timer_;

        private IOutput output_;

        [SetUp]
        public void setup()
        {
            userInterface_ = Substitute.For<IUserInterface>();
            powerTube_ = Substitute.For<IPowerTube>();
            timer_ = Substitute.For<ITimer>();
            output_ = Substitute.For<IOutput>();

            UUTdisplay_ = new Display(output_);
            UUTcookController_ = new CookController(timer_,UUTdisplay_,powerTube_);
            UUTcookController_.UI = userInterface_;

        }


        //ud fra sekvensdiagrammet kan vi se vi skal teste showtime() metoden for at teste forbindelsen imellem display og cookcontroller. 
        // vi ved ikke helt hvordan vi skal asserte? 
        [TestCase(99, 59)]
        [TestCase(12, 1)]
        [TestCase(0, 12)]
        [TestCase(12, 0)]
        [TestCase(0,0)]
        public void showTime_showWithMinAndSec_outputContainsCorrectMinAndSec(int min, int sec)
        {
            UUTdisplay_.ShowTime(min,sec);
            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains(Convert.ToString(min)) && s.Contains(Convert.ToString(sec))));
        }

        [TestCase(100, 59, 10, 59)]
        [TestCase(12, 100, 12, 10)]
        public void showTime_showWithMinAndSec_outputContainsWrongMinAndSEC(int min, int sec, int expectedmin, int expectedsec)
        {
            UUTdisplay_.ShowTime(min, sec);
            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains(Convert.ToString(expectedmin)) && s.Contains(Convert.ToString(expectedsec))));
        }
    }
}
