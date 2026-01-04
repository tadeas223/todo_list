namespace UI.Controller;

using UI.View;

public class ErrorController: IController
{
    private ErrorView view;
    private MainWindow main;

    public ErrorController(MainWindow main)
    {
        this.main = main;
        view = new ErrorView();
    }

    public void Start(params object[] args)
    {

        view.ErrorMessage.Text = (string)args[0];
        view.BackButton.Click += (sender, e) => ((Action)args[1])();

        main.Present(view);
    }
}