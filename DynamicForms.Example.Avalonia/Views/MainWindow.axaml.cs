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
        this.Find<TextBox>(nameof(OutputTextBox))!.Text = JsonSerializer.Serialize(DataContext as MainWindowViewModel, new JsonSerializerOptions() {  WriteIndented = true});
    }
}