namespace UI;

using Avalonia.Controls;
using Microsoft.CSharp.RuntimeBinder;
using UI.Controller;
using Avalonia.Layout;
using Avalonia;
using Avalonia.Controls.Primitives;

public class MainWindow : ScrollViewer
{
    public MainWindow()
    {
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;

        StartUI("login");
    }

    public void StartUI(string name, params object[] args)
    {
        IController? controller = null;
        switch(name)
        {
            case "login":
                controller = new LoginController(this);
                break;
            case "error":
                controller = new ErrorController(this);
                break;
            case "database_setup":
                controller = new DatabaseSetupController(this);
                break;
            case "project_selection":
                controller = new ProjectSelectionController(this);
                break;
            case "project":
                controller = new ProjectController(this);
                break;
            case "add_board":
                controller = new AddBoardController(this);
                break;
            case "add_project":
                controller = new AddProjectController(this);
                break;
            case "kanban":
                controller = new KanbanController(this);
                break;
            case "add_task":
                controller = new AddTaskController(this);
                break;
            case "task":
                controller = new TaskController(this);
                break;
            case "add_calendar":
                controller = new AddCalendarController(this);
                break;
            case "calendar":
                controller = new CalendarController(this);
                break;
            case "calendar_date":
                controller = new CalendarDateController(this);
                break;
            case "add_to_calendar":
                controller = new AddToCalendarController(this);
                break;
            case "calendar_task":
                controller = new CalendarTaskController(this);
                break;
            case "progress_report":
                controller = new ProgressReportController(this);
                break;
        }

        if(controller == null)
        {
            throw new Exception($"invalid ui name {name}");            
        }


        controller.Start(args);
    }

    public void Present(UserControl view)
    {
        Content = view;
    }
}