using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace pr9;

public partial class ProductAdmin : Window
{
   
    private string _connString="server=localhost; database=yp_odnovalov; port=3306;User Id=root;password=root";
    private List<Product> _products;
    private List<Provider> _providers;
    private List<Category> _categories;
    private List<Delivery> _deliveries;
    private MySqlConnection _connection;
    private DataGrid _ticketsGrid;
    private DataGrid _AgentGrid;

    /// <summary>
    ///Фильтрация
    /// </summary>
   
    
    /// <summary>
    /// 
    /// </summary>
    public void ShowProductTable()
    {
        string sql = "SELECT product.ID, Names, category.Name, Price, provider.Name FROM product " +
                     "join category ON product.category_id = category.ID " +
                     "join provider ON product.provider_id = provider.ID";
        _products = new List<Product>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentYch = new Product()
            {
                ID = reader.GetInt32("ID"),
                Name = reader.GetValue(1).ToString(),
                category_id= reader.GetValue(2).ToString(),
                Price = reader.GetInt32("Price"),
                provider_id = reader.GetValue(4).ToString(),
            };
            _products.Add(currentYch);
        }
        _connection.Close();
        TicketsGrid.ItemsSource = _products;
    }
    public void ShowProviderTable()
    {
        string sql = "Select * From provider";
        _providers = new List<Provider>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentAgent = new Provider()
            {
                ID = reader.GetInt32("ID"),
                Name=reader.GetString("Name"),
                city = reader.GetString("city"),
                Adress = reader.GetString("Adress"),
                Phone = reader.GetString("Phone")
            };
            _providers.Add(currentAgent);
        }
        _connection.Close();
        AgentsGrid.ItemsSource = _providers;
    }
    public ProductAdmin()
    {
        InitializeComponent();
        ShowProviderTable();
        ShowProductTable();
    }
    private void Comeback(object? sender, RoutedEventArgs routedEventArgs)//Переход на другую форму
    {
        var newWindow = new MainWindow();
        newWindow.Show();
        this.Close();
    }
    private void btnDelete_Click(object? sender, RoutedEventArgs routedEventArgs)
    {
        //Получить значение идентификатора агента из TextBox
        int productId = Convert.ToInt32(Delete.Text);
    
        // Вызвать метод удаления агента
        DeleteProduct(productId);
    
        // Обновить таблицу Product после удаления
        ShowProductTable();
    }
    public void DeleteProduct(int productId)
    {
        // Установить строку подключения к базе данных
        string connectionString = "server=localhost; database=yp_odnovalov; port=3306;User Id=root;password=root";
    
        // Создать подключение к базе данных
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            // Открыть подключение
            connection.Open();
        
            // Создать SQL-запрос для удаления агента с указанным идентификатором
            string sql = "DELETE FROM product WHERE ID = @ProductId";
        
            // Создать команду с параметрами
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                // Добавить параметр для идентификатора агента
                command.Parameters.AddWithValue("@ProductId", productId);
            
                // Выполнить команду удаления
                command.ExecuteNonQuery();
            }
            // Закрыть подключение
            connection.Close();
        }
    }
    private void Add(object? sender, RoutedEventArgs routedEventArgs)//Переход на другую форму
    {
        var newWindow = new Add();
        newWindow.Show();
        this.Close();
    }
}