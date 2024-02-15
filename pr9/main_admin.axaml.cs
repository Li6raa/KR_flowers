using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace pr9;

public partial class main_admin : Window
{
    public main_admin()
    {
        InitializeComponent();
    }
    private void exit(object? sender, RoutedEventArgs routedEventArgs)
    {
        this.Close();
    }
    private void OpenClientVersion(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new MainWindow();
        newWindow.Show();
        this.Close();
    }
    private void OpenProductWindow(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new ProductAdmin();
        newWindow.Show();
        this.Close();
    }
    private void OpenOrderWindow(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new Order1();
        newWindow.Show();
        this.Close();
    }
    
}