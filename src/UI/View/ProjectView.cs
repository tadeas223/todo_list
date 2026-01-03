namespace UI.View;

using UI.Components;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Gdk;

public class ProjectView : UserControl
{
    public TittleBarComponent TittleBar { get; private set; }
    public SelectionComponent Selection {get; private set;}
    public Button DeleteProjectButton {get; private set;}
    public Button AddBoardButton {get; private set;}
    public Button BackButton {get; private set;}

    public ProjectView()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;

        DeleteProjectButton = new Button
        {
            Content = "delete project",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0, 0, 5, 0)
        };
        
        AddBoardButton = new Button
        {
            Content = "add board",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0, 0, 5, 0)
        };

        BackButton = new Button
        {
            Content = "back",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center
        };

        TittleBar = new TittleBarComponent("", AddBoardButton, DeleteProjectButton, BackButton);
        Selection = new SelectionComponent();

        Content = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 10,
            Children =
            {
                TittleBar,
                Selection
            }
        };
    }
}
