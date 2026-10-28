namespace LessonsMainCSharp;
using System.ComponentModel;
using static System.Console;
using static System.Convert;
using static System.Array;
using static System.Exception;
using System.Numerics;
using System.Collections;

class Program
{

   static void Main(string[] args)
    {
        Aquarium oceanarium = new Aquarium(10);

        AquariumAnimal fish = new AquariumAnimal(CreatureType.Fish, 5, 4, true, "Акула", true, true, 50000, 150, "Серая");
        AquariumAnimal crab = new AquariumAnimal(CreatureType.Crustaceans, 10, 2, true, "Краб", true, false, 300, 15, "Красный");
        AquariumAnimal dolphin = new AquariumAnimal(CreatureType.Fish, 3, 0, false, "Дельфин", true, true, 150000, 200, "Серый");

        oceanarium.AddAnimal(fish);
        oceanarium.AddAnimal(crab);
        oceanarium.AddAnimal(dolphin);

        int initialPredators = oceanarium.GetPredatorsCount();

        foreach (AquariumAnimal animal in oceanarium)
        {
            string name = animal.Species;
        }

        oceanarium.RemoveAnimal("Краб");

        int finalPredators = oceanarium.GetPredatorsCount();

        foreach (AquariumAnimal animal in oceanarium)
        {
            string name = animal.Species;
        }

        WriteLine("=== ПРОВЕРКА РАБОТЫ ===");
        WriteLine($"Найдено хищников в памяти: {initialPredators}");
    }
}

public enum CreatureType
{
    Fish,          // Рыбы
    Crustaceans,   // Ракообразные
    Amphibia,      // Земноводные
    Mollusk,       // Моллюски
    Reptile,       // Рептилии
    Invertebrate   // Другие беспозвоночные
}

public class AquariumAnimal
{
    // Тот самый enum, который объединяет все твои классы в один!
    public CreatureType Type { get; set; }

    // Твои 9 параметров
    public int LimbsCount { get; set; }
    public int GillsCount { get; set; }
    public bool CanBreatheUnderWater { get; set; }
    public string Species { get; set; }
    public bool IsMarine { get; set; }
    public bool IsPredator { get; set; }
    public double Weight { get; set; }
    public double Length { get; set; }
    public string Color { get; set; }

    public AquariumAnimal()
    {
        this.Type = CreatureType.Fish; // по дефолту пусть будет рыбой
        this.LimbsCount = 0;
        this.GillsCount = 4;
        this.CanBreatheUnderWater = true;
        this.Species = "Неизвестный вид";
        this.IsMarine = false;
        this.IsPredator = false;
        this.Weight = 0.0;
        this.Length = 0.0;
        this.Color = "Прозрачный";
    }

    public AquariumAnimal(CreatureType type, int limbsCount, int gillsCount, bool canBreatheUnderWater,
                          string species, bool isMarine, bool isPredator,
                          double weight, double length, string color)
    {
        this.Type = type;
        this.LimbsCount = limbsCount;
        this.GillsCount = gillsCount;
        this.CanBreatheUnderWater = canBreatheUnderWater;
        this.Species = species;
        this.IsMarine = isMarine;
        this.IsPredator = isPredator;
        this.Weight = weight;
        this.Length = length;
        this.Color = color;
    }

    public override string ToString()
    {
        string waterType = this.IsMarine ? "Морская" : "Пресноводная";
        string dietType = this.IsPredator ? "Хищник" : "Мирное существо";
        string breathInfo = this.CanBreatheUnderWater ? "Дышит под водой" : "Нужен воздух (всплывает)";

        return $"Группа: {this.Type} | Вид: {this.Species}\n" +
               $"-------------------------------------\n" +
               $"Параметры: {this.Color} цвет, длина {this.Length} см, вес {this.Weight} г.\n" +
               $"Анатомия: органов движения: {this.LimbsCount}, количество жабр: {this.GillsCount}.\n" +
               $"Среда и поведение: {waterType} вода, {dietType}, {breathInfo}.\n";
    }
}

public class Aquarium : IEnumerable
{
    private AquariumAnimal[] _animals;
    private int _count;

    public Aquarium(int capacity = 100)
    {
        this._animals = new AquariumAnimal[capacity];
        this._count = 0;
    }

    public void AddAnimal(AquariumAnimal animal)
    {
        if (animal == null || this._count >= this._animals.Length) return;

        this._animals[this._count] = animal;
        this._count++;
    }

    public bool RemoveAnimal(string species)
    {
        int indexToRemove = -1;

        for (int i = 0; i < this._count; i++)
        {
            if (this._animals[i].Species.Equals(species, StringComparison.OrdinalIgnoreCase))
            {
                indexToRemove = i;
                break;
            }
        }

        if (indexToRemove == -1) return false;

        for (int i = indexToRemove; i < this._count - 1; i++)
        {
            this._animals[i] = this._animals[i + 1];
        }

        this._count--;
        this._animals[this._count] = null;
        return true;
    }

    public AquariumAnimal[] GetAllAnimals()
    {
        AquariumAnimal[] result = new AquariumAnimal[this._count];
        Array.Copy(this._animals, result, this._count);
        return result;
    }

    public AquariumAnimal[] GetByCreatureType(CreatureType type)
    {
        int size = 0;
        for (int i = 0; i < this._count; i++)
        {
            if (this._animals[i].Type == type) size++;
        }

        AquariumAnimal[] result = new AquariumAnimal[size];
        int index = 0;
        for (int i = 0; i < this._count; i++)
        {
            if (this._animals[i].Type == type)
            {
                result[index] = this._animals[i];
                index++;
            }
        }
        return result;
    }

    public int GetPredatorsCount()
    {
        int count = 0;
        for (int i = 0; i < this._count; i++)
        {
            if (this._animals[i].IsPredator) count++;
        }
        return count;
    }

    public System.Collections.IEnumerator GetEnumerator()
    {
    for (int i = 0; i < this._count; i++)
    {
        yield return this._animals[i];
    }
    }

}
