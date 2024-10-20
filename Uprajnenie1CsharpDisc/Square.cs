using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uprajnenie1CsharpDisc
{
    internal class Square : Rectangle
    {
        public Square(double width, double height) : base(width, height)
        {
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override double GetArea()
        {
            return base.GetArea();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override double GetPerimeter()
        {
            return base.GetPerimeter();
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        public static double GetArea(double side)
        {
            return side * side;
        }
    }
}
