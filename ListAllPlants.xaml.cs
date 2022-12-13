//using Android.App;
//using Android.App.AppSearch;

using System.Collections.ObjectModel;

namespace UWOsh_InteractiveMap;

// Written by Rudy Liljeberg
// Reviewed by Shabbar Kazmi
public partial class ListAllPlants : ContentPage
{
    PlantDatabase plantDatabase;
    static public ObservableCollection<Plant> SearchHistoryCollection = new ObservableCollection<Plant>();
    public ListAllPlants()
	{
		InitializeComponent();

        plantDatabase = new PlantDatabase();
        ListOfAllPlants.ItemsSource = plantDatabase.GetPlants();
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));

    }

    public ListAllPlants(ObservableCollection<Plant> plants)
    {
        InitializeComponent();

        plantDatabase = new PlantDatabase();
        ListOfAllPlants.ItemsSource = plants;
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));

    }

    async void OnItemTapped(object sender, SelectedItemChangedEventArgs e)
    {
        SearchHistoryCollection.Insert(0, (Plant) e.SelectedItem);
        await Navigation.PushAsync(new DetailPage((Plant) e.SelectedItem));
        //await Shell.Current.GoToAsync("DetailPage");
    }
        
    private void OnButtonClicked(object sender, EventArgs e)
    {
        Console.Write("Goodbye!");
    }

    private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
    {
        ListOfAllPlants.ItemsSource = plantDatabase.GetPlants().Where(
                                        s=>s.PopularName.StartsWith(e.NewTextValue));
    }
}