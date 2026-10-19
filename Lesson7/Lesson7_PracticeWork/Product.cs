public class Product
{
    protected string _name;
    protected double _price;
    protected string _factorylocation;
    
    public Product(string name, double price, string factorylocation)
    {
        this._name = name;
        this._price = price;
        this._factorylocation = factorylocation;
    }

    public Product(){
        _name = "Unknown";
        _price = 0.0;
        _factorylocation = "Unknown";
    }


    public string Name
    {
        get { return _name; }
        set {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            {
                throw new ArgumentException("Name must be at least 3 characters long.");
            }
            _name = value; 
            }
    }

    public double Price
    {
        get { return _price; }
        set {
            if(value < 0)
            {
                throw new ArgumentException("Price cannot be negative.");
            }
            _price = value; 
            }
    }

    public string FactoryLocation
    {
        get { return _factorylocation; }
        set {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            {
                throw new ArgumentException("Factory location must be at least 3 characters long.");
            }
            _factorylocation = value; 
            }
    }

    public override string ToString()
    {
        return $"Product Name: {_name}, Price: {_price}, Factory Location: {_factorylocation}";
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Product Name: {_name}");
        Console.WriteLine($"Price: {_price}");
        Console.WriteLine($"Factory Location: {_factorylocation}");
    }

    public override bool Equals(object obj)
    {
        if (obj != null && obj is Product Product)
        {
            return ToString().Equals(Product.ToString());
        }

        return false;
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

}