namespace LessonsMainCSharp;
using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Linq;


class Program
{
    static void Main(string[] args)
    {
       OutputEncoding = Encoding.UTF8;

Firm[] firms = new Firm[]
{
    new Firm("FastFood Inc.", new DateTime(2015, 5, 20), "Food", "John White", 150, "London"),
    new Firm("Smart Marketing", DateTime.Now.AddYears(-3), "Marketing", "Alice Green", 80, "New York"),
    new Firm("Tech IT Solutions", new DateTime(2020, 10, 10), "IT", "Bob Black", 250, "London"),
    new Firm("White Media Group", DateTime.Now.AddDays(-123), "Marketing", "Charlie Black", 45, "Paris"),
    new Firm("Food & Beverage Co.", DateTime.Now.AddMonths(-6), "Food", "David White", 310, "Berlin"),
    new Firm("IT Global", new DateTime(2018, 1, 1), "IT", "Emma Watson", 120, "London")
};


// 1. Отримати інформацію про всі фірми.
(from f in firms select f).ToList().ForEach(f => WriteLine(f));

// 2. Отримати фірми, які мають у назві слово «Food».
(from f in firms where f.Name.Contains("Food") select f).ToList().ForEach(f => WriteLine(f));

// 3. Отримати фірми, які працюють у галузі маркетингу.
(from f in firms where f.Profile == "Marketing" select f).ToList().ForEach(f => WriteLine(f));

// 4. Отримати фірми, які працюють у галузі маркетингу або IT.
(from f in firms where f.Profile == "Marketing" || f.Profile == "IT" select f).ToList().ForEach(f => WriteLine(f));

// 5. Отримати фірми з кількістю працівників більшою, ніж 100.
(from f in firms where f.EmployeesCount > 100 select f).ToList().ForEach(f => WriteLine(f));

// 6. Отримати фірми з кількістю працівників у діапазоні від 100 до 300.
(from f in firms where f.EmployeesCount >= 100 && f.EmployeesCount <= 300 select f).ToList().ForEach(f => WriteLine(f));

// 7. Отримати фірми, які знаходяться в Лондоні.
(from f in firms where f.Address == "London" select f).ToList().ForEach(f => WriteLine(f));

// 8. Отримати фірми, в яких прізвище директора White.
(from f in firms where f.DirectorFullName.EndsWith("White") select f).ToList().ForEach(f => WriteLine(f));

// 9. Отримати фірми, які засновані більше двох років тому.
(from f in firms where f.FoundationDate < DateTime.Now.AddYears(-2) select f).ToList().ForEach(f => WriteLine(f));

// 10. Отримати фірми з дня заснування яких минуло 123 дні.
(from f in firms where (DateTime.Now.Date - f.FoundationDate.Date).Days == 123 select f).ToList().ForEach(f => WriteLine(f));

// 11. Отримати фірми, в яких прізвище директора Black і мають у назві фірми слово «White».
(from f in firms where f.DirectorFullName.EndsWith("Black") && f.Name.Contains("White") select f).ToList().ForEach(f => WriteLine(f));



    }
}

public record Firm(string Name, DateTime FoundationDate, string Profile, string DirectorFullName, int EmployeesCount, string Address)
{

}
