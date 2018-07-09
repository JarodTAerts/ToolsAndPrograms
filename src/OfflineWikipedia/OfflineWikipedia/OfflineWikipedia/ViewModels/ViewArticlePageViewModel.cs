using OfflineWikipedia.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace OfflineWikipedia.ViewModels
{
	public class ViewArticlePageViewModel : ViewModelBase
	{
        private string _articleTitle;
        public string ArticleTitle
        {
            get => _articleTitle;
            set => SetProperty(ref _articleTitle, value);
        }

        private string _articleText;
        public string ArticleText
        {
            get => _articleText;
            set => SetProperty(ref _articleText, value);
        }

        public ViewArticlePageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("TITLE"))
            {
                ArticleTitle = (string)parameters.Where(i => i.Key == "TITLE").SingleOrDefault().Value;
                ArticleText = await StorageService.GetHTMLTextFromFile(ArticleTitle);
            }
        }
    }
}
