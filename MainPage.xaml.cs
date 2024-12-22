using ReactiveUI;
using ReactiveUI.Maui;
using Microsoft.Maui.Controls;


namespace ShinyApp
{

    public partial class MainPage : ReactiveContentPage<MainViewModel>
    {
        public MainPage(MainViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            this.WhenActivated(_ => { });
        }
    }



    //public partial class MainPage : ContentPage
    //{
    //    public MainPage()
    //    {

    //        InitializeComponent();

    //        this.WhenActivated(disposables =>
    //        {
    //            // Bind search input
    //            this.Bind(ViewModel,
    //                    vm => vm.SearchText,
    //                    view => view.SearchEntry.Text)
    //                .DisposeWith(disposables);

    //            // Bind input fields
    //            this.Bind(ViewModel,
    //                    vm => vm.NewContact.Name,
    //                    view => view.NameEntry.Text)
    //                .DisposeWith(disposables);

    //            this.Bind(ViewModel,
    //                    vm => vm.NewContact.Email,
    //                    view => view.EmailEntry.Text)
    //                .DisposeWith(disposables);

    //            this.Bind(ViewModel,
    //                    vm => vm.NewContact.Phone,
    //                    view => view.PhoneEntry.Text)
    //                .DisposeWith(disposables);

    //            // Bind validation message
    //            this.OneWayBind(ViewModel,
    //                    vm => vm.ValidationMessage,
    //                    view => view.ValidationLabel.Text)
    //                .DisposeWith(disposables);

    //            // Bind stats
    //            this.OneWayBind(ViewModel,
    //                    vm => vm.Stats,
    //                    view => view.StatsLabel.Text)
    //                .DisposeWith(disposables);

    //            // Bind contact list
    //            this.OneWayBind(ViewModel,
    //                    vm => vm.FilteredContacts,
    //                    view => view.ContactList.ItemsSource)
    //                .DisposeWith(disposables);

    //            // Bind loading indicator
    //            this.OneWayBind(ViewModel,
    //                    vm => vm.IsLoading,
    //                    view => view.LoadingIndicator.IsVisible)
    //                .DisposeWith(disposables);

    //            // Bind save command
    //            this.BindCommand(ViewModel,
    //                    vm => vm.SaveCommand,
    //                    view => view.SaveButton)
    //                .DisposeWith(disposables);

    //            // Handle delete swipe action
    //            this.ContactList.ItemSwiped += (sender, args) =>
    //            {
    //                if (args.Item is Contact contact)
    //                {
    //                    ViewModel.DeleteCommand.Execute(contact).Subscribe();
    //                }
    //            };
    //        });
    //    }
    //}
}