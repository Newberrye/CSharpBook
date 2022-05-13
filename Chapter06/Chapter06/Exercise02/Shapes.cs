namespace Exercise02;

public abstract class Shape
{
    public double Height { get; set; }
    public double Width { get; set; }
    public double Area { get; set; }
}

public class Rectangle : Shape
{
    public Rectangle(double height, double width)
    {
        Height = height;
        Width = width;
        Area = Height * Width;
    }
}

public class Square : Shape
{
    public Square(double side)
    {
        Height = side;
        Width = side;
        Area = Height * Width;
    }
}

public class Circle : Shape
{

    public Circle(double radius)
    {
        Height = radius * 2;
        Width = Height;
        Area = Math.PI * Math.Pow(radius, 2);
    }
}
