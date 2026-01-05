using UI.View;
using DI;
using Domain.Model;

namespace UI.Controller;

public class AddCalendarController : IController
{
    private MainWindow main;
    private AddCalendarView view;

    public AddCalendarController(MainWindow main)
    {
        this.main = main;
        view = new AddCalendarView();
    }

    public void Start(params object[] args)
    {
        Project project = (Project)args[0];

        view.AddButton.Click += (sender, e) =>
        {
            if(project.Locked)
            {
                main.StartUI("error", $"project is locked", () => main.StartUI("add_calendar", project));
                return;
            }

            var calendarRepo = Provider.Instance.ProvideCalendarRepository();

            string? name = view.NameField.Text;

            if(name == null)
            {
                main.StartUI("error", "missing fields", () => main.StartUI("add_calendar", project));
                return;
            }

            Calendar calendar = new CalendarBuilder()
                .WithName(name)
                .WithProject(project)
                .Build();

            calendarRepo.Insert(ref calendar);

            main.StartUI("project", project);
        };

        main.Present(view);
    }

}