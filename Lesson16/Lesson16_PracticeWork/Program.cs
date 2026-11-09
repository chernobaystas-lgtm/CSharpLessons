namespace LessonsMainCSharp;


class Program
{
  static void Main(string[] args)
  {
    OutputEncoding = Encoding.UTF8;

    while (true)
    {
      Write("Введіть шлях до оригінальної папки: ");
      string? directoryPath = ReadLine();

      Write("Введіть шлях, куди потрібно перемістити: ");
      string? directoryPath2 = ReadLine();

      Write("Введіть ім'я файлу: ");
      string? filePart1 = ReadLine();

      if (Directory.Exists(directoryPath) && Directory.Exists(directoryPath2) &&
          File.Exists(Combine(directoryPath, filePart1)) &&
          !File.Exists(Combine(directoryPath2, filePart1)))
          {
            try
            {
              File.Move(Combine(directoryPath, filePart1), Combine(directoryPath2, filePart1));
              WriteLine($"Файл {filePart1} успішно переміщено.");
            }
            catch (Exception ex)
            {
              WriteLine($"Помилка при переміщенні: {ex.Message}");
            }
            }
            else
            {
                    WriteLine("Помилка: Неправильні шляхи, файл не знайдено або він вже існує в цільовій папці. Спробуйте ще раз.\n");
            }
    }

    Collect();
    WaitForPendingFinalizers();
    WriteLine("\n[Система] Пам'ять успішно оптимізована та очищена.");
    ReadKey();
  }
}


