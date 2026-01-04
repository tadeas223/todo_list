namespace UI.View;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

public class AddProjectView : UserControl
{
    public TextBlock TitleText { get; private set; }
    public TextBlock NameLabel { get; private set; }
    public TextBox NameField { get; private set; }
    public Button AddButton { get; private set; }
    
    public AddProjectView()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;

        TitleText = new TextBlock 
        { 
            Text = "database login",
            FontSize=34, 
            Margin = new Thickness(10)
        };

        NameLabel = new TextBlock { Text = "project name:" };
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
                TitleText,
                NameLabel,
                NameField,
                AddButton,
            }
        };
        
    }

}