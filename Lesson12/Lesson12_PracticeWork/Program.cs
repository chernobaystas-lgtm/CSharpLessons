namespace LessonsMainCSharp;
using System.ComponentModel;
using static System.Console;
using static System.Convert;
using static System.Array;
using static System.Exception;
using static System.Collections.ArrayList;
using System.Collections;

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = System.Text.Encoding.UTF8;

        EmployerManager manager = new EmployerManager();

        Employer emp1 = new Employer(id: 105, name: new FullName("Иван", "Иванов", "Иванович"), role: "Developer", salary: 2500.0, gmail: "ivan@gmail.com");
        Employer emp2 = new Employer(id: 101, name: new FullName("Петр", "Петров", "Петрович"), role: "Manager", salary: 1800.0, gmail: "petr@gmail.com");
        Employer emp3 = new Employer(id: 103, name: new FullName("Анна", "Сидорова", "Александровна"), role: "QA Engineer", salary: 2000.0, gmail: "anya@gmail.com");

        manager.AddEmployer(emp1);
        manager.AddEmployer(emp2);
        manager.AddEmployer(emp3);

        WriteLine("--- Исходный список сотрудников ---");
        manager.PrintAll();

        int searchId = 101;
        Employer? found = manager.SearchById(searchId);
        WriteLine($"\nПоиск ID {searchId}: {(found != null ? found.ToString() : "Не найден")}");

        manager.SortByIdInsertion();
        WriteLine("\n--- Список после сортировки по ID ---");
        manager.PrintAll();

        int idToChange = 103;
        manager.ChangingEmpInfo(idToChange);
        WriteLine("\n--- Список после изменения данных ---");
        manager.PrintAll();

        string targetLastName = "Петров";
        manager.RemoveEmployer(emp => emp.Name?.LastName == targetLastName);
        WriteLine($"\n--- Финальный список после удаления ({targetLastName}) ---");
        manager.PrintAll();

        ReadLine();
    }
}

