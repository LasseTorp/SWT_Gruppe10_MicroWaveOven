using System;
using System.Collections.Generic;
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

        private IDoor door_; 

        [SetUp]
        public void setup()
        {
            timer_ = Substitute.For<ITimer>();
            door_ = new Door();
            output_ = new Output();
            display_ = new Display(output_);
            powertube_ = new PowerTube(output_);
            light_ = new Light(output_);
            userInterface_ = new UserInterface(powerButton_, timeButton_, startCancelButton_, door_, display_, light_, cookController_);
            cookController_ = new CookController(timer_, display_, powertube_);

            startCancelButton_ = new Button(); 
            powerButton_ = new Button();
            timeButton_ = new Button();
            
        }
    }
}
