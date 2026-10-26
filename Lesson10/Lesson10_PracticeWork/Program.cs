namespace LessonsMainCSharp;
using System.ComponentModel;
using static System.Console;
using static System.Convert;
using static System.Array;
using static System.Exception;

class Program
{
      static void Main(string[] args)
        {
            Person[] people = new Person[3];
            
            people[0] = new Person("Ivan", "Ivanov", 20);
            people[1] = new Person("Anna", "Petrova", 25);
            
            WriteLine("--- Заполнение данных для третьего человека ---");
            people[2] = new Person();
            people[2].Input();

            WriteLine("\n--- Список всех людей ---");
            foreach (var p in people)
            {
                WriteLine(p.ToString());
            }

            WriteLine("\n--- Результаты работы методов расширения ---");
            
            Person youngest = people.FindYounger();
            WriteLine($"Самый молодой: {(youngest != null ? youngest.ToString() : "Нет данных")}");

            Person oldest = people.FindOlder();
            WriteLine($"Самый старший: {(oldest != null ? oldest.ToString() : "Нет данных")}");

            double midAge = people.FindMiddleAge();
            WriteLine($"Средний возраст группы: {midAge:F2}");

            // 3. Тестируем Equals
            WriteLine("\n--- Проверка сравнения (Equals) ---");
            Person clone = new Person(people[0].Name, people[0].LastName, people[0].Age);
            WriteLine($"Первый человек равен своему клону? {people[0].Equals(clone)}");
            WriteLine($"Первый человек равен второму? {people[0].Equals(people[1])}");
            
            ReadLine(); 
        }

}



public class Person
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public Person()
    {
        Name = "No name";
        LastName = "No last name";
        Age = 0;
    }

    public Person(string Name, string LastName, int Age)
    {
        this.Name = Name;
        this.LastName = LastName;
        this.Age = Age;
    }

    public void Input()
    {
        Write("Input name: ");
        Name = ReadLine(); 
        if (string.IsNullOrEmpty(Name))
        {
            Write("No name\n");
        }

        Write("Input last name: ");
        LastName = ReadLine(); 
        if (string.IsNullOrEmpty(LastName))
        {
            Write("No last name\n");
        }

        Write("Input Age: ");
        string ageInput = ReadLine(); 
        
        if (short.TryParse(ageInput, out short parsedAge))
        {
            Age = parsedAge;
        }
        else
        {
            Write("Age is 0\n");
            Age = 0;
        }
    }

        public override string ToString()
    {
        return $"Name: {Name}, Last name: {LastName}, Age: {Age}";
    }


 public override bool Equals(object? obj)
    {
        if (obj is Person other)
        {
            return Name == other.Name && 
                   LastName == other.LastName && 
                   Age == other.Age;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, LastName, Age);
    }

}

public static class ExtensuinsFunc
{
public static Person FindYounger(this Person[] array)
    {
        if (array == null || array.Length == 0) return null;

        Person youngest = array[0];

        for (int i = 1; i < array.Length; i++)
        {
            if (array[i].Age < youngest.Age)
            {
                youngest = array[i]; 
            }
        }

        return youngest; 
    }
    public static Person FindOlder(this Person[] array)
    {
        if (array == null || array.Length == 0) return null;

        Person oldest = array[0];

        for (int i = 1; i < array.Length; i++)
        {
            if (array[i].Age > oldest.Age) 
            {
                oldest = array[i]; 
            }
        }

        return oldest; 
    }

    public static double FindMiddleAge(this Person[] array)
    {
        if (array == null || array.Length == 0) return 0;

        int ageSum = 0;

        foreach (Person person in array)
        {
            ageSum += person.Age;
        }

        return (double)ageSum / array.Length;
    }


}