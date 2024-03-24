using System.Linq;
using System.Text.Json;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DynamicForms.Example.Avalonia.ViewModels;

namespace DynamicForms.Example.Avalonia.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Find<DockPanel>(nameof(MainDockPanel))!.IsVisible = false;
        this.Find<TextBox>(nameof(OutputTextBox))!.IsVisible = true;
        this.Find<TextBox>(nameof(OutputTextBox))!.Text = JsonSerializer.Serialize(DataContext as MainWindowViewModel, new JsonSerializerOptions() {  WriteIndented = true});
    }

    private void TestButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not MainWindowViewModel viewModel)
        {
            return;
        }

        var property = viewModel.Example.GetType().GetProperties()
            .First(x => x.Name == nameof(viewModel.Example.TestCheckbox));
        property.SetValue(viewModel.Example, true);
    }
}