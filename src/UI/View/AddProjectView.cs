namespace UI.View;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Domain.Model;

using UI.Components;

public class AddProjectView : UserControl
{
    public TittleBarComponent TittleBar { get; private set; }
    public TextBlock NameLabel { get; private set; }
    public TextBox NameField { get; private set; }
    public Button AddButton { get; private set; }
    public Button BackButton { get; private set; }
    
    public TextBlock BoardLabel {get; private set;}
    public TextBox BoardField {get; private set;}
    public StackPanel BoardPanel {get; private set;}
    public Button AddBoardButton {get; private set;}
    
    public AddProjectView()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;

        BoardPanel = new StackPanel();

        BoardLabel = new TextBlock {Text = "board name:" };
        BoardField = new TextBox { Watermark = "name" };
        
        AddBoardButton = new Button
        {
            Content = "add board",
        };

        BackButton = new Button
        {
            Content = "back",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0,0,5,0)
        };

        TittleBar = new TittleBarComponent("add project", BackButton);

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
                TittleBar,
                NameLabel,
                NameField,
                //BoardLabel,
                //BoardField,
                //BoardPanel,
                //AddBoardButton,
                AddButton,
            }
        };
        
    }

    public void SetBoardList(string[] boards, Action<int> onBoardDelete)
    {
        BoardPanel.Children.Clear();

        for(int i = 0; i < boards.Length; i++)
        {
            Grid grid = new Grid();
            grid.ColumnDefinitions = new ColumnDefinitions("2*,1*");

            TextBlock text = new TextBlock {Text = boards[i]};
            Button btn = new Button {Content = "delete"};

            btn.Click += (sender ,e) => onBoardDelete(i);

            Grid.SetColumn(text, 0);
            Grid.SetColumn(btn, 1);

            grid.Children.Add(text);
            grid.Children.Add(btn);

            BoardPanel.Children.Add(grid);
        }
    }

}