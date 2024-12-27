
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Unit = System.Reactive.Unit;

namespace ShinyApp
{
    public class Contact : ReactiveObject
    {
        [Reactive] public string Name { get; set; }
        [Reactive] public string Email { get; set; }
        [Reactive] public string Phone { get; set; }
    }

    public class MainViewModel : ReactiveObject, IActivatableViewModel
    {
        private static readonly string[] Traits = {
            "expressive",
            "clear",
            "responsive",
            "concurrent",
            "reactive"
        };
        [Reactive] public string Greeting { get; set; }

        [Reactive] public int Count { get; set; }
        [ObservableAsProperty] public string CounterButtonText { get; }
        public ReactiveCommand<Unit, Unit> ButtonClickedCommand { get; }

        public ViewModelActivator Activator { get; } = new();

        private void ButtonClicked() => Count++;


        public MainViewModel()
        {
            ButtonClickedCommand = ReactiveCommand.Create(ButtonClicked);

            // Create the text for the counterbutton
            this.WhenAnyValue(vm => vm.Count)
                .Select(c => c switch
                {
                    0 => "Click me",
                    1 => "Clicked 1 time",
                    _ => $"Clicked {c} times"
                })
                .ToPropertyEx(this, vm => vm.CounterButtonText);

            this.WhenActivated(disposables =>
            {
                // Just log the ViewModel's activation
                // https://github.com/kentcb/YouIandReactiveUI/blob/master/ViewModels/Samples/Chapter%2018/Sample%2004/ChildViewModel.cs
                Console.WriteLine(
                    $"[vm {Thread.CurrentThread.ManagedThreadId}]: " +
                    "ViewModel activated");

                // Asynchronously generate a new greeting message every second
                // https://reactiveui.net/docs/guidelines/framework/ui-thread-and-schedulers
                Observable
                    .Timer(
                        TimeSpan.FromMilliseconds(100), // give the view time to activate
                        TimeSpan.FromMilliseconds(1000))
                    .Do(
                        t => {
                            var newGreeting = $"Hello, {Traits[t % Traits.Length]} world !";
                            Console.WriteLine(
                                $"[vm {Thread.CurrentThread.ManagedThreadId}]: " +
                                $"Timer Observable -> " +
                                $"Setting greeting to: \"{newGreeting}\"");
                            Greeting = newGreeting;
                        },
                        () =>
                            Console.WriteLine(
                                "Those are all the greetings, folks! " +
                                "Feel free to close the window now...\n"))
                    .Subscribe()
                    .DisposeWith(disposables);

                // Just log the ViewModel's deactivation
                // https://github.com/kentcb/YouIandReactiveUI/blob/master/ViewModels/Samples/Chapter%2018/Sample%2004/ChildViewModel.cs
                Disposable
                    .Create(
                        () =>
                            Console.WriteLine(
                                $"[vm {Thread.CurrentThread.ManagedThreadId}]: " +
                                "ViewModel deactivated"))
                    .DisposeWith(disposables);
            });
        }


    }




    //public partial class MainViewModel : ReactiveObject
    //{
    //    private readonly IContactService _contactService;

    //    // Observable properties
    //    [Reactive] public string SearchText { get; set; }
    //    [Reactive] public Contact NewContact { get; set; }
    //    [Reactive] public ObservableCollection<Contact> Contacts { get; private set; }
    //    [Reactive] public bool IsLoading { get; private set; }

    //    // Derived properties
    //    private readonly ObservableAsPropertyHelper<string> _validationMessage;
    //    public string ValidationMessage => _validationMessage.Value;

    //    private readonly ObservableAsPropertyHelper<string> _stats;
    //    public string Stats => _stats.Value;

    //    private readonly ObservableAsPropertyHelper<IEnumerable<Contact>> _filteredContacts;
    //    public IEnumerable<Contact> FilteredContacts => _filteredContacts.Value;

    //    // Commands
    //    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    //    public ReactiveCommand<Contact, Unit> DeleteCommand { get; }

    //    public MainViewModel(IContactService contactService)
    //    {
    //        _contactService = contactService;
    //        NewContact = new Contact();
    //        Contacts = new ObservableCollection<Contact>();

    //        // Setup validation
    //        var canSave = this.WhenAnyValue(
    //            x => x.NewContact.Name,
    //            x => x.NewContact.Email,
    //            (name, email) =>
    //                !string.IsNullOrEmpty(name) &&
    //                !string.IsNullOrEmpty(email) &&
    //                email.Contains("@")
    //        );

    //        // Create validation message
    //        _validationMessage = this.WhenAnyValue(
    //            x => x.NewContact.Name,
    //            x => x.NewContact.Email,
    //            (name, email) =>
    //            {
    //                if (string.IsNullOrEmpty(name)) return "Name is required";
    //                if (string.IsNullOrEmpty(email)) return "Email is required";
    //                if (!email.Contains("@")) return "Invalid email format";
    //                return string.Empty;
    //            })
    //            .ToProperty(this, x => x.ValidationMessage);

    //        //// Create stats message
    //        //_stats = this.WhenAnyValue<MainViewModel,int, string>(
    //        //    x => x.Contacts.Count,
    //        //    count => $"Total Contacts: {count}")
    //        //    .ToProperty(this, x => x.Stats);

    //        // Setup filtered contacts
    //        _filteredContacts = this.WhenAnyValue(
    //            x => x.SearchText,
    //            x => x.Contacts,
    //            (searchText, contacts) =>
    //            {
    //                if (string.IsNullOrEmpty(searchText))
    //                    return contacts;

    //                return contacts.Where(c =>
    //                    c.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
    //                    c.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
    //                    c.Phone.Contains(searchText, StringComparison.OrdinalIgnoreCase));
    //            })
    //            .ToProperty(this, x => x.FilteredContacts);

    //        // Setup commands
    //        SaveCommand = ReactiveCommand.CreateFromTask(
    //            async () =>
    //            {
    //                IsLoading = true;
    //                try
    //                {
    //                    await _contactService.AddContact(NewContact);
    //                    Contacts.Add(NewContact);
    //                    NewContact = new Contact();
    //                }
    //                finally
    //                {
    //                    IsLoading = false;
    //                }
    //            },
    //            canSave);

    //        DeleteCommand = ReactiveCommand.CreateFromTask<Contact>(
    //            async contact =>
    //            {
    //                IsLoading = true;
    //                try
    //                {
    //                    await _contactService.DeleteContact(contact);
    //                    Contacts.Remove(contact);
    //                }
    //                finally
    //                {
    //                    IsLoading = false;
    //                }
    //            });

    //        // Load initial data
    //        LoadContacts();
    //    }

    //    private async void LoadContacts()
    //    {
    //        IsLoading = true;
    //        try
    //        {
    //            var contacts = await _contactService.GetContacts();
    //            Contacts = new ObservableCollection<Contact>(contacts);
    //        }
    //        finally
    //        {
    //            IsLoading = false;
    //        }
    //    }
    //}
}