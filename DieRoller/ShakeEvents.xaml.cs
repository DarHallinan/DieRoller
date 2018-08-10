using System;
using Windows.Devices.Sensors;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DieRoller
{
    public sealed partial class ShakeEvents : Page
    {
        private Accelerometer _accelerometer;
        public ShakeEvents()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _accelerometer = Accelerometer.GetDefault();
            if(_accelerometer != null)
            {

            }
        }
    }
}
