namespace UI.View;

using UI.Components;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

public class ProjectView : UserControl
{
    public TittleBarComponent TittleBar { get; private set; }
    public Button DeleteProjectButton {get; private set;}
    public Button AddBoardButton {get; private set;}
    public Button ImportBoardButton {get; private set;}
    public Button AddCalendarButton {get; private set;}

    public CheckBox LockedCheckBox {get; private set;}
    public Button BackButton {get; private set;}
    
    public SelectionComponent BoardSelection {get; private set;}
    public SelectionComponent CalendarSelection {get; private set;}

    public StackPanel BoardPanel {get; private set;}
    public StackPanel CalendarPanel {get; private set;}
    
    public TextBlock BoardLabel {get; private set;}
    public TextBlock CalendarLabel {get; private set;}
    
    public Grid SelectionGrid {get; private set;}

    public ProjectView()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;

        BoardLabel = new TextBlock { Text = "boards: " };
        CalendarLabel = new TextBlock { Text = "calendars: " };
        
        LockedCheckBox = new CheckBox
        {
            Content = "lock",
            IsChecked = false
        };

        SelectionGrid = new Grid();
        SelectionGrid.ColumnDefinitions = new ColumnDefinitions("1*,1*");

        BoardSelection = new SelectionComponent();
        CalendarSelection = new SelectionComponent();

        BoardPanel = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 10,
            VerticalAlignment = VerticalAlignment.Top,
            Children =
            {
                BoardLabel,
                BoardSelection
            }
        };
        
        CalendarPanel = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 10,
            VerticalAlignment = VerticalAlignment.Top,
            Children =
            {
                CalendarLabel,
                CalendarSelection
            }
        };

        Grid.SetColumn(BoardPanel, 0);
        Grid.SetColumn(CalendarPanel, 1);

        SelectionGrid.Children.Add(BoardPanel);
        SelectionGrid.Children.Add(CalendarPanel);
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
        
        ImportBoardButton = new Button
        {
            Content = "import boards",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0, 0, 5, 0)
        };
        
        AddCalendarButton = new Button
        {
            Content = "add calendar",
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

        TittleBar = new TittleBarComponent("", LockedCheckBox, ImportBoardButton, AddBoardButton, AddCalendarButton, DeleteProjectButton, BackButton);

        Content = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 10,
            VerticalAlignment = VerticalAlignment.Top,
            Children =
            {
                TittleBar,
                SelectionGrid
            }
        };
    }
}
