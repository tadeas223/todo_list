namespace UI.View;

using UI.Components;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

public class TaskView : UserControl 
{
    public TittleBarComponent TittleBar {get; private set;}
    public Button BackButton {get; private set;}
    public Button DeleteButton {get; private set;}
    public TextBlock NameLabel {get; private set;}
    public TextBox NameField {get; private set;}
    public TextBlock StateSelectLabel {get; private set;}
    public TextBlock DescLabel {get; private set;}
    public TextBox DescField {get; private set;}
    public TextBlock ProgressLabel {get; private set;}
    public ProgressBarComponent ProgressBar {get; private set;}
    public ComboBox StateSelect {get; private set;}
    public Button UpdateButton {get; private set;}

    public TaskView()
    {
        BackButton = new Button
        {
            Content = "back",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };
        
        DeleteButton = new Button
        {
            Content = "delete",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };

        TittleBar = new TittleBarComponent("", DeleteButton, BackButton);

        NameLabel = new TextBlock {Text = "name:" };
        StateSelectLabel = new TextBlock {Text = "state:" };
        DescLabel = new TextBlock { Text = "description:" };
        ProgressLabel = new TextBlock { Text = "progress:" };

        NameField = new TextBox { Watermark = "name" };
        StateSelect = new ComboBox
        {
            ItemsSource = new[]
            {
                "TODO",
                "DOING",
                "DONE",
                "BACKLOG"
            },
            SelectedIndex = 0,
        };

        DescField = new TextBox
        {
            Watermark = "description",
            AcceptsReturn = true,
            AcceptsTab = true,
            TextWrapping = TextWrapping.Wrap,
            HorizontalAlignment = HorizontalAlignment.Left,
            Width = 400,
            Height = 200,
        };

        ProgressBar = new();

        UpdateButton = new Button
        {
            Content = "update",
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
                DescLabel,
                DescField,
                ProgressLabel,
                ProgressBar,
                StateSelectLabel,
                StateSelect,
                UpdateButton
            }
        };
    }
}