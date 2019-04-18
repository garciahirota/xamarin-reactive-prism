using Prism;
using Prism.Ioc;
using Colorful.DemoApp.ViewModels;
using Colorful.DemoApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Colorful.DemoApp.Contracts;
using Colorful.DemoApp.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Colorful.DemoApp
{
    public partial class App
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

            await NavigationService.NavigateAsync("NavigationPage/LogingPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Services
            containerRegistry.Register<ILoginServices, LoginServices>();
            containerRegistry.Register<IColorPalleteServices, ColorPalleteServices>();

            // View & ViewModels
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<LogingPage>();
            containerRegistry.RegisterForNavigation<PalletesPage>();
            containerRegistry.RegisterForNavigation<PalleteDetailsPage>();

            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LogingPage, LogingPageViewModel>();
            containerRegistry.RegisterForNavigation<PalletesPage, PalletesPageViewModel>();
            containerRegistry.RegisterForNavigation<PalleteDetailsPage, PalleteDetailsPageViewModel>();
        }
    }
}
