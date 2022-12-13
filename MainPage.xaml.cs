using System.Collections.ObjectModel;
using Microsoft.Maui.ApplicationModel;
using MMC = Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
using Microsoft.Maui.Controls.Maps;

namespace UWOsh_InteractiveMap;

public partial class MainPage : ContentPage
{
    Pin Pin;
    Location PlantLoc;
    String Loc;
    ObservableCollection<Plant> Plants;
    ObservableCollection<Plant> SameLocPlants;
    PlantDatabase pd;



    public MainPage()
    {
        InitializeComponent();
        pd = new PlantDatabase();
        this.Plants = pd.GetPlantsOrderedByLoc();
        AddPins();
    }



    public void AddPins()
    {
        Loc = Plants[0].Coordinates;
        foreach (Plant p in Plants)
        {
            // need an if statement which checks if the location are the same or not. 

            Pin = new Pin
            {
                Label = p.PopularName,
                Address = p.Coordinates,
                Type = PinType.Place,
                Location = p.Plantloc

            };

            map.Pins.Add(Pin);


            Pin.MarkerClicked += (s, args) =>
            {
                pd.selectedLocation = p.Plantloc; // sets the location of selected location.Couldnt find a better way so I made the variable in the Database;
                args.HideInfoWindow = false;
            };


            Pin.InfoWindowClicked += async (s, args) =>
            {
                args.HideInfoWindow = true;
                await Navigation.PushAsync(new DetailPage(p));
            };
        }
    }

    void OnTextChanged(object sender, EventArgs e)
    {
        Searchbar.IsTextPredictionEnabled = true;
    }
}