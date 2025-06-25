using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Asana.Core.Models;
using Asana.Core.Services;

namespace Asana.Maui.ViewModels;

public class ProjectDetailViewModel : INotifyPropertyChanged
{
    public ProjectDetailViewModel()
    {
        Model = new Project();
    }

    public ProjectDetailViewModel(int projectId)
    {
        Model = UnitService.Current.GetProjectById(projectId);
    }

    public ProjectDetailViewModel(Project model)
    {
        Model = model ?? new Project();
    }

    private Project? model;
    public Project? Model
    {
        get => model;
        set
        {
            if (model != value)
            {
                model = value;
                NotifyPropertyChanged();
            }
        }
    }

    public void RefreshPage()
    {
        Model = new Project();
        NotifyPropertyChanged(nameof(Model));
    }

    public void AddProject()
    {
        UnitService.Current.AddProject(Model ?? new Project());
        RefreshPage();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
