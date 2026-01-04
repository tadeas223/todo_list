namespace UI.View;

using UI.Components;
using Avalonia.Controls;
using Avalonia;
using Avalonia.Layout;
using Avalonia.Controls.Primitives;
using System.Data.Common;
using Avalonia.Media;

public class AddTaskView : UserControl
{
    public TittleBarComponent TittleBar {get; private set;}
    public Button BackButton {get; private set;}
    public TextBlock NameLabel {get; private set;}
    public TextBox NameField {get; private set;}
    public TextBlock StateSelectLabel {get; private set;}
    public TextBlock DescLabel {get; private set;}
    public TextBox DescField {get; private set;}
    public ComboBox StateSelect {get; private set;}
    public Button AddButton {get; private set;}

    public AddTaskView()
    {
        BackButton = new Button
        {
            Content = "back",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };

        TittleBar = new TittleBarComponent("", BackButton);

        NameLabel = new TextBlock {Text = "name:" };
        StateSelectLabel = new TextBlock {Text = "state:" };
        DescLabel = new TextBlock { Text = "description:" };

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
            HorizontalAlignment = HorizontalAlignment.Right,
            Width = 400,
            Height = 200,
        };

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
                DescLabel,
                DescField,
                StateSelectLabel,
                StateSelect,
                AddButton
            }
        };
    }
}