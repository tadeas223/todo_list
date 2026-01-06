namespace UI.View;

using Avalonia.Collections;
using Avalonia;
using Avalonia.Layout;
using Avalonia.Controls;
using UI.Components;
using Avalonia.VisualTree;
using Domain.Model;

public class KanbanReportView : UserControl
{
    private int row = 1;
    public TittleBarComponent TittleBar { get; private set; }
    public AvaloniaList<string> Rows { get; private set; }
    public Grid RowGrid { get; private set; }
    public Button BackButton { get; private set; }

    public KanbanReportView()
    {
        BackButton = new Button
        {
            Content = "back",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0, 0, 5, 0)
        };

        TittleBar = new TittleBarComponent("kanban report", BackButton);

        Rows = new AvaloniaList<string>();

        // Grid now has 6 columns for Kanban states
        RowGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("*,*,*,*,*,*"),
            RowDefinitions = new RowDefinitions("Auto")
        };

        // Header
        var projectBlock = new TextBlock { Text = "Project" };
        var boardBlock = new TextBlock { Text = "Board" };
        var todoBlock = new TextBlock { Text = "Todo" };
        var doingBlock = new TextBlock { Text = "Doing" };
        var doneBlock = new TextBlock { Text = "Done" };
        var backlogBlock = new TextBlock { Text = "Backlog" };

        Grid.SetRow(projectBlock, 0);
        Grid.SetRow(boardBlock, 0);
        Grid.SetRow(todoBlock, 0);
        Grid.SetRow(doingBlock, 0);
        Grid.SetRow(doneBlock, 0);
        Grid.SetRow(backlogBlock, 0);

        Grid.SetColumn(projectBlock, 0);
        Grid.SetColumn(boardBlock, 1);
        Grid.SetColumn(todoBlock, 2);
        Grid.SetColumn(doingBlock, 3);
        Grid.SetColumn(doneBlock, 4);
        Grid.SetColumn(backlogBlock, 5);

        RowGrid.Children.Add(projectBlock);
        RowGrid.Children.Add(boardBlock);
        RowGrid.Children.Add(todoBlock);
        RowGrid.Children.Add(doingBlock);
        RowGrid.Children.Add(doneBlock);
        RowGrid.Children.Add(backlogBlock);

        Content = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 10,
            VerticalAlignment = VerticalAlignment.Top,
            Children =
            {
                TittleBar,
                new ScrollViewer
                {
                    Content = RowGrid
                }
            }
        };
    }

    public void AddRow(KanbanReport report)
    {
        RowGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));

        var projectBlock = new TextBlock { Text = report.ProjectName };
        var boardBlock = new TextBlock { Text = report.BoardName };
        var todoBlock = new TextBlock { Text = report.TodoCount.ToString() };
        var doingBlock = new TextBlock { Text = report.DoingCount.ToString() };
        var doneBlock = new TextBlock { Text = report.DoneCount.ToString() };
        var backlogBlock = new TextBlock { Text = report.BacklogCount.ToString() };

        Grid.SetRow(projectBlock, row);
        Grid.SetRow(boardBlock, row);
        Grid.SetRow(todoBlock, row);
        Grid.SetRow(doingBlock, row);
        Grid.SetRow(doneBlock, row);
        Grid.SetRow(backlogBlock, row);

        Grid.SetColumn(projectBlock, 0);
        Grid.SetColumn(boardBlock, 1);
        Grid.SetColumn(todoBlock, 2);
        Grid.SetColumn(doingBlock, 3);
        Grid.SetColumn(doneBlock, 4);
        Grid.SetColumn(backlogBlock, 5);

        RowGrid.Children.Add(projectBlock);
        RowGrid.Children.Add(boardBlock);
        RowGrid.Children.Add(todoBlock);
        RowGrid.Children.Add(doingBlock);
        RowGrid.Children.Add(doneBlock);
        RowGrid.Children.Add(backlogBlock);

        row++;
    }
}
