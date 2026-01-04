namespace UI.Controller;

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
        Project project= (Project)args[2];
        view.TittleBar.TittleText.Text = calendar.Name;

        view.BackButton.Click += (sender ,e) =>
        {
            main.StartUI("calendar", calendar, project);   
        };

        main.Present(view);
    }
}