using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace pr9;

public partial class product : Window
{
    public product()
    {
        InitializeComponent();
        string sql =
            "SELECT basket.ID, order1.Deliver_address, delivery.Name, order1.Amount_deliver, order1.Amount From order1 " +
            "join basket ON order1.Basket_id = basket.ID" +
            "join delivery On order1.Delivery_id = delivery.ID";
        ShowOrderTable();
        ShowAgentTable();
        FillStatusList();
        this.Width = 700;
        this.Height = 400;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
}
public class Order1
{
    public int ID { get; set; }
    public int Basket_id { get; set; }
    public string Delivery_address { get; set; }
    public int Delivery_id { get; set; }
    public int Amount_deliver { get; set; }
    public int Amount { get; set; }
}

public class Basket
{
    public int ID { get; set; }
    public string product_id { get; set; }
    public int quantity { get; set; }
}

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int category_id { get; set; }
    public int Price { get; set; }
    public int provider_id { get; set; }
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
    public string Address { get; set; }
    public string city { get; set; }
    public int Phone { get; set; }
}