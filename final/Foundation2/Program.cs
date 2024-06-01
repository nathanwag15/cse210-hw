using System;

class Program
{
    static void Main(string[] args)
    {
        Customer customerOne = new Customer("Steven Hawking", new Address("119 N Prospect Ave", "Streamwood", "Illinois", "USA"));
        Order orderOne = new Order(customerOne);
        orderOne.AddProduct(new Product("Cereal", "12345", 5.50, 1));
        orderOne.AddProduct(new Product("Ice Cream", "2343521", 10.00, 2));
        orderOne.AddProduct(new Product("Kelp", "34534", 20.00, 1));
        Customer customerTwo = new Customer("Alfred Hawks", new Address("1472 N 300 W", "Provo", "Utah", "Japan"));
        Order orderTwo = new Order(customerTwo);
        orderTwo.AddProduct(new Product("Sushi", "00000", 12.00, 2));
        orderTwo.AddProduct(new Product("Shoyu", "1234", 5.50, 2));

        orderOne.getShippingLabel();
        orderOne.getPackingLabel();        
        Console.WriteLine($"Total: {orderOne.GetTotalCost()}\n");

        orderTwo.getShippingLabel();
        orderTwo.getPackingLabel();        
        Console.WriteLine($"Total: {orderTwo.GetTotalCost()}");
    }
}

class Product
{
    private string _name = "";
    private string _productId = "";
    private double _price = 0.00;
    private int _quantity = 0;

    public Product(string name, string productID, double price, int quantity)
    {
        _name = name;
        _productId = productID;
        _price = price;
        _quantity = quantity;
    }
    public double getPrice()
    {
        return _price*_quantity;
    }

    public string getPackingLabelInfo()
    {
        return $"{_name}: {_productId}";
    }
}


class Customer
{
    string _name = "";
    Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public bool IsUSA()
    {
        return _address.IsUSA();
    }

    public string getShippingLabelInfo()
    {
        return $"{_name}: {_address.GetAddress()}";
    }

}

class Address
{
    private string _streetAddress = "";
    private string _city = "";
    private string _state = "";
    private string _country = "";

    public Address(string streetAddress, string city, string state, string country)
    {
        _streetAddress = streetAddress;
        _city = city;
        _state = state;
        _country = country;
    }

    public bool IsUSA()
    {
        if(_country == "USA")
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public string GetAddress()
    {
        return($"{_streetAddress} {_city} {_state} {_country}");
    }
}

class Order
{
    private List<Product> _products = new List<Product>();
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
    }

    public void AddProduct(Product product)
    {   
        _products.Add(product);
    }

    public double GetTotalCost()
    {
        double total = 0.00;

        foreach(Product product in _products)
        {
            double price = product.getPrice();
            total += price;
        }

        if (_customer.IsUSA())
        {
            total += 5.00;
        } else
        {
            total += 35.00;
        }

        return total;
    }

    public void getPackingLabel()
    {
        foreach(Product product in _products)
        {
            Console.WriteLine(product.getPackingLabelInfo());
        }
    }

    public void getShippingLabel()
    {
        Console.WriteLine(_customer.getShippingLabelInfo());
    }



}