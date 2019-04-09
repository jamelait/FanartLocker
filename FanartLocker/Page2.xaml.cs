using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using FanartLocker.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace FanartLocker
{
    public partial class Page2 : PhoneApplicationPage
    {
        private int _imageIndex;

        public Page2()
        {
            InitializeComponent();

            BuildApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string query = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("ImageSelected", out query))
            {
                _imageIndex = Int32.Parse(query);
                // set image
                double width = Application.Current.Host.Content.ActualWidth;
                //img.Source = new BitmapImage(new Uri("../Images/alice.jpg", UriKind.Relative));
                SelectedImage.Source = new BitmapImage(new Uri(LockscreenImages.GetPathAsRelative(_imageIndex), UriKind.Relative));
                //img.Source = new BitmapImage(LockscreenImages.GetAsUri(i));
                SelectedImage.Width = width;
                //img.Height = height;
            }
        }

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            // Choose
            var choose = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Check.png", UriKind.Relative)) { Text = AppResources.ButtonChoose };
            choose.Click += Choose_Click;
            ApplicationBar.Buttons.Add(choose);

            // RemoveAllBlocks
            //var removeAllBlocks = new ApplicationBarMenuItem(AppResources.ButtonNew);
            //removeAllBlocks.Click += RemoveAllBlocks;
            //ApplicationBar.MenuItems.Add(removeAllBlocks);
        }

        private void Choose_Click(object sender, EventArgs e)
        {
            var l = new LockscreenImages();
            l.SetImage(_imageIndex);
        }
    }
}