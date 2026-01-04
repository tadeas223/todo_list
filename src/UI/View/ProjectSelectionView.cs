namespace UI.View;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Domain.Model;
using Gdk;
using UI.Components;

public class ProjectSelectionView : UserControl
{
    public Button AddButton {get; private set;}
    public Button LogoutButton {get; private set;}
    public TittleBarComponent TittleBar {get; private set;}
    public SelectionComponent Selection {get; private set;}

    public ProjectSelectionView()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;

        AddButton = new Button
        {
            Content = "add",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };
        
        LogoutButton = new Button
        {
            Content = "logout",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center
        };

        TittleBar = new TittleBarComponent("projects",
            AddButton, LogoutButton
        );  

        Selection = new SelectionComponent();

        Content = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 10,
            VerticalAlignment = VerticalAlignment.Top,
            Children =
            {
                TittleBar,
                Selection
            }
        };
    }
}