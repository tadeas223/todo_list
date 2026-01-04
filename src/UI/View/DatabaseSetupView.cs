namespace UI.View;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;


public class DatabaseSetupView: UserControl
{

    public TextBlock TitleText { get; private set; }
    public TextBlock UrlLabel { get; private set; }
    public TextBox UrlField { get; private set; }
    public TextBlock SysUsernameLabel { get; private set; }
    public TextBox SysUsernameField { get; private set; }
    public TextBlock SysPasswordLabel { get; private set; }
    public TextBox SysPasswordField { get; private set; }
    public TextBlock UsernameLabel { get; private set; }
    public TextBox UsernameField { get; private set; }
    public TextBlock PasswordLabel { get; private set; }
    public TextBox PasswordField { get; private set; }
    public Button LoginButton { get; private set; }
    public Button CreateButton { get; private set; }
    public Button DeleteButton { get; private set; }
    public TextBlock NoteText { get; private set; }
    public TextBlock Note2Text { get; private set; }

    public DatabaseSetupView()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;

        TitleText = new TextBlock 
        { 
            Text = "database setup",
            FontSize=34, 
            Margin = new Thickness(10)
        };
        
        NoteText = new TextBlock
        {
            Text = "system admin credentials"
        };

        UrlLabel = new TextBlock { Text = "url:" };
        UrlField = new TextBox { Watermark = "url" };
        
        SysUsernameLabel = new TextBlock { Text = "system username:" };
        SysUsernameField = new TextBox { Watermark = "system username" };
        
        SysPasswordLabel = new TextBlock { Text = "system password:" };
        SysPasswordField = new TextBox 
        { 
            Watermark = "system password",
            PasswordChar = '*'
        };
        
        Note2Text = new TextBlock
        {
            Text = "new database credentials"
        };
        
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
            Content = "create database",
        };
        
        DeleteButton = new Button 
        {
            Content = "delete database",
        };

        Content = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 10,
            VerticalAlignment = VerticalAlignment.Top,
            Children =
            {
                TitleText,
                NoteText,
                UrlLabel,
                UrlField,
                SysUsernameLabel,
                SysUsernameField,
                SysPasswordLabel,
                SysPasswordField,
                Note2Text,
                UsernameLabel,
                UsernameField,
                PasswordLabel,
                PasswordField,
                LoginButton,
                CreateButton,
                DeleteButton,
            }
        };
    }
}