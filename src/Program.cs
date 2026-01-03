/* 100% ai generated */

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Themes.Fluent;
using Avalonia.Styling; // for ThemeVariant
using Avalonia;

namespace TodoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                         .UsePlatformDetect()
                         .LogToTrace();
    }

    public class App : Application
    {
        public override void Initialize()
        {
            // Add the Fluent theme (no Mode property)
            Styles.Add(new FluentTheme());

            // Optional: choose light or dark
            RequestedThemeVariant = ThemeVariant.Light; // or ThemeVariant.Dark
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }

    public class MainWindow : Window
    {
        public MainWindow()
        {
            Title = "Avalonia 11+ Themed App";
            Width = 400;
            Height = 300;

            var button = new Button
            {
                Content = "Click Me",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                Margin = new Avalonia.Thickness(10),
            };

            Content = button;
        }
    }
}
