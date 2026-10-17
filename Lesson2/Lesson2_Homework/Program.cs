using System;
using System.Text;
using static System.Console;

namespace Lesson2_Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 3: шифр Цезаря (шифрування і розшифрування)
            WriteLine("1 - зашифрувати");
            WriteLine("2 - розшифрувати");
            Write("Ваш вибір: ");
            string choice = ReadLine()!;

            if (choice != "1" && choice != "2")
            {
                WriteLine("Помилка: потрібно ввести 1 або 2!");
                return;
            }

            Write("Введіть рядок: ");
            string text = ReadLine()!;

            Write("Введіть зсув (ціле число): ");
            if (!int.TryParse(ReadLine(), out int shift))
            {
                WriteLine("Помилка: зсув має бути цілим числом!");
                return;
            }

            // розшифрування це зсув в протилежний бік
            if (choice == "2")
            {
                shift = -shift;
            }

            WriteLine($"Результат: {Caesar(text, shift)}");
        }

        // Зсуває кожну літеру англійського або українського алфавіту на shift позицій.
        // Регістр зберігається, інші символи (пробіли, цифри, знаки) не змінюються.
        static string Caesar(string text, int shift)
        {
            string[] alphabets =
            {
                "abcdefghijklmnopqrstuvwxyz",
                "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя"
            };

            string result = "";

            for (int i = 0; i < text.Length; i++)
            {
                char symbol = text[i];
                char lower = char.ToLower(symbol);
                bool found = false;

                for (int a = 0; a < alphabets.Length; a++)
                {
                    int position = alphabets[a].IndexOf(lower);

                    if (position != -1)
                    {
                        int length = alphabets[a].Length;
                        int newPosition = ((position + shift) % length + length) % length;
                        char newSymbol = alphabets[a][newPosition];

                        if (char.IsUpper(symbol))
                        {
                            newSymbol = char.ToUpper(newSymbol);
                        }

                        result += newSymbol;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    result += symbol;
                }
            }

            return result;
        }
    }
}