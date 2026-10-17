using System;
using System.Text;
using static System.Console;

namespace Lesson6_Homework;

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;
        InputEncoding = Encoding.UTF8;

        // Завдання 1: клас «Магазин» з площею та перевантаженням операторів
        Shop shop1 = ReadShop("першого");
        Shop shop2 = ReadShop("другого");

        WriteLine();
        WriteLine($"Магазин 1: {shop1}");
        WriteLine($"Магазин 2: {shop2}");

        // + і - змінюють площу на вказаний розмір
        int size = (int)ReadArea("На скільки змінити площу першого магазину: ");

        Shop bigger = shop1 + size;
        WriteLine($"Магазин 1 після збільшення: {bigger}");

        if (size < shop1.Area)
        {
            Shop smaller = shop1 - size;
            WriteLine($"Магазин 1 після зменшення: {smaller}");
        }
        else
        {
            WriteLine("Зменшити не можна, площа стане нульовою або від'ємною");
        }

        // порівняння магазинів за площею
        WriteLine();

        if (shop1 > shop2)
        {
            WriteLine("Перший магазин більший");
        }
        else if (shop1 < shop2)
        {
            WriteLine("Перший магазин менший");
        }

        if (shop1 == shop2)
        {
            WriteLine("Площі однакові");
        }

        if (shop1 != shop2)
        {
            WriteLine("Площі різні");
        }

        if (shop1.Equals(shop2))
        {
            WriteLine("Equals: магазини рівні");
        }
        else
        {
            WriteLine("Equals: магазини не рівні");
        }
    }

    // Питає назву, продавця і площу та створює магазин
    static Shop ReadShop(string number)
    {
        Write($"Назва {number} магазину: ");
        string name = ReadLine()!;

        Write($"Ім'я продавця {number} магазину: ");
        string seller = ReadLine()!;

        double area = ReadArea($"Площа {number} магазину: ");

        return new Shop(name, seller, area);
    }

    // Питає площу, поки не введуть число більше нуля
    static double ReadArea(string text)
    {
        while (true)
        {
            Write(text);

            if (double.TryParse(ReadLine(), out double value) && value > 0)
            {
                return value;
            }

            WriteLine("Треба ввести число більше нуля, ще раз.");
        }
    }
}

class Shop
{
    public string Name { get; set; } = "";
    public string Seller { get; set; } = "";
    public double Area { get; set; }

    public Shop(string name, string seller, double area)
    {
        Name = name;
        Seller = seller;
        Area = area;
    }

    public override string ToString()
    {
        return $"{Name} (продавець: {Seller}), площа {Area} кв. м";
    }

    // Збільшення площі
    public static Shop operator +(Shop shop, double size)
    {
        return new Shop(shop.Name, shop.Seller, shop.Area + size);
    }

    // Зменшення площі
    public static Shop operator -(Shop shop, double size)
    {
        return new Shop(shop.Name, shop.Seller, shop.Area - size);
    }

    public static bool operator ==(Shop a, Shop b)
    {
        return a.Area == b.Area;
    }

    public static bool operator !=(Shop a, Shop b)
    {
        return a.Area != b.Area;
    }

    public static bool operator >(Shop a, Shop b)
    {
        return a.Area > b.Area;
    }

    public static bool operator <(Shop a, Shop b)
    {
        return a.Area < b.Area;
    }

    public override bool Equals(object? obj)
    {
        return obj is Shop other && Area == other.Area;
    }
}