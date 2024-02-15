using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace pr9;

public partial class Ticket : Window
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
        ProviderGrid.ItemsSource = _providers;
    }
    public void FillStatusList()
    {
        _categories = new List<Category>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        //создается объект и используется для установления связи с базой данных
        MySqlCommand command = new MySqlCommand("select ID, Name from category", _connection);//Создание конструктора которому передаются Sql команды
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentstatus = new Category()
            {
                ID = reader.GetInt32("ID"),
                Name = reader.GetString("Name"),
            };
            _categories.Add(currentstatus);
        }
        _connection.Close();
        var statusComboBox = this.Find<ComboBox>("Cmdstatus");
        statusComboBox.ItemsSource = _categories;
    }

    public Ticket()
    {
        InitializeComponent();
        string sql =
            "SELECT ID, Names, category.Name, Price, provider.Name FROM product " +
            "join category ON product.category_id = category.ID " +
            "join provider ON product.provider_id = provider.ID";
        ShowProductTable();
        ShowProviderTable();
        FillStatusList();
        this.Width = 700;
        this.Height = 400;
    }
  
    private void TxtSearch_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Product> cat = _products;
        cat = cat.Where(x => x.Name.Contains(txtSearch.Text)).ToList();
        TicketsGrid.ItemsSource = cat;
    }
    private void Cmdstatus_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var statusComboBox = (ComboBox)sender;
        var currentstatus = statusComboBox.SelectedItem as Category;
        var filteredstatus = _products.Where(x => x.category_id == currentstatus.Name).ToList();
        TicketsGrid.ItemsSource = filteredstatus;
        
    }
    private void Comeback(object? sender, RoutedEventArgs routedEventArgs)//Переход на другую форму
    {
        var newWindow = new MainWindow();
        newWindow.Show();
        this.Close();
    }
   
    
}

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string category_id { get; set; }
    public int Price { get; set; }
    public string provider_id { get; set; }
}

public class Delivery
{
    public int ID { get; set; }
    public string Name { get; set; }
}

public class Category
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
public class Provider
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Adress { get; set; }
    public string city { get; set; }
    public string Phone { get; set; }
}

public class Order11
{
    public int ID { get; set; }
    public string Deliver_address { get; set; }
    public string Delivery_id { get; set; }
    public string Amount_deliver { get; set; }
    public string Amount { get; set; }
}
