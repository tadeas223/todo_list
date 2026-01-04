namespace UI.View;

using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia;
using UI.Components;

public class CalendarView : UserControl
{
    public Button BackButton {get; private set;}
    public Button DeleteButton {get; private set;}
    public TittleBarComponent TittleBar {get; private set;}

    public Calendar Calendar {get; private set;}

    public CalendarView()
    {
        BackButton = new Button
        {
            Content = "back",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };
        
        DeleteButton = new Button
        {
            Content = "delete calendar",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };

        TittleBar = new TittleBarComponent("", DeleteButton, BackButton);

        Calendar = new Calendar
        {
            SelectionMode = CalendarSelectionMode.SingleDate
        };

        Content = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 10,
            VerticalAlignment = VerticalAlignment.Top,
            Children =
            {
                TittleBar,
                Calendar
            }
        };
    }
}