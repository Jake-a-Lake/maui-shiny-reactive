using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShinyApp.Views;

namespace ShinyApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for navigation
            //Routing.RegisterRoute("home", typeof(MainPage));
            // Add more routes as needed
        }
    }
}