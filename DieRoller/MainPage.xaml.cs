using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace DieRoller
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.settingsButton.Click += Settings_Click;
        }

        // generates random number
        private void rollButton_Click(object sender, RoutedEventArgs e)
        {
             Random rollDie = new Random();
             int rolledNum = 0;

            // generates random number in range of radio button checked and unchecks button after
            if (twoSide.IsChecked == true)
            {
                rolledNum = rollDie.Next(1, 2);
                twoSide.IsChecked = false;
            }
            else if (fourSide.IsChecked == true)
            {
                rolledNum = rollDie.Next(1, 4);
                fourSide.IsChecked = false;
            }
            else if (sixSide.IsChecked == true)
            {
                rolledNum = rollDie.Next(1, 6);
                sixSide.IsChecked = false;
            }
            else if (eightSide.IsChecked == true)
            {
                rolledNum = rollDie.Next(1, 8);
                eightSide.IsChecked = false;
            }
            else if (tenSide.IsChecked == true)
            {
                rolledNum = rollDie.Next(1, 10);
                tenSide.IsChecked = false;
            }
            else if (twelveSide.IsChecked == true)
            {
                rolledNum = rollDie.Next(1, 12);
                twelveSide.IsChecked = false;
            }
            else if (twentySide.IsChecked == true)
            {
                rolledNum = rollDie.Next(1, 20);
                twentySide.IsChecked = false;
            }
            else if (hundredSide.IsChecked == true)
            {
                rolledNum = rollDie.Next(1, 10) * 10;
                hundredSide.IsChecked = false;
            }
            else
                return;

            // takes number and displays it back to the user
            rollResult.Text = rolledNum.ToString();            
        }

        // navigates to settings page
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings));
        }
    }
}
