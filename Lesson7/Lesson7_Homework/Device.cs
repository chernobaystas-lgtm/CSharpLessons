enum DeviceStatus
{
    New,
    Used,
    Broken,
    Refurbished
}

class Device
{
    protected string _name;
    protected string _description;
    protected double _price;
    protected string _color;
    protected DeviceStatus _status;

    public Device(string name, string description, double price, string color, DeviceStatus status)
    {
        _name = name;
        _description = description;
        _price = price;
        _color = color;
        _status = status;
    }

    public virtual void Sound()
    {
        Console.WriteLine("Пристрій видає якийсь звук.");
    }

    public void Show()
    {
        Console.WriteLine($"Назва пристрою: {_name}");
    }

    public void Desc()
    {
        Console.WriteLine($"Опис: {_description}");
        Console.WriteLine($"Ціна: {_price} UAH | Колір: {_color} | Стан: {_status}");
    }

    public override string ToString()
    {
        return $"Device [Name={_name}, Price={_price}, Color={_color}, Status={_status}, Description={_description}]";
    }

    public override bool Equals(object obj)
    {
        if (obj is Device)
        {
            Device other = (Device)obj;
            return this._name == other._name && 
                   this._description == other._description && 
                   this._price == other._price && 
                   this._color == other._color && 
                   this._status == other._status;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_name, _description, _price, _color, _status);
    }
}

class Teapot : Device
{
    private double _volume;

    public Teapot(string name, string description, double price, string color, DeviceStatus status, double volume) 
        : base(name, description, price, color, status)
    {
        _volume = volume;
    }

    public override void Sound()
    {
        Console.WriteLine("Чайник: Ссссссссс! Буль-буль-буль... Свист!");
    }

    public override string ToString()
    {
        return $"Teapot [Name={_name}, Price={_price}, Color={_color}, Status={_status}, Volume={_volume}L, Description={_description}]";
    }

    public override bool Equals(object obj)
    {
        if (obj is Teapot)
        {
            Teapot other = (Teapot)obj;
            return base.Equals(other) && this._volume == other._volume;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), _volume);
    }
}

class Microwave : Device
{
    private int _power;

    public Microwave(string name, string description, double price, string color, DeviceStatus status, int power) 
        : base(name, description, price, color, status)
    {
        _power = power;
    }

    public override void Sound()
    {
        Console.WriteLine("Мікрохвильовка: Вууууууууууууууум... Пік! Пік! Пік!");
    }

    public override string ToString()
    {
        return $"Microwave [Name={_name}, Price={_price}, Color={_color}, Status={_status}, Power={_power}W, Description={_description}]";
    }

    public override bool Equals(object obj)
    {
        if (obj is Microwave)
        {
            Microwave other = (Microwave)obj;
            return base.Equals(other) && this._power == other._power;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), _power);
    }
}

class Car : Device
{
    private string _fuelType;

    public Car(string name, string description, double price, string color, DeviceStatus status, string fuelType) 
        : base(name, description, price, color, status)
    {
        _fuelType = fuelType;
    }

    public override void Sound()
    {
        Console.WriteLine("Автомобіль: Врум-врум! Ррррррррр! Бі-біііііп!");
    }

    public override string ToString()
    {
        return $"Car [Name={_name}, Price={_price}, Color={_color}, Status={_status}, Fuel={_fuelType}, Description={_description}]";
    }

    public override bool Equals(object obj)
    {
        if (obj is Car)
        {
            Car other = (Car)obj;
            return base.Equals(other) && this._fuelType == other._fuelType;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), _fuelType);
    }
}

class Steamboat : Device
{
    private double _length;

    public Steamboat(string name, string description, double price, string color, DeviceStatus status, double length) 
        : base(name, description, price, color, status)
    {
        _length = length;
    }

    public override void Sound()
    {
        Console.WriteLine("Пароплав: ТУУУУУУУУУУУУУУУУУУУУУУУУУУУУУУ!");
    }

    public override string ToString()
    {
        return $"Steamboat [Name={_name}, Price={_price}, Color={_color}, Status={_status}, Length={_length}m, Description={_description}]";
    }

    public override bool Equals(object obj)
    {
        if (obj is Steamboat)
        {
            Steamboat other = (Steamboat)obj;
            return base.Equals(other) && this._length == other._length;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), _length);
    }
}