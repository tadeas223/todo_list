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
        
        view.BackButton.Click += (sender, e) =>
        {
            main.StartUI("task", task);
        };

        var calendarRepo = Provider.Instance.ProvideCalendarRepository();
        try
        {
            foreach(Calendar c in calendarRepo.SelectByProject(task.Board.Project))
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
             () => main.StartUI("task", task));        
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
                 () => main.StartUI("add_to_calendar", task));        
            }

            try
            {
                if(selectedCalendar == null || !selectedDate.HasValue)
                {
                    main.StartUI("error", $"select a calendar and date",
                        () => main.StartUI("add_to_calendar", task));
                    return;        
                }

                calendarRepo.InsertTask(selectedCalendar!, selectedDate!.Value, task);
                main.StartUI("task", task);
            }
            catch(Exception ex)
            {
                main.StartUI("error", $"failed to insert task: {ex.Message}",
                 () => main.StartUI("add_to_calendar", task));        
            }

        };

        main.Present(view);
    }
}