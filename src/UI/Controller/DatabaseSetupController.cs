using UI.View;
using DI;

namespace UI.Controller;

public class DatabaseSetupController : IController
{
    private MainWindow main;
    private DatabaseSetupView view;

    public DatabaseSetupController(MainWindow main)
    {
        this.main = main;
        this.view = new DatabaseSetupView();
    }

    public void Start(params object[] args)
    {
        view.CreateButton.Click += (sender, e) =>
        {
            string? url = view.UrlField.Text;
            string? sysUsername = view.SysUsernameField.Text;
            string? sysPassword = view.SysPasswordField.Text;
            string? username = view.UsernameField.Text;
            string? password = view.PasswordField.Text;

            if(url == null
                || sysUsername == null
                || sysPassword == null
                || username == null
                || password == null)
            {
                main.StartUI("error", "missing fields", () => main.StartUI("database_setup"));
                return;
            }

            try
            {
                Provider.Instance.ProvideDBConnection().Disconnect();
                Provider.Instance.ProvideDBConnection().Create(sysUsername!, sysPassword!, url!, username!, password!);
            } 
            catch(Exception ex)
            {
                main.StartUI("error", ex.Message, () => main.StartUI("database_setup"));
                return;
            }

            main.StartUI("login");
        };

        view.DeleteButton.Click += (sender, e) =>
        {
            string? url = view.UrlField.Text;
            string? sysUsername = view.SysUsernameField.Text;
            string? sysPassword = view.SysPasswordField.Text;
            string? username = view.UsernameField.Text;

            if(url == null
                || sysUsername == null
                || sysPassword == null
                || username == null
                )
            {
                main.StartUI("error", "missing fields", () => main.StartUI("database_setup"));
                return;
            }

            try
            {
                Provider.Instance.ProvideDBConnection().Disconnect();
                Provider.Instance.ProvideDBConnection().Delete(sysUsername!, sysPassword!, url!, username!);
            } 
            catch(Exception ex)
            {
                main.StartUI("error", ex.Message, () => main.StartUI("database_setup"));
                return;
            }

            main.StartUI("login");
        };

        view.LoginButton.Click += (sender, e) => main.StartUI("login");

        main.Content = view;
    }
}