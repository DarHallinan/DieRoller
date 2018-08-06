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
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();

            this.homeButton.Click += Home_Click;
        }

        // navigates to home page
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        public static BitmapImage BitmapImage = new BitmapImage();
        // changes theme of the app
        // custom theme
        // code taken from https://www.youtube.com/watch?v=Z_JNhMe43Rg
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
        } // end of themes

        // loads background image when page is loaded
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            IStorageItem fileCheck = await folder.TryGetItemAsync("customBackground.png");

            if(fileCheck != null)
            {
                StorageFile file = await folder.GetFileAsync("customBackground.png");
                IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                BitmapImage.SetSource(fileStream);
                backgroundImage.ImageSource = BitmapImage;
            }
            else
            {
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets/default.jpg"));
                IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                BitmapImage.SetSource(fileStream);
                backgroundImage.ImageSource = BitmapImage;
            }
        }
    }
}
