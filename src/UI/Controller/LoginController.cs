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
                return;
            }

            try
            {
                var con = Provider.Instance.ProvideDBConnection();
                con.Connect(username!, password!, url!);
                if(!con.Connected)
                {
                    main.StartUI("error", "failed to connect", () => {
                        main.StartUI("login");
                    });
                    return;
                }
                main.StartUI("project_selection");
                return;
            }
            catch(Exception ex)
            {
                main.StartUI("error", ex.Message, () => {
                    main.StartUI("login");
                });
                return;
            }
            
        };

        view.DbSetupButton.Click += (sender, e) => main.StartUI("database_setup");

        main.Content = view;
    }


}