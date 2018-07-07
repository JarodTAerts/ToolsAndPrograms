using OfflineWikipedia.Helpers;
using System.Diagnostics;
using Xamarin.Forms;

namespace OfflineWikipedia.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

        }

        private void CellDownloadSwitch_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsToggled"))
            {
                if (Settings.DownloadOverCell)
                {
                    Settings.DownloadOverCell = false;
                }
                else
                {
                    Settings.DownloadOverCell = true;
                }
            }
        }

        private void DownloadImagesSwitch_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsToggled"))
            {
                if (Settings.DownloadImages)
                {
                    Settings.DownloadImages = false;
                }
                else
                {
                    Settings.DownloadImages = true;
                }
            }
        }

    }
}
