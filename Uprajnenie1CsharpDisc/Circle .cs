using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uprajnenie1CsharpDisc
{
    class Circle : Shape
    {
        private double radius;

        public Circle(double radius)
        {
            this.radius = radius;
        }
        public override double GetPerimeter()
        {
            return 2 * Math.PI * radius;
        }

        public override double GetArea()
        {
            return Math.PI * Math.Pow(radius, 2);
        }
    }
}
