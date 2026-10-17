using System;
using System.Globalization;
using System.Text;
using static System.Console;

namespace Lesson6_PracticeWork;

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;
        InputEncoding = Encoding.UTF8;

        // Завдання 3: клас «Кредитна картка» з перевантаженням операторів
        try
        {
            CreditCard card = new CreditCard();
            card.Input();

            decimal increase = ReadMoney("Введіть, на скільки збільшити суму грошей: ");
            card = card + increase;

            decimal decrease = ReadMoney("Введіть, на скільки зменшити суму грошей: ");
            card = card - decrease;

            WriteLine();
            WriteLine("Дані картки:");
            card.Show();

            CreditCard other = new CreditCard("5500 0000 0000 0004", "Петренко Петро",
                "321", new DateTime(2030, 12, 1), 5000);

            WriteLine();
            WriteLine($"Інша картка: сума {other.Money}, CVC 321");

            // < і > порівнюють суму грошей
            if (card > other)
            {
                WriteLine("На вашій картці грошей більше");
            }
            else if (card < other)
            {
                WriteLine("На вашій картці грошей менше");
            }
            else
            {
                WriteLine("Сума грошей однакова");
            }

            // == і != порівнюють CVC код
            if (card == other)
            {
                WriteLine("CVC коди однакові");
            }

            if (card != other)
            {
                WriteLine("CVC коди різні");
            }

            if (card.Equals(other))
            {
                WriteLine("Equals підтверджує рівність CVC");
            }
            else
            {
                WriteLine("Equals: CVC не рівні");
            }
        }
        catch (ArgumentException ex)
        {
            WriteLine($"Помилка: {ex.Message}");
        }
    }

    // Запитує суму не менше нуля, поки не введуть коректну (можна з крапкою або комою)
    static decimal ReadMoney(string message)
    {
        while (true)
        {
            Write(message);
            string input = (ReadLine() ?? "").Replace(',', '.');

            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal value)
                && value >= 0)
            {
                return value;
            }

            WriteLine("Помилка: потрібно ввести число не менше нуля.");
        }
    }
}

public class CreditCard
{
    private string number = "";
    private string ownerName = "";
    private string cvc = "";
    private DateTime expiryDate;
    private decimal money;

    public CreditCard()
    {
    }

    public CreditCard(string number, string ownerName, string cvc, DateTime expiryDate, decimal money)
    {
        Number = number;
        OwnerName = ownerName;
        Cvc = cvc;
        ExpiryDate = expiryDate;
        Money = money;
    }

    // Номер картки: 13-19 цифр і перевірка за алгоритмом Луна
    public string Number
    {
        get { return number; }
        set
        {
            string cleaned = (value ?? "").Replace(" ", "").Replace("-", "");

            if (cleaned.Length < 13 || cleaned.Length > 19 || !IsDigits(cleaned))
                throw new ArgumentException("Номер картки має складатися з 13-19 цифр");

            if (!IsLuhnValid(cleaned))
                throw new ArgumentException("Номер картки не пройшов перевірку за алгоритмом Луна");

            number = cleaned;
        }
    }

    public string OwnerName
    {
        get { return ownerName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("ПІБ власника не може бути порожнім");
            ownerName = value.Trim();
        }
    }

    public string Cvc
    {
        get { return cvc; }
        set
        {
            value = (value ?? "").Trim();

            if (value.Length != 3 || !IsDigits(value))
                throw new ArgumentException("CVC має складатися рівно з 3 цифр");

            cvc = value;
        }
    }

    // Дата завершення роботи картки: картка діє до кінця вказаного місяця
    public DateTime ExpiryDate
    {
        get { return expiryDate; }
        set
        {
            if (new DateTime(value.Year, value.Month, 1).AddMonths(1) <= DateTime.Today)
                throw new ArgumentException("Термін дії картки вже закінчився");

            expiryDate = value;
        }
    }

    // Сума грошей на картці. Оператори <, > та +, - працюють саме з нею
    public decimal Money
    {
        get { return money; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Сума грошей не може бути від'ємною");
            money = value;
        }
    }

