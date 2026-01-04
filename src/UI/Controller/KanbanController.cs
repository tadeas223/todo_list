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
            main.StartUI("error", $"failed while fetching tasks: {ex.Message}", () => main.StartUI("board", args[0]));
            return;
        }

        foreach(TodoTask task in todoTasks)
        {
            view.AddTask(task, (sender ,e) =>
            {
                main.StartUI("error", "not implemented :(", () => main.StartUI("kanban", args[0], args[1]));
            });
        }

        view.TittleBar.TittleText.Text = board.Name;

        view.BackButton.Click += (sender, e) => main.StartUI("project", args[1]);
        view.DeleteBoardButton.Click += (sender ,e) =>
        {
            try
            {
                Provider.Instance.ProvideBoardRepository().Delete(board);
                main.StartUI("project", args[1]);
            }
            catch(Exception ex)
            {
                main.StartUI("error", $"failed to delete board: {ex.Message}", () => main.StartUI("board", args));
            }
        };

        view.AddTaskButton.Click += (sender, e) =>
        {
            main.StartUI("add_task", board, args[1]);
        };

        main.Present(view);
    }
}