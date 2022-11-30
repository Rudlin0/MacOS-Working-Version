using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace UWOsh_InteractiveMap
{
    public interface IDatabase : INotifyPropertyChanged
    {
        Plant FindEntry(int id);
        ObservableCollection<Plant> GetEntries();
    }
}