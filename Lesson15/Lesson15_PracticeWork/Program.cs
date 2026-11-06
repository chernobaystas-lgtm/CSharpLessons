namespace LessonsMainCSharp;
using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Linq;
using static System.GC;


class Program
{
static void Main(string[] args)
{
    OutputEncoding = Encoding.UTF8;

    WriteLine("=== ЕТАП 1: РУЧНЕ УПРАВЛІННЯ (БЛОК USING) ===");
    using (Spectacle play1 = Spectacle.Create("Гамлет", "Національний театр", "Трагедія", 150, new List<string> { "Бенедикт Камбербетч", "Том Хіддлстон" }))
    {
        WriteLine($"Успішно створено виставу: {play1.ToString()}");
        WriteLine("Зараз блок using закриється...");
    }

    WriteLine("\n=== ЕТАП 2: АВТОМАТИЧНЕ ЗБИРАННЯ СМІТТЯ (GC) ===");
    CreateAndForgetSpectacle();

    WriteLine("Об'єкт втратив посилання і просто залишений у пам'яті.");
    WriteLine("Примусово кличемо Сборщик мусора...");

    Collect();
    WaitForPendingFinalizers();

    WriteLine("\n=== Збирання сміття завершено, програма завершує роботу ===");
}

static void CreateAndForgetSpectacle()
{
    Spectacle play2 = Spectacle.Create("Ромео і Джульєтта", "Театр Драми", "Мелодрама", 120, new List<string> { "Актор 1", "Актор 2" });
    WriteLine($"Створено другу виставу всередині методу: {play2.ToString()}");
}

}

