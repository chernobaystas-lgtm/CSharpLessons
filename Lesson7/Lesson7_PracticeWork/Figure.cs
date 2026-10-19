abstract class Figure
{
    protected double SideA;
    protected double SideB;
    protected double SideC; 
    protected double RadiusX; 
    protected double RadiusY; 

    public abstract double GetArea();
    public abstract double GetPerimeter();
}

class Rectangle : Figure
{
    public Rectangle(double width, double height)
    {
        SideA = width;
        SideB = height;
    }

    public override double GetArea() => SideA * SideB;

    public override double GetPerimeter() => 2 * (SideA + SideB);

    public override string ToString()
    {
        return $"Rectangle [Width={SideA}, Height={SideB}, Area={GetArea():F2}, Perimeter={GetPerimeter():F2}]";
    }

    public override bool Equals(object obj)
    {
        if (obj is Rectangle other)
        {
            return this.SideA == other.SideA && this.SideB == other.SideB;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(SideA, SideB);
    }
}

class Ellipse : Figure
{
    public Ellipse(double radiusX, double radiusY)
    {
        RadiusX = radiusX;
        RadiusY = radiusY;
    }

    public override double GetArea() => Math.PI * RadiusX * RadiusY;

    public override double GetPerimeter()
    {
        return Math.PI * (3 * (RadiusX + RadiusY) - Math.Sqrt((3 * RadiusX + RadiusY) * (RadiusX + 3 * RadiusY)));
    }

    public override string ToString()
    {
        return $"Ellipse [RadiusX={RadiusX}, RadiusY={RadiusY}, Area={GetArea():F2}, Perimeter={GetPerimeter():F2}]";
    }

    public override bool Equals(object obj)
    {
        if (obj is Ellipse other)
        {
            return this.RadiusX == other.RadiusX && this.RadiusY == other.RadiusY;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(RadiusX, RadiusY);
    }
}

class Circle : Ellipse
{
    public Circle(double radius) : base(radius, radius)
    {
    }

    public override string ToString()
    {
        return $"Circle [Radius={RadiusX}, Area={GetArea():F2}, Perimeter={GetPerimeter():F2}]";
    }

    public override bool Equals(object obj)
    {
        if (obj is Circle other)
        {
            return this.RadiusX == other.RadiusX;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(RadiusX);
    }
}

class Triangle : Figure
{
    public Triangle(double a, double b, double c)
    {
        SideA = a;
        SideB = b;
        SideC = c;
    }

    public override double GetArea()
    {
        double p = GetPerimeter() / 2;
        return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
    }

    public override double GetPerimeter() => SideA + SideB + SideC;

    public override string ToString()
    {
        return $"Triangle [SideA={SideA}, SideB={SideB}, SideC={SideC}, Area={GetArea():F2}, Perimeter={GetPerimeter():F2}]";
    }

    public override bool Equals(object obj)
    {
        if (obj is Triangle other)
        {
            return this.SideA == other.SideA && this.SideB == other.SideB && this.SideC == other.SideC;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(SideA, SideB, SideC);
    }
}