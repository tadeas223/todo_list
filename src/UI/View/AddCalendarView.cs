namespace UI.View;

using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia;
using UI.Components;

public class AddCalendarView : UserControl
{
    public TittleBarComponent TittleBar { get; private set; }
    public Button BackButton { get; private set; } 
    public TextBlock NameLabel { get; private set; }
    public TextBox NameField { get; private set; }
    public Button AddButton { get; private set; }
    
    public AddCalendarView()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;

        BackButton = new Button
        {
            Content = "back",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };

        TittleBar = new TittleBarComponent("add calendar", BackButton);

        NameLabel = new TextBlock { Text = "calendar name:" };
        NameField = new TextBox { Watermark = "name" };
        
        AddButton = new Button 
        {
            Content = "add",
        };

        Content = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 10,
            VerticalAlignment = VerticalAlignment.Top,
            Children =
            {
                TittleBar,
                NameLabel,
                NameField,
                AddButton,
            }
        };
        
    }
}