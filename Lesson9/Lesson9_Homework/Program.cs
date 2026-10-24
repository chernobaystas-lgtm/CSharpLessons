using System.Text;
using System.Text.Unicode;
using static System.Console;


namespace Lesson9_PracticeWork
{
    class Program
    {
static void Main(string[] args)
{
    WriteLine("--- Testing Action Delegates (Date/Time) ---");
    
    Action timeAction = DateTimeAndGeometry.DisplayCurrentTime;
    Action dateAction = DateTimeAndGeometry.DisplayCurrentDate;
    Action dayAction = DateTimeAndGeometry.DisplayCurrentDayOfWeek;

    // Вызываем делегаты
    timeAction();
    dateAction();
    dayAction();

    WriteLine("\n--- Testing Func Delegates (Geometry) ---");
    Func<double, double, double> triangleAreaFunc = DateTimeAndGeometry.CalculateTriangleArea;
    Func<double, double, double> rectangleAreaFunc = DateTimeAndGeometry.CalculateRectangleArea;

    // Считаем площади через делегаты
    double triangleArea = triangleAreaFunc(5.0, 4.0); // основание 5, высота 4
    double rectangleArea = rectangleAreaFunc(6.0, 3.5); // ширина 6, высота 3.5

    WriteLine($"Triangle area (base 5, height 4): {triangleArea}");
    WriteLine($"Rectangle area (width 6, height 3.5): {rectangleArea}");
}


    }
}

public static class DateTimeAndGeometry
{
    public static void DisplayCurrentTime()
    {
        WriteLine("Current Time: " + DateTime.Now.ToString("HH:mm:ss"));
    }

    public static void DisplayCurrentDate()
    {
        WriteLine("Current Date: " + DateTime.Now.ToString("dd.MM.yyyy"));
    }

    public static void DisplayCurrentDayOfWeek()
    {
        WriteLine("Current Day of Week: " + DateTime.Now.DayOfWeek);
    }

    public static double CalculateTriangleArea(double b, double h)
    {
        return (b * h) / 2.0;
    }

    // Площадь прямоугольника: width * height
    public static double CalculateRectangleArea(double w, double h)
    {
        return w * h;
    }
}