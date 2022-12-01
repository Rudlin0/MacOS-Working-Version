namespace UWOsh_InteractiveMap;

//Written by Rudy Liljeberg

public partial class SearchHistory : ContentPage
{
    PlantDatabase plantDatabase;
    public SearchHistory()
    {
        InitializeComponent();

        plantDatabase = new PlantDatabase();
        UserSearchHistory.ItemsSource = plantDatabase.GetPlants();
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));

    }
    async void OnItemTapped(object sender, SelectedItemChangedEventArgs e)
    {
        await Shell.Current.GoToAsync("DetailPage");
    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        Console.Write("Goodbye!");
    }

    private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
    {

    }
}