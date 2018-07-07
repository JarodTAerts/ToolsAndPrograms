using OfflineWikipedia.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OfflineWikipedia.ViewModels
{
	public class SettingsPageViewModel : ViewModelBase
	{
        private string _pickedItem;
        public string PickedItem
        {
            get => _pickedItem;
            set { SetProperty(ref _pickedItem, value); OnPickerChanged(); }
        }

        public DelegateCommand BackButtonCommand { get; set; }

        public SettingsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            PickedItem = Settings.NumberOfResults.ToString();
            BackButtonCommand = new DelegateCommand(OnBackButton);
        }

        private async void OnBackButton()
        {
            await NavigationService.NavigateAsync("StartPage"); ;
        }

        private void OnPickerChanged()
        {
            Settings.NumberOfResults = Convert.ToInt32(PickedItem);
        }
    }
}
