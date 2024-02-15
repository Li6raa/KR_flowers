using Avalonia.Controls;
using Avalonia.Interactivity;

namespace pr9;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.Width = 200;
        this.Height = 300;
    }
    private void OpenAdminVersion(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new Authorization();
        newWindow.Show();
        this.Close();
    }
    private void OpenNewWindow(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new Ticket();
        newWindow.Show();
        this.Close();
    }
    private void exit(object? sender, RoutedEventArgs routedEventArgs)
    {
        this.Close();
    }
}
