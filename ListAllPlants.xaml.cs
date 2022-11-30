namespace UWOsh_InteractiveMap;

public partial class ListAllPlants : ContentPage
{
	public ListAllPlants()
	{
		InitializeComponent();
        BindingContext = new PlantDatabase();
    }
    private void OnButtonClicked(object sender, EventArgs e)
    {

    }
}