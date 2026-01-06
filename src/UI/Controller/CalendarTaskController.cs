namespace UI.Controller;

using UI.View;
using Domain.Model;
using DI;

public class CalendarTaskController : IController
{
    private MainWindow main;
    private CalendarTaskView view;
    public CalendarTaskController(MainWindow main)
    {
        this.main = main;
        view = new CalendarTaskView();
    }

    public void Start(params object[] args)
    {
        TodoTask task = (TodoTask) args[0];
        Calendar calendar = (Calendar) args[1];
        DateTime date = (DateTime) args[2];

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
            main.StartUI("calendar_date", calendar, date);
        };

        view.DeleteButton.Click += (sender, e) =>
        {
            if(calendar.Project.Locked)
            {
                main.StartUI("error", $"project is locked", () => main.StartUI("calendar_task", task, calendar));
                return;
            }

            try
            {
                Provider.Instance.ProvideCalendarRepository().DeleteTask(calendar, task);
                main.StartUI("calendar_date", calendar, date);
                return;
            }
            catch(Exception ex)
            {
                main.StartUI("error", $"error while deleting task: {ex.Message}", () => main.StartUI("kanban", task.Board));
                return;
            }
        };

        view.UpdateButton.Click += (sender, e) =>
        {
            if(calendar.Project.Locked)
            {
                main.StartUI("error", $"project is locked", () => main.StartUI("calendar_task", task, calendar));
                return;
            }

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
                main.StartUI("error", $"error while updating task: {ex.Message}", () => main.StartUI("calendar_date", calendar, date));
                return;
            }

            main.StartUI("calendar_date", calendar, date);
        };

        main.Present(view);
    }
}