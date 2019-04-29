using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

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
            timer_ = new Timer();
            output_ = Substitute.For<IOutput>();

            UUTdisplay_ = new Display(output_);
            UUTcookController_ = new CookController(timer_,UUTdisplay_,powerTube_);
            UUTcookController_.UI = userInterface_;
        }

        [TestCase(0, "00:00")]
        [TestCase(10, "00:10")]
        [TestCase(120, "02:00")]
        [TestCase(5999, "99:59")]
        public void showTime_showWithMinAndSec_outputContainsCorrectMinAndSec(int time, string expected)
        {
            UUTcookController_.StartCooking(350, time);
            UUTcookController_.OnTimerTick(this, EventArgs.Empty);
            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains((expected))));
        }

        //[TestCase(6000, "10:00")] DETTE ER KOMMENTERET I RAPPORTEN
        [TestCase(6000, "100:00")]
        public void showTime_showWithMinAndSec_outputContainsTooHighMinAndSEC(int time, string expected)
        {
            UUTcookController_.StartCooking(350, time);
            UUTcookController_.OnTimerTick(this, EventArgs.Empty);
            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains((expected))));
        }

        [TestCase(10, "00:05")]
        [TestCase(120, "01:55")]
        [TestCase(5999, "99:54")]
        public void showTime_showWithMinAndSecAfter5Seconds_outputContainsCorrectMinAndSec(int time, string expected)
        {
            UUTcookController_.StartCooking(350, time);
            UUTcookController_.OnTimerTick(this, EventArgs.Empty);

            ManualResetEvent pause = new ManualResetEvent(false);
            pause.WaitOne(5100);

            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains((expected))));
        }

    }
}
