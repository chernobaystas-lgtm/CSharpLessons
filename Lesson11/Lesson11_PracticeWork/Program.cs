namespace LessonsMainCSharp;
using System.ComponentModel;
using static System.Console;
using static System.Convert;
using static System.Array;
using static System.Exception;
using System.Numerics;

class Program
{
static void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}

    static void Main(string[] args)
    {
        Line<int> line1 = new Line<int>(0, 0, 3, 4);

        WriteLine(line1);
        WriteLine($"Длина отрезка: {line1.GetLength()}");


        Point<double> p1 = new Point<double>(1.5, 2.0);
        Point<double> p2 = new Point<double>(4.5, 6.0);

        Line<double> line2 = new Line<double>(p1, p2);
        WriteLine($"\n{line2}");


        Point<int> p3 = new Point<int>(6, 8);
        bool liesOnLine = line1.ContainsPoint(p3);

        WriteLine($"\nЛежит ли точка {p3} на первой прямой? {liesOnLine}");
    }
}

public class Point<T> where T : INumber<T>
{
    public T X { get; set; }
    public T Y { get; set; }

    public Point(T x, T y)
    {
      X = x;
      Y = y;
    }


    public Point()
    {
      X = default(T);
      Y = default(T);
    }



    public override string ToString()
    {
        return $"({X}; {Y})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Point<T> other)
        {
            return EqualityComparer<T>.Default.Equals(this.X, other.X) &&
                   EqualityComparer<T>.Default.Equals(this.Y, other.Y);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}

public class Line<T> where T : INumber<T>
{
    public Point<T> Point1 { get; set; }
    public Point<T> Point2 { get; set; }

    public Line(Point<T> p1, Point<T> p2)
    {
        Point1 = p1;
        Point2 = p2;
    }

    public Line(T x1, T y1, T x2, T y2)
    {
        Point1 = new Point<T>(x1, y1);
        Point2 = new Point<T>(x2, y2);
    }

    public double GetLength()
    {
        double x1 = ToDouble(Point1.X);
        double y1 = ToDouble(Point1.Y);
        double x2 = ToDouble(Point2.X);
        double y2 = ToDouble(Point2.Y);

        // Формула: корень из ((x2-x1)^2 + (y2-y1)^2)
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }

    public bool ContainsPoint(Point<T> p3)
    {
        T x1 = Point1.X;
        T y1 = Point1.Y;
        T x2 = Point2.X;
        T y2 = Point2.Y;

        T x3 = p3.X;
        T y3 = p3.Y;

        return (x3 - x1) * (y2 - y1) - (y3 - y1) * (x2 - x1) == T.Zero;
    }

    public override string ToString()
    {
        return $"Прямая линия через точки {Point1} и {Point2}";
    }
}