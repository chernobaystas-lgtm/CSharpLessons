using System;
using System.Collections.Generic;

class Program
{
    // Dictionary, де ключ - англійське слово, а значення - список французьких перекладів
    static Dictionary<string, List<string>> Dictionary = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n--- Англо-французький словник ---");
            Console.WriteLine("1. Додати слово та варіанти перекладу");
            Console.WriteLine("2. Видалити слово зі словника");
            Console.WriteLine("3. Видалити конкретний варіант перекладу");
            Console.WriteLine("4. Змінити саме слово");
            Console.WriteLine("5. Змінити варіант перекладу");
            Console.WriteLine("6. Пошук перекладу слова");
            Console.WriteLine("7. Показати весь словник");
            Console.WriteLine("8. Вихід");
            Console.Write("Виберіть дію: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddWord(); break;
                case "2": DeleteWord(); break;
                case "3": DeleteTranslation(); break;
                case "4": UpdateWord(); break;
                case "5": UpdateTranslation(); break;
                case "6": SearchTranslation(); break;
                case "7": ShowDictionary(); break;
                case "8": running = false; break;
                default: Console.WriteLine("Некоректний вибір."); break;
            }
        }
    }

    // 1. Додати слово і варіанти перекладу
    static void AddWord()
    {
        Console.Write("Введіть англійське слово: ");
        string word = Console.ReadLine().Trim();

        if (string.IsNullOrEmpty(word)) return;

        Console.Write("Введіть варіанти перекладу через кому: ");
        string inputTranslations = Console.ReadLine();

        // Ручний розбір рядка без LINQ
        string[] rawTranslations = inputTranslations.Split(',');
        List<string> cleanTranslations = new List<string>();

        foreach (string t in rawTranslations)
        {
            string trimmed = t.Trim();
            if (!string.IsNullOrEmpty(trimmed))
            {
                cleanTranslations.Add(trimmed);
            }
        }

        if (Dictionary.ContainsKey(word))
        {
            // Якщо слово вже є, додаємо лише унікальні переклади
            foreach (string t in cleanTranslations)
            {
                bool alreadyExists = false;
                foreach (string existingTranslation in Dictionary[word])
                {
                    if (existingTranslation.Equals(t, StringComparison.OrdinalIgnoreCase))
                    {
                        alreadyExists = true;
                        break;
                    }
                }

                if (!alreadyExists)
                {
                    Dictionary[word].Add(t);
                }
            }
            Console.WriteLine("Нові варіанти перекладу додано до існуючого слова.");
        }
        else
        {
            Dictionary.Add(word, cleanTranslations);
            Console.WriteLine("Слово успішно додано до словника!");
        }
    }

    // 2. Видалити слово
    static void DeleteWord()
    {
        Console.Write("Введіть англійське слово для видалення: ");
        string word = Console.ReadLine().Trim();

        if (Dictionary.Remove(word))
        {
            Console.WriteLine($"Слово '{word}' успішно видалено.");
        }
        else
        {
            Console.WriteLine("Слово не знайдено в словнику.");
        }
    }

    // 3. Видалити варіанти перекладу
    static void DeleteTranslation()
    {
        Console.Write("Введіть англійське слово: ");
        string word = Console.ReadLine().Trim();

        if (Dictionary.TryGetValue(word, out List<string> translations))
        {
            Console.Write("Введіть французький переклад, який треба видалити: ");
            string toDelete = Console.ReadLine().Trim();

            string found = null;
            // Ручний пошук елемента без LINق
            foreach (string t in translations)
            {
                if (t.Equals(toDelete, StringComparison.OrdinalIgnoreCase))
                {
                    found = t;
                    break;
                }
            }

            if (found != null)
            {
                translations.Remove(found);
                Console.WriteLine("Варіант перекладу видалено.");

                if (translations.Count == 0)
                {
                    Dictionary.Remove(word);
                    Console.WriteLine("Оскільки перекладів більше немає, слово видалено зі словника.");
                }
            }
            else
            {
                Console.WriteLine("Такого варіанту перекладу не знайдено.");
            }
        }
        else
        {
            Console.WriteLine("Слово не знайдено.");
        }
    }

    // 4. Зміна слова
    static void UpdateWord()
    {
        Console.Write("Введіть англійське слово, яке хочете змінити: ");
        string oldWord = Console.ReadLine().Trim();

        if (!Dictionary.ContainsKey(oldWord))
        {
            Console.WriteLine("Слово не знайдено.");
            return;
        }

        Console.Write("Введіть нове написання слова: ");
        string newWord = Console.ReadLine().Trim();

        if (string.IsNullOrEmpty(newWord)) return;

        if (Dictionary.ContainsKey(newWord) && !oldWord.Equals(newWord, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Таке слово вже є в словнику.");
            return;
        }

        List<string> translations = Dictionary[oldWord];
        Dictionary.Remove(oldWord);
        Dictionary.Add(newWord, translations);
        Console.WriteLine("Слово успішно змінено.");
    }

    // 5. Зміна варіанта перекладу
    static void UpdateTranslation()
    {
        Console.Write("Введіть англійське слово: ");
        string word = Console.ReadLine().Trim();

        if (Dictionary.TryGetValue(word, out List<string> translations))
        {
            Console.Write("Введіть французький переклад, який хочете змінити: ");
            string oldTranslation = Console.ReadLine().Trim();

            int index = -1;
            // Ручний пошук індексу елемента
            for (int i = 0; i < translations.Count; i++)
            {
                if (translations[i].Equals(oldTranslation, StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                Console.Write("Введіть новий переклад: ");
                string newTranslation = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(newTranslation))
                {
                    translations[index] = newTranslation;
                    Console.WriteLine("Переклад успішно змінено.");
                }
            }
            else
            {
                Console.WriteLine("Такого перекладу не знайдено.");
            }
        }
        else
        {
            Console.WriteLine("Слово не знайдено.");
        }
    }

    // 6. Пошук перекладу слова
    static void SearchTranslation()
    {
        Console.Write("Введіть англійське слово для пошуку перекладу: ");
        string word = Console.ReadLine().Trim();

        if (Dictionary.TryGetValue(word, out List<string> translations))
        {
            // Ручне об'єднання рядків через кому
            string result = "";
            for (int i = 0; i < translations.Count; i++)
            {
                result += translations[i];
                if (i < translations.Count - 1)
                {
                    result += ", ";
                }
            }
            Console.WriteLine($"Переклад для '{word}': {result}");
        }
        else
        {
            Console.WriteLine("Переклад для цього слова не знайдено.");
        }
    }

    // Додатковий метод для виведення всього словника
    static void ShowDictionary()
    {
        if (Dictionary.Count == 0)
        {
            Console.WriteLine("Словник порожній.");
            return;
        }

        Console.WriteLine("\nВміст словника:");
        foreach (KeyValuePair<string, List<string>> pair in Dictionary)
        {
            string result = "";
            for (int i = 0; i < pair.Value.Count; i++)
            {
                result += pair.Value[i];
                if (i < pair.Value.Count - 1)
                {
                    result += ", ";
                }
            }
            Console.WriteLine($"{pair.Key} -> {result}");
        }
    }
}
