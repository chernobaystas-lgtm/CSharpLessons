public class Toy : Product
{
    private string _factoryLocation;
    private string _AgeGroup;


    public Toy()
    {
        _AgeGroup = "Unknown";
        _factoryLocation = "Unknown";
    }

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


