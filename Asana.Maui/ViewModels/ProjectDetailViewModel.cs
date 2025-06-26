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

    public Project? Model { get; set; }

    public void RefreshPage()
    {
        Model = new Project();
        NotifyPropertyChanged(nameof(Model));
    }

    public void AddProject()
    {
        // As long as the Name is not null add the project
        var modelName = Model?.Name;
        if(modelName != null && modelName != "")
            UnitService.Current.AddProject(Model ?? new Project());
        RefreshPage();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
