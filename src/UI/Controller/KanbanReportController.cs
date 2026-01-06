namespace UI.Controller;

using DI;
using Domain.Model;
using UI.View;

public class KanbanReportController : IController
{
    private MainWindow main;
    private KanbanReportView view;

    public KanbanReportController(MainWindow main)
    {
        this.main = main;
        view = new KanbanReportView();
    }

    public void Start(params object[] args)
    {
        view.BackButton.Click += (sender, e) =>
        {
            main.StartUI("project_selection");
        };

        try
        {
            var kanbanGen = Provider.Instance.ProvideKanbanReportGen();

            List<KanbanReport> reports = kanbanGen.Generate();

            foreach (var report in reports)
            {
                view.AddRow(report);
            }
        }
        catch (Exception ex)
        {
            main.StartUI(
                "error",
                $"Failed to generate kanban report: {ex.Message}",
                () => main.StartUI("project_selection")
            );
        }

        main.Present(view);
    }
}
