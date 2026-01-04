namespace UI.View;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Gdk;
using UI.Components;

public class CalendarDateView : UserControl
{
    public Button BackButton {get; private set;}
    public Button AddButton {get; private set;}
    public TittleBarComponent TittleBar {get; private set;}

    public SelectionComponent Selection {get; private set;}

    public CalendarDateView()
    {
        BackButton = new Button
        {
            Content = "back",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };

        AddButton = new Button
        {

            Content = "add task",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };

        TittleBar = new TittleBarComponent("", AddButton, BackButton);

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