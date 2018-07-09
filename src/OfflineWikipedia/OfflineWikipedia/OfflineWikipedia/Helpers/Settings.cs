using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace OfflineWikipedia.Helpers
{
    public class Settings
    {
        protected static ISettings AppSettings=CrossSettings.Current;

        public static bool DownloadOverCell
        {
            get
            {
                return AppSettings.GetValueOrDefault("DownloadOverCell", false);
            }
            set
            {
                AppSettings.AddOrUpdateValue("DownloadOverCell", value);
            }
        }

        public static bool DownloadImages
        {
            get
            {
                return AppSettings.GetValueOrDefault("DownloadImages", false);
            }
            set
            {
                AppSettings.AddOrUpdateValue("DownloadImages", value);
            }
        }

        public static int NumberOfResults
        {
            get
            {
                return AppSettings.GetValueOrDefault("NumberOfResults", 5);
            }
            set
            {
                AppSettings.AddOrUpdateValue("NumberOfResults", value);
            }
        }

        public static bool FirstTimeOpened
        {
            get
            {
                return AppSettings.GetValueOrDefault("FirstTimeOpened",true);
            }
            set
            {
                AppSettings.AddOrUpdateValue("FirstTimeOpened", value);
            }
        }
    }
}
