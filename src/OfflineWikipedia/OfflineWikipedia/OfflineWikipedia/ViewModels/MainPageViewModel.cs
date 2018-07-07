using LearningAPIs;
using OfflineWikipedia.Helpers;
using OfflineWikipedia.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineWikipedia.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand SearchButtonClickedCommand { get; set; }

        private WikipediaSearchResult _searchResult;
        public WikipediaSearchResult SearchResult
        {
            get => _searchResult;
            set => SetProperty(ref _searchResult, value);
        }

        private WikipediaSearchItem _selectedItem;
        public WikipediaSearchItem SelectedItem
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

        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            SearchButtonClickedCommand = new DelegateCommand(OnSearchButtonClicked);
            ResultsReturned = false;
            ReturnedText = "Search any topic you are interested in to get some results.";
            IsSearching = false;
        }

        private async void OnSearchButtonClicked()
        {
            Debug.WriteLine(App.Current.Properties.ContainsKey("NumberOfArticles"));
            try
            {
                IsSearching = true;
                int numberOfArticlesReturned = Settings.NumberOfResults;
                SearchResult = await APIServices.SearchTopic(EntryText,numberOfArticlesReturned);
                IsSearching = false;
                Debug.WriteLine(SearchResult.ToString());                
                ReturnedText = "Total Articles: "+SearchResult.Totalhits+"\n\n"+SearchResult.Items.Count+" Example Article:";
                ResultsReturned = true;
            }
            catch (Exception)
            {
                ReturnedText = "Results could not be loaded. Internet connection is required for this functionality, please check your connection.";
                IsSearching = false;
            }
        }


        private void OnSelectedItemChanged()
        {
            if (SelectedItem != null)
            {
                Debug.WriteLine("Starting Write to file...");
                StorageService.SaveHTMLFileToStorage(SelectedItem.Title);
                Debug.WriteLine("Finished Write to file...");
            }
        }
    }
}
