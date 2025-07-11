using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Asana.Core.Models;
using Asana.Core.Services;

namespace Asana.Maui.ViewModels;

public class ProjectDetailViewModel : INotifyPropertyChanged
{
    UnitService _unitSvc;
    public ProjectDetailViewModel()
    {
        InitializeViewModel();
        Model = new Project();
    }

    public ProjectDetailViewModel(int projectId)
    {
        InitializeViewModel();

        // Passing 0 is for adding a new model / Passing an existing ID is for editing
        if (projectId == 0)
            Model = new Project();
        else
            Model = _unitSvc.Projects.FirstOrDefault(p => p.Id == projectId) ?? new Project();

    }

    public ProjectDetailViewModel(Project model)
    {
        InitializeViewModel();
        Model = model ?? new Project();
    }

    public Project? Model { get; set; }


    public void InitializeViewModel()
    {
        _unitSvc = UnitService.Current;

        RefreshPage();
    }
    public void RefreshPage()
    {
        Model = new();
        NotifyPropertyChanged(nameof(Model));
    }

    public void AddUpdateProject()
    {
        // As long as the Name is not null add the project
        _unitSvc.AddUpdateProject(Model);
        RefreshPage();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
