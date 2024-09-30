using System;
using System.IO;

namespace Lab1
{
    class Cube
    {
        public int front;
        public int back;
        public int top;
        public int bottom;
        public int left;
        public int right;

        public static Cube ReadFromLine(string line)
        {
            string[] input = line.Split(' ');
            int front = int.Parse(input[0]);
            int back = int.Parse(input[1]);
            int top = int.Parse(input[2]);
            int bottom = int.Parse(input[3]);
            int left = int.Parse(input[4]);
            int right = int.Parse(input[5]);

            return new Cube { front = front, back = back, top = top, bottom = bottom, left = left, right = right };
        }

        public Cube RotateToTop()
        {
            return new Cube { front = this.bottom, back = this.top, top = this.front, bottom = this.back, left = this.left, right = this.right };
        }

        public Cube RotateRight()
        {
            return new Cube { front = this.left, back = this.right, top = this.top, bottom = this.bottom, left = this.back, right = this.front };
        }

        public Cube RotateCW()
        {
            return new Cube { front = this.front, back = this.back, top = this.left, bottom = this.right, left = this.bottom, right = this.top };
        }

        public static bool operator ==(Cube a, Cube b)
        {
            return a.front == b.front && a.back == b.back && a.top == b.top && a.bottom == b.bottom && a.left == b.left && a.right == b.right;
        }

        public static bool operator !=(Cube a, Cube b)
        {
            return !(a == b);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Читання вхідного файлу INPUT.TXT
            string inputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab1", "INPUT.txt");
            string[] lines = File.ReadAllLines(inputFilePath);

            // Перший куб
            Cube cube1 = Cube.ReadFromLine(lines[0]);

            // Другий куб
            Cube cube2 = Cube.ReadFromLine(lines[1]);

            bool cubesMatch = false;

            // Порівняння кубиків після всіх можливих обертів
            for (int i = 0; i < 4; i++)
            {
                cube1 = cube1.RotateToTop();
                for (int j = 0; j < 4; j++)
                {
                    cube1 = cube1.RotateRight();
                    for (int k = 0; k < 4; k++)
                    {
                        cube1 = cube1.RotateCW();
                        if (cube1 == cube2)
                        {
                            cubesMatch = true;
                            break;
                        }
                    }
                    if (cubesMatch) break;
                }
                if (cubesMatch) break;
            }

            // Виведення результату у файл OUTPUT.TXT
            string outputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab1", "OUTPUT.txt");
            File.WriteAllText(outputFilePath, cubesMatch ? "YES" : "NO");
        }
    }

}