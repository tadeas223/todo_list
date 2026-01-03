namespace UI.Components;

using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia;
using Avalonia.Media;

public class SelectionComponent : WrapPanel
{

    public SelectionComponent()
    {
        Margin = new Thickness(10);
        Orientation = Orientation.Horizontal;
        HorizontalAlignment = HorizontalAlignment.Left;
        VerticalAlignment = VerticalAlignment.Top;
        ItemWidth = 100;
        ItemHeight = 100;
    }

    public void AddProjectSquare(string name, 
        EventHandler<Avalonia.Interactivity.RoutedEventArgs> onClick)
    {
        var btn = new Button
        {
            Content = name,
            Width = 100,
            Height = 100,
            Background = Brushes.DarkBlue
        };

        btn.Click += onClick;

        Children.Add(btn);
    }

    public void ResetSquares()
    {
        Children.Clear();
    }
}