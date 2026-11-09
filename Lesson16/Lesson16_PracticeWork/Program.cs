namespace LessonsMainCSharp;


class Program
{
  static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Write("Введіть шлях до оригінальної папки: ");
                string sourceDir = ReadLine();

                Write("Введіть шлях, куди потрібно перемістити: ");
                string destDir = ReadLine();

                if (Directory.Exists(sourceDir) && !string.IsNullOrWhiteSpace(destDir))
                {
                    try
                    {
                        Directory.Move(sourceDir, destDir);
                        Write("Папку успішно переміщено!");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Write($"Помилка при переміщенні: {ex.Message}");
                    }
                }
                else
                {
                    Write("Помилка: Оригінальна папка не знайдена або невірні шляхи. Спробуйте ще раз.\n");
                }
            }

            ReadKey();
        }
}


