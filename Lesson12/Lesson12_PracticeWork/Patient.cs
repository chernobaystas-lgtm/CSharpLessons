
namespace LessonsMainCSharp;
using System.ComponentModel;
using static System.Console;
using System.Threading;
using static System.Array;

public enum DiseaseCategory
{
    Respiratory, // Дыхательная система
    Cardio,      // Сердечно-сосудистая
    Trauma,      // Травмы и повреждения
    General      // Общие недомогания / Плановые
}

// Конкретные диагнозы, привязанные к категориям
public enum Diagnosis
{
    // Дыхательные (Respiratory)
    Cough,          // Простой кашель (1 степень)
    Bronchitis,     // Бронхит (2 степень)
    Pneumonia,      // Пневмония (3 степень)
    Tuberculosis,   // Туберкулез (4 степень)

    // Сердечные (Cardio)
    Arrhythmia,     // Легкая аритмия (1 степень)
    Hypertension,   // Гипертония / Давление (2 степень)
    Tachycardia,    // Сильная тахикардия (3 степень)
    HeartAttack,    // Инфаркт (4 степень)

    // Травмы (Trauma)
    Bruise,         // Ушиб (1 степень)
    Sprain,         // Растяжение (2 степень)
    Fracture,       // Перелом (3 степень)
    BleedingSevere, // Тяжелое кровотечение (4 степень)

    // Общие (General)
    RoutineCheck,   // Плановый осмотр (1 степень)
    Cold,           // Легкая простуда (2 степень)
    Poisoning,      // Отравление (3 степень)
}


public record class Patient
{
    // Личные и физические данные
    public int Id { get; init; }
    public FullName Name { get; init; }
    public int Age { get; init; }
    public double Height { get; init; } // Рост в см
    public double Weight { get; init; } // Вес в кг
    public double Temperature { get; init; } // Температура тела

    // Медицинские данные
    public DiseaseCategory Category { get; init; }
    public Diagnosis PatientDiagnosis { get; init; }
    public int SeverityLevel { get; init; } // Степень тяжести болезни (от 1 до 4)
    public int PriorityValue { get; init; } // Итоговый приоритет для очереди (1 - срочно, 3 - планово)

    public Patient(int id, FullName name, int age, double height, double weight, double temperature, Diagnosis diagnosis)
    {
        Id = id;
        Name = name;
        Age = age;
        Height = height;
        Weight = weight;
        Temperature = temperature;
        PatientDiagnosis = diagnosis;

        (Category, SeverityLevel) = EvaluateDiagnosis(diagnosis);

        PriorityValue = CalculatePriority();
    }

    private (DiseaseCategory, int) EvaluateDiagnosis(Diagnosis diagnosis)
    {
        return diagnosis switch
        {
            Diagnosis.Cough          => (DiseaseCategory.Respiratory, 1),
            Diagnosis.Bronchitis     => (DiseaseCategory.Respiratory, 2),
            Diagnosis.Pneumonia      => (DiseaseCategory.Respiratory, 3),
            Diagnosis.Tuberculosis   => (DiseaseCategory.Respiratory, 4),

            Diagnosis.Arrhythmia     => (DiseaseCategory.Cardio, 1),
            Diagnosis.Hypertension   => (DiseaseCategory.Cardio, 2),
            Diagnosis.Tachycardia    => (DiseaseCategory.Cardio, 3),
            Diagnosis.HeartAttack    => (DiseaseCategory.Cardio, 4),

            Diagnosis.Bruise         => (DiseaseCategory.Trauma, 1),
            Diagnosis.Sprain         => (DiseaseCategory.Trauma, 2),
            Diagnosis.Fracture       => (DiseaseCategory.Trauma, 3),
            Diagnosis.BleedingSevere => (DiseaseCategory.Trauma, 4),

            Diagnosis.RoutineCheck   => (DiseaseCategory.General, 1),
            Diagnosis.Cold           => (DiseaseCategory.General, 2),
            Diagnosis.Poisoning      => (DiseaseCategory.General, 3),
            _                        => (DiseaseCategory.General, 1)
        };
    }

    private int CalculatePriority()
    {
        if (SeverityLevel == 4 || Temperature >= 39.5)
        {
            return 1;
        }

        else if (SeverityLevel == 3 || SeverityLevel == 2 || Temperature > 37.8)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    public override string ToString()
    {
        return $"[ID: {Id}] {Name} ({Age} лет, {Temperature}°C) | Диагноз: {PatientDiagnosis} (Степень: {SeverityLevel}) | Приоритет в очереди: {PriorityValue}";
    }
}

public struct FullName
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? SecondName { get; set; }

    public FullName(string? name, string? lastName, string? secondName)
    {
        Name = name;
        LastName = lastName;
        SecondName = secondName;
    }

    public override string ToString() => $"{LastName} {Name} {SecondName}";
}

public static class PatientGenerator
{
    private static Random _random = new Random();

    private static string[] _firstNames = { "Иван", "Петр", "Сергей", "Анна", "Мария", "Елена", "Ольга", "Дмитрий" };
    private static string[] _lastNames = { "Иванов", "Петров", "Сидоров", "Смирнов", "Попов", "Козлов", "Васильев" };
    private static string[] _secondNames = { "Иванович", "Петрович", "Сергеевич", "Александровна", "Дмитриевна" };

    public static Patient GenerateRandomPatient(int id)
    {
        string firstName = _firstNames[_random.Next(_firstNames.Length)];
        string lastName = _lastNames[_random.Next(_lastNames.Length)];
        string secondName = _secondNames[_random.Next(_secondNames.Length)];
        FullName name = new FullName(firstName, lastName, secondName);

        int age = _random.Next(18, 85);
        double height = _random.Next(155, 195);
        double weight = _random.Next(50, 110);

        // Рандомим температуру от 36.0 до 40.0 градусов
        double temperature = Math.Round(36.0 + (_random.NextDouble() * 4.0), 1);

        // Берем случайный диагноз из перечисления
        Array diagnoses = Enum.GetValues(typeof(Diagnosis));
        Diagnosis randomDiagnosis = (Diagnosis)diagnoses.GetValue(_random.Next(diagnoses.Length))!;

        return new Patient(id, name, age, height, weight, temperature, randomDiagnosis);
    }
}