using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace UWOsh_InteractiveMap
{
    // Written By Rudy Liljeberg
    // Reviewed by Shabbar Kazmi
    public interface IDatabase : INotifyPropertyChanged
    {
        Plant FindEntry(int id);
        ObservableCollection<Plant> GetPlants();
    }
}