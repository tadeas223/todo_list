namespace UI.Controller;

using UI.View;
using DI;

public class LoginController : IController
{
    private LoginView view;
    private MainWindow main;

    public LoginController(MainWindow main)
    {
        this.main = main;
        view = new LoginView();
    }

    public void Start(params object[] args)
    {
        view.LoginButton.Click += (sender, e) =>
        {
            string? url = view.UrlField.Text;
            string? username = view.UsernameField.Text;
            string? password = view.PasswordField.Text;

            if(url == null 
            || username == null 
            || password == null)
            {
                main.StartUI("error", "missing fields", () => {
                    main.StartUI("login");
                });
            }

            try
            {
                Provider.Instance.ProvideDBConnection().Connect(username!, password!, url!);

                main.StartUI("project_selection");
            }
            catch(Exception ex)
            {
                main.StartUI("error", ex.Message, () => {
                    main.StartUI("login");
                });
            }
            
        };

        main.Content = view;
    }


}