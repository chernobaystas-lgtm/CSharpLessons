using System;
using System.Text;
using static System.Console;

namespace Lesson4_Homework
{
    // Клас «Місто»
    class City
    {
        // Поля класу закриті, доступ до них тільки через методи
        private string name = "";
        private string country = "";
        private int population;
        private string phoneCode = "";
        private string[] districts = new string[0];

        // Введення даних з клавіатури
        public void Input()
        {
            Write("Назва міста: ");
            name = ReadLine()!;

            Write("Назва країни: ");
            country = ReadLine()!;

            // повторюємо запит, поки не введуть коректну кількість жителів
            while (true)
            {
                Write("Кількість жителів: ");

                if (int.TryParse(ReadLine(), out population) && population >= 0)
                {
                    break;
                }

                WriteLine("Помилка: потрібно ввести ціле число не менше нуля!");
            }

            Write("Телефонний код міста: ");
            phoneCode = ReadLine()!;

            int count;

            while (true)
            {
                Write("Кількість районів: ");

                if (int.TryParse(ReadLine(), out count) && count >= 0)
                {
                    break;
                }

                WriteLine("Помилка: потрібно ввести ціле число не менше нуля!");
            }

            districts = new string[count];

            for (int i = 0; i < districts.Length; i++)
            {
                Write($"Назва району {i + 1}: ");
                districts[i] = ReadLine()!;
            }
        }

        // Виведення даних на екран
        public void Print()
        {
            WriteLine($"Місто: {name}");
            WriteLine($"Країна: {country}");
            WriteLine($"Кількість жителів: {population}");
            WriteLine($"Телефонний код: {phoneCode}");

            if (districts.Length == 0)
            {
                WriteLine("Райони: немає");
            }
            else
            {
                WriteLine("Райони:");

                for (int i = 0; i < districts.Length; i++)
                {
                    WriteLine($"  {i + 1}. {districts[i]}");
                }
            }
        }

        // Доступ до окремих полів: отримати значення
        public string GetName() { return name; }
        public string GetCountry() { return country; }
        public int GetPopulation() { return population; }
        public string GetPhoneCode() { return phoneCode; }
        public string[] GetDistricts() { return districts; }

        // Доступ до окремих полів: змінити значення
        public void SetName(string value) { name = value; }
        public void SetCountry(string value) { country = value; }
        public void SetPhoneCode(string value) { phoneCode = value; }
        public void SetDistricts(string[] value) { districts = value; }

        // Для кількості жителів повертає true, якщо значення коректне і було збережене
        public bool SetPopulation(int value)
        {
            if (value < 0)
            {
                return false;
            }

            population = value;
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 3: клас «Місто»
            City city = new City();

            WriteLine("Введіть дані міста:");
            city.Input();

            WriteLine();
            WriteLine("Дані міста:");
            city.Print();

            // доступ до окремих полів через методи класу
            WriteLine();
            WriteLine($"Назва (через GetName): {city.GetName()}");

            Write("Введіть нову кількість жителів: ");
            if (int.TryParse(ReadLine(), out int newPopulation) && city.SetPopulation(newPopulation))
            {
                WriteLine();
                WriteLine("Дані після зміни кількості жителів:");
                city.Print();
            }
            else
            {
                WriteLine("Помилка: невірне значення, кількість жителів не змінено.");
            }
        }
    }
}