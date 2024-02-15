using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace pr9;

public partial class Authorization : Window
{
    public Authorization()
    {
        InitializeComponent();
    }
    private void exit(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new MainWindow();
        newWindow.Show();
        this.Close();
    }
    private void OpenAdminVersion(object? sender, RoutedEventArgs routedEventArgs)
    {
        if (Login.Text == "user" && Password.Text == "user")
        {
            var newWindow = new main_admin();
            newWindow.Show();
            this.Close();
        }
        
    }
}