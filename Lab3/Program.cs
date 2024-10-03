using System.Drawing;
using System.Globalization;

namespace Lab3
{
    public class Program
    {
        public class Point
        {
            public int x { get; set; } // Координата x
            public int y { get; set; } // Координата y
        }

        public static int dist2(Point a, Point b)
        {
            int dx = a.x - b.x;
            int dy = a.y - b.y;
            return dx * dx + dy * dy;
        }

        public static void Main()
        {
            // Шлях до файлів
            string inputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "INPUT.txt");
            string outputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "OUTPUT.txt");

            // Читання вхідного файлу
            string[] inputLines = File.ReadAllLines(inputFilePath);
            int n = int.Parse(inputLines[0]);

            // Якщо немає веж, записуємо нульову потужність і виходимо
            if (n == 0)
            {
                using (StreamWriter outputFile = new StreamWriter(outputFilePath))
                {
                    outputFile.WriteLine("0.000000000"); // Нульова потужність
                    outputFile.WriteLine(); // Порожній рядок для частот
                }
                return; // Завершуємо виконання програми
            }

            List<Point> p = new List<Point>(n);

            // Парсинг координат веж
            for (int i = 1; i <= n; i++)
            {
                var tokens = inputLines[i].Split(' ');
                p.Add(new Point { x = int.Parse(tokens[0]), y = int.Parse(tokens[1]) });
            }

            // Бінарний пошук мінімальної потужності
            int left = 0;
            int right = 20000 * 20000 * 2 + 1;
            List<int> ansColor = new List<int>();

            while (left + 1 < right)
            {
                int mid = (left + right) / 2;
                const int UNDEF = 0;
                List<int> color = new List<int>(new int[n]);
                List<int> stack = new List<int>();
                bool good = true;

                for (int i = 0; i < n && good; i++)
                {
                    if (color[i] == UNDEF)
                    {
                        stack.Add(i);
                        color[i] = 1;

                        while (stack.Count > 0)
                        {
                            int cur = stack[stack.Count - 1];
                            stack.RemoveAt(stack.Count - 1);

                            for (int next = 0; next < n; next++)
                            {
                                if (next != cur && dist2(p[cur], p[next]) < mid)
                                {
                                    if (color[next] == UNDEF)
                                    {
                                        color[next] = 3 - color[cur]; // Чергування між 1 і 2
                                        stack.Add(next);
                                    }
                                    else if (color[next] == color[cur])
                                    {
                                        good = false;
                                        break;
                                    }
                                }
                            }

                            if (!good)
                                break;
                        }
                    }
                }

                if (good)
                {
                    left = mid;
                    ansColor = new List<int>(color);
                }
                else
                {
                    right = mid;
                }
            }

            // Запис результату у вихідний файл
            using (StreamWriter outputFile = new StreamWriter(outputFilePath))
            {
                // Запис потужності з точністю до 10^-9
                outputFile.WriteLine((Math.Sqrt(left) / 2).ToString("F9", CultureInfo.InvariantCulture));

                // Запис частот для кожної вежі
                for (int i = 0; i < n; i++)
                {
                    if (i > 0) outputFile.Write(" ");
                    outputFile.Write(ansColor[i]);
                }
                outputFile.WriteLine();
            }
        }
    }

}
