using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace UWOsh_InteractiveMap
{
    // Written By Rudy Liljeberg
    // Reviewed by Shabbar Kazmi
    public interface IDatabase : INotifyPropertyChanged
    {
  
        ObservableCollection<Plant> GetPlants();
        ObservableCollection<ObservableCollection<Plant>> GetPlantFromLocation(List<String> plantLocs);
        List<String> GetPlantsLoc();
    }
}