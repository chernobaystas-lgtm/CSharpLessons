using LessonsMainCSharp;

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;


        string filePath = "magazines.json";

        Write("Сколько журналов вы хотите создать? ");
        int count;
        while (!int.TryParse(ReadLine(), out count) || count <= 0)
        {
            Write("Введите корректное число (больше 0): ");
        }

        Magazine[] magazines = new Magazine[count];

        for (int i = 0; i < count; i++)
        {
            WriteLine($"\n=== Ввод данных для журнала #{i + 1} ===");
            magazines[i] = new Magazine();
            magazines[i].Input();
        }

        WriteLine("\n ВВЕДЕННЫЕ ЖУРНАЛЫ ");
        foreach (var mag in magazines)
        {
            WriteLine(mag);
        }

        Magazine.SaveArrayToJSON(magazines, filePath);
        WriteLine($"\nМассив журналов успешно сохранен в файл {filePath}!");

        WriteLine("\n ЗАГРУЖЕНО ИЗ ФАЙЛА ");
        Magazine.PrintArrayFromJSON(filePath);
    }
}



