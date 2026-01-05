using UI.View;
using DI;
using Domain.Model;

namespace UI.Controller;

public class KanbanController : IController
{
    private MainWindow main;
    private KanbanView view;
    
    public KanbanController(MainWindow main)
    {
        this.main = main;
        view = new KanbanView();
    }

    public void Start(params object[] args)
    {
        Board board = (Board)args[0];

        var taskRepo = Provider.Instance.ProvideTodoTaskRepository();
        HashSet<TodoTask> todoTasks = new HashSet<TodoTask>();
        try
        {
            todoTasks = taskRepo.SelectByBoard(board);
        }
        catch(Exception ex)
        {
            main.StartUI("error", $"failed while fetching tasks: {ex.Message}", () => main.StartUI("board", board));
            return;
        }

        foreach(TodoTask task in todoTasks)
        {
            view.AddTask(task, (sender ,e) =>
            {
                main.StartUI("task", task, board);
            });
        }

        view.TittleBar.TittleText.Text = board.Name;

        view.BackButton.Click += (sender, e) => main.StartUI("project", board.Project);
        view.DeleteBoardButton.Click += (sender ,e) =>
        {
            if(board.Project.Locked)
            {
                main.StartUI("error", $"project is locked", () => main.StartUI("board", board));
                return;
            }

            try
            {
                Provider.Instance.ProvideBoardRepository().Delete(board);
                main.StartUI("project", board.Project);
            }
            catch(Exception ex)
            {
                main.StartUI("error", $"failed to delete board: {ex.Message}", () => main.StartUI("board", board));
            }
        };

        view.AddTaskButton.Click += (sender, e) =>
        {
            if(board.Project.Locked)
            {
                main.StartUI("error", $"project is locked", () => main.StartUI("board", board));
            }

            main.StartUI("add_task", board);
        };

        main.Present(view);
    }
}