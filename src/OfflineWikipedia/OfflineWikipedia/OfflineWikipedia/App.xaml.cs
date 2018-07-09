using Prism;
using Prism.Ioc;
using OfflineWikipedia.ViewModels;
using OfflineWikipedia.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.DryIoc;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OfflineWikipedia
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/StartPage");
        }
        
        protected void InitializeSettings()
        {
            if (!App.Current.Properties.ContainsKey("OpenedBefore"))
            {
                App.Current.Properties["OpenedBefore"] = true;
                App.Current.Properties["NumberOfArticles"] = 5;
                App.Current.Properties["DownloadOverCell"]= false;
                App.Current.Properties["DownloadImages"] = true;
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<StartPage>();
            containerRegistry.RegisterForNavigation<SettingsPage>();
            containerRegistry.RegisterForNavigation<BrowseLibraryPage>();
            containerRegistry.RegisterForNavigation<ViewArticlePage>();
        }
    }
}
