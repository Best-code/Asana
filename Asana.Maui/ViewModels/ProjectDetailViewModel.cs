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
        Model = UnitService.Current.Projects.First(p => p.Id == projectId) ?? new Project();
    }

    public ProjectDetailViewModel(Project model)
    {
        Model = model ?? new Project();
    }

    public Project? Model { get; set; }

    public void AddProject()
    {
        UnitService.Current.AddProject(Model ?? new Project());
    }

    

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
