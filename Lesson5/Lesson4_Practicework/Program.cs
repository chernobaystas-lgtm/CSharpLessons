using System;
using System.Globalization;
using System.Text;
using static System.Console;

namespace Lesson5_PracticeWork
{
    // Структура «День народження»
    struct Birthday
    {
        private DateTime date;

        private static readonly string[] DayNames =
        {
            "неділя", "понеділок", "вівторок", "середа", "четвер", "п'ятниця", "субота"
        };

        // Встановлення дати дня народження.
        // Повертає false, якщо дата неправильна або в майбутньому.
        public bool SetDate(int day, int month, int year)
        {
            if (year < 1 || year > DateTime.Today.Year || month < 1 || month > 12)
            {
                return false;
            }

            if (day < 1 || day > DateTime.DaysInMonth(year, month))
            {
                return false;
            }

            DateTime newDate = new DateTime(year, month, day);

            if (newDate > DateTime.Today)
            {
                return false;
            }

            date = newDate;
            return true;
        }

        public DateTime GetDate()
        {
            return date;
        }

        // День тижня, коли народилася людина
        public string GetDayOfWeekOfBirth()
        {
            return DayNames[(int)date.DayOfWeek];
        }

        // День тижня, на який припадає день народження в зазначеному році
        public string GetDayOfWeekInYear(int year)
        {
            return DayNames[(int)BirthdayInYear(year).DayOfWeek];
        }

        // Кількість днів, що залишилися до наступного дня народження.
        // Якщо день народження сьогодні, повертає 0.
        public int DaysUntilNextBirthday()
        {
            DateTime today = DateTime.Today;
            DateTime next = BirthdayInYear(today.Year);

            if (next < today)
            {
                next = BirthdayInYear(today.Year + 1);
            }

            return (next - today).Days;
        }

        // Дата дня народження в заданому році.
        // Для 29 лютого в невисокосному році беремо 28 лютого.
        private DateTime BirthdayInYear(int year)
        {
            int day = date.Day;

            if (date.Month == 2 && date.Day == 29 && !DateTime.IsLeapYear(year))
            {
                day = 28;
            }

            return new DateTime(year, date.Month, day);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 1: структура «День народження»
            Write("Введіть дату народження (дд.мм.рррр): ");

            if (!DateTime.TryParseExact(ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out DateTime input))
            {
                WriteLine("Помилка: невірний формат дати! Приклад: 01.04.1990");
                return;
            }

            Birthday birthday = new Birthday();

            if (!birthday.SetDate(input.Day, input.Month, input.Year))
            {
                WriteLine("Помилка: дата не може бути в майбутньому!");
                return;
            }

            Write("Введіть рік, у якому цікавить день народження: ");

            if (!int.TryParse(ReadLine(), out int year) || year < 1 || year > 9999)
            {
                WriteLine("Помилка: потрібно ввести рік від 1 до 9999!");
                return;
            }

            WriteLine();
            WriteLine($"Дата народження: {birthday.GetDate():dd.MM.yyyy}");
            WriteLine($"Народилася людина в такий день тижня: {birthday.GetDayOfWeekOfBirth()}");
            WriteLine($"У {year} році день народження буде в такий день тижня: {birthday.GetDayOfWeekInYear(year)}");
            WriteLine($"Днів до наступного дня народження: {birthday.DaysUntilNextBirthday()}");
        }
    }
}