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

        view.BackButton.Click += (sender, e) => main.StartUI("project_selection");
        view.DeleteProjectButton.Click += (sender, e) =>
        {
            Provider.Instance.ProvideProjectRepository().Delete(proj);
            main.StartUI("project_selection");
        };
        
        view.AddBoardButton.Click += (sender, e) =>
        {
            main.StartUI("project_selection");
        };
        
        try
        {
            var boardRepo = Provider.Instance.ProvideBoardRepository();
            foreach(var board in boardRepo.SelectAll())
            {
                view.Selection.AddProjectSquare(board.Name, (sender, e) =>
                {
                    main.StartUI("error", "not implemented", () => {
                        main.StartUI("project_selection");
                    });
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

        main.Content = view;
    }
}