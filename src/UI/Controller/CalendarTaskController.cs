namespace UI.Controller;

using UI.View;
using Domain.Model;
using DI;

public class CalendarTaskController : IController
{
    private MainWindow main;
    private TaskView view;
    public CalendarTaskController(MainWindow main)
    {
        this.main = main;
        view = new TaskView();
    }

    public void Start(params object[] args)
    {
        TodoTask task = (TodoTask) args[0];
        Board board = (Board) args[1];
        Project project = (Project) args[2];
        Calendar calendar = (Calendar) args[3];
        DateTime date = (DateTime) args[4];

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
            main.StartUI("calendar_date", calendar, date, project);
        };

        view.DeleteButton.Click += (sender, e) =>
        {
            try
            {
                Provider.Instance.ProvideCalendarRepository().DeleteTask(calendar, task);
                main.StartUI("calendar_date", calendar, date, project);
                return;
            }
            catch(Exception ex)
            {
                main.StartUI("error", $"error while deleting task: {ex.Message}", () => main.StartUI("kanban", board, project));
                return;
            }
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
                main.StartUI("error", $"error while updating task: {ex.Message}", () => main.StartUI("calendar_date", calendar, date, project));
                return;
            }

            main.StartUI("calendar_date", calendar, date, project);
        };

        main.Present(view);
    }
}