using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace OfflineWikipedia.ViewModels
{
	public class StartPageViewModel : ViewModelBase
    {

        private string _MessageText;
        public string MessageText
        {
            get => _MessageText;
            set => SetProperty(ref _MessageText, value);
        }

        public DelegateCommand AddToLibraryButtonClickedCommand { get; set; }
        public DelegateCommand BrowseLibraryButtonClickedCommand { get; set; }
        public DelegateCommand SettingsButtonClickedCommand { get; set; }


        public StartPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            AddToLibraryButtonClickedCommand = new DelegateCommand(OnAddToLibrary);
            BrowseLibraryButtonClickedCommand = new DelegateCommand(OnBrowseLibrary);
            SettingsButtonClickedCommand = new DelegateCommand(OnSettings);
            MessageText = "Welcome to YoWiki, your one stop shot for offline wikipedia. With this app you are able" +
                "to download and store locally wikipedia articles onnvarious topics of your choosing. To get started" +
                "just click the 'Add to Library' button and search anything you are interested in. You will be guided " +
                "through the process and soon you will be able to browse your local wikipedia library!";
        }

        private async void OnSettings()
        {
            Debug.WriteLine("Settings");
            await NavigationService.NavigateAsync("SettingsPage");
        }

        private async void OnBrowseLibrary()
        {
            Debug.WriteLine("Browse Library");
            await NavigationService.NavigateAsync("BrowseLibraryPage");
        }

        private async void OnAddToLibrary()
        {
            Debug.WriteLine("Add to library");
            await NavigationService.NavigateAsync("MainPage");
        }
    }
}
