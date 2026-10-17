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

            // Завдання 6: конвертація температури Фаренгейт <-> Цельсій
            WriteLine("Оберіть напрямок конвертації:");
            WriteLine("1 - з Фаренгейта в Цельсій");
            WriteLine("2 - з Цельсія в Фаренгейт");
            Write("Ваш вибір: ");
            string choice = ReadLine()!;

            if (choice != "1" && choice != "2")
            {
                WriteLine("Помилка: потрібно ввести 1 або 2!");
                return;
            }

            Write("Введіть температуру: ");
            string input = ReadLine()!.Replace(',', '.');

            if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double temperature))
            {
                WriteLine("Помилка: потрібно ввести число!");
                return;
            }

            if (choice == "1")
            {
                double celsius = (temperature - 32) * 5 / 9;
                WriteLine($"{temperature} °F = {celsius:F2} °C");
            }
            else
            {
                double fahrenheit = temperature * 9 / 5 + 32;
                WriteLine($"{temperature} °C = {fahrenheit:F2} °F");
            }
        }
    }
}