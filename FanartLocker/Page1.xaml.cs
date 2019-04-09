using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using FanartLocker.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;

namespace FanartLocker
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();

            LoadThumbnails();
            //LockscreenImages li = new LockscreenImages();
            //li.LockHelper("", true);

            BuildLocalizedApplicationBar();
        }

        private void LoadThumbnails()
        {
            int m = 15;
            var margin = new Thickness(0, 0, 15, 15);
            //var margin = new Thickness(0,0,0,15);
            double width = (Application.Current.Host.Content.ActualWidth / 2) - 15 * 4;
            double height = Application.Current.Host.Content.ActualHeight / 3;
            for (int i = 0; i < LockscreenImages.ImageCount(); i++)
            {
                Image img = new Image();
                //img.Source = new BitmapImage(new Uri("../Images/alice.jpg", UriKind.Relative));
                img.Source = new BitmapImage(new Uri(LockscreenImages.GetPathAsRelative(i), UriKind.Relative));
                //img.Source = new BitmapImage(LockscreenImages.GetAsUri(i));
                img.Width = width;
                //img.Height = height;
                img.Margin = margin;
                ThumbnailPanel.Children.Add(img);
                img.Tag = i;
                //img.Style = Application.Current.Resources["ButtonStyle"] as Style;
                img.Tap += img_Tap;
            }
            //ThumbnailPanel.ItemHeight = 200;
            //ThumbnailPanel.ItemWidth = 200;
            //ThumbnailPanel.Margin = margin;

            //var b = new BitmapImage(new Uri("../Assets/PanoramaBackground.png", UriKind.Relative));
            //b.DecodePixelWidth = width;
            //b.DecodePixelHeight = height;

            //var ms = new MemoryStream(AppResources.Pic);
            //Image image = Image.FromFile(fileName);
            //Image thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
            //thumb.Save(Path.ChangeExtension(fileName, "thumb"));
            //var collection = new ObservableCollection<Image>();

            //for (int i = 0; i < 38; i++)
            //{
            //    Image img = new Image();
            //    img.Source = b;
            //    img.Width = width;
            //    img.Height = height;
            //    img.Margin = new Thickness(5, 5, 5, 5);
            //    ThumbnailPanel.Children.Add(img);
            //    img.Tag = i;
            //    img.Tap += img_Tap;
            //    //collection.Add(img);
            //}
            //List.ItemsSource = collection;
        }

        void img_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var img = (Image) sender;
            var s = Int32.Parse(img.Tag.ToString());

            NavigationService.Navigate(new Uri("/Page2.xaml?ImageSelected=" + s, UriKind.Relative));
            //var l = new LockscreenImages();
            //l.SetImage(s);
            Debug.WriteLine("tap : " + s);
        }

        private void BuildLocalizedApplicationBar()
        {
            // Crée un bouton et définit la valeur du texte sur la chaîne localisée issue d'AppResources.
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/feature.settings.png", UriKind.Relative));
            appBarButton.Text = AppResources.AppBarAboutText;
            appBarButton.Click += About_Click;
            ApplicationBar.Buttons.Add(appBarButton);

            // Crée un nouvel élément de menu avec la chaîne localisée d'AppResources.
            //ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            //ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/YourLastAboutDialog;component/AboutPage.xaml", UriKind.Relative));
        }
    }
}