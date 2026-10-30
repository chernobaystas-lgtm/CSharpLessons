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