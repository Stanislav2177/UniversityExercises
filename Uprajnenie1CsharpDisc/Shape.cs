using System;

abstract class Shape
{

    public abstract double GetPerimeter();

    public abstract double GetArea();

    private int color;

    public int Color
    {
        get
        {

            int red = (color >> 16) & 0xFF;
            int green = (color >> 8) & 0xFF;
            int blue = color & 0xFF;

            if (red == 255 && green == 0 && blue == 0)
            {
                return PrimaryColors.Red;
            }
            else if (red == 0 && green == 255 && blue == 0)
            {
                return PrimaryColors.Green;
            }
            else if (red == 0 && green == 0 && blue == 255)
            {
                return PrimaryColors.Blue;
            }

            return 0; 
        }

        set
        {
            switch (value)
            {
                case PrimaryColors.Red:
                    color = (255 << 16); 
                    break;
                case PrimaryColors.Green:
                    color = (255 << 8);   
                    break;
                case PrimaryColors.Blue:
                    color = 255;          
                    break;
                default:
                    throw new ArgumentException("Невалиден цвят.");
            }
        }
    }

    public class PrimaryColors
    {
        public const int Red = 1;
        public const int Green = 2;
        public const int Blue = 3;
    }
}
