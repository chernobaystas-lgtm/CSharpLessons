using System;
using System.Text;
using static System.Console;

namespace Lesson6_PracticeWork;

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;
        InputEncoding = Encoding.UTF8;

        // Завдання 2: клас «Місто» з індексатором і перевантаженням операторів
        try
        {
            City city = new City();
            city.Input();

            int increase = ReadInt("Введіть, на скільки збільшити кількість жителів: ");
            city = city + increase;

            int decrease = ReadInt("Введіть, на скільки зменшити кількість жителів: ");
            city = city - decrease;

            WriteLine();
            WriteLine("Дані міста:");
            city.Show();

            // індексатор: назва району за індексом
            int index = ReadInt($"Введіть індекс району (від 0 до {city.DistrictCount - 1}): ");
            WriteLine($"Район з індексом {index}: {city[index]}");

            City other = new City("Каунас", "Литва", 290000, "+370 37",
                new string[] { "Центр", "Жалякальніс" });

            WriteLine();
            WriteLine($"Кількість жителів в іншому місті ({other.Name}): {other.Population}");

            if (city > other)
            {
                WriteLine($"У місті {city.Name} жителів більше, ніж у {other.Name}");
            }
            else if (city < other)
            {
                WriteLine($"У місті {city.Name} жителів менше, ніж у {other.Name}");
            }

            if (city == other)
            {
                WriteLine("Кількість жителів однакова");
            }

            if (city != other)
            {
                WriteLine("Кількість жителів різна");
            }

            if (city.Equals(other))
            {
                WriteLine("Equals підтверджує рівність кількості жителів");
            }
            else
            {
                WriteLine("Equals: кількість жителів не рівна");
            }
        }
        catch (ArgumentException ex)
        {
            WriteLine($"Помилка: {ex.Message}");
        }
    }

    // Запитує ціле число не менше нуля, поки не введуть коректне
    static int ReadInt(string message)
    {
        while (true)
        {
            Write(message);

            if (int.TryParse(ReadLine(), out int value) && value >= 0)
            {
                return value;
            }

            WriteLine("Помилка: потрібно ввести ціле число не менше нуля.");
        }
    }
}

public class City
{
    private string name = "";
    private string country = "";
    private int population;
    private string phoneCode = "";
    private string[] districts = new string[0];

    public City()
    {
    }

    public City(string name, string country, int population, string phoneCode, string[] districts)
    {
        Name = name;
        Country = country;
        Population = population;
        PhoneCode = phoneCode;

        // копіюємо масив, щоб місто не залежало від переданого масиву
        this.districts = new string[districts.Length];

        for (int i = 0; i < districts.Length; i++)
        {
            this.districts[i] = districts[i];
        }
    }

    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва міста не може бути порожньою");
            name = value.Trim();
        }
    }

    public string Country
    {
        get { return country; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва країни не може бути порожньою");
            country = value.Trim();
        }
    }

    // Кількість жителів. Усі оператори та Show працюють саме з цим полем
    public int Population
    {
        get { return population; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Кількість жителів не може бути від'ємною");
            population = value;
        }
    }

    public string PhoneCode
    {
        get { return phoneCode; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Телефонний код не може бути порожнім");
            phoneCode = value.Trim();
        }
    }

    // Кількість районів міста (тільки для читання)
    public int DistrictCount
    {
        get { return districts.Length; }
    }

    // Індексатор: назва району міста за індексом (з нуля)
    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= districts.Length)
                throw new ArgumentOutOfRangeException(nameof(index), "Району з таким індексом немає");
            return districts[index];
        }
    }

    public void Input()
    {
        Name = Ask("Назва міста: ");
        Country = Ask("Назва країни: ");

        while (true)
        {
            string populationInput = Ask("Кількість жителів: ");
            if (int.TryParse(populationInput, out int value) && value >= 0)
            {
                Population = value;
                break;
            }
            WriteLine("Потрібно ввести ціле число не менше нуля. Спробуйте ще раз.");
        }

        PhoneCode = Ask("Телефонний код міста: ");

        int count;

        while (true)
        {
            string countInput = Ask("Кількість районів: ");
            if (int.TryParse(countInput, out count) && count > 0)
            {
                break;
            }
            WriteLine("Потрібно ввести ціле число більше нуля. Спробуйте ще раз.");
        }

        districts = new string[count];

        for (int i = 0; i < districts.Length; i++)
        {
            string district = Ask($"Назва району {i}: ");

            if (string.IsNullOrWhiteSpace(district))
                throw new ArgumentException("Назва району не може бути порожньою");

            districts[i] = district.Trim();
        }
    }

    private string Ask(string message)
    {
        Write(message);
        return ReadLine() ?? "";
    }

    public void Show()
    {
        WriteLine($"Місто: {name}");
        WriteLine($"Країна: {country}");
        WriteLine($"Кількість жителів: {population}");
        WriteLine($"Телефонний код: {phoneCode}");
        WriteLine("Райони:");

        for (int i = 0; i < districts.Length; i++)
        {
            WriteLine($"  {i}. {districts[i]}");
        }
    }

    public override string ToString()
    {
        return $"{name} ({country}), жителів: {population}, код: {phoneCode}, районів: {districts.Length}";
    }

    // Збільшення кількості жителів на вказану кількість. Повертає нове місто
    public static City operator +(City city, int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Кількість не може бути від'ємною");

        return city.WithPopulation(city.population + amount);
    }

    // Зменшення кількості жителів на вказану кількість. Повертає нове місто
    public static City operator -(City city, int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Кількість не може бути від'ємною");

        if (amount > city.population)
            throw new ArgumentException("Кількість жителів не може стати від'ємною");

        return city.WithPopulation(city.population - amount);
    }

    // Перевірка на рівність двох міст за кількістю жителів
    public static bool operator ==(City? city1, City? city2)
    {
        if (ReferenceEquals(city1, city2)) return true;
        if (city1 is null || city2 is null) return false;

        return city1.population == city2.population;
    }

    public static bool operator !=(City? city1, City? city2)
    {
        return !(city1 == city2);
    }

    // Перевірка на більшу кількість жителів
    public static bool operator >(City city1, City city2)
    {
        return city1.population > city2.population;
    }

    // Перевірка на меншу кількість жителів
    public static bool operator <(City city1, City city2)
    {
        return city1.population < city2.population;
    }

    // Equals узгоджений з ==: порівнює кількість жителів
    public override bool Equals(object? obj)
    {
        return obj is City other && population == other.population;
    }
    // Копія міста з іншою кількістю жителів (оригінал не змінюється)
    private City WithPopulation(int newPopulation)
    {
        return new City(name, country, newPopulation, phoneCode, districts);
    }
}