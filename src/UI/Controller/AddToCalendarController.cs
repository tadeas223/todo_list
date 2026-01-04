namespace UI.Controller;

using UI.View;
using DI;
using Domain.Model;

public class AddToCalendarController : IController
{
    private MainWindow main;
    private AddToCalendarView view;
    private Calendar? selectedCalendar = null;
    private DateTime? selectedDate = null;

    public AddToCalendarController(MainWindow main)
    {
        this.main = main;
        view = new AddToCalendarView();
    }

    public void Start(params object[] args)
    {
        TodoTask task = (TodoTask)args[0];
        Board board = (Board)args[1];
        Project project = (Project)args[2];
        
        view.BackButton.Click += (sender, e) =>
        {
            main.StartUI("task", board, task, project);
        };

        var calendarRepo = Provider.Instance.ProvideCalendarRepository();
        try
        {
            foreach(Calendar c in calendarRepo.SelectByProject((Project)args[2]))
            {
                view.CalendarSelection.AddSquare(c.Name, (sender ,e) =>
                {
                    view.SelectedCalendar.Text = c.Name;
                    selectedCalendar = c;
                });
            }
        }
        catch(Exception ex)
        {
            main.StartUI("error", $"failed to select calendars: {ex.Message}",
             () => main.StartUI("task", board, task, project));        
        }

        view.Calendar.SelectedDatesChanged += (sender ,e) =>
        {
            selectedDate = view.Calendar.SelectedDate;
        };

        view.AddButton.Click += (sender, e) =>
        {
            if(selectedCalendar == null || selectedDate == null)
            {
                main.StartUI("error", $"missing fields",
                 () => main.StartUI("add_to_calendar", task, board, project));        
            }

            try
            {
                calendarRepo.InsertTask(selectedCalendar!, selectedDate!.Value, task);
                main.StartUI("task", task, board,  project);
            }
            catch(Exception ex)
            {
                main.StartUI("error", $"failed to insert task: {ex.Message}",
                 () => main.StartUI("task", task, board,  project));        
            }

        };

        main.Present(view);
    }
}