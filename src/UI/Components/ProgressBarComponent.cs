namespace UI.Components;

using Avalonia.Layout;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using System;

public class ProgressBarComponent : StackPanel
{
    private double _value;
    private readonly Ellipse[] _circles = new Ellipse[5];

    public event Action<double>? ValueChanged;

    public double Value
    {
        get => _value;
        set
        {
            // Wrap around from 5 -> 0
            double newValue = value > 5 ? 0 : value;
            if (Math.Abs(_value - newValue) > 0.001)
            {
                _value = newValue;
                UpdateCircles();
                ValueChanged?.Invoke(_value);
            }
        }
    }

    public ProgressBarComponent()
    {
        Orientation = Orientation.Horizontal;
        Spacing = 10;

        // Create 5 circles
        for (int i = 0; i < 5; i++)
        {
            var circle = new Ellipse
            {
                Width = 30,
                Height = 30,
                Fill = Brushes.Gray,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            _circles[i] = circle;
            Children.Add(circle);
        }

        UpdateCircles();

        // Click handler
        AddHandler(InputElement.PointerPressedEvent, OnClick);
    }

    private void OnClick(object? sender, PointerPressedEventArgs e)
    {
        Value += 0.5; // increment by half step
    }

    private void UpdateCircles()
    {
        for (int i = 0; i < 5; i++)
        {
            double diff = _value - i;

            if (diff >= 1) // full
            {
                _circles[i].Fill = new SolidColorBrush(Color.FromRgb(128, 255, 128));
            }
            else if (diff >= 0.5) // half
            {
                _circles[i].Fill = Brushes.Green;
            }
            else // empty
            {
                _circles[i].Fill = Brushes.Gray;
            }
        }
    }
}


        