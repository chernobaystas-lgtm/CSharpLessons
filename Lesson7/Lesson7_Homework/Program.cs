using System;


class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Money jpy = new Money(15000, 0, "Японська єна", "сен", 150.25);
        Money cny = new Money(540, 50, "Китайський юань", "фень", 7.24);
        Money mxn = new Money(850, 75, "Мексиканське песо", "сентаво", 17.10);
        Money zar = new Money(2300, 20, "Південноафриканський ранд", "цент", 19.05);
        Money usd = new Money(100, 0, "Долар США", "цент", 1.0);

        Money[] wallet = new Money[] { jpy, cny, mxn, zar, usd };

        Console.WriteLine("=== ХАРАКТЕРИСТИКИ ТА СПИСОК УСІХ ВАЛЮТ ===");
        foreach (Money currency in wallet)
        {
            Console.WriteLine(currency.ToString());
        }

        Console.WriteLine("\n=== АВТОМАТИЧНЕ СПІВВІДНОШЕННЯ ВАЛЮТ (КРОС-КОНВЕРТАЦІЯ) ===");
        
        cny.ConvertTo(jpy);

        zar.ConvertTo(mxn);

        Console.WriteLine("\n=== РОБОТА З КЛАСОМ PRODUCT (ТОВАРИ) ===");
        
        Product smartphone = new Product("Смартфон", 3000, 95, "Китайський юань", "фень", 7.24);
        Console.WriteLine(smartphone.ToString());

        Console.WriteLine("\nЗастосовуємо знижку 450.50 юанів...");
        smartphone.ReducePrice(450.50);
        Console.WriteLine(smartphone.ToString());

        Console.ReadLine();
    }
}