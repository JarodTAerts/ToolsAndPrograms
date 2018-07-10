using LearningAPIs;
using OfflineWikipedia.Helpers;
using OfflineWikipedia.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineWikipedia.ViewModels
{
    /// <summary>
    /// Class that controls the functionality of the MainPage. The MainPage is the search and add page. The app started on this page and the name hasnt been changed
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {

        #region Properties and Bindings
        private IPageDialogService _dialogService;

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

        private double _barProgress;
        public double BarProgress
        {
            get => _barProgress;
            set => SetProperty(ref _barProgress, value);
        }
        #endregion

        #region Delagate Commands
        public DelegateCommand SearchButtonClickedCommand { get; set; }
        public DelegateCommand DownloadAllArticlesCommand { get; set; }
        #endregion

        #region Constructor
        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialog) 
            : base (navigationService)
        {
            //Set properties to what they need to the the list doesnt show until results are loaded and set search text
            SearchButtonClickedCommand = new DelegateCommand(OnSearchButtonClicked);
            DownloadAllArticlesCommand = new DelegateCommand(OnDownloadAllClicked);
            ResultsReturned = false;
            ReturnedText = "Search any topic you are interested in to get some results.";
            IsSearching = false;
            _dialogService = dialog;
        }
        #endregion

        #region Command Functions
        /// <summary>
        /// Function to handle when the search button is clicked
        /// </summary>
        private async void OnSearchButtonClicked()
        {
            try
            {
                //Set is searching to true, get the number of articles to get as examples and call function to search fromm APIService
                IsSearching = true;
                int numberOfArticlesReturned = Settings.NumberOfResults;
                SearchResult = await APIServices.SearchTopic(EntryText,numberOfArticlesReturned);
                //Set is searching to false and set the returned text and show the list
                IsSearching = false;
                ReturnedText = "Total Articles: "+SearchResult.Totalhits+"\n\n"+SearchResult.Items.Count+" Example Article:";
                ResultsReturned = true;
            }
            catch (Exception)
            {
                //If there is an exception, probably due to internet connectivity then let them know and stop searching
                ReturnedText = "Results could not be loaded. Internet connection is required for this functionality, please check your connection.";
                IsSearching = false;
                ResultsReturned = false;
            }
        }

        /// <summary>
        /// Function to handle when an item is selected from the list
        /// </summary>
        private async void OnSelectedItemChanged()
        {
            //If the item you selected is not null then use the storage service to save that article to storage
            if (SelectedItem != null)
            {
                Debug.WriteLine("Starting Write to file...");
                IsSearching = true;
                await StorageService.SaveHTMLFileToStorage(SelectedItem.Title);
                IsSearching = false;
                Debug.WriteLine("Finished Write to file...");
                await _dialogService.DisplayAlertAsync("Article Added","Article \""+SelectedItem.Title+"\" has been downloaded and added to your library.","Ok");               
            }
        }

        /// <summary>
        /// Function to handle when the download all articles button is clicked
        /// </summary>
        private async void OnDownloadAllClicked()
        {
            if (SearchResult.Items != null)
            {
                IsSearching = true;

                ReturnedText = "Fetching the names of all articles from Wikipedia.";
                List<string> names=await APIServices.GetAllNamesFromSearch(EntryText,SearchResult.Totalhits);

                Debug.WriteLine("Number of Article names: "+names.Count);
                
                for(int i = 0; i < names.Count; i++)
                {
                    ReturnedText = "Downloading " + i + " out of "+names.Count+" Articles...";
                    BarProgress = i / names.Count;
                    await StorageService.SaveHTMLFileToStorage(names[i]);
                    await HTMLHandler.CleanHTMLFile(names[i]);
                }

                IsSearching = false;
                ReturnedText = "Downloaded "+names.Count+" Articles.";
                await _dialogService.DisplayAlertAsync("Articles Added", names.Count+" articles have been downloaded and added to your library.", "Ok");              
            }
        }
        #endregion
    }
}
