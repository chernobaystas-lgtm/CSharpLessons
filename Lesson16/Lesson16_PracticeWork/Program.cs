namespace LessonsMainCSharp;


class Program
{
 static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Write("Введіть шлях до початкової папки: ");
                string sourceDir = ReadLine();

                Write("Введіть шлях, куди потрібно скопіювати: ");
                string destDir = ReadLine();

                if (Directory.Exists(sourceDir) && !string.IsNullOrWhiteSpace(destDir))
                {
                    try
                    {
                        CopyDirectory(sourceDir, destDir);
                        WriteLine("Папку та весь її вміст успішно скопійовано!");
                        break;
                    }
                    catch (Exception ex)
                    {
                        WriteLine($"Помилка при копіюванні: {ex.Message}");
                    }
                }
                else
                {
                    WriteLine("Помилка: Початкова папка не знайдена або невірні шляхи. Спробуйте ще раз.\n");
                }
            }

            Collect();
            WaitForPendingFinalizers();
            WriteLine("\n[Система] Пам'ять оптимізована.");

            ReadKey();
        }

        static void CopyDirectory(string source, string dest)
        {
            if (!Directory.Exists(dest))
            {
                CreateDirectory(dest);
            }

            foreach (string file in GetFiles(source))
            {
                string name = GetFileName(file);
                string destFile = Combine(dest, name);
                Copy(file, destFile, true);
            }

            foreach (string subDir in GetDirectories(source))
            {
                string name = GetFileName(subDir);
                string destSubDir = Combine(dest, name);
                CopyDirectory(subDir, destSubDir);
            }
        }
}


