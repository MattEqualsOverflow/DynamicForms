using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaControls.Controls;
using DynamicForms.Example.Avalonia.ViewModels;
using DynamicForms.Example.Avalonia.Views;

namespace DynamicForms.Example.Avalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
            MessageWindow.GlobalParentWindow = desktop.MainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}