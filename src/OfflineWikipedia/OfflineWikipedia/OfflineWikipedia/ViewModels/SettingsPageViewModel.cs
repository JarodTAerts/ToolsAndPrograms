using OfflineWikipedia.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace OfflineWikipedia.ViewModels
{
    /// <summary>
    /// Class to control the functionality and bindings of the Settings page
    /// </summary>
	public class SettingsPageViewModel : ViewModelBase
	{
        #region Properties and Bindings
        private string _pickedItemNumber;
        public string PickedItemNumber
        {
            get => _pickedItemNumber;
            set { SetProperty(ref _pickedItemNumber, value); OnItemNumberPickerChanged(); }
        }

        private bool _downloadOverCellular;
        public bool DownloadOverCeullular
        {
            get => _downloadOverCellular;
            set { SetProperty(ref _downloadOverCellular,value); OnDownloadOverCellularChanged(); }
        }

        private bool _downloadImages;
        public bool DownloadImages
        {
            get => _downloadImages;
            set { SetProperty(ref _downloadImages, value); OnDownloadImagesChanged(); }
        }
        #endregion

        #region Delegate Commands
        public DelegateCommand BackButtonCommand { get; set; }
        #endregion

        #region Constructor
        public SettingsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            //Set the UI elements to the values stored in settings
            PickedItemNumber = Settings.NumberOfResults.ToString();
            DownloadOverCeullular = Settings.DownloadOverCell;
            DownloadImages = Settings.DownloadImages;
            BackButtonCommand = new DelegateCommand(OnBackButton);
        }
        #endregion

        #region Command Functions
        /// <summary>
        /// Function that sends you back to the startpage when you press the back button at the bottom
        /// </summary>
        private async void OnBackButton()
        {
            await NavigationService.NavigateAsync("StartPage"); ;
        }

        /// <summary>
        /// Function that handlels when you change the number in the picker to select how many example articles to return
        /// Changes the stored setting to what you entered
        /// </summary>
        private void OnItemNumberPickerChanged()
        {
            Settings.NumberOfResults = Convert.ToInt32(PickedItemNumber);
        }

        /// <summary>
        /// Function to handle when you change the switch to control if you want to download images
        /// Changes the stored setting to what you entered
        /// </summary>
        private void OnDownloadImagesChanged()
        {
            Settings.DownloadImages = (DownloadImages);
        }
    
        /// <summary>
        /// Funtion to control when you change the switch to control if you want to download over cell connection
        /// Changes the stored setting to what you entered
        /// </summary>
        private void OnDownloadOverCellularChanged()
        {
            Debug.WriteLine("Cellular: " + DownloadOverCeullular);
            Settings.DownloadOverCell = (DownloadOverCeullular);
        }
        #endregion

    }
}
