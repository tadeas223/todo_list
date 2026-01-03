namespace UI.View;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Domain.Model;

public class ProjectSelectionView : UserControl
{
    public TextBlock TitleText {get; private set;}
    public Button AddButton {get; private set;}
    public Button LogoutButton {get; private set;}
    public DockPanel RootDock {get; private set;}
    public Grid TopBar {get; private set;}
    public WrapPanel SquarePanel {get; private set;}

    public ProjectSelectionView()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;
        
        TitleText = new TextBlock 
        { 
            Text = "projects",
            FontSize=34, 
            Margin = new Thickness(10)
        };

        RootDock = new DockPanel
        {
            LastChildFill = true
        };

        TopBar = new Grid
        {
            Height = 40,
            Margin = new Thickness(10)
        };

        TopBar.ColumnDefinitions = new ColumnDefinitions("*,Auto,Auto");
        Grid.SetColumn(TitleText, 0);


        AddButton = new Button
        {
            Content = "add",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };
        Grid.SetColumn(AddButton, 1);

        LogoutButton = new Button
        {
            Content = "logout",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center
        };
        Grid.SetColumn(LogoutButton, 2);

        TopBar.Children.Add(TitleText);
        TopBar.Children.Add(AddButton);
        TopBar.Children.Add(LogoutButton);

        DockPanel.SetDock(TopBar, Dock.Top);
        RootDock.Children.Add(TopBar);
        
        SquarePanel = new WrapPanel
        {
            Margin = new Thickness(10),
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            ItemWidth = 100,
            ItemHeight = 100,
        };

        RootDock.Children.Add(SquarePanel);

        Content = RootDock;
    }

    public void AddProjectSquare(Project project, 
        EventHandler<Avalonia.Interactivity.RoutedEventArgs> onClick)
    {
        var btn = new Button
        {
            Content = project.Name,
            Width = 100,
            Height = 100,
            Background = Brushes.DarkBlue
        };

        btn.Click += onClick;

        SquarePanel.Children.Add(btn);
    }

    public void ResetSquares()
    {
        SquarePanel.Children.Clear();
    }
}