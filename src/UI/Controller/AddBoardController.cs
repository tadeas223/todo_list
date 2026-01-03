namespace UI.Controller;

using Domain.Model;
using UI.View;
using DI;

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
        view.AddButton.Click += (sender, e) => {
            var boardRepo = Provider.Instance.ProvideBoardRepository();

            string? name = view.NameField.Text;
            if(name == null)
            {
                main.StartUI("error", "missing fields", () => main.StartUI("add_project"));
                return;
            }

            try
            {
                var board = new BoardBuilder().WithName(name!).WithProject((Project)args[0]).Build();
                boardRepo.Insert(ref board);
            }
            catch(Exception ex)
            {
                main.StartUI("error", $"error while adding new board: {ex.Message}", () => main.StartUI("project", (Project)args[0]));
                return;
            }

            main.StartUI("project", args[0]);
        };

        main.Content = view;
    }
}