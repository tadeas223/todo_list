namespace UI.Controller;

using Domain.Model;
using UI.View;
using DI;

public class ProjectController : IController
{
    private MainWindow main;
    private ProjectView view;
    public ProjectController(MainWindow main)
    {
        this.main = main;
        view = new ProjectView(); 
    }

    public void Start(params object[] args)
    {
        Project proj = (Project)args[0];

        view.TittleBar.TittleText.Text = proj.Name;

        view.LockedCheckBox.IsChecked = proj.Locked;

        view.BackButton.Click += (sender, e) => main.StartUI("project_selection");
        view.DeleteProjectButton.Click += (sender, e) =>
        {
            if(proj.Locked)
            {
                main.StartUI("error", $"project is locked", () => main.StartUI("project", proj));
                return;
            }

            Provider.Instance.ProvideProjectRepository().Delete(proj);
            main.StartUI("project_selection");
        };
        
        view.AddBoardButton.Click += (sender, e) =>
        {
            if(proj.Locked)
            {
                main.StartUI("error", $"project is locked", () => main.StartUI("project", proj));
                return;
            }

            main.StartUI("add_board", proj);
        };
        
        view.AddCalendarButton.Click += (sender, e) =>
        {

            if(proj.Locked)
            {
                main.StartUI("error", $"project is locked", () => main.StartUI("project", proj));
                return;
            }

            main.StartUI("add_calendar", proj);
        };
        
        try
        {
            var boardRepo = Provider.Instance.ProvideBoardRepository();
            foreach(var board in boardRepo.SelectByProject(proj))
            {
                view.BoardSelection.AddSquare(board.Name, (sender, e) =>
                {
                    main.StartUI("kanban", board, proj);
                    return;
                });
            }
        }
        catch(Exception ex)
        {
            main.StartUI("error", ex.Message, () => {
                main.StartUI("login");
            });
            return;
        }

        try
        {
            var calendarRepo = Provider.Instance.ProvideCalendarRepository();
            foreach(var calendar in calendarRepo.SelectByProject(proj))
            {
                view.CalendarSelection.AddSquare(calendar.Name, (sender, e) =>
                {
                    main.StartUI("calendar", calendar, proj);
                    return;
                });
            }
        }
        catch(Exception ex)
        {
            main.StartUI("error", ex.Message, () => {
                main.StartUI("login");
            });
            return;
        }

        view.LockedCheckBox.IsCheckedChanged += (sender ,e) =>
        {
            try
            {
                Project newProject = new ProjectBuilder(proj)
                    .WithLocked(view.LockedCheckBox.IsChecked!.Value)
                    .Build();

                Provider.Instance.ProvideProjectRepository().Update(newProject);
                proj = newProject;
            }
            catch(Exception ex)
            {
                main.StartUI("error", $"failed to update locked: {ex.Message}", () => main.StartUI("project", proj));
            }
        };

        main.Present(view);
    }
}