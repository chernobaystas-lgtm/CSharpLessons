using System.Text;
using System.Text.Unicode;
using static System.Console;


namespace Lesson9_PracticeWork
{
    class Program
    {
        public delegate void delPrintMessage(string mes);
         static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;
            Write("Write something message: ");
            string? message = ReadLine();

            delPrintMessage message1;


             message1 = ShowSimple;
            message1(message);

            message1 = ShowUpper;
            message1(message);

            message1 = ShowReversed;
            message1(message);

            message1 = ShowGreen;
            message1(message);

            message1 = ShowWithTime;
            message1(message);

            WriteLine();
            WriteLine("Усі методи разом:");

            message1 = ShowSimple;
            message1 += ShowUpper;
            message1 += ShowReversed;
            message1(message);
        }

    static void ShowSimple(string message)
    {
        WriteLine($"Звичайне: {message}");
    }

    // Великими літерами
    static void ShowUpper(string message)
    {
        WriteLine($"Великими літерами: {message.ToUpper()}");
    }

    // Навпаки
    static void ShowReversed(string message)
    {
        string reversed = "";

        for (int i = message.Length - 1; i >= 0; i--)
        {
            reversed += message[i];
        }

        WriteLine($"Навпаки: {reversed}");
    }

    // Зеленим кольором
    static void ShowGreen(string message)
    {
        ForegroundColor = ConsoleColor.Green;
        WriteLine($"Зеленим: {message}");
        ResetColor();
    }

    // З поточним часом
    static void ShowWithTime(string message)
    {
        WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }
    }
}