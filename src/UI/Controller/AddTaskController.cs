namespace UI.Controller;

using DI;
using UI.View;
using Domain.Model;

public class AddTaskController : IController
{
    private MainWindow main;
    private AddTaskView view;
    
    public AddTaskController(MainWindow main)
    {
        this.main = main;
        view = new AddTaskView();
    }

    public void Start(params object[] args)
    {
        Board board = (Board)args[0];

        view.TittleBar.TittleText.Text = board.Name;
        view.AddButton.Click += (sender, e) =>
        {
            string? name = view.NameField.Text;
            string? desc = view.DescField.Text;
            TaskState state = (TaskState)Enum.Parse(typeof(TaskState), (string)view.StateSelect.SelectedValue!);

            if(name == null)
            {
                main.StartUI("error", "missing fields", () => {
                    main.StartUI("add_task", board);
                });
                return;
            }

            try
            {
                var taskRepo = Provider.Instance.ProvideTodoTaskRepository();
                TodoTask task = new TodoTaskBuilder()
                    .WithBoard(board)
                    .WithDesc(desc ?? "")
                    .WithProgress(0)
                    .WithName(name)
                    .WithState(state)
                    .Build();

                taskRepo.Insert(ref task);

                main.StartUI("kanban", board, args[1]);
                return;
            }
            catch(Exception ex)
            {
                main.StartUI("error", ex.Message, () => {
                    main.StartUI("add_task", board);
                });
                return;
            }
            
        };


        main.Present(view);
    }
}