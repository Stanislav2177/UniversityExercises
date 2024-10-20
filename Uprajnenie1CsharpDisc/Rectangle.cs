using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uprajnenie1CsharpDisc
{
    class Rectangle : Shape
    {
        private double width;
        private double height;

        // Конструктор за Rectangle
        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        // Имплементиране на периметър
        public override double GetPerimeter()
        {
            return 2 * (width + height);
        }

        // Имплементиране на лице
        public override double GetArea()
        {
            return width * height;
        }
    }
}
