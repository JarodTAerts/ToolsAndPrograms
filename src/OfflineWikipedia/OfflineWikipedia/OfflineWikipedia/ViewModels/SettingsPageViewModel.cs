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
	public class SettingsPageViewModel : ViewModelBase
	{
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

        private void OnDownloadImagesChanged()
        {
            Settings.DownloadImages = (DownloadImages);
        }

        private void OnDownloadOverCellularChanged()
        {
            Debug.WriteLine("Cellular: "+DownloadOverCeullular);
            Settings.DownloadOverCell = (DownloadOverCeullular);
        }

        public DelegateCommand BackButtonCommand { get; set; }

        public SettingsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            PickedItemNumber = Settings.NumberOfResults.ToString();
            DownloadOverCeullular = Settings.DownloadOverCell;
            DownloadImages = Settings.DownloadImages;
            BackButtonCommand = new DelegateCommand(OnBackButton);
        }

        private async void OnBackButton()
        {
            await NavigationService.NavigateAsync("StartPage"); ;
        }

        private void OnItemNumberPickerChanged()
        {
            Settings.NumberOfResults = Convert.ToInt32(PickedItemNumber);
        }

    }
}
