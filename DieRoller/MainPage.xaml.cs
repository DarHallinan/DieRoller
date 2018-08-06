using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
            int rolledNum1 = 0, rolledNum2 = 0;
            int finalRoll = 0;

            // generates random number in range of radio button checked and unchecks button after
            if (twoSide.IsChecked == true)
            {
                rolledNum1 = rollDie.Next(1, 2);
                rolledNum2 = rollDie.Next(1, 2);

                twoSide.IsChecked = false;
            }
            else if (fourSide.IsChecked == true)
            {
                rolledNum1 = rollDie.Next(1, 4);
                rolledNum2 = rollDie.Next(1, 4);

                fourSide.IsChecked = false;
            }
            else if (sixSide.IsChecked == true)
            {
                rolledNum1 = rollDie.Next(1, 6);
                rolledNum2 = rollDie.Next(1, 6);

                sixSide.IsChecked = false;
            }
            else if (eightSide.IsChecked == true)
            {
                rolledNum1 = rollDie.Next(1, 8);
                rolledNum2 = rollDie.Next(1, 8);

                eightSide.IsChecked = false;
            }
            else if (tenSide.IsChecked == true)
            {
                rolledNum1 = rollDie.Next(1, 10);
                rolledNum2 = rollDie.Next(1, 10);

                tenSide.IsChecked = false;
            }
            else if (twelveSide.IsChecked == true)
            {
                rolledNum1 = rollDie.Next(1, 12);
                rolledNum2 = rollDie.Next(1, 12);

                twelveSide.IsChecked = false;
            }
            else if (twentySide.IsChecked == true)
            {
                rolledNum1 = rollDie.Next(1, 20);
                rolledNum2 = rollDie.Next(1, 20);

                twentySide.IsChecked = false;
            }
            else if (hundredSide.IsChecked == true)
            {
                rolledNum1 = rollDie.Next(1, 10) * 10;
                rolledNum2 = rollDie.Next(1, 10) * 10;

                hundredSide.IsChecked = false;
            }
            else
                return;
            /*
            // takes best/worst roll for whether or not advantage/disadvantage is checked
            if (advantage.IsChecked == true)
            {
                if (rolledNum1 > rolledNum2)
                    finalRoll = rolledNum1;
                else
                    finalRoll = rolledNum2;

            }
            else if (disadvantage.IsChecked == true)
            {
                if (rolledNum1 > rolledNum2)
                    finalRoll = rolledNum2;
                else
                    finalRoll = rolledNum1;
            }
            else
                finalRoll = rolledNum1;
                */
            // takes number and displays it back to the user
            //rollResult.Text = finalRoll.ToString();
            rollResult.Text = rolledNum1.ToString();

        }

        // navigates to settings page
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings));
        }

        // background
        // copied from Settings.xaml.cs
        public static BitmapImage BitmapImage = new BitmapImage();

        private async void Custom_Background(object sender, RoutedEventArgs e)
        {
            IRandomAccessStream fileStream;
            FileOpenPicker pickTheme = new FileOpenPicker();
            pickTheme.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            pickTheme.ViewMode = PickerViewMode.Thumbnail;

            // include a subset of file types
            pickTheme.FileTypeFilter.Clear();
            pickTheme.FileTypeFilter.Add(".bmp");
            pickTheme.FileTypeFilter.Add(".png");
            pickTheme.FileTypeFilter.Add(".jpeg");
            pickTheme.FileTypeFilter.Add(".jpg");
            pickTheme.FileTypeFilter.Add(".tiff");

            // open file picker
            StorageFile file = await pickTheme.PickSingleFileAsync();

            // error handling for null file
            if (file != null)
            {
                fileStream = await file.OpenAsync(FileAccessMode.Read);

                BitmapImage.SetSource(fileStream);
                backgroundImage.ImageSource = BitmapImage;
                this.DataContext = file;

                await file.CopyAsync(ApplicationData.Current.LocalFolder, "customBackground.png", NameCollisionOption.ReplaceExisting);
            }
        }

        // loads background image when page is loaded
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            IStorageItem fileCheck = await folder.TryGetItemAsync("customBackground.png");

            if (fileCheck != null)
            {
                StorageFile file = await folder.GetFileAsync("customBackground.png");
                IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                BitmapImage.SetSource(fileStream);
                backgroundImage.ImageSource = BitmapImage;
            }
            else
            {
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/default.jpg"));
                IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                BitmapImage.SetSource(fileStream);
                backgroundImage.ImageSource = BitmapImage;
            }
        }
    }
}
