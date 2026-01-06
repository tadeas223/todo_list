namespace UI.View;

using Avalonia.Collections;
using Avalonia;
using Avalonia.Layout;
using Avalonia.Controls;
using UI.Components;
using Avalonia.VisualTree;
using Domain.Model;

public class ProgressReportView : UserControl
{
    private int row = 1;
    public TittleBarComponent TittleBar {get; private set;}
    public AvaloniaList<string> Rows {get; private set;}
    public Grid RowGrid {get; private set;}
    public Button BackButton {get; private set;}

    public ProgressReportView()
    {
        BackButton = new Button
        {
            Content = "back",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };

        TittleBar = new TittleBarComponent("progress report", BackButton);

        Rows = new AvaloniaList<string>();

        RowGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("*,*,*,*"),
            RowDefinitions = new RowDefinitions("Auto")
        };

        var projectBlock = new TextBlock { Text = "project" };
        var boardBlock = new TextBlock { Text = "board" };
        var taskBlock = new TextBlock { Text = "task count" };
        var progressBlock = new TextBlock { Text = "progress" };

        Grid.SetRow(projectBlock, 0);
        Grid.SetRow(boardBlock, 0);
        Grid.SetRow(taskBlock, 0);
        Grid.SetRow(progressBlock, 0);
        
        Grid.SetColumn(projectBlock, 0);
        Grid.SetColumn(boardBlock, 1);
        Grid.SetColumn(taskBlock, 2);
        Grid.SetColumn(progressBlock, 3);

        RowGrid.Children.Add(projectBlock);
        RowGrid.Children.Add(boardBlock);
        RowGrid.Children.Add(taskBlock);
        RowGrid.Children.Add(progressBlock);

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

    public void AddRow(TaskProgressReport report)
    {
        RowGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
        var projectBlock = new TextBlock { Text = report.ProjectName };
        var boardBlock = new TextBlock { Text = report.BoardName };
        var taskBlock = new TextBlock { Text = report.TaskCount.ToString() };
        var progressBlock = new TextBlock { Text = report.ProgressAvg.ToString() };

        Grid.SetRow(projectBlock, row);
        Grid.SetRow(boardBlock, row);
        Grid.SetRow(taskBlock, row);
        Grid.SetRow(progressBlock, row);
        
        Grid.SetColumn(projectBlock, 0);
        Grid.SetColumn(boardBlock, 1);
        Grid.SetColumn(taskBlock, 2);
        Grid.SetColumn(progressBlock, 3);

        RowGrid.Children.Add(projectBlock);
        RowGrid.Children.Add(boardBlock);
        RowGrid.Children.Add(taskBlock);
        RowGrid.Children.Add(progressBlock);

        row++;        
    }
}