using System;
using System.Globalization;
using System.Text;
using static System.Console;

namespace Lesson10_Homework;

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;
        InputEncoding = Encoding.UTF8;

        // Завдання 4: клас «Кредитна картка» з подіями
        CreditCard card = new CreditCard("4111 1111 1111 1111", "Іваненко Іван",
            new DateTime(2030, 12, 1), "1234", 1000, 500);

        // цільова сума, при досягненні якої спрацює подія
        card.Target = 1000;

        // підписуємось на події: кожній події відповідає свій метод
        card.Replenished += OnReplenished;
        card.Spent += OnSpent;
        card.CreditStarted += OnCreditStarted;
        card.TargetReached += OnTargetReached;
        card.PinChanged += OnPinChanged;

        while (true)
        {
            WriteLine();
            WriteLine("1 - показати дані картки");
            WriteLine("2 - поповнити рахунок");
            WriteLine("3 - витратити кошти");
            WriteLine("4 - змінити PIN");
            WriteLine("5 - задати цільову суму");
            WriteLine("0 - вихід");
            Write("Ваш вибір: ");
            string choice = ReadLine() ?? "";
            WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        card.Show();
                        break;
                    case "2":
                        card.Replenish(ReadMoney("Сума поповнення: "));
                        break;
                    case "3":
                        SpendMoney(card);
                        break;
                    case "4":
                        ChangePin(card);
                        break;
                    case "5":
                        card.Target = ReadMoney("Цільова сума: ");
                        WriteLine($"Цільову суму задано: {card.Target:F2}");
                        break;
                    case "0":
                        return;
                    default:
                        WriteLine("Помилка: потрібно ввести число від 0 до 5!");
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                WriteLine($"Помилка: {ex.Message}");
            }
        }
    }

    // Обробники подій
    static void OnReplenished(decimal amount)
    {
        WriteLine($"[Подія] Рахунок поповнено на {amount:F2}");
    }

    static void OnSpent(decimal amount)
    {
        WriteLine($"[Подія] Витрачено {amount:F2}");
    }

    static void OnCreditStarted(decimal creditUsed)
    {
        WriteLine($"[Подія] Почато використання кредитних коштів, у борг: {creditUsed:F2}");
    }

    static void OnTargetReached(decimal target)
    {
        WriteLine($"[Подія] Досягнуто цільової суми {target:F2}");
    }

    static void OnPinChanged()
    {
        WriteLine("[Подія] PIN змінено");
    }

    static void SpendMoney(CreditCard card)
    {
        Write("PIN: ");

        if (!card.CheckPin(ReadLine() ?? ""))
        {
            WriteLine("Помилка: невірний PIN!");
            return;
        }

        decimal amount = ReadMoney("Сума витрати: ");

        if (!card.Spend(amount))
        {
            WriteLine("Помилка: недостатньо коштів, кредитний ліміт буде перевищено!");
        }
    }

    static void ChangePin(CreditCard card)
    {
        Write("Поточний PIN: ");
        string oldPin = ReadLine() ?? "";

        Write("Новий PIN (4 цифри): ");
        string newPin = ReadLine() ?? "";

        if (!card.ChangePin(oldPin, newPin))
        {
            WriteLine("Помилка: невірний поточний PIN!");
        }
    }

    // Питає суму більше нуля (можна з крапкою або комою)
    static decimal ReadMoney(string text)
    {
        while (true)
        {
            Write(text);
            string input = (ReadLine() ?? "").Replace(',', '.');

            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal value)
                && value > 0)
            {
                return value;
            }

            WriteLine("Треба ввести число більше нуля, ще раз.");
        }
    }
}

class CreditCard
{
    private string pin;

    public string Number { get; }
    public string OwnerName { get; }
    public DateTime ExpiryDate { get; }
    public decimal CreditLimit { get; }
    public decimal Money { get; private set; }

    // Задана сума грошей: при її досягненні після поповнення спрацює подія
    public decimal Target { get; set; }

