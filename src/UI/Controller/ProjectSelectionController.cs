using UI.View;
using DI;

namespace UI.Controller;

public class ProjectSelectionController : IController
{
    private ProjectSelectionView view;
    private MainWindow main;

    public ProjectSelectionController(MainWindow main)
    {
        this.main = main;
        view = new ProjectSelectionView();
    }

    public void Start(params object[] args)
    {
        try
        {
            var projRepo = Provider.Instance.ProvideProjectRepository();
            foreach(var project in projRepo.SelectAll())
            {
                view.AddProjectSquare(project, (sender, e) =>
                {
                    main.StartUI("error", "not implemented :(", () => main.StartUI("project_selection"));
                    return;
                });
            }
        }
        catch
        {
            main.StartUI("error", $"failed to load projects, propably a database error", () => main.StartUI("login"));
            return;
        }

        view.LogoutButton.Click += (sender, e) =>
        {
            Provider.Instance.ProvideDBConnection().Disconnect();
            main.StartUI("login");
            return;
        };
        
        view.AddButton.Click += (sender, e) =>
        {
            Provider.Instance.ProvideDBConnection().Disconnect();
            main.StartUI("error", "not implemented :(", () => main.StartUI("project_selection"));
            return;
        };

        main.Content = view;
    }
}