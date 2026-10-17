using System;
using System.Globalization;
using System.Text;
using static System.Console;

namespace Lesson1_Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 5: відобразити пору року і день тижня за датою
            Write("Введіть дату (дд.мм.рррр): ");

            if (!DateTime.TryParseExact(ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out DateTime date))
            {
                WriteLine("Помилка: невірний формат дати! Приклад: 22.12.2021");
                return;
            }

            string season;

            switch (date.Month)
            {
                case 12:
                case 1:
                case 2:
                    season = "Winter";
                    break;
                case 3:
                case 4:
                case 5:
                    season = "Spring";
                    break;
                case 6:
                case 7:
                case 8:
                    season = "Summer";
                    break;
                default:
                    season = "Autumn";
                    break;
            }

            WriteLine($"{season} {date.DayOfWeek}");
        }
    }
}