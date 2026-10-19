using System;
using System.Text;
using static System.Console;

namespace Lesson7_PracticeWork;

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;
        InputEncoding = Encoding.UTF8;

        Product[] products = new Product[5];
    
    products[0] = new Product("Хліб", 25.50, "Сільпо");
    products[1] = new Toy("Конструктор LEGO", 1200.00, "Україна", "6"); 
    products[2] = new Toy("М'яка ведмедик", 450.00, "Україна", "2");
    products[3] = new Toy("Настільна гра", 350.00, "Україна", "8");
    products[4] = new Product("Молоко", 40.00, "АТБ");

    Console.WriteLine("=== 1. ТОВАРИ ВІД УКРАЇНСЬКИХ ВИРОБНИКІВ ===");
    foreach (var item in products)
    {
        if (item is Toy toy && toy.FactoryLocation.Equals("Україна", StringComparison.OrdinalIgnoreCase))
        {
            toy.DisplayInfo();
            Console.WriteLine(new string('-', 30));
        }
    }

    Console.WriteLine("\n=== 2. ІГРАШКИ ДЛЯ ДІТЕЙ СТАРШЕ 4 РОКІВ ===");
    foreach (var item in products)
    {
        if (item is Toy toy && int.TryParse(toy.AgeGroup, out int age) && age > 4)
        {
            toy.DisplayInfo();
            Console.WriteLine(new string('-', 30));
        }
    }
    
    Console.ReadLine();
    }
}