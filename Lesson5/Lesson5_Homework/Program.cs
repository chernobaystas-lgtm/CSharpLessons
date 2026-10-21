using System;
using System.Text;
using static System.Console;

namespace Lesson5_Homework
{
    // Статус завдання
    enum TaskStatus
    {
        NotStarted = 1,
        InProgress,
        Completed,
        Deferred
    }

    // Завдання: назва, опис і статус
    class Task
    {
        // Назва та опис задаються один раз у конструкторі, змінити можна тільки статус
        public string Name { get; }
        public string Description { get; }
        public TaskStatus Status { get; set; }

        public Task(string name, string description)
        {
            Name = name;
            Description = description;
            Status = TaskStatus.NotStarted;
        }
    }

    // Список завдань: створення, зміна статусу, виведення за статусом
    class TaskList
    {
        private Task[] tasks = new Task[0];

        public int Count
        {
            get { return tasks.Length; }
        }

        public void Add(Task task)
        {
            Task[] newTasks = new Task[tasks.Length + 1];

            for (int i = 0; i < tasks.Length; i++)
            {
                newTasks[i] = tasks[i];
            }

            newTasks[tasks.Length] = task;
            tasks = newTasks;
        }

        // Змінює статус завдання за його номером у списку (з 1). Повертає false, якщо номер неправильний.
        public bool ChangeStatus(int number, TaskStatus status)
        {
            if (number < 1 || number > tasks.Length)
            {
                return false;
            }

            tasks[number - 1].Status = status;

            return true;
        }

        // Виводить усі завдання
        public void PrintAll()
        {
            if (tasks.Length == 0)
            {
                WriteLine("Список завдань порожній.");
                return;
            }

            for (int i = 0; i < tasks.Length; i++)
            {
                PrintTask(i);
            }
        }

        // Виводить тільки завдання з обраним статусом
        public void PrintByStatus(TaskStatus status)
        {
            int found = 0;

            for (int i = 0; i < tasks.Length; i++)
            {
                if (tasks[i].Status == status)
                {
                    PrintTask(i);
                    found++;
                }
            }

            if (found == 0)
            {
                WriteLine($"Завдань зі статусом {status} немає.");
            }
        }

        private void PrintTask(int index)
        {
            WriteLine($"{index + 1}. {tasks[index].Name} [{tasks[index].Status}]");
            WriteLine($"   {tasks[index].Description}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 2: To-Do List
            TaskList list = new TaskList();

            // кілька завдань для прикладу
            list.Add(new Task("Зробити домашку", "Виконати завдання з C#"));
            list.Add(new Task("Купити хліб", "Зайти в магазин після навчання"));
            list.Add(new Task("Прочитати книгу", "Розділ про структури"));
            list.ChangeStatus(1, TaskStatus.InProgress);
            list.ChangeStatus(2, TaskStatus.Deferred);

            while (true)
            {
                WriteLine();
                WriteLine("1 - показати всі завдання");
                WriteLine("2 - додати завдання");
                WriteLine("3 - змінити статус завдання");
                WriteLine("4 - показати завдання за статусом");
                WriteLine("0 - вихід");
                Write("Ваш вибір: ");
                string choice = ReadLine()!;
                WriteLine();

                switch (choice)
                {
                    case "1":
                        list.PrintAll();
                        break;
                    case "2":
                        AddTask(list);
                        break;
                    case "3":
                        ChangeTaskStatus(list);
                        break;
                    case "4":
                        TaskStatus status = ReadStatus();
                        WriteLine();
                        list.PrintByStatus(status);
                        break;
                    case "0":
                        return;
                    default:
                        WriteLine("Помилка: потрібно ввести число від 0 до 4!");
                        break;
                }
            }
        }

        static void AddTask(TaskList list)
        {
            Write("Назва: ");
            string name = ReadLine()!;

            if (string.IsNullOrWhiteSpace(name))
            {
                WriteLine("Помилка: назва не може бути порожньою!");
                return;
            }

            Write("Опис: ");
            string description = ReadLine()!;

            list.Add(new Task(name.Trim(), description));
            WriteLine("Завдання додано зі статусом NotStarted.");
        }

        static void ChangeTaskStatus(TaskList list)
        {
            if (list.Count == 0)
            {
                WriteLine("Список завдань порожній.");
                return;
            }

            list.PrintAll();
            WriteLine();

            Write("Номер завдання: ");

            if (!int.TryParse(ReadLine(), out int number))
            {
                WriteLine("Помилка: потрібно ввести число!");
                return;
            }

            TaskStatus status = ReadStatus();

            if (list.ChangeStatus(number, status))
            {
                WriteLine("Статус змінено.");
            }
            else
            {
                WriteLine("Помилка: завдання з таким номером немає!");
            }
        }

        // Запитує статус, поки не введуть число від 1 до 4
        static TaskStatus ReadStatus()
        {
            WriteLine("Статус: 1 - NotStarted, 2 - InProgress, 3 - Completed, 4 - Deferred");

            while (true)
            {
                Write("Ваш вибір: ");

                if (int.TryParse(ReadLine(), out int value) && value >= 1 && value <= 4)
                {
                    return (TaskStatus)value;
                }

                WriteLine("Помилка: потрібно ввести число від 1 до 4.");
            }
        }
    }
}