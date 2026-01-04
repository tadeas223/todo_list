namespace UI.Controller;

using UI.View;
using Domain.Model;
using DI;

public class TaskController : IController
{
    private MainWindow main;
    private TaskView view;
    public TaskController(MainWindow main)
    {
        this.main = main;
        view = new TaskView();
    }

    public void Start(params object[] args)
    {
        TodoTask task = (TodoTask) args[0];

        view.NameField.Text = task.Name;
        view.DescField.Text = task.Desc ?? "";
        view.StateSelect.SelectedValue = task.State.ToString();
        view.ProgressBar.Value = (double)task.Progress;

        if(task.FinishDate != null)
        {
            view.FinishDateLabel.Text = "DONE at: " + task.FinishDate!.ToString();
        }

        view.BackButton.Click += (sender, e) =>
        {
            main.StartUI("kanban", args[1], args[2]);
        };

        view.DeleteButton.Click += (sender, e) =>
        {
            try
            {
                Provider.Instance.ProvideTodoTaskRepository().Delete(task);
            }
            catch(Exception ex)
            {
                main.StartUI("error", $"error while updating task: {ex.Message}", () => main.StartUI("kanban", args[1], args[2]));
                return;
            }

            main.StartUI("kanban", args[1], args[2]);
        };

        view.UpdateButton.Click += (sender, e) =>
        {
            string? name = view.NameField.Text;
            string? desc = view.DescField.Text;
            TaskState state = (TaskState)Enum.Parse(typeof(TaskState), (string)view.StateSelect.SelectedValue!);
            double progress = view.ProgressBar.Value;

            TodoTask newTask = new TodoTaskBuilder(task)
                .WithName(name!)
                .WithDesc(desc!)
                .WithState(state)
                .WithProgress((float)progress)
                .WithFinishDate((state == TaskState.DONE)? DateTime.Now : null)
                .Build();

            try
            {
                Provider.Instance.ProvideTodoTaskRepository().Update(newTask);
            }
            catch(Exception ex)
            {
                main.StartUI("error", $"error while updating task: {ex.Message}", () => main.StartUI("kanban", args[1], args[2]));
                return;
            }

            main.StartUI("kanban", args[1], args[2]);
        };

        main.Present(view);
    }
}