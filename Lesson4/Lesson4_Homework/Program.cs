using System;
using System.Globalization;
using System.Text;
using static System.Console;

namespace Lesson4_Homework
{
    // Клас «Кредитна картка»
    class CreditCard
    {
        // Поля класу закриті, доступ до них тільки через методи
        private string number;
        private string ownerName;
        private string cvc;
        private DateTime expiryDate;

        // Конструктор ініціалізує поля.
        // Якщо будь-яке значення неправильне, генерує виняток ArgumentException.
        public CreditCard(string number, string ownerName, string cvc, string expiry)
        {
            // прибираємо пробіли та дефіси, щоб можна було вводити 4111 1111 1111 1111
            string cleanNumber = number.Replace(" ", "").Replace("-", "");

            if (cleanNumber.Length < 13 || cleanNumber.Length > 19 || !IsDigitsOnly(cleanNumber))
            {
                throw new ArgumentException("Номер картки має складатися з 13-19 цифр.");
            }

            if (!IsLuhnValid(cleanNumber))
            {
                throw new ArgumentException("Номер картки не пройшов перевірку за алгоритмом Луна.");
            }

            if (string.IsNullOrWhiteSpace(ownerName))
            {
                throw new ArgumentException("ПІБ власника не може бути порожнім.");
            }

            if (cvc.Length != 3 || !IsDigitsOnly(cvc))
            {
                throw new ArgumentException("CVC має складатися рівно з 3 цифр.");
            }

            if (!DateTime.TryParseExact(expiry, "MM/yy", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out DateTime date))
            {
                throw new ArgumentException("Дата завершення має бути у форматі ММ/рр, наприклад 12/30.");
            }

            // картка діє до кінця вказаного місяця
            if (date.AddMonths(1) <= DateTime.Today)
            {
                throw new ArgumentException("Термін дії картки вже закінчився.");
            }

            this.number = cleanNumber;
            this.ownerName = ownerName.Trim();
            this.cvc = cvc;
            this.expiryDate = date;
        }

        // Виведення даних на екран (номер приховуємо, залишаємо останні 4 цифри)
        public void Print()
        {
            string hidden = new string('*', number.Length - 4) + number.Substring(number.Length - 4);

            WriteLine($"Номер картки: {hidden}");
            WriteLine($"Власник: {ownerName}");
            WriteLine($"Дійсна до: {expiryDate:MM/yy}");
        }

        // Доступ до окремих полів
        public string GetNumber() { return number; }
        public string GetOwnerName() { return ownerName; }
        public string GetCvc() { return cvc; }
        public DateTime GetExpiryDate() { return expiryDate; }

        // Перевіряє, що в рядку тільки цифри
        private static bool IsDigitsOnly(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] < '0' || text[i] > '9')
                {
                    return false;
                }
            }

            return true;
        }

        // Алгоритм Луна: контрольна сума номера картки
        private static bool IsLuhnValid(string cardNumber)
        {
            int sum = 0;
            bool doubleDigit = false;

            // йдемо з кінця; кожну другу цифру подвоюємо
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

    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 5: клас «Кредитна картка»
            WriteLine("Введіть дані кредитної картки:");

            Write("Номер картки: ");
            string number = ReadLine()!;

            Write("ПІБ власника: ");
            string owner = ReadLine()!;

            Write("CVC: ");
            string cvc = ReadLine()!;

            Write("Дата завершення (ММ/рр): ");
            string expiry = ReadLine()!;

            try
            {
                CreditCard card = new CreditCard(number, owner, cvc, expiry);

                WriteLine();
                WriteLine("Картку створено:");
                card.Print();
            }
            catch (ArgumentException ex)
            {
                WriteLine();
                WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}