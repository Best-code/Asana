using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Asana.Core.Models;
using Asana.Core.Services;

// TODO Pickers are fucked when I leave a page and come back

namespace Asana.Maui.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    ProjectService _projSvc;
    UnitService _unitSvc;
    public MainPageViewModel()
    {
        _projSvc = ProjectService.Current;
        _unitSvc = UnitService.Current;
        RefreshPage();
    }

    // Reset the Selected ToDo, and the Displayed ToDos based on the Selected Project which may be reset if needed
    public void RefreshPage()
    {
        // Get all the Project Names on refresh and add an All option to the top
        ProjectNames = new ObservableCollection<string> { "All" };
        foreach (Project p in _unitSvc.Projects)
        {
            ProjectNames.Add(p.Name);
        }

        // If the new list doesn't have the name you selected then select first available option
        if (!ProjectNames.Contains(SelectedProject))
            SelectedProject = ProjectNames.First();
        else
            UpdateShownProjects();

        // ToDo selected for delete / Edit
        SelectedToDo = null;
    }

    public ToDo? DeleteToDo(ToDo? toDo)
    {
        _projSvc.DeleteTodo(toDo);
        UpdateShownProjects();
        selectedToDo = null;
        NotifyPropertyChanged(nameof(SelectedToDo));
        return toDo;
    }

    public Project? DeleteProject(Project? project)
    {
        _unitSvc.DeleteProject(project);
        ClearToDosFromProject(project);
        RefreshPage();
        return project;
    }

    private void ClearToDosFromProject(Project? proj)
    {
        for (int x = _projSvc.ToDos.Count() - 1; x >= 0; x--)
        {
            if (_projSvc.ToDos[x]?.ProjectId == proj?.Id)
            {
                _projSvc.ToDos.RemoveAt(x);
            }
        }
        SelectedProject = "All";
    }

    private ObservableCollection<ToDoDetailViewModel> displayedToDos;
    public ObservableCollection<ToDoDetailViewModel> ToDos
    {
        get
        {
            return displayedToDos;
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
                NotifyPropertyChanged(nameof(IsShowCompleteToDos));
            }
        }
    }

    private ObservableCollection<String>? _projectNames;
    public ObservableCollection<String> ProjectNames
    {
        get => _projectNames ?? new ObservableCollection<String>();
        private set
        {
            if (_projectNames != value)
            {
                _projectNames = value;
                NotifyPropertyChanged(nameof(ProjectNames));
            }
        }
    }

    // Selected Project for displaying only the associated ToDos
    private string? selectedProject;
    public string SelectedProject
    {
        get => selectedProject ?? "Null Project";
        set
        {
            if (selectedProject != value)
            {
                selectedProject = value;
                NotifyPropertyChanged(nameof(SelectedProject));
                UpdateShownProjects();
            }
        }
    }



    private ToDoDetailViewModel? selectedToDo;
    public ToDoDetailViewModel? SelectedToDo
    {
        get => selectedToDo;
        set
        {
            if (value != selectedToDo)
            {
                selectedToDo = value;
                NotifyPropertyChanged(nameof(SelectedToDo));
            }
        }
    }


    // Updates the ToDos being shown based on the top menu bar - IsShowCompleted and the currently selected project
    public void UpdateShownProjects()
    {
        var toDos = _projSvc.ToDos.Select(t => new ToDoDetailViewModel(t)).Take(100);
        // If you don't want to show complete projects
        if (!IsShowCompleteToDos)
            // Show todos where IsComplete is not true
            toDos = _projSvc.ToDos.Select(t => new ToDoDetailViewModel(t)).Where(t => !t?.Model?.IsComplete ?? false).Take(100);


        // Only get ToDos in selected project
        if (SelectedProject != null && SelectedProject != "All")
        {
            int selectedId = _unitSvc.GetProjectByName(SelectedProject).Id;
            toDos = toDos.Where(t => t?.Model?.ProjectId == selectedId);
        }

        ToDos = new ObservableCollection<ToDoDetailViewModel>(toDos);
    }

    public void InlineDeleteClicked()
    {
        UpdateShownProjects();
        SelectedToDo = null;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
