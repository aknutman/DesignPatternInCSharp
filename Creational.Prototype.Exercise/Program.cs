using System;

namespace Creational.Prototype.Exercise
{
    public class Point
    {
        public int X, Y;
    }

    public class Line
    {
        public Point Start, End;

        public Line DeepCopy()
        {
            // todo

            return new Line()
            {
                Start = new Point()
                {
                    X = Start.X,
                    Y = Start.Y
                },
                End = new Point()
                {
                    Y = End.Y,
                    X = End.X,
                }
            };
        }
    }

    public static class Program
    {
        static void Main()
        {
            Console.WriteLine("Test");
        }
    }
}
