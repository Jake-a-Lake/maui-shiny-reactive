using ShinyApp.Views;

namespace ShinyApp
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            // MainPage = new AppShell();
        }


        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = new Window(new AppShell());

            // Make sure to set the Page property
            if (window.Page == null)
                window.Page = new AppShell();

            return window;
        }

        //protected override void RegisterTypes(IContainerRegistry containerRegistry)
        //{
        //    containerRegistry.RegisterForNavigation<MainPage>();
        //    containerRegistry.RegisterForNavigation<SearchPage>();
        //    containerRegistry.RegisterForNavigation<ProfilePage>();
        //}

        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();
        //    // Optional: Initial navigation if needed
        //    // NavigationService.NavigateAsync("///MainPage");
        //}
    }
}
