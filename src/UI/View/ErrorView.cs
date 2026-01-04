using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace UI.View;
public class ErrorView : UserControl
{
    public TextBlock TitleText {get; private set;}
    public TextBlock ErrorMessage {get; private set;}
    public Button BackButton {get; private set;}

    public ErrorView()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        
        TitleText = new TextBlock 
        { 
            Text = "error",
            FontSize=34, 
            Margin = new Thickness(10)
        };

        ErrorMessage = new TextBlock
        {
            Text = "",
        };

        BackButton = new Button
        {
            Content = "back",
        };
        
        Content = new StackPanel
        {
            Margin = new Avalonia.Thickness(20),
            Spacing = 10,
            VerticalAlignment = VerticalAlignment.Top,
            Children =
            {
                ErrorMessage,
                BackButton
            }
        };
    }

}