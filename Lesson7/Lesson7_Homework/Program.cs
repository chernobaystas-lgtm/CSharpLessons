using System;


class Program
{
 static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Device[] devices = new Device[]
        {
            new Teapot("Електричний Чайник Tefal", "Швидко кип'ятить воду", 1500.00, "Білий", DeviceStatus.New, 1.7),
            new Microwave("Мікрохвильовка Samsung", "Має режим грилю", 4500.00, "Чорний", DeviceStatus.Used, 800),
            new Car("Спорткар Porsche", "Швидкісний автомобіль", 3200000.00, "Червоний", DeviceStatus.Refurbished, "Бензин"),
            new Steamboat("Вантажний Пароплав", "Перевозить контейнери", 85000000.00, "Сірий", DeviceStatus.Broken, 150.5)
        };

        Console.WriteLine("=== ДЕМОНСТРАЦІЯ РОБОТИ ОНОВЛЕНИХ ПРИСТРОЇВ ===");
        foreach (Device dev in devices)
        {
            dev.Show();       
            dev.Desc();       
            dev.Sound();      
            Console.WriteLine($"ToString: {dev.ToString()}"); 
            Console.WriteLine(new string('-', 60));
        }

        Console.WriteLine("\n=== ПЕРЕВІРКА МЕТОДУ EQUALS ===");
        Car car1 = new Car("Tesla Model S", "Електрокар", 2500000.00, "Білий", DeviceStatus.New, "Електрика");
        Car car2 = new Car("Tesla Model S", "Електрокар", 2500000.00, "Білий", DeviceStatus.New, "Електрика");
        Console.WriteLine($"Чи однакові автомобілі car1 та car2? Відповідь: {car1.Equals(car2)}");

        Console.ReadLine();
    }
}