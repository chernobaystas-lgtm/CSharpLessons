namespace LessonsMainCSharp;
using System.ComponentModel;
using static System.Console;
using static System.Convert;
using static System.Array;
using static System.Exception;
using static System.Collections.ArrayList;
using System.Collections;
public struct AuthorName
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? SecondName { get; set; }

    public AuthorName(string? name, string? lastName, string? secondName)
    {
        Name = name;
        LastName = lastName;
        SecondName = secondName;
    }

    public override string ToString() => $"{LastName} {Name} {SecondName}";
}

// Рекорд книги
public record class Book
{
    public string Title { get; set; }
    public AuthorName Author { get; set; }
    public string Genre { get; set; }
    public int Year { get; set; }

    public Book(string title, AuthorName author, string genre, int year)
    {
        Title = title;
        Author = author;
        Genre = genre;
        Year = year;
    }

    public override string ToString()
    {
        return $"«{Title}» | Автор: {Author} | Жанр: {Genre} | Год: {Year}";
    }
}

// Менеджер коллекции на базе LinkedList<T>
public class BookManager
{
    private LinkedList<Book> _books = new LinkedList<Book>();

    // Вставить книгу в начало списка
    public void AddFirst(Book book) => _books.AddFirst(book);

    // Вставить книгу в конец списка
    public void AddLast(Book book) => _books.AddLast(book);

    // Вставить книгу в определенную позицию
    public bool InsertAt(int index, Book book)
    {
        if (index < 0 || index > _books.Count) return false;
        if (index == 0) { AddFirst(book); return true; }
        if (index == _books.Count) { AddLast(book); return true; }

        var current = _books.First;
        for (int i = 0; i < index; i++) current = current!.Next;

        _books.AddBefore(current!, book);
        return true;
    }

    // Удалить книгу с начала списка
    public bool RemoveFirst()
    {
        if (_books.Count == 0) return false;
        _books.RemoveFirst();
        return true;
    }

    // Удалить книгу с конца списка
    public bool RemoveLast()
    {
        if (_books.Count == 0) return false;
        _books.RemoveLast();
        return true;
    }

    // Удалить книгу из определенной позиции
    public bool RemoveAt(int index)
    {
        if (index < 0 || index >= _books.Count) return false;
        if (index == 0) return RemoveFirst();
        if (index == _books.Count - 1) return RemoveLast();

        var current = _books.First;
        for (int i = 0; i < index; i++) current = current!.Next;

        _books.Remove(current!);
        return true;
    }

    // Изменить информацию о книге
    public bool UpdateBook(int index, Book newDetails)
    {
        if (index < 0 || index >= _books.Count) return false;

        var current = _books.First;
        for (int i = 0; i < index; i++) current = current!.Next;

        current!.Value = newDetails;
        return true;
    }

    // Поиск книг по названию
    public List<Book> SearchByTitle(string title)
    {
        List<Book> result = new List<Book>();
        foreach (var book in _books)
        {
            if (book.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                result.Add(book);
        }
        return result;
    }

    // Вывод всей библиотеки на экран
    public void PrintAll()
    {
        if (_books.Count == 0)
        {
            WriteLine("Библиотека пуста.");
            return;
        }
        int index = 0;
        foreach (var book in _books)
        {
            WriteLine($"[{index}] {book}");
            index++;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = System.Text.Encoding.UTF8;
        BookManager manager = new BookManager();

        // Стартовые дефолтные книги для проверки функционала
        manager.AddLast(new Book("Кобзарь", new AuthorName("Тарас", "Шевченко", "Григорьевич"), "Поэзия", 1840));
        manager.AddLast(new Book("Захар Беркут", new AuthorName("Иван", "Франко", "Яковлевич"), "Повесть", 1883));

        while (true)
        {
            Clear(); // Полная очистка экрана консоли на каждом шаге цикла

            WriteLine("======================================");
            WriteLine("       УЧЕТ КНИГ (LinkedList<T>)      ");
            WriteLine("======================================");
            manager.PrintAll();
            WriteLine("======================================");
            WriteLine("1. Добавить книгу в конец");
            WriteLine("2. Добавить книгу в начало");
            WriteLine("3. Вставить книгу в определенную позицию");
            WriteLine("4. Удалить книгу с начала");
            WriteLine("5. Удалить книгу с конца");
            WriteLine("6. Удалить книгу из определенной позиции");
            WriteLine("7. Изменить информацию о книге");
            WriteLine("8. Поиск книг по названию");
            WriteLine("0. Выход");
            WriteLine("======================================");
            Write("Выберите пункт меню: ");

            if (!int.TryParse(ReadLine(), out int choice)) continue;
            if (choice == 0) break;

            switch (choice)
            {
                case 1:
                case 2:
                    Book newBook = InputBook();
                    if (choice == 1) manager.AddLast(newBook);
                    else manager.AddFirst(newBook);
                    break;

                case 3:
                    Write("Введите индекс для вставки: ");
                    int.TryParse(ReadLine(), out int insIdx);
                    Book insBook = InputBook();
                    if (!manager.InsertAt(insIdx, insBook)) WriteLine("Ошибка: Неверный индекс.");
                    WaitKey();
                    break;

                case 4:
                    if (!manager.RemoveFirst()) WriteLine("Ошибка: Список пуст.");
                    else WriteLine("Книга удалена из начала списка.");
                    WaitKey();
                    break;

                case 5:
                    if (!manager.RemoveLast()) WriteLine("Ошибка: Список пуст.");
                    else WriteLine("Книга удалена из конца списка.");
                    WaitKey();
                    break;

                case 6:
                    Write("Введите индекс для удаления: ");
                    int.TryParse(ReadLine(), out int remIdx);
                    if (!manager.RemoveAt(remIdx)) WriteLine("Ошибка: Неверный индекс.");
                    else WriteLine("Книга успешно удалена.");
                    WaitKey();
                    break;

                case 7:
                    Write("Введите индекс книги для изменения: ");
                    int.TryParse(ReadLine(), out int updIdx);
                    WriteLine("Введите новые характеристики для книги:");
                    Book updBook = InputBook();
                    if (!manager.UpdateBook(updIdx, updBook)) WriteLine("Ошибка: Индекс не найден.");
                    else WriteLine("Информация о книге обновлена.");
                    WaitKey();
                    break;

                case 8:
                    Write("Введите название книги для поиска: ");
                    string search = ReadLine() ?? "";
                    var found = manager.SearchByTitle(search);
                    WriteLine("\n--- Результаты поиска ---");
                    if (found.Count == 0) WriteLine("Книги с таким названием не найдены.");
                    foreach (var b in found) WriteLine(b);
                    WaitKey();
                    break;
            }
        }
    }

    // Ввод характеристик книги с клавиатуры
    static Book InputBook()
    {
        Write("Название книги: ");
        string title = ReadLine() ?? "Без названия";

        Write("Имя автора: ");
        string name = ReadLine() ?? "";
        Write("Фамилия автора: ");
        string lastName = ReadLine() ?? "";
        Write("Отчество автора: ");
        string secName = ReadLine() ?? "";

        Write("Жанр книги: ");
        string genre = ReadLine() ?? "Общий";

        Write("Год издания: ");
        int.TryParse(ReadLine(), out int year);

        return new Book(title, new AuthorName(name, lastName, secName), genre, year);
    }

    // Задержка экрана перед следующей очисткой Clear()
    static void WaitKey()
    {
        WriteLine("\nНажмите любую клавишу для возврата в меню...");
        ReadKey();
    }
}