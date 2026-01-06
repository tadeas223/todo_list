using UI.View;
using DI;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Data;

namespace UI.Controller;

public class ProjectSelectionController : IController
{
    private ProjectSelectionView view;
    private MainWindow main;

    public ProjectSelectionController(MainWindow main)
    {
        this.main = main;
        view = new ProjectSelectionView();
    }

    public void Start(params object[] args)
    {
        try
        {
            var projRepo = Provider.Instance.ProvideProjectRepository();
            foreach(var project in projRepo.SelectAll())
            {
                view.Selection.AddSquare(project.Name, (sender, e) =>
                {
                    main.StartUI("project", project);
                    return;
                });
            }
        }
        catch
        {
            main.StartUI("error", $"failed to load projects, propably a database error", () => main.StartUI("login"));
            return;
        }

        view.LogoutButton.Click += (sender, e) =>
        {
            Provider.Instance.ProvideDBConnection().Disconnect();
            main.StartUI("login");
            return;
        };
        
        view.AddButton.Click += (sender, e) =>
        {
            main.StartUI("add_project");
            return;
        };

        view.ImportProjectButton.Click += async (sender ,e) =>
        {
            var topLevel = TopLevel.GetTopLevel(view);
            if(topLevel == null) return;

            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "open csv file",
                AllowMultiple = false
            });

            if (files.Count >= 1)
            {
                string? path = files[0].TryGetLocalPath();
                if(path == null) return;

                try
                {
                    CsvProjectDataImport dataImport = new CsvProjectDataImport(path);
                    dataImport.Import();
                } 
                catch(Exception ex)
                {
                    main.StartUI("error", $"failed to import project: {ex.Message}", () => main.StartUI("project_selection"));
                    return;
                }
            }

            main.StartUI("project_selection");
        };

        view.ProgressReportButton.Click += (sender ,e) =>
        {
            main.StartUI("progress_report");
        };

        main.Present(view);
    }
}