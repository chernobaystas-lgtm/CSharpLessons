namespace LessonsMainCSharp;


class Program
{
  static void Main(string[] args)
  {
    OutputEncoding = Encoding.UTF8;

while (true)
{
    Write("Введіть шлях до файлу(або 0 для виходу): ");
    string? FilePath = ReadLine();

    if (FilePath == "0")
    {
        break;
    }
    if(!File.Exists(FilePath))
    {
        WriteLine("[Система] Файл не знайдено. Будь ласка, перевірте шлях та спробуйте ще раз. невдача");
        continue;
    }
    string content = ReadAllText(FilePath);
    WriteLine(content);
    break;
}

    ReadKey();
  }
}


