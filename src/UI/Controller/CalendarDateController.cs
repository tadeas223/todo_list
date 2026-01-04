namespace UI.Controller;

using DI;
using UI.View;
using Domain.Model;

public class CalendarDateController : IController
{
    private MainWindow main;
    private CalendarDateView view;

    public CalendarDateController(MainWindow main)
    {
        this.main = main;
        view = new CalendarDateView();
    }

    public void Start(params object[] args)
    {
        Calendar calendar = (Calendar)args[0];
        DateTime date = (DateTime)args[1];
        Project project = (Project)args[2];

        view.TittleBar.TittleText.Text = date.ToShortDateString();

        view.BackButton.Click += (sender ,e) =>
        {
            main.StartUI("calendar", calendar, project);   
        };

            var calendarRepo = Provider.Instance.ProvideCalendarRepository();
            var dict = calendarRepo.SelectCalendarTasks(calendar);
            if(!dict.ContainsKey(date))
            {
                main.StartUI("error", "date does not have any tasks", () =>
                {
                    main.StartUI("calendar", calendar, project);
                });
                return;
            }
            foreach(TodoTask task in dict[date])
            {
                view.Selection.AddSquare(task.Name, (sender ,e) =>
                {
                    main.StartUI("calendar_task", task, task.Board, project, calendar, date);
                });
            }
        try
        {

        }
        catch(Exception ex)
        {
            main.StartUI("error", $"falied to load tasks: {ex.Message}", () => main.StartUI("calendar", calendar, project));
            return;
        }

        main.Present(view);
    }
}