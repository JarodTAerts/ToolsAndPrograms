using LearningAPIs;
using OfflineWikipedia.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace OfflineWikipedia.ViewModels
{
	public class BrowseLibraryPageViewModel : ViewModelBase
	{

        public DelegateCommand SearchButtonClickedCommand { get; set; }

        private List<string> _savedArticles;
        public List<string> SavedArticles
        {
            get => _savedArticles;
            set => SetProperty(ref _savedArticles, value);
        }

        private List<string> _allSavedArticles;
        public List<string> AllSavedArticles
        {
            get => _allSavedArticles;
            set => SetProperty(ref _allSavedArticles, value);
        }

        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set { SetProperty(ref _selectedItem, value); OnSelectedItemChanged(); }
        }

        private string _returnedText;
        public string ReturnedText
        {
            get => _returnedText;
            set => SetProperty(ref _returnedText, value);
        }

        private string _entryText;
        public string EntryText
        {
            get => _entryText;
            set => SetProperty(ref _entryText, value);
        }

        private bool _resultsReturned;
        public bool ResultsReturned
        {
            get => _resultsReturned;
            set => SetProperty(ref _resultsReturned, value);
        }

        private bool _isSearching;
        public bool IsSearching
        {
            get => _isSearching;
            set => SetProperty(ref _isSearching, value);
        }

        private async void OnSelectedItemChanged()
        {
            if (SelectedItem != null)
            {
                Debug.WriteLine("Selected thing changed" + SelectedItem);
                var navparams = new NavigationParameters();
                navparams.Add("TITLE", SelectedItem);
                await NavigationService.NavigateAsync("ViewArticlePage",navparams);
            }
        }

        public BrowseLibraryPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            SearchButtonClickedCommand = new DelegateCommand(OnSearchButtonClicked);
            SavedArticles = new List<string>();
            ResultsReturned = false;
            ReturnedText = "Search your local library to read articles.";
            IsSearching = false;
        }

        private void OnSearchButtonClicked()
        {
            Debug.WriteLine("Searched something");


            SavedArticles = AllSavedArticles.Where(a => a.ToUpper().StartsWith(EntryText.ToUpper())).ToList();
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            SelectedItem = null;
            IsSearching = true;
            AllSavedArticles = await StorageService.GetNamesOfSavedArticles();
            SavedArticles = AllSavedArticles;
            IsSearching = false;
            if (SavedArticles != null && SavedArticles.Count > 0)
            {
                ResultsReturned = true;
            }
            else
            {
                ReturnedText = "It doesn't seem that you have any articles saved. Go and add download some articles in the Add to Library page.";
                ResultsReturned = false;
            }
        }

        
    }
}
