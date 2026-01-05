namespace UI.Controller;

using Domain.Model;
using UI.View;
using DI;
using Cairo;

public class AddBoardController : IController
{
    private MainWindow main;
    private AddBoardView view;

    public AddBoardController(MainWindow main)
    {
        this.main = main;
        view = new AddBoardView();
    }

    public void Start(params object[] args)
    {
        Project project = (Project) args[0];

        view.AddButton.Click += (sender, e) => {
            if(project.Locked)
            {
                main.StartUI("error", $"project is locked", () => main.StartUI("add_baord", project));
                return;
            }

            var boardRepo = Provider.Instance.ProvideBoardRepository();

            string? name = view.NameField.Text;
            if(name == null)
            {
                main.StartUI("error", "missing fields", () => main.StartUI("add_board", project));
                return;
            }

            try
            {
                var board = new BoardBuilder().WithName(name!).WithProject(project).Build();
                boardRepo.Insert(ref board);
            }
            catch(Exception ex)
            {
                main.StartUI("error", $"error while adding new board: {ex.Message}", () => main.StartUI("project", project));
                return;
            }

            main.StartUI("project", args[0]);
        };

        view.BackButton.Click += (sender, e) =>
        {
            main.StartUI("project", args[0]);
        };

        main.Present(view);
    }
}