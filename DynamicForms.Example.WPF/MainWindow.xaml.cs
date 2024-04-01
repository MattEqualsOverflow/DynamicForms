using System.Text.Json;
using System.Windows;

namespace DynamicForms.Example.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var viewModel = new MainWindowViewModel();
        viewModel.ExampleSettings.ButtonTest += (sender, args) =>
        {
            Console.WriteLine("Button pressed");
        };
        DataContext = viewModel;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        OutputTextBox.Text = JsonSerializer.Serialize(DataContext as MainWindowViewModel, new JsonSerializerOptions() {  WriteIndented = true});
    }
}