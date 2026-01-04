namespace UI.Components;

using Avalonia.Controls;
using Avalonia;

public class TittleBarComponent: UserControl
{
    public TextBlock TittleText {get; private set;}
    public DockPanel RootDock {get; private set;}
    public Grid TopBar {get; private set;}

    public TittleBarComponent(string tittle, params Button[] buttons)
    {
        TittleText = new TextBlock 
        { 
            Text = tittle,
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

        string definitions = "*";
        foreach(var btn in buttons)
        {
            definitions += ",Auto";
        }

        TopBar.ColumnDefinitions = new ColumnDefinitions(definitions);

        Grid.SetColumn(TittleText, 0);
        
        for(int i = 0; i < buttons.Length; i++)
        {
            Grid.SetColumn(buttons[i], i+1);
        }
        
        TopBar.Children.Add(TittleText);
        foreach(var btn in buttons)
        {
            TopBar.Children.Add(btn);
        }

        DockPanel.SetDock(TopBar, Dock.Top);
        RootDock.Children.Add(TopBar);

        Content = RootDock;
    }
}