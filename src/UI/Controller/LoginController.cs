namespace UI.Controller;

using UI.View;
using DI;

using Domain.Model;
using Avalonia.Interactivity;
using Avalonia.Controls;

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
        Configuration? config = null; 
        try
        {
            config = Provider.Instance
                .ProvideConfigurationRepository("config.ini")
                .Load();
        }
        catch(Exception) {}


        if(config != null)
        {
            view.UsernameField.Text = config.Username;
            view.PasswordField.Text = config.Password;
            view.UrlField.Text = config.Url;
        }

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

        main.Present(view);
    }


}