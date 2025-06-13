using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Asana.Core.Models;

namespace Asana.Maui.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    AsanaUnit _unit;
    public MainPageViewModel()
    {
        _unit = new();
    }

    public ObservableCollection<Project> Projects
    {
        get
        {
            var projects = _unit.Projects;
            // if (IsShowCompleteProjects)
            // {
            //     projects = _unit.Projects;
            // }
            return new ObservableCollection<Project>(projects);
        }
    }

    private bool isShowCompleteProjects;
    public bool IsShowCompleteProjects
    {
        get { return isShowCompleteProjects; }
        set
        {
            if (isShowCompleteProjects != value)
            {
                isShowCompleteProjects = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Projects));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
