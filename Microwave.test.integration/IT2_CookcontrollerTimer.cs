using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.test.integration
{
    class IT2_CookcontrollerTimer
    {
        private CookController UUTCookcontroller_;
        private IDisplay display_;
        private IPowerTube powertube_;
        private Timer UUTtimer_;
        private IUserInterface userInterface_;
        private IOutput output_;

        [SetUp]
        public void setup()
        {
            userInterface_ = Substitute.For<IUserInterface>();
            output_ = Substitute.For<IOutput>();
            display_ = Substitute.For<IDisplay>();

            powertube_ = new PowerTube(output_);

            UUTtimer_ = new Timer();
            UUTCookcontroller_ = new CookController(UUTtimer_, display_, powertube_);
            UUTCookcontroller_.UI = userInterface_; 

        }


        [TestCase(1200, 19, 55)]
        [TestCase(2400, 39, 55)]
        [TestCase(600, 9, 55)]
        [TestCase(60, 0, 55)]
        [TestCase(30, 0, 25)]
        public void StartCooking_WaitingFiveSeconds_CheckingTimeCorrect(int s1, int min, int sec)
        {

            UUTCookcontroller_.StartCooking(333,s1);

            ManualResetEvent pause = new ManualResetEvent(false);
            pause.WaitOne(5100);

            display_.Received().ShowTime(min,sec);
            
        }


    }
}
