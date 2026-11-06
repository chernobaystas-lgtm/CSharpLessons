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

        using (Film movie1 = Film.Create("Інтерстеллар", 2014, "Кристофер Нолан", DateTime.Now, "Фантастика"))
        {
            WriteLine($"Успешно создали первый фильм: {movie1.ToString()}");
            WriteLine("Сейчас блок using закроется...");
        }

        CreateAndForgetFilm();

        WriteLine("Объект потерял ссылку и просто брошен в памяти.");
        WriteLine("Принудительно будим Сборщик мусора (GC.Collect)...");

        Collect();
        WaitForPendingFinalizers();

        WriteLine("\n=== Сборка мусора завершена, программа выходит из Main ===");
    }

    static void CreateAndForgetFilm()
    {
        Film movie2 = Film.Create("Аватар", 2009, "Джеймс Кэмерон", DateTime.Now, "Фантастика");
        WriteLine($"Создали второй фильм внутри метода: {movie2.ToString()}");
    }
}






