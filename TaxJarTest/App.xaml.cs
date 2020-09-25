using Prism;
using Prism.Ioc;
using TaxJarTest.Interfaces;
using TaxJarTest.RestCalls;
using Xamarin.Forms;

namespace TaxJarTest
{
    [AutoRegisterForNavigation]
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("MainView");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Register the views for navigation.
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterSingleton<IGetDataCalls, GetDataCalls>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}