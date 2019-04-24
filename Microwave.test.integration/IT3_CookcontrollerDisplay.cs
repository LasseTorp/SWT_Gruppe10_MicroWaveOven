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

        [TestCase(99, 99, 0)]
        [TestCase(12, 12, 0)]
        public void showTime_showWithMinAndSec_outputContainsCorrectMinAndSec(int time, int expectedmin, int expectedsec)
        {
            for (int i = 0; i < time; i++)
            {
                timer_.TimerTick += Raise.Event();
            }
            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains(Convert.ToString(time))));
        }

        [TestCase(100, 10, 0)]
        [TestCase(123, 12, 0)]
        public void showTime_showWithMinAndSec_outputContainsWrongMinAndSEC(int time, int expectedmin, int expectedsec)
        {
            for (int i = 0; i < time; i++)
            {
                timer_.TimerTick += Raise.Event();
            }
            output_.Received().OutputLine(Arg.Is<string>(s => s.Contains(Convert.ToString(time))));
        }
    }
}
