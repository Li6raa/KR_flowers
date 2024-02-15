using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace pr9;

public partial class Order1 : Window
{
    private string _connString="server=localhost; database=yp_odnovalov; port=3306;User Id=root;password=root";
    private List<Order11> _order1;
    private MySqlConnection _connection;
    private DataGrid _ticketsGrid;
    public Order1()
    {
        InitializeComponent();
        ShowOrderTable();
    }
    public void ShowOrderTable()
    {
        string sql = "SELECT order1.ID, order1.Deliver_address, delivery.Name, order1.Amount_deliver, order1.Amount FROM order1 " +
                     "join delivery ON order1.Delivery_id = delivery.ID";
        _order1 = new List<Order11>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentYch = new Order11()
            {
                ID = reader.GetInt32("ID"),
                Deliver_address = reader.GetValue(1).ToString(),
                Delivery_id= reader.GetValue(2).ToString(),
                Amount_deliver = reader.GetValue(3).ToString(),
                Amount = reader.GetValue(4).ToString(),
            };
            _order1.Add(currentYch);
        }
        _connection.Close();
        TicketsGrid.ItemsSource = _order1;
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
        int orderId = Convert.ToInt32(Delete.Text);
    
        // Вызвать метод удаления агента
        DeleteOrder(orderId);
    
        // Обновить таблицу Product после удаления
        ShowOrderTable();
    }
    public void DeleteOrder(int orderId)
    {
        // Установить строку подключения к базе данных
        string connectionString = "server=localhost; database=yp_odnovalov; port=3306;User Id=root;password=root";
    
        // Создать подключение к базе данных
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            // Открыть подключение
            connection.Open();
        
            // Создать SQL-запрос для удаления агента с указанным идентификатором
            string sql = "DELETE FROM order1 WHERE ID = @OrderId";
        
            // Создать команду с параметрами
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                // Добавить параметр для идентификатора агента
                command.Parameters.AddWithValue("@OrderId", orderId);
            
                // Выполнить команду удаления
                command.ExecuteNonQuery();
            }
            // Закрыть подключение
            connection.Close();
        }
    }
}