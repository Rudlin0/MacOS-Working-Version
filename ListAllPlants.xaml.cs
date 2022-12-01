namespace UWOsh_InteractiveMap;

public partial class ListAllPlants : ContentPage
{
	public ListAllPlants()
	{
		InitializeComponent();

        //BindingContext = new PlantDatabase();

        PlantDatabase plantDatabase = new PlantDatabase();
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