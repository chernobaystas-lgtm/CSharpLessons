using System;

// 1. Абстрактный базовый класс
abstract class Figure
{
    public string Name { get; set; }

    protected Figure(string name)
    {
        Name = name;
    }

    // Абстрактный метод для подсчета площади
    public abstract double GetArea();

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Фигура: {Name}, Площадь: {GetArea():F2}");
    }
}

// 2. Класс Прямоугольник
class Rectangle : Figure
{
    private double _width;
    private double _height;

    public Rectangle(double width, double height) : base("Прямоугольник")
    {
        _width = width;
        _height = height;
    }

    public override double GetArea()
    {
        return _width * _height;
    }
}

// 3. Класс Круг
class Circle : Figure
{
    private double _radius;

    public Circle(double radius) : base("Круг")
    {
        _radius = radius;
    }

    public override double GetArea()
    {
        return Math.PI * Math.Pow(_radius, 2);
    }
}

// 4. Класс Прямоугольный треугольник
class RightTriangle : Figure
{
    private double _baseSide;
    private double _height;

    public RightTriangle(double baseSide, double height) : base("Прямоугольный треугольник")
    {
        _baseSide = baseSide;
        _height = height;
    }

    public override double GetArea()
    {
        return 0.5 * _baseSide * _height;
    }
}

// 5. Класс Трапеция
class Trapezoid : Figure
{
    private double _a; // Верхнее основание
    private double _b; // Нижнее основание
    private double _height;

    public Trapezoid(double a, double b, double height) : base("Трапеция")
    {
        _a = a;
        _b = b;
        _height = height;
    }

    public override double GetArea()
    {
        return 0.5 * (_a + _b) * _height;
    }
}
