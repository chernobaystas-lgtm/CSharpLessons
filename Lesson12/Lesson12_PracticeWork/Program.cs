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


public struct FullName
{
  public string? Name{get;set;}
  public string? LastName{get;set;}
  public string? SecondName{get;set;}

  public FullName(string? Name, string? LastName, string? SecondName)
  {
    this.Name = Name;
    this.LastName = LastName;
    this.SecondName = SecondName;
  }

public void Input()
    {
        Write("Write name: ");
        this.Name = ReadLine();

        Write("Write last name: ");
        this.LastName = ReadLine();

        Write("Write second name: ");
        this.SecondName = ReadLine();
    }

    public override string ToString()
    {
        return $" Last name: {LastName}, Name: {Name}, Second name: {SecondName}";
    }

}


public record class Employer
{
  private int? _Id;
  private FullName? _Name;
  private string? _Role;

  private double? _Salary;

  private string? _Gmail;

public Employer()
    {
      _Id = 0;
        _Name = new FullName("No name", "No last name", "No second name");
        _Role = "Unassigned";
        _Salary = 0.0;
        _Gmail = "example@gmail.com";
    }

    public Employer(FullName? name) : this()
    {
        _Name = name;
    }

    public Employer(FullName? name, string? role) : this(name)
    {
        _Role = role;
    }

    public Employer(FullName? name, string? role, double? salary) : this(name, role)
    {
        _Salary = salary;
    }

    public Employer(FullName? name, string? role, double? salary, string? gmail) : this(name, role, salary)
    {
        _Gmail = gmail;
    }
       public Employer(FullName? name, string? role, double? salary, string? gmail, int? id) : this(name, role, salary, gmail)
    {
          _Id = id;
    }

    public int? Id => _Id;

    public FullName? Name => _Name;
    public string? Role => _Role;
    public double? Salary => _Salary;
    public string? Gmail => _Gmail;

    public virtual bool Equals(Employer? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return _Id == other._Id &&
               EqualityComparer<FullName?>.Default.Equals(_Name, other._Name) &&
               _Role == other._Role &&
               Nullable.Equals(_Salary, other._Salary) &&
               _Gmail == other._Gmail;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_Id, _Name, _Role, _Salary, _Gmail);
    }

    public override string ToString()
    {
        return $"ID: [{_Id}] | {_Name} | Role: {_Role} | Salary: {_Salary}$ | Email: {_Gmail}";
    }

}

public class EmployerManager
{
  private List<Employer> _employers = new List<Employer>();


public void AddEmployer(Employer employer)
{
    if (_employers == null) return;
    this._employers.Add(employer);
}

public bool RemoveEmployer(Func<Employer, bool> condition)
{
    if (_employers == null || condition == null) return false;

    Employer? target = _employers.Find(new Predicate<Employer>(condition));

    if (target != null)
    {
        return _employers.Remove(target);
    }

    return false;
}



public Employer? SearchById(int targetId)
{
    foreach (var emp in _employers)
    {
        if (emp.Id == targetId)
        {
            return emp;
        }
    }

    return null;
}

public Employer? ChangingEmpInfo(int targetId)
{
    for (int i = 0; i < _employers.Count; i++)
    {
        if (_employers[i].Id == targetId)
        {

            string? currentName = _employers[i].Name?.Name;
            string? currentLastName = _employers[i].Name?.LastName;
            string? currentSecondName = _employers[i].Name?.SecondName;
            string? currentRole = _employers[i].Role;
            double? currentSalary = _employers[i].Salary;
            string? currentGmail = _employers[i].Gmail;
            int currentId = _employers[i].Id ?? 0;


            WriteLine($"\nСотрудник найден: {_employers[i]}");
            PrintMenu();
            WriteLine("Что будем редактировать? ");
            Write("\nВаш выбор: ");
            int choice;
            while (!int.TryParse(ReadLine(), out choice))
            {
                Write("Ошибка! Введите корректное число: ");
            }

            if (choice == 0) return _employers[i];

            switch (choice)
            {
                case 1:
                    Write("Введите новое имя: ");
                    currentName = ReadLine();
                    break;
                case 2:
                    Write("Введите новое отчество (Second Name): ");
                    currentSecondName = ReadLine();
                    break;
                case 3:
                    Write("Введите новую фамилию (Last Name): ");
                    currentLastName = ReadLine();
                    break;
                case 4:
                    Write("Введите новую должность (Role): ");
                    currentRole = ReadLine();
                    break;
                case 5:
                    Write("Введите новую зарплату (Salary): ");
                    double salary;
                    while (!double.TryParse(ReadLine(), out salary))
                    {
                        Write("Ошибка! Введите корректное число для зарплаты: ");
                    }
                    currentSalary = salary;
                    break;
                case 6:
                    Write("Введите новый Gmail: ");
                    currentGmail = ReadLine();
                    break;
                case 7:
                    Write("Введите новый ID: ");
                    while (!int.TryParse(ReadLine(), out currentId))
                    {
                        Write("Ошибка! Введите корректное число для ID: ");
                    }
                    break;
                default:
                    WriteLine("Неверный пункт меню. Изменения отменены.");
                    return _employers[i];
            }

            FullName updatedFullName = new FullName(currentName, currentLastName, currentSecondName);

           Employer updatedEmployer = new Employer(updatedFullName, currentRole, currentSalary, currentGmail, currentId);


            _employers[i] = updatedEmployer;

            WriteLine("Информация успешно изменена!");
        }
    }

    WriteLine($"Сотрудник с ID {targetId} не найден.");
    return null;
}



  public void PrintMenu()
  {
    Write("1.Name \n" +
    "2. Second Name\n" +
    "3. Last Name\n" +
    "4. Role\n" +
    "5. Salary\n"+
    "6. Gmail\n" +
    "7. ID\n" +
    "0. Back\n"
    );
  }


// Сортировка сотрудников по возрастанию ID методом вставок (Insertion sort)
public void SortByIdInsertion()
{
    if (_employers.Count <= 1) return;

    for (int i = 1; i < _employers.Count; i++)
    {
        Employer key = _employers[i];
        int j = i - 1;

        while (j >= 0 && (_employers[j].Id ?? 0) > (key.Id ?? 0))
        {
            _employers[j + 1] = _employers[j];
            j = j - 1;
        }

        _employers[j + 1] = key;
    }

    WriteLine("Список сотрудников отсортирован по ID методом Insertion Sort!");
}

// 1. Вывод ВСЕХ сотрудников (убираем старый PrintAll и заменяем этим)
public void PrintAll()
{
    if (_employers.Count == 0)
    {
        WriteLine("Список сотрудников пуст.");
        return;
    }

    foreach (var emp in _employers)
    {
        if (emp != null)
        {
            WriteLine(emp);
        }
    }
}

// 2. Поиск и вывод ОДНОГО конкретного сотрудника по ID
public void PrintById(int targetId)
{
    Employer? emp = SearchById(targetId);

    if (emp != null)
    {
        WriteLine($"Найден сотрудник с ID [{targetId}]: {emp}");
    }
    else
    {
        WriteLine($"Ошибка: Сотрудник с ID [{targetId}] не зарегистрирован в базе.");
    }
}


}
