using System.Text;
using System.Text.Unicode;
using static System.Console;


namespace Lesson9_PracticeWork
{
    class Program
    {
static void Main(string[] args)
{
    WriteLine("--- Testing Lambda for Word Verification ---");

    Func<string, string, bool> checkWord = (text, word) => 
        text.Contains(word, StringComparison.OrdinalIgnoreCase);

    // Исходные данные для проверки
    string textToTest = "Hello world, we are learning advanced C# programming today.";
    string wordToFind1 = "programming";
    string wordToFind2 = "Java";

    WriteLine($"Source text: \"{textToTest}\"");

    // Тест 1: Слово есть в тексте
    bool result1 = checkWord(textToTest, wordToFind1);
    WriteLine($"Is the word \"{wordToFind1}\" in text? Answer: {result1}");

    // Тест 2: Слова нет в тексте
    bool result2 = checkWord(textToTest, wordToFind2);
    WriteLine($"Is the word \"{wordToFind2}\" in text? Answer: {result2}");
}




    }
}
