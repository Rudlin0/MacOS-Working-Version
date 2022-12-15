using System.Collections.ObjectModel;
using Microsoft.Maui.ApplicationModel;
using MMC = Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
using Microsoft.Maui.Controls.Maps;

namespace UWOsh_InteractiveMap;

public partial class MapUwosh : ContentPage
{
    // Author : Shabbar Kazmi
    // Reviewer: Rudy Liljeberg
    Pin Pin;
    Location PlantLoc;
    String Loc;
    ObservableCollection<ObservableCollection<Plant>> Plants;
    List<String> plantcoordinates;
    PlantDatabase pd;

    public MapUwosh()
	{
        InitializeComponent();
        pd = new PlantDatabase();
        this.plantcoordinates = pd.GetPlantsLoc();
        this.Plants = pd.GetPlantFromLocation(this.plantcoordinates);
        AddPins();

    }
    
    public void AddPins()
    {

        for (int i = 0; i < Plants.Count; i++)
        {
            if (Plants[i].Count > 1)
            {
                int num = i;
                Pin = new Pin
                {
                    Label = "Multiple Plants Here",
                    Address = Plants[num][0].Coordinates,
                    Type = PinType.Place,
                    Location = Plants[num][0].Plantloc
                };

                map.Pins.Add(Pin);

                Pin.MarkerClicked += (s, args) =>
                {
                    pd.selectedLocation = Plants[num][0].Plantloc; // sets the location of selected location.Couldnt find a better way so I made the variable in the Database;
                    args.HideInfoWindow = false;
                };

                Pin.InfoWindowClicked += async (s, args) =>
                {
                    args.HideInfoWindow = true;
                    await Navigation.PushAsync(new ListAllPlants(Plants[num]));
                };
            }
            else {

                int currPlant = i;
                Pin = new Pin
                {
                    Label = Plants[currPlant][0].PopularName,
                    Address = Plants[currPlant][0].Coordinates,
                    Type = PinType.Place,
                    Location = Plants[currPlant][0].Plantloc

                };

                map.Pins.Add(Pin);

                Pin.MarkerClicked += (s, args) =>
                {
                    pd.selectedLocation = Plants[currPlant][0].Plantloc; // sets the location of selected location.Couldnt find a better way so I made the variable in the Database;
                    args.HideInfoWindow = false;
                };

                Pin.InfoWindowClicked += async (s, args) =>
                {
                    args.HideInfoWindow = true;
                    await Navigation.PushAsync(new DetailPage(Plants[currPlant][0]));
                };
            }
        }

        }

    async void OnTextChanged(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ListAllPlants"); // on text changed it 
    }

 } 
