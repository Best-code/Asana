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
                // Passing negative one is for adding a new model / Passing an existing ID is for editing
        if (projectId == -1)
            Model = new Project();
        else
            Model = UnitService.Current.Projects.FirstOrDefault(p => p.Id == projectId) ?? new Project();
    }

    public ProjectDetailViewModel(Project model)
    {
        Model = model ?? new Project();
    }

    public Project? Model { get; set; }

    public void RefreshPage()
    {
        NotifyPropertyChanged(nameof(Model));
    }

    public void AddUpdateProject()
    {
        // As long as the Name is not null add the project
        UnitService.Current.AddUpdateProject(Model);
        RefreshPage();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
