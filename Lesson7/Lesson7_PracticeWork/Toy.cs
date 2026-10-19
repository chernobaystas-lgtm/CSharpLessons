class Product
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

    Product(){
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

class Toy : Product
{
    private string _factoryLocation;
    private string _AgeGroup;

    public Toy(string name, double price, string factorylocation, string ageGroup) : base(name, price, factorylocation)
    {
        _AgeGroup = ageGroup;
        _factoryLocation = factorylocation;
    }

    public string AgeGroup
    {
        get { return _AgeGroup; }
        set {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            {
                throw new ArgumentException("Age group must be at least 3 characters long.");
            }
            _AgeGroup = value; 
            }
    }

    public string FactoryLocation
    {
        get { return _factoryLocation; }
        set {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            {
                throw new ArgumentException("Factory location must be at least 3 characters long.");
            }
            _factoryLocation = value; 
            }
    }

    public override string ToString()
    {
        return base.ToString() + $", Age Group: {_AgeGroup}, Factory Location: {_factoryLocation}";
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Age Group: {_AgeGroup}");
        Console.WriteLine($"Factory Location: {_factoryLocation}");
    }

    public override bool Equals(object obj)
    {
        if (obj != null && obj is Toy toy)
        {
            return base.Equals(toy) && _AgeGroup.Equals(toy._AgeGroup) && _factoryLocation.Equals(toy._factoryLocation);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ _AgeGroup.GetHashCode() ^ _factoryLocation.GetHashCode();
    }
}


