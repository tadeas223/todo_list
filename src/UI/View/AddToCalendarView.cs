namespace UI.View;

using Avalonia;
using Avalonia.Layout;
using Avalonia.Controls;
using UI.Components;

public class AddToCalendarView : UserControl
{
    public TittleBarComponent TittleBar {get; private set;}
    public Button BackButton {get; private set;}
    public TextBlock SelectedCalendar {get; private set;}
    public SelectionComponent CalendarSelection {get; private set;}
    public Button AddButton {get; private set;}
    public Grid SelectionGrid {get; private set;}
    public Calendar Calendar;

    public AddToCalendarView()
    {
        BackButton = new Button
        {
            Content = "back",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };

        TittleBar = new TittleBarComponent("", BackButton);

        Calendar = new Calendar
        {
            SelectionMode = CalendarSelectionMode.SingleDate
        };

        AddButton = new Button
        {
            Content = "add"
        };

        CalendarSelection = new SelectionComponent();

        SelectionGrid = new Grid();
        SelectionGrid.ColumnDefinitions = new ColumnDefinitions("1*,1*");
        
        Grid.SetColumn(CalendarSelection, 0);
        Grid.SetColumn(Calendar, 1);

        SelectionGrid.Children.Add(CalendarSelection);
        SelectionGrid.Children.Add(Calendar);

        SelectedCalendar = new TextBlock
        {
            Text = "(no calendar selected)"
        };

        Content = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 10,
            VerticalAlignment = VerticalAlignment.Top,
            Children =
            {
                TittleBar,
                SelectionGrid,
                SelectedCalendar,
                AddButton 
            }
        };
    }
}