namespace UWOsh_InteractiveMap;

//Written by Rudy Liljeberg
// Reviewed by Shabbar Kazmi
public partial class SearchHistory : ContentPage
{
    PlantDatabase plantDatabase;
    public SearchHistory()
    {
        InitializeComponent();

        plantDatabase = new PlantDatabase();
        UserSearchHistory.ItemsSource = ListAllPlants.SearchHistoryCollection;
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));

    }
    async void OnItemTapped(object sender, SelectedItemChangedEventArgs e)
    {
        await Navigation.PushAsync(new DetailPage((Plant)e.SelectedItem));
    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        Console.Write("Goodbye!");
    }

    private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
    {
        UserSearchHistory.ItemsSource = ListAllPlants.SearchHistoryCollection.Where(
                                        s => s.PopularName.StartsWith(e.NewTextValue));
    }
}