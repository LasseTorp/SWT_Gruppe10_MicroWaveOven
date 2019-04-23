using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute; 

namespace Microwave.test.integration
{
    class IT7_AllButtonsUI
    {
        private Button startCancelButton_;
        private Button powerButton_;
        private Button timeButton_; 

        private ILight light_; 
        private IDisplay display_;
        private IPowerTube powertube_;
        private ITimer timer_;
        private IUserInterface userInterface_;
        private ICookController cookController_;
        private IOutput output_;

        [SetUp]
        public void setup()
        {
            timer_ = Substitute.For<ITimer>(); 
            startCancelButton_ = new Button(); 
            powerButton_ = new Button();
            timeButton_ = new Button();
            
        }


    }
}
