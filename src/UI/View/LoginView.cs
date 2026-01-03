namespace UI.View;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;


public class LoginView : UserControl
{

    public TextBlock TitleText { get; private set; }
    public TextBlock UrlLabel { get; private set; }
    public TextBox UrlField { get; private set; }
    public TextBlock UsernameLabel { get; private set; }
    public TextBox UsernameField { get; private set; }
    public TextBlock PasswordLabel { get; private set; }
    public TextBox PasswordField { get; private set; }
    public Button LoginButton { get; private set; }
    public Button CreateButton { get; private set; }
    public Button DeleteButton { get; private set; }
    public TextBlock NoteText { get; private set; }

    public LoginView()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;

        TitleText = new TextBlock 
        { 
            Text = "database login",
            FontSize=34, 
            Margin = new Thickness(10)
        };

        UrlLabel = new TextBlock { Text = "url:" };
        UrlField = new TextBox { Watermark = "url" };
        
        UsernameLabel = new TextBlock { Text = "username:" };
        UsernameField = new TextBox { Watermark = "username" };
        
        PasswordLabel = new TextBlock { Text = "password:" };
        PasswordField = new TextBox 
        { 
            Watermark = "password",
            PasswordChar = '*'
        };

        LoginButton = new Button 
        {
            Content = "login",
        };
        
        CreateButton = new Button 
        {
            Content = "create",
        };
        
        DeleteButton = new Button 
        {
            Content = "delete",
        };

        NoteText = new TextBlock
        {
            Text = "To create/delete a database, put your system admin credentials and use the create/delete buttons"
        };

        Content = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 10,
            Children =
            {
                TitleText,
                UrlLabel,
                UrlField,
                UsernameLabel,
                UsernameField,
                PasswordLabel,
                PasswordField,
                LoginButton,
                CreateButton,
                DeleteButton,
                NoteText
            }
        };
    }
}