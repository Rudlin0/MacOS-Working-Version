namespace UWOsh_InteractiveMap;

public partial class MapUwosh : ContentPage
{
	// Author : Shabbar Kazmi
	public MapUwosh()
	{
		InitializeComponent();
		PlantDatabase pd = new PlantDatabase();
		map.ItemsSource = pd.GetPlants(); // initializes itemsource from
										  // the list of all plants
	}

    async void OnTextChanged(object sender, EventArgs e)
	{
     await Shell.Current.GoToAsync("ListAllPlants"); // on text changed it 
    }
}
