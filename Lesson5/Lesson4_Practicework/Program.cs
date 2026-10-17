using System;
using System.Text;
using static System.Console;

namespace Lesson5_PracticeWork
{
    // Структура «RGB колір»
    struct RgbColor
    {
        // Складові кольору, кожна від 0 до 255
        private byte r;
        private byte g;
        private byte b;

        // Конструктор. Якщо якась складова поза діапазоном 0-255, генерує виняток.
        public RgbColor(int r, int g, int b)
        {
            if (r < 0 || r > 255 || g < 0 || g > 255 || b < 0 || b > 255)
            {
                throw new ArgumentException("Кожна складова кольору має бути від 0 до 255.");
            }

            this.r = (byte)r;
            this.g = (byte)g;
            this.b = (byte)b;
        }

        public int GetR() { return r; }
        public int GetG() { return g; }
        public int GetB() { return b; }

        // Переведення в HEX формат, наприклад #FF8000
        public string ToHex()
        {
            return $"#{r:X2}{g:X2}{b:X2}";
        }

        // Переведення в HSL: h у градусах (0-360), s і l у відсотках (0-100)
        public void ToHsl(out double h, out double s, out double l)
        {
            double rf = r / 255.0;
            double gf = g / 255.0;
            double bf = b / 255.0;

            double max = Math.Max(rf, Math.Max(gf, bf));
            double min = Math.Min(rf, Math.Min(gf, bf));
            double delta = max - min;

            l = (max + min) / 2;

            if (delta == 0)
            {
                // відтінок сірого: відтінку і насиченості немає
                h = 0;
                s = 0;
            }
            else
            {
                s = delta / (1 - Math.Abs(2 * l - 1));

                if (max == rf)
                {
                    h = 60 * (((gf - bf) / delta) % 6);
                }
                else if (max == gf)
                {
                    h = 60 * ((bf - rf) / delta + 2);
                }
                else
                {
                    h = 60 * ((rf - gf) / delta + 4);
                }

                if (h < 0)
                {
                    h += 360;
                }
            }

            s *= 100;
            l *= 100;
        }

        // Переведення в CMYK: усі значення у відсотках (0-100)
        public void ToCmyk(out double c, out double m, out double y, out double k)
        {
            double rf = r / 255.0;
            double gf = g / 255.0;
            double bf = b / 255.0;

            double max = Math.Max(rf, Math.Max(gf, bf));

            double kf = 1 - max;

            if (kf == 1)
            {
                // чорний колір
                c = 0;
                m = 0;
                y = 0;
            }
            else
            {
                c = (1 - rf - kf) / (1 - kf) * 100;
                m = (1 - gf - kf) / (1 - kf) * 100;
                y = (1 - bf - kf) / (1 - kf) * 100;
            }

            k = kf * 100;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 2: структура «RGB колір»
            Write("Введіть R (0-255): ");
            if (!int.TryParse(ReadLine(), out int r))
            {
                WriteLine("Помилка: потрібно ввести ціле число!");
                return;
            }

            Write("Введіть G (0-255): ");
            if (!int.TryParse(ReadLine(), out int g))
            {
                WriteLine("Помилка: потрібно ввести ціле число!");
                return;
            }

            Write("Введіть B (0-255): ");
            if (!int.TryParse(ReadLine(), out int b))
            {
                WriteLine("Помилка: потрібно ввести ціле число!");
                return;
            }

            try
            {
                RgbColor color = new RgbColor(r, g, b);

                color.ToHsl(out double h, out double s, out double l);
                color.ToCmyk(out double c, out double m, out double y, out double k);

                WriteLine();
                WriteLine($"RGB:  ({color.GetR()}, {color.GetG()}, {color.GetB()})");
                WriteLine($"HEX:  {color.ToHex()}");
                WriteLine($"HSL:  ({h:F1}°, {s:F1}%, {l:F1}%)");
                WriteLine($"CMYK: ({c:F1}%, {m:F1}%, {y:F1}%, {k:F1}%)");
            }
            catch (ArgumentException ex)
            {
                WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}