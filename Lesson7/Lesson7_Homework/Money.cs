class Money
{
    // Поля класса (характеристики)
    protected long _wholePart;        // Целая часть суммы (например, доллары, юани, иены)
    protected int _fractionalPart;    // Дробная часть суммы (например, центы, фэни, копейки)
    protected string _currencyName;   // Название самой валюты (например, "Японская иена")
    protected string _subUnitName;    // Название разменной монеты (например, "сен")
    protected double _rateToUsd;      // Курс валюты к доллару США (сколько единиц этой валюты дают за 1 USD)

    public Money(long wholePart, int fractionalPart, string currencyName, string subUnitName, double rateToUsd)
    {
        if (wholePart < 0 || fractionalPart < 0)
        {
            throw new ArgumentException("Значения денег не могут быть отрицательными.");
        }

        _wholePart = wholePart + (fractionalPart / 100);
        _fractionalPart = fractionalPart % 100;
        
        _currencyName = currencyName;
        _subUnitName = subUnitName;
        _rateToUsd = rateToUsd;
    }

    public long WholePart
    {
        get { return _wholePart; }
        set { if (value >= 0) _wholePart = value; }
    }

    public int FractionalPart
    {
        get { return _fractionalPart; }
        set { if (value >= 0) _fractionalPart = value % 100; }
    }

    public string CurrencyName
    {
        get { return _currencyName; }
    }

    public double RateToUsd
    {
        get { return _rateToUsd; }
    }

    public double ToDouble()
    {
        return _wholePart + (_fractionalPart / 100.0);
    }

    public void ConvertTo(Money targetCurrency)
    {
        double amountInUsd = this.ToDouble() / this._rateToUsd;
        
        double convertedAmount = amountInUsd * targetCurrency.RateToUsd;

        long newWhole = (long)convertedAmount;
        int newFraction = (int)Math.Round((convertedAmount - newWhole) * 100);

        Console.WriteLine($"{this.ToDouble()} {this._currencyName} = {newWhole}.{newFraction:D2} {targetCurrency.CurrencyName}");
    }

    public override string ToString()
    {
        return $"{_wholePart} {_currencyName} {_fractionalPart:D2} {_subUnitName} [Курс к 1 USD: {_rateToUsd}]";
    }

    public override bool Equals(object obj)
    {
        if (obj is Money)
        {
            Money other = (Money)obj;
            return this._wholePart == other._wholePart && 
                   this._fractionalPart == other._fractionalPart && 
                   this._currencyName == other._currencyName;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_wholePart, _fractionalPart, _currencyName);
    }
}

class Product : Money
{
    private string _productName; 

    public Product(string productName, long wholePart, int fractionalPart, string currencyName, string subUnitName, double rateToUsd)
        : base(wholePart, fractionalPart, currencyName, subUnitName, rateToUsd)
    {
        _productName = productName;
    }

    public string ProductName
    {
        get { return _productName; }
    }

    public void ReducePrice(double reductionAmount)
    {
        double currentPrice = this.ToDouble();
        if (reductionAmount > currentPrice)
        {
            throw new ArgumentException("Скидка не может быть больше, чем стоимость товара.");
        }

        double newPrice = currentPrice - reductionAmount;
        
        _wholePart = (long)newPrice;
        _fractionalPart = (int)Math.Round((newPrice - _wholePart) * 100);
    }

    public override string ToString()
    {
        return $"Товар: {_productName} | Цена: {base.ToString()}";
    }

    public override bool Equals(object obj)
    {
        if (obj is Product)
        {
            Product other = (Product)obj;
            return base.Equals(other) && this._productName == other._productName;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), _productName);
    }
}
