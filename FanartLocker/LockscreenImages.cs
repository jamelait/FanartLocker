using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Phone.System.UserProfile;
using FanartLocker.Resources;

namespace FanartLocker
{
    class LockscreenImages
    {
        public static readonly string RootFolderAsRelative = "../Images/";
        public static readonly string RootFolder = "ms-appx:///" + "Images/";
        //public static readonly string RootFolder = "../Resources/";
        public static readonly string Extension = ".jpg";
        private static readonly string[] Images = new[]
        {
            "alice",
            "babar",
            "barbie",
            "buzzlightyear",
            "denver",
            "inspecteurgadget",
            "mario",
            "mylittlepony",
            "ouioui",
            "picsou",
            "ronald",
            "shrek",
            "teletubbies",
            "tintin",
            "tomsawyer",
            "winnie"
        };

        public static int ImageCount()
        {
            return Images.Count();
        }

        public static string GetPath(int i = 0)
        {
            //string s = string.Concat("ms-appx:////Resources/", "alice", ".jpg");
            //string s = string.Concat("ms-appx:///", "alice", ".jpg");
            //return s;
            //return string.Concat(RootFolder, "DefaultLockScreen.jpg");
            return string.Concat(RootFolder, Images[i], Extension);
        }

        public static string GetPathAsRelative(int i = 0)
        {
            //string s = string.Concat("ms-appx:////Resources/", "alice", ".jpg");
            //string s = string.Concat("ms-appx:///", "alice", ".jpg");
            //return s;
            //return string.Concat(RootFolder, "DefaultLockScreen.jpg");
            return string.Concat(RootFolderAsRelative, Images[i], Extension);
        }

        public static Uri GetAsUri(int i = 0)
        {
            return new Uri(GetPath(i), UriKind.RelativeOrAbsolute);
        }

        public async void LockHelper(string filePathOfTheImage, bool isAppResource)
        {
            try
            {
                var isProvider = Windows.Phone.System.UserProfile.LockScreenManager.IsProvidedByCurrentApplication;
                if (!isProvider)
                {
                    // If you're not the provider, this call will prompt the user for permission.
                    // Calling RequestAccessAsync from a background agent is not allowed.
                    var op = await Windows.Phone.System.UserProfile.LockScreenManager.RequestAccessAsync();

                    // Only do further work if the access was granted.
                    isProvider = op == Windows.Phone.System.UserProfile.LockScreenRequestResult.Granted;
                }

                if (isProvider)
                {
                    SetLockScreen();
                    return;

                    // At this stage, the app is the active lock screen background provider.

                    // The following code example shows the new URI schema.
                    // ms-appdata points to the root of the local app data folder.
                    // ms-appx points to the Local app install folder, to reference resources bundled in the XAP package.
                    var schema = isAppResource ? "ms-appx:///" : "ms-appdata:///Local/";
                    var uri = new Uri(schema + filePathOfTheImage, UriKind.Absolute);

                    // Set the lock screen background image.
                    Windows.Phone.System.UserProfile.LockScreen.SetImageUri(uri);

                    // Get the URI of the lock screen background image.
                    var currentImage = Windows.Phone.System.UserProfile.LockScreen.GetImageUri();
                    System.Diagnostics.Debug.WriteLine("The new lock screen background image is set to {0}", currentImage.ToString());
                }
                else
                {
                    MessageBox.Show(AppResources.UserRefusedProvider);
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        public async void SetImage(int i)
        {
            try
            {
                var isProvider = Windows.Phone.System.UserProfile.LockScreenManager.IsProvidedByCurrentApplication;
                if (!isProvider)
                {
                    var op = await Windows.Phone.System.UserProfile.LockScreenManager.RequestAccessAsync();

                    isProvider = op == Windows.Phone.System.UserProfile.LockScreenRequestResult.Granted;
                }

                if (isProvider)
                {
                    LockScreen.SetImageUri(GetAsUri(i));
                }
                else
                {
                    MessageBox.Show(AppResources.UserRefusedProvider);
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void SetLockScreen()
        {
            //Check to see if the app is currently the lock screen provider
            if (!LockScreenManager.IsProvidedByCurrentApplication)
            {
                //Request to be lock screen provider
                await LockScreenManager.RequestAccessAsync();
            }

            //Check to see if the app is currently the lock screen provider
            if (LockScreenManager.IsProvidedByCurrentApplication)
            {
                //Set the image to the lock screen image
                //Uri imageUri = new Uri("ms-appx:///Images/lockscreen.png", UriKind.RelativeOrAbsolute);

                // this works : string.Concat(RootFolder, Images[i], Extension)


                Uri imageUri = GetAsUri(9);
                LockScreen.SetImageUri(imageUri);
            }
        }
    }
}
