namespace LessonsMainCSharp;
using System;
using System.ComponentModel;
using static System.Console;

// Делегат для событий рюкзака
public delegate void BagEventHandler(string message);

// 1. ИНТЕРФЕЙС, который задает контракт для рюкзака
public interface IBag
{
    string Color { get; set; }
    string ManufactureName { get; set; }
    double Weight { get; set; }
    double Volume { get; set; }
    BagItem[] Items { get; }

    void AddItem(BagItem item);
    void RemoveItem(int itemIndex);
    double GetCurrentItemsVolume();
}

class Program
{
    static void Main(string[] args)
    {
        IBag myBag = new Bag("Синий", "Adidas", 500.0, 6.0);

        if (myBag is Bag bagEvents)
        {
            bagEvents.OnItemAdded += (msg) => WriteLine($"[УСПЕХ]: {msg}");
            bagEvents.OnItemRemoved += (msg) => WriteLine($"[УДАЛЕНО]: {msg}");
            bagEvents.OnBagFull += (msg) => WriteLine($"[ВНИМАНИЕ!]: {msg}");
        }

        WriteLine(myBag.ToString());
        WriteLine("---------------------------------------------");

        BagItem book = new BagItem(1, "Учебник по C#", ItemType.BooksAndTextbooks, 300, 2.5, ItemQuality.Excellent);
        BagItem pen = new BagItem(2, "Ручка", ItemType.WritingInstruments, 50, 0.5, ItemQuality.Normal);
        BagItem jacket = new BagItem(3, "Куртка", ItemType.Clothing, 1200, 4.0, ItemQuality.Normal);

        myBag.AddItem(book);
        myBag.AddItem(pen);

        myBag.AddItem(jacket);

        WriteLine("\nСодержимое рюкзака:");
        foreach (var item in myBag.Items)
        {
            WriteLine($"  -> {item}");
        }

        ReadLine();
    }
}

public enum ItemType
{
    BooksAndTextbooks,
    WritingInstruments,
    MeasuringTools,
    MechanicalDevices,
    ElectricalDevices,
    Clothing,
    Other
}

public enum ItemQuality
{
    Poor,
    Normal,
    Excellent
}

public record BagItem(int Index, string Name, ItemType Type, double Weight, double Volume, ItemQuality Quality);


public class Bag : IBag
{
    public string Color { get; set; }
    public string ManufactureName { get; set; }
    public double Weight { get; set; }
    public double Volume { get; set; }

    public BagItem[] Items { get; private set; }

    // События
    public event BagEventHandler OnItemAdded;
    public event BagEventHandler OnItemRemoved;
    public event BagEventHandler OnBagFull;

    public Bag()
    {
        Color = "Black";
        ManufactureName = "Without manufacture";
        Weight = 500.0;
        Volume = 15.0;
        Items = new BagItem[0];
    }

    public Bag(string color, string manufactureName, double weight, double volume)
    {
        Color = color;
        ManufactureName = manufactureName;
        Weight = weight;
        Volume = volume;
        Items = new BagItem[0];
    }

    public double GetCurrentItemsVolume()
    {
        double currentVolume = 0;
        foreach (var item in Items)
        {
            currentVolume += item.Volume;
        }
        return currentVolume;
    }

    public void AddItem(BagItem item)
    {
        if (item == null) return;

        if (GetCurrentItemsVolume() + item.Volume > Volume)
        {
            OnBagFull?.Invoke($"Невозможно положить '{item.Name}'. Недостаточно места! Свободно {Volume - GetCurrentItemsVolume()} л.");
            return;
        }

        var updatedItems = Items;
        Array.Resize(ref updatedItems, updatedItems.Length + 1);
        Items = updatedItems;
        Items[Items.Length - 1] = item;

        OnItemAdded?.Invoke($"Предмет '{item.Name}' добавлен.");
    }

    public void RemoveItem(int itemIndex)
    {
        int targetIdx = -1;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].Index == itemIndex)
            {
                targetIdx = i;
                break;
            }
        }

        if (targetIdx == -1) return;

        string removedName = Items[targetIdx].Name;

        for (int i = targetIdx; i < Items.Length - 1; i++)
        {
            Items[i] = Items[i + 1];
        }

        var remainingItems = Items;
        Array.Resize(ref remainingItems, remainingItems.Length - 1);
        Items = remainingItems;
        OnItemRemoved?.Invoke($"Предмет '{removedName}' удален.");
    }

    public void Input()
    {
        Write("Введіть колір рюкзака: ");
        Color = ReadLine() ?? "Black";

        Write("Введіть виробника: ");
        ManufactureName = ReadLine() ?? "Without manufacture";

        Write("Введіть вагу (г): ");
        if (!double.TryParse(ReadLine(), out double w)) Weight = 500.0;
        else Weight = w;

        Write("Введіть загальний об'єм (л): ");
        if (!double.TryParse(ReadLine(), out double v)) Volume = 15.0;
        else Volume = v;
    }

    public override string ToString()
    {
        return $"Рюкзак: {Color}, Виробник: {ManufactureName}, Макс. Об'єм: {Volume}л. Занято: {GetCurrentItemsVolume()}л. Предметів: {Items.Length}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Bag other)
        {
            return Color == other.Color &&
                   ManufactureName == other.ManufactureName &&
                   Weight == other.Weight &&
                   Volume == other.Volume;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Color, ManufactureName, Weight, Volume);
    }
}

