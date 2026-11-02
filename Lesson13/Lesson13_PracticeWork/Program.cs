namespace LessonsMainCSharp;
using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main(string[] args)
    {
       OutputEncoding = Encoding.UTF8;

     var firms = new Firm[]
        {
            new("Tech IT Solutions", new DateTime(2020, 10, 10), "IT", "Bob Black", 250, "London", new Employee[] {
                new("Lionel Messi", "Developer", "23-456-78", "leo@tech.com", 5000),
                new("John Doe", "Manager", "55-123-45", "john@tech.com", 3500)
            }),
            new("Smart Marketing", new DateTime(2023, 1, 1), "Marketing", "Alice Green", 80, "New York", new Employee[] {
                new("Lionel Richie", "Manager", "23-999-11", "di_richie@smart.com", 4200),
                new("Anna Smith", "Designer", "44-888-22", "anna@smart.com", 2800)
            })
        };


        WriteLine($"1. Сотрудники Tech IT Solutions: {string.Join(", ", from e in firms.GetEmployees("Tech IT Solutions") select e.FullName)}");
        WriteLine($"2. ЗП > 4000 в Tech IT: {string.Join(", ", from e in firms.GetEmployeesWithHighSalary("Tech IT Solutions", 4000) select e.FullName)}");
        WriteLine($"3. Все Менеджеры: {string.Join(", ", from e in firms.GetEmployeesByPosition("Manager") select e.FullName)}");
        WriteLine($"4. Телефон на 23: {string.Join(", ", from e in firms.GetEmployeesByPhonePrefix("23") select e.FullName)}");
        WriteLine($"5. Email на 'di': {string.Join(", ", from e in firms.GetEmployeesByEmailPrefix("di") select e.FullName)}");
        WriteLine($"6. С именем Lionel: {string.Join(", ", from e in firms.GetEmployeesByName("Lionel") select e.FullName)}");


    }
}


public record Employee(string FullName, string Position, string Phone, string Email, decimal Salary);

public record Firm(string Name, DateTime FoundationDate, string Profile, string DirectorFullName, int EmployeesCount, string Address, Employee[] Employees);


    public static class FirmExtensions
{
    public static IEnumerable<Employee> GetEmployees(this IEnumerable<Firm> firms, string firmName) =>
        from f in firms
        where string.Equals(f.Name, firmName, StringComparison.OrdinalIgnoreCase)
        from e in f.Employees
        select e;

    public static IEnumerable<Employee> GetEmployeesWithHighSalary(this IEnumerable<Firm> firms, string firmName, decimal minSalary) =>
        from e in firms.GetEmployees(firmName)
        where e.Salary > minSalary
        select e;

    public static IEnumerable<Employee> GetEmployeesByPosition(this IEnumerable<Firm> firms, string position) =>
        from f in firms
        from e in f.Employees
        where string.Equals(e.Position, position, StringComparison.OrdinalIgnoreCase)
        select e;

    public static IEnumerable<Employee> GetEmployeesByPhonePrefix(this IEnumerable<Firm> firms, string prefix) =>
        from f in firms
        from e in f.Employees
        where e.Phone.StartsWith(prefix, StringComparison.Ordinal)
        select e;

    public static IEnumerable<Employee> GetEmployeesByEmailPrefix(this IEnumerable<Firm> firms, string prefix) =>
        from f in firms
        from e in f.Employees
        where e.Email.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
        select e;

    public static IEnumerable<Employee> GetEmployeesByName(this IEnumerable<Firm> firms, string name) =>
        from f in firms
        from e in f.Employees
        where e.FullName.StartsWith(name, StringComparison.OrdinalIgnoreCase)
        select e;
}