    // Події
    public event Action<decimal>? Replenished;     // поповнення рахунку
    public event Action<decimal>? Spent;           // витрата коштів
    public event Action<decimal>? CreditStarted;   // старт використання кредитних коштів
    public event Action<decimal>? TargetReached;   // досягнення заданої суми
    public event Action? PinChanged;               // зміна PIN

    public CreditCard(string number, string ownerName, DateTime expiryDate,
                      string pin, decimal creditLimit, decimal money)
    {
        string cleaned = number.Replace(" ", "").Replace("-", "");

        if (cleaned.Length < 13 || cleaned.Length > 19 || !IsDigits(cleaned))
            throw new ArgumentException("Номер картки має складатися з 13-19 цифр");

        if (!IsLuhnValid(cleaned))
            throw new ArgumentException("Номер картки не пройшов перевірку за алгоритмом Луна");

        if (string.IsNullOrWhiteSpace(ownerName))
            throw new ArgumentException("ПІБ власника не може бути порожнім");

        if (new DateTime(expiryDate.Year, expiryDate.Month, 1).AddMonths(1) <= DateTime.Today)
            throw new ArgumentException("Термін дії картки вже закінчився");

        if (pin.Length != 4 || !IsDigits(pin))
            throw new ArgumentException("PIN має складатися рівно з 4 цифр");

        if (creditLimit < 0 || money < 0)
            throw new ArgumentException("Кредитний ліміт і сума грошей не можуть бути від'ємними");

        Number = cleaned;
        OwnerName = ownerName.Trim();
        ExpiryDate = expiryDate;
        this.pin = pin;
        CreditLimit = creditLimit;
        Money = money;
    }

    // Перевірка PIN
    public bool CheckPin(string enteredPin)
    {
        return pin == enteredPin;
    }

    // Поповнення рахунку
    public void Replenish(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Сума поповнення має бути більше нуля");

        decimal before = Money;
        Money += amount;

        Replenished?.Invoke(amount);

        // подія спрацьовує, коли сума вперше стає не меншою за задану
        if (Target > 0 && before < Target && Money >= Target)
        {
            TargetReached?.Invoke(Target);
        }
    }

    // Витрата коштів. Можна йти в мінус, але не глибше за кредитний ліміт.
    // Повертає false, якщо коштів (разом із кредитними) не вистачає.
    public bool Spend(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Сума витрати має бути більше нуля");

        if (Money - amount < -CreditLimit)
        {
            return false;
        }

        decimal before = Money;
        Money -= amount;

        Spent?.Invoke(amount);

        // перший перехід в мінус це старт використання кредитних коштів
        if (before >= 0 && Money < 0)
        {
            CreditStarted?.Invoke(-Money);
        }

        return true;
    }

    // Зміна PIN. Повертає false, якщо поточний PIN невірний.
    public bool ChangePin(string oldPin, string newPin)
    {
        if (!CheckPin(oldPin))
        {
            return false;
        }

        if (newPin.Length != 4 || !IsDigits(newPin))
            throw new ArgumentException("PIN має складатися рівно з 4 цифр");

        pin = newPin;

        PinChanged?.Invoke();

        return true;
    }

    // Виведення даних (номер приховано, PIN не показуємо)
    public void Show()
    {
        string hidden = new string('*', Number.Length - 4) + Number.Substring(Number.Length - 4);

        WriteLine($"Номер картки: {hidden}");
        WriteLine($"Власник: {OwnerName}");
        WriteLine($"Дійсна до: {ExpiryDate:MM/yy}");
        WriteLine($"Сума грошей: {Money:F2}");
        WriteLine($"Кредитний ліміт: {CreditLimit:F2}");
        WriteLine($"Доступно для витрат: {Money + CreditLimit:F2}");
        WriteLine($"Цільова сума: {Target:F2}");
    }

    private static bool IsDigits(string text)
    {
        foreach (char c in text)
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
}