using System;
using System.Globalization;
using System.Text;
using static System.Console;

namespace Lesson5_Homework
{
    // Категория предмета
    enum ItemCategory
    {
        Weapon = 1,
        Armor,
        Consumable,
        Material
    }

    // Тип расходника (используется, если категория Consumable)
    enum ConsumableType
    {
        None,
        HP,
        MP,
        Buff,
        Scroll
    }

    // Структура «Предмет» магазина
    struct Item
    {
        public string Name { get; set; }
        public ItemCategory Category { get; set; }
        public ConsumableType Consumable { get; set; }
        public int Power { get; set; }      // характеристика: урон, захист або сила ефекту
        public int Quantity { get; set; }
        public double Price { get; set; }

        public Item(string name, ItemCategory category, ConsumableType consumable,
                    int power, int quantity, double price)
        {
            Name = name;
            Category = category;
            Consumable = consumable;
            Power = power;
            Quantity = quantity;
            Price = price;
        }
    }

    // Магазин: зберігає предмети та вміє додавати, видаляти й оновлювати їх
    class Shop
    {
        private Item[] items = new Item[0];

        // Додає предмет. Повертає false, якщо предмет із такою назвою вже є.
        public bool Add(Item item)
        {
            if (FindIndex(item.Name) != -1)
            {
                return false;
            }

            Item[] newItems = new Item[items.Length + 1];

            for (int i = 0; i < items.Length; i++)
            {
                newItems[i] = items[i];
            }

            newItems[items.Length] = item;
            items = newItems;

            return true;
        }

        // Видаляє предмет за назвою. Повертає false, якщо такого немає.
        public bool Remove(string name)
        {
            int index = FindIndex(name);

            if (index == -1)
            {
                return false;
            }

            Item[] newItems = new Item[items.Length - 1];
            int position = 0;

            for (int i = 0; i < items.Length; i++)
            {
                if (i != index)
                {
                    newItems[position] = items[i];
                    position++;
                }
            }

            items = newItems;

            return true;
        }

        // Оновлює кількість і ціну предмета. Повертає false, якщо такого немає.
        public bool Update(string name, int quantity, double price)
        {
            int index = FindIndex(name);

            if (index == -1)
            {
                return false;
            }

            items[index].Quantity = quantity;
            items[index].Price = price;

            return true;
        }

        public void Print()
        {
            if (items.Length == 0)
            {
                WriteLine("Магазин порожній.");
                return;
            }

            WriteLine($"{"Назва",-20}{"Категорія",-12}{"Тип",-8}{"Сила",6}{"К-сть",7}{"Ціна",10}");
            WriteLine(new string('-', 63));

            for (int i = 0; i < items.Length; i++)
            {
                Item item = items[i];

                WriteLine($"{item.Name,-20}{item.Category,-12}{item.Consumable,-8}{item.Power,6}{item.Quantity,7}{item.Price,10:F2}");
            }
        }

        // Шукає предмет за назвою без урахування регістру. Повертає -1, якщо не знайдено.
        private int FindIndex(string name)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (string.Equals(items[i].Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 1: магазин цифрових товарів у грі
            Shop shop = new Shop();

            // кілька предметів для прикладу
            shop.Add(new Item("Iron Sword", ItemCategory.Weapon, ConsumableType.None, 25, 5, 120.5));
            shop.Add(new Item("Steel Armor", ItemCategory.Armor, ConsumableType.None, 40, 3, 300));
            shop.Add(new Item("Health Potion", ItemCategory.Consumable, ConsumableType.HP, 50, 20, 15));

            while (true)
            {
                WriteLine();
                WriteLine("1 - показати предмети");
                WriteLine("2 - додати предмет");
                WriteLine("3 - оновити кількість і ціну");
                WriteLine("4 - видалити предмет");
                WriteLine("0 - вихід");
                Write("Ваш вибір: ");
                string choice = ReadLine()!;
                WriteLine();

                switch (choice)
                {
                    case "1":
                        shop.Print();
                        break;
                    case "2":
                        AddItem(shop);
                        break;
                    case "3":
                        UpdateItem(shop);
                        break;
                    case "4":
                        RemoveItem(shop);
                        break;
                    case "0":
                        return;
                    default:
                        WriteLine("Помилка: потрібно ввести число від 0 до 4!");
                        break;
                }
            }
        }

        static void AddItem(Shop shop)
        {
            Write("Назва: ");
            string name = ReadLine()!;

            if (string.IsNullOrWhiteSpace(name))
            {
                WriteLine("Помилка: назва не може бути порожньою!");
                return;
            }

            WriteLine("Категорія: 1 - Weapon, 2 - Armor, 3 - Consumable, 4 - Material");
            int categoryNumber = ReadInt("Ваш вибір: ", 1, 4);
            ItemCategory category = (ItemCategory)categoryNumber;

            ConsumableType consumable = ConsumableType.None;

            if (category == ItemCategory.Consumable)
            {
                WriteLine("Тип розхідника: 1 - HP, 2 - MP, 3 - Buff, 4 - Scroll");
                consumable = (ConsumableType)ReadInt("Ваш вибір: ", 1, 4);
            }

            int power = ReadInt("Сила (урон, захист або ефект): ", 0, int.MaxValue);
            int quantity = ReadInt("Кількість: ", 0, int.MaxValue);
            double price = ReadPrice("Ціна: ");

            if (shop.Add(new Item(name.Trim(), category, consumable, power, quantity, price)))
            {
                WriteLine("Предмет додано.");
            }
            else
            {
                WriteLine("Помилка: предмет із такою назвою вже є!");
            }
        }

        static void UpdateItem(Shop shop)
        {
            Write("Назва предмета: ");
            string name = ReadLine()!;

            int quantity = ReadInt("Нова кількість: ", 0, int.MaxValue);
            double price = ReadPrice("Нова ціна: ");

            if (shop.Update(name, quantity, price))
            {
                WriteLine("Предмет оновлено.");
            }
            else
            {
                WriteLine("Помилка: такого предмета немає!");
            }
        }

        static void RemoveItem(Shop shop)
        {
            Write("Назва предмета: ");
            string name = ReadLine()!;

            if (shop.Remove(name))
            {
                WriteLine("Предмет видалено.");
            }
            else
            {
                WriteLine("Помилка: такого предмета немає!");
            }
        }

        // Запитує ціле число, поки не введуть коректне в діапазоні min-max
        static int ReadInt(string message, int min, int max)
        {
            while (true)
            {
                Write(message);

                if (int.TryParse(ReadLine(), out int value) && value >= min && value <= max)
                {
                    return value;
                }

                WriteLine("Помилка: невірне значення, спробуйте ще раз.");
            }
        }

        // Запитує ціну (число не менше нуля), можна з крапкою або комою
        static double ReadPrice(string message)
        {
            while (true)
            {
                Write(message);
                string input = ReadLine()!.Replace(',', '.');

                if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value) && value >= 0)
                {
                    return value;
                }

                WriteLine("Помилка: потрібно ввести число не менше нуля.");
            }
        }
    }
}