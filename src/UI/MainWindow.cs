namespace UI;

using Avalonia.Controls;
using Microsoft.CSharp.RuntimeBinder;
using UI.Controller;

public class MainWindow : Window
{
    private ContentControl content;

    public MainWindow()
    {
        content = new ContentControl();
        Content = content;

        StartUI("login");
    }

    public void StartUI(string name, params object[] args)
    {
        IController? controller = null;
        switch(name)
        {
            case "login":
                controller = new LoginController(this);
                break;
            case "error":
                controller = new ErrorController(this);
                break;
            case "database_setup":
                controller = new DatabaseSetupController(this);
                break;
            case "project_selection":
                controller = new ProjectSelectionController(this);
                break;
        }

        if(controller == null)
        {
            throw new Exception($"invalid ui name {name}");            
        }


        controller.Start(args);
    }
}