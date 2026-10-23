using System.Text;
using System.Text.Unicode;
using static System.Console;


namespace Lesson9_PracticeWork
{
public static class StringExtensions
{
    public static int CountSentences(this string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return 0;

        int count = 0;

        for (int i = 0; i < text.Length; i++)
        {
            char current = text[i];

            if (current == '.' || current == '!' || current == '?')
            {
                if (i == text.Length - 1)
                {
                    count++;
                }
                else
                {
                    char next = text[i + 1];
                    if (next != '.' && next != '!' && next != '?')
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }
}

class Program
{
    public static void inputtoIntArr(int[] arr, int how_much)
    {
        for (int i = 0; i < how_much; ++i)
        {
            Console.Write("Write something message: ");
            arr[i] = Convert.ToInt16(ReadLine());
        }
    }

    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;
        InputEncoding = Encoding.UTF8;

        Write("how much el will be in array: ");
        int number = Convert.ToInt16(ReadLine());
        int[] arr = new int[number];
        inputtoIntArr(arr, number);

        Func<int, bool> isOdd = x => x % 2 != 0;

        WriteLine("\nНепарні числа з масиву:");

        foreach (int el in arr)
        {
            if (isOdd(el))
            {
                WriteLine(el);
            }
        }

        WriteLine("\n--- Тестування Задання 7 ---");

        string testText = "Привіт! Як твої справи? Це текст для перевірки роботи методу... Сподіваюсь, все ок.";

        int sentences = testText.CountSentences();

        WriteLine($"Текст: {testText}");
        WriteLine($"Кількість речень у тексті: {sentences}");
    }
}
}