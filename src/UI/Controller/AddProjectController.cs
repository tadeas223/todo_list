namespace UI.Controller;

using Domain.Model;
using UI.View;
using DI;

public class AddProjectController : IController
{
    private MainWindow main;
    private AddProjectView view;
    private List<string> boards = new();

    public AddProjectController(MainWindow main)
    {
        this.main = main;
        view = new AddProjectView();
    }

    public void Start(params object[] args)
    {
        view.AddButton.Click += (sender, e) => {
            var projRepo = Provider.Instance.ProvideProjectRepository();

            string? name = view.NameField.Text;
            if(name == null)
            {
                main.StartUI("error", "missing fields", () => main.StartUI("add_project"));
                return;
            }

            try
            {
                var proj = new ProjectBuilder().WithName(name!).WithLocked(false).Build();
                projRepo.Insert(ref proj);
            }
            catch(Exception ex)
            {
                main.StartUI("error", $"error while adding new project: {ex.Message}", () => main.StartUI("project_selection"));
                return;
            }

            main.StartUI("project_selection");
        };

        view.BackButton.Click += (sender, e) =>
        {
            main.StartUI("project_selection");
        };

        view.AddBoardButton.Click += (sender ,e) =>
        {
            if(view.BoardField.Text == null) return;
            boards.Add(view.BoardField.Text!);

            view.SetBoardList(boards.ToArray(), (index) => 
            {
                boards.RemoveAt(index);
            });
        };

        main.Present(view);
    }
}