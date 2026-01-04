namespace UI.Controller;

using DI;
using UI.View;
using Domain.Model;

public class CalendarController : IController
{
    private MainWindow main;
    private CalendarView view;

    public CalendarController(MainWindow main)
    {
        this.main = main;
        view = new CalendarView();
    }

    public void Start(params object[] args)
    {
        Calendar calendar = (Calendar)args[0];
        Project project = (Project)args[1];

        var calendarRepo = Provider.Instance.ProvideCalendarRepository();
        
        view.TittleBar.TittleText.Text = calendar.Name;

        view.BackButton.Click += (sender, e) =>
        {
            main.StartUI("project", project);   
        };

        view.DeleteButton.Click += (sender, e) =>
        {
            try
            {
                calendarRepo.Delete(calendar);
                main.StartUI("project", project);
                return;
            }
            catch(Exception ex)
            {
                main.StartUI("error", $"error while deleting calendar: {ex.Message}", () => main.StartUI("calendar", calendar, project));
            }
        };

        view.Calendar.SelectedDatesChanged += (sender, e) =>
        {
            DateTime? date = view.Calendar.SelectedDate;

            if(date == null)
            {
                main.StartUI("calendar", calendar, project);
                return;
            }

            main.StartUI("calendar_date", calendar, date, project);
        };

        main.Present(view);
    }
}