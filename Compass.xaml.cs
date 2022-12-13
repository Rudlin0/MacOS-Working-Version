using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Gauges;
using System.Timers;
using Location = Microsoft.Maui.Devices.Sensors.Location;

namespace UWOsh_InteractiveMap;
/*
Author: Benjamin Wastart
Reviewer: Rudy Liljeberg
*/
public partial class Compass : ContentPage
{
    private Location NORTH = new Location(81.3, 110.8); //Magnetic North
    private ShapePointer pointer = new ShapePointer();
    private RadialAxis radialAxis = new RadialAxis();
    private Location myLocation = new Location();
    private LocationFeaturescs featurescs = new LocationFeaturescs();
    private Location pointb; //The third point in the triangle
    private System.Timers.Timer aTimer;
    private Location location;

    public Compass()
    {
        InitializeComponent();

        SetCompass();
        SetPointer();
        location = NORTH;
        SetGuide(NORTH);
        SetTimer();
        aTimer.Stop();
        aTimer.Dispose();

    }

    public Compass(Location plant)
    {
        InitializeComponent();
        location = plant;
        SetCompass();
        SetPointer();
        SetGuide(plant);
        SetTimer();
        aTimer.Stop();
        aTimer.Dispose();
    }

    public void SetGuide()
    {
        SetGuide(NORTH);

    }

    public void SetGuide(Location plant)
    {
        myLocation = featurescs.myLocation;
        compassGauge.Axes.Add(radialAxis);


        double sidec = Location.CalculateDistance(myLocation, plant, DistanceUnits.Miles);
        pointb = new Location(myLocation.Latitude, plant.Longitude);

        double sideb = Location.CalculateDistance(pointb, plant, DistanceUnits.Miles);

        double sidea = Location.CalculateDistance(myLocation, pointb, DistanceUnits.Miles);

        pointer.Value = (320 + (Math.Acos(((sidea * sidea + sidec * sidec - sideb * sideb) / (2 * sidea * sidec)))) * 180 / Math.PI) % 360;

        radialAxis.Pointers.Add(pointer);
    }

    private void SetCompass()
    {
        radialAxis.Pointers.Clear();
        radialAxis.ShowAxisLine = false;
        radialAxis.TickPosition = GaugeElementPosition.Outside;
        radialAxis.LabelPosition = GaugeLabelsPosition.Outside;
        radialAxis.StartAngle = 320;
        radialAxis.EndAngle = 320;
        radialAxis.RadiusFactor = 0.6;
        radialAxis.MinorTicksPerInterval = 10;
        radialAxis.Minimum = 0;
        radialAxis.Maximum = 360;
        radialAxis.ShowLastLabel = false;
        radialAxis.Interval = 0;
        radialAxis.OffsetUnit = SizeUnit.Factor;
    }

    private void SetPointer()
    {
        pointer.ShapeHeight = 40;
        pointer.ShapeWidth = 40;
        pointer.ShapeType = ShapeType.Triangle;
        pointer.Stroke = Color.Parse("Black");
        pointer.Fill = Color.Parse("Blue");
        pointer.Offset = 18;
    }

    private void SetTimer()
    {
        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(2000);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        SetGuide(location);
    }
}

/*
 * The Location constructor accepts the latitude and longitude arguments, respectively. 
 * Positive latitude values are north of the equator, and positive longitude values are east of the Prime Meridian. 
 * Use the final argument to CalculateDistance to specify miles or kilometers. 
 * The UnitConverters class also defines KilometersToMiles and MilesToKilometers methods for converting between the two units.
 * Distance function
 * 
      c  /|  b
        / |
        ---
        a
 c = Location.CalculateDistance(UserLocation , PlantLocation)
 b = Location.CalculateDistance(PlantLocation x UserLocation y   , PlantLocation)
 a = Location.CalculateDistance(UserLocation , PlantLocation x  UserLocation y)
A = arccos((b*b + c*c - a*a) / (2bc)) opposite angle of side a
B = arccos((a*a + c*c - b*b) / (2ac)) opposite angle of side b
Location self = new Location(42.358056, -71.063611); lat, log
Location plant = new Location(37.783333, -122.416667); y, x - n, w
double clength = Location.CalculateDistance(self, plant, DistanceUnits.Kilometers);
north = 81.3 N 110.8 W
Math.Acos() is the cos-1 function in .NET
 c = Location.CalculateDistance(UserLocation , north)
 b = Location.CalculateDistance( UserLocation.N 110.8  , north)
 a = Location.CalculateDistance(UserLocation ,  UserLocation.n 110.8)

 */