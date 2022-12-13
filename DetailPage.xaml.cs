using Microsoft.Maui.Graphics.Text;
using System.Web;

namespace UWOsh_InteractiveMap;

public partial class DetailPage : ContentPage
{
    public DetailPage(Plant detailPlant)
    {
        InitializeComponent();

        string plantNames = detailPlant.PopularName + " (" + detailPlant.ScientificName + ")";
        DisplayTitle.Text = plantNames;
        DisplayDescription.Text = detailPlant.Description;
        ImagePath.Source = detailPlant.ImageUrl;
    }

}