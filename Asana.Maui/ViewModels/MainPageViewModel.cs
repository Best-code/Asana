using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Asana.Core.Models;
using Asana.Core.Services;


namespace Asana.Maui.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    ProjectService _projSvc;
    UnitService _unitSvc;
    public MainPageViewModel()
    {
        _projSvc = ProjectService.Current;
        _unitSvc = UnitService.Current;

        ProjectNames = new ObservableCollection<string>(_unitSvc.Projects.Select(p => p.Name));
        if (ProjectNames != null && ProjectNames.Any())
            SelectedProject = ProjectNames.First();
    }

    public ToDo? Model { get; set; } = new();

    private ObservableCollection<String>? _projectNames;
    public ObservableCollection<String> ProjectNames
    {
        get => _projectNames ?? new ObservableCollection<String>();
        private set
        {
            if (_projectNames != value)
            {
                _projectNames = value;
                NotifyPropertyChanged();
            }
        }
    }


    private string? selectedProject;
    public string SelectedProject
    {
        get => selectedProject ?? "Null Project";
        set
        {
            if (selectedProject != value)
            {
                selectedProject = value;
                NotifyPropertyChanged();
                UpdateShownProjects();
            }
        }
    }


    private bool? isShowCompleteToDos;
    public bool IsShowCompleteToDos
    {
        get { return isShowCompleteToDos ?? true; }
        set
        {
            if (isShowCompleteToDos != value)
            {
                isShowCompleteToDos = value;
                UpdateShownProjects();
                NotifyPropertyChanged();
            }
        }
    }

    private ObservableCollection<ToDoDetailViewModel>? displayedToDos;
    public ObservableCollection<ToDoDetailViewModel> ToDos
    {
        get
        {
            return displayedToDos ?? new ObservableCollection<ToDoDetailViewModel>();
        }
        set
        {
            if (value != displayedToDos)
            {
                displayedToDos = value;
                NotifyPropertyChanged(nameof(ToDos));
            }
        }
    }

    public void RefreshPage()
    {
        UpdateShownProjects();
    }

    private void UpdateShownProjects()
    {
        var toDos = _projSvc.ToDos.Select(t => new ToDoDetailViewModel(t)).Take(100);
        // If you don't want to show complete projects
        if (!IsShowCompleteToDos)
            // Show todos where IsComplete is not true
            toDos = _projSvc.ToDos.Select(t => new ToDoDetailViewModel(t)).Where(t => !t.Model?.IsComplete ?? false).Take(100);


        // Only get ToDos in selected project
        if (SelectedProject != null)
        {
            int selectedId = _unitSvc.GetProjectByName(SelectedProject).Id;
            toDos = toDos.Where(t => t.Model?.ProjectId == selectedId);
        }

        ToDos = new ObservableCollection<ToDoDetailViewModel>(toDos);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
