using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using Domain.Model;
using System.Collections.ObjectModel;
using UI.Components;

namespace UI.View;

public class KanbanView: UserControl
{
    public TittleBarComponent TittleBar {get; private set;}
    public Button DeleteBoardButton {get; private set;} 
    public Button AddTaskButton {get; private set;} 
    public Button BackButton {get; private set;} 
    public ItemsControl TodoItems { get; private set; }
    public ItemsControl DoingItems { get; private set; }
    public ItemsControl DoneItems { get; private set; }
    public ItemsControl BacklogItems { get; private set; }

    private readonly ObservableCollection<Button> TodoCollection = new();
    private readonly ObservableCollection<Button> DoingCollection = new();
    private readonly ObservableCollection<Button> DoneCollection = new();
    private readonly ObservableCollection<Button> BacklogCollection = new();

    public KanbanView()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;


        BackButton = new Button
        {
            Content = "back",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };
        
        AddTaskButton = new Button
        {
            Content = "add task",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };
        
        DeleteBoardButton = new Button
        {
            Content = "delete board",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };

        TittleBar = new TittleBarComponent("",
            AddTaskButton,
            DeleteBoardButton,
            BackButton);

        var grid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("1*,1*,1*,1*")
        };

        var todoPanel = CreateColumn("TODO", Brushes.LightGray, TodoCollection, out var todoControl);
        TodoItems = todoControl;

        var doingPanel = CreateColumn("DOING", Brushes.LightBlue, DoingCollection, out var doingControl);
        DoingItems = doingControl;

        var donePanel = CreateColumn("DONE", Brushes.LightGreen, DoneCollection, out var doneControl);
        DoneItems = doneControl;

        var backlogPanel = CreateColumn("BACKLOG", Brushes.LightSalmon, BacklogCollection, out var backlogControl);
        BacklogItems = backlogControl;

        Grid.SetColumn(todoPanel, 0);
        Grid.SetColumn(doingPanel, 1);
        Grid.SetColumn(donePanel, 2);
        Grid.SetColumn(backlogPanel, 3);
        
        grid.Children.Add(todoPanel);
        grid.Children.Add(doingPanel);
        grid.Children.Add(donePanel);
        grid.Children.Add(backlogPanel);

        Content = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 10,
            Children =
            {
                TittleBar,
                grid
            }
        };
    }

    private StackPanel CreateColumn(string header, IBrush background, ObservableCollection<Button> collection, out ItemsControl itemsControl)
    {
        var stackPanel = new StackPanel
        {
            Margin = new Thickness(5),
            Background = background
        };

        stackPanel.Children.Add(new TextBlock
        {
            Text = header,
            FontWeight = Avalonia.Media.FontWeight.Bold,
            FontSize = 16,
            Margin = new Thickness(0, 0, 0, 5)
        });

        itemsControl = new ItemsControl
        {
            ItemsSource = collection
        };

        stackPanel.Children.Add(itemsControl);

        return stackPanel;
    }

    public void AddTask(TodoTask task, EventHandler<Avalonia.Interactivity.RoutedEventArgs> onClick)
    {
        var button = new Button
        {
            Content = task.Name,
            Width = 100,
            Height = 100,
            Background = Brushes.DarkBlue,
            Foreground = Brushes.White,
            Margin = new Thickness(2)
        };

        button.Click += onClick;

        switch (task.State)
        {
            case TaskState.TODO:
                TodoCollection.Add(button);
                break;
            case TaskState.DOING:
                DoingCollection.Add(button);
                break;
            case TaskState.DONE:
                DoneCollection.Add(button);
                break;
            case TaskState.BACKLOG:
                BacklogCollection.Add(button);
                break;
        }
    }
}

