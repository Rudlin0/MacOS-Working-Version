//using Android.App;
//using Android.App.AppSearch;

namespace UWOsh_InteractiveMap;

// Written by Rudy Liljeberg
// Reviewed by Shabbar Kazmi
public partial class ListAllPlants : ContentPage
{
    PlantDatabase plantDatabase;
    public ListAllPlants()
	{
		InitializeComponent();

        plantDatabase = new PlantDatabase();
        ListOfAllPlants.ItemsSource = plantDatabase.GetPlants();
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