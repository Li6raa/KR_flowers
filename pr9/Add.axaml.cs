using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace pr9;

public partial class Add : Window
{
    private MySqlConnection _connection;
    private string _connString="server=localhost; database=yp_odnovalov; port=3306;User Id=root;password=root";

    public Add()
    {
        InitializeComponent();
        this.Width = 200;
        this.Height = 200;
    }
    private void AddProvider(object sender, RoutedEventArgs e)
    {
        string name = Name.Text; // Получите значение из элемента управления для ввода имени агента
        string city = City.Text;
        string address = Address.Text;
        string phone = Phone.Text;
        string sql = "INSERT INTO provider (Name, City, Adress, Phone) VALUES (@Name, @city, @Adress, @Phone)";

        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.AddWithValue("@Name", name);
        command.Parameters.AddWithValue("@city", city);
        command.Parameters.AddWithValue("@Adress", address);
        command.Parameters.AddWithValue("@Phone", phone);
        command.ExecuteNonQuery();
        _connection.Close();
    }
    private void Comeback(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new ProductAdmin();
        newWindow.Show();
        this.Close();
    }
}