    public void Input()
    {
        Number = Ask("Номер картки: ");
        OwnerName = Ask("ПІБ власника: ");
        Cvc = Ask("CVC: ");

        while (true)
        {
            string dateInput = Ask("Дата завершення (ММ/рр): ");
            if (DateTime.TryParseExact(dateInput, "MM/yy", CultureInfo.InvariantCulture,
                                       DateTimeStyles.None, out DateTime date))
            {
                ExpiryDate = date;
                break;
            }
            WriteLine("Невірний формат дати, приклад: 12/30. Спробуйте ще раз.");
        }

        while (true)
        {
            string moneyInput = Ask("Сума грошей на картці: ").Replace(',', '.');
            if (decimal.TryParse(moneyInput, NumberStyles.Number, CultureInfo.InvariantCulture,
                                 out decimal value) && value >= 0)
            {
                Money = value;
                break;
            }
            WriteLine("Потрібно ввести число не менше нуля. Спробуйте ще раз.");
        }
    }

    private string Ask(string message)
    {
        Write(message);
        return ReadLine() ?? "";
    }

    // Номер приховуємо, залишаємо останні 4 цифри, CVC не показуємо
    public void Show()
    {
        WriteLine($"Номер картки: {HiddenNumber()}");
        WriteLine($"Власник: {ownerName}");
        WriteLine($"CVC: ***");
        WriteLine($"Дійсна до: {expiryDate:MM/yy}");
        WriteLine($"Сума грошей: {money}");
    }

    public override string ToString()
    {
        return $"Картка {HiddenNumber()}, власник: {ownerName}, дійсна до {expiryDate:MM/yy}, сума: {money}";
    }

    private string HiddenNumber()
    {
        return new string('*', number.Length - 4) + number.Substring(number.Length - 4);
    }

    private static bool IsDigits(string s)
    {
        foreach (char c in s)
        {
            if (c < '0' || c > '9') return false;
        }
        return true;
    }

    // Алгоритм Луна: контрольна сума номера картки
    private static bool IsLuhnValid(string cardNumber)
    {
        int sum = 0;
        bool doubleDigit = false;

        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {
            int digit = cardNumber[i] - '0';

            if (doubleDigit)
            {
                digit *= 2;

                if (digit > 9)
                {
                    digit -= 9;
                }
            }

            sum += digit;
            doubleDigit = !doubleDigit;
        }

        return sum % 10 == 0;
    }

    // Збільшення суми грошей на вказану кількість. Повертає нову картку
    public static CreditCard operator +(CreditCard card, decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Сума не може бути від'ємною");

        return card.WithMoney(card.money + amount);
    }

    // Зменшення суми грошей на вказану кількість. Повертає нову картку
    public static CreditCard operator -(CreditCard card, decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Сума не може бути від'ємною");

        if (amount > card.money)
            throw new ArgumentException("На картці недостатньо грошей");

        return card.WithMoney(card.money - amount);
    }

    // Перевірка на рівність CVC коду
    public static bool operator ==(CreditCard? card1, CreditCard? card2)
    {
        if (ReferenceEquals(card1, card2)) return true;
        if (card1 is null || card2 is null) return false;

        return card1.cvc == card2.cvc;
    }

    public static bool operator !=(CreditCard? card1, CreditCard? card2)
    {
        return !(card1 == card2);
    }

    // Перевірка на більшу суму грошей
    public static bool operator >(CreditCard card1, CreditCard card2)
    {
        return card1.money > card2.money;
    }

    // Перевірка на меншу суму грошей
    public static bool operator <(CreditCard card1, CreditCard card2)
    {
        return card1.money < card2.money;
    }

    // Equals узгоджений з ==: порівнює CVC
    public override bool Equals(object? obj)
    {
        return obj is CreditCard other && cvc == other.cvc;
    }

    // Копія картки з іншою сумою грошей (оригінал не змінюється)
    private CreditCard WithMoney(decimal newMoney)
    {
        return new CreditCard(number, ownerName, cvc, expiryDate, newMoney);
    }
}