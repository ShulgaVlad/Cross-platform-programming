using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace Lab3
{
    public class Point
    {
        public int x;
        public int y;
    }

    public class Program
    {
        static int dist2(Point a, Point b)
        {
            int dx = a.x - b.x;
            int dy = a.y - b.y;
            return dx * dx + dy * dy;
        }

        public static void Main()
        {
            try
            {
                // Update the paths to include the Lab3 folder
                string projectDirectory = Directory.GetCurrentDirectory();
                string inputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "INPUT.txt");
                string outputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "OUTPUT.txt");

                // Read input from INPUT.TXT
                string[] inputLines = File.ReadAllLines(inputFilePath);
                int n = int.Parse(inputLines[0]);

                // Ensure the input file has enough lines
                if (inputLines.Length != n + 1)
                {
                    throw new Exception("Input file format is invalid. Number of points does not match the declared value.");
                }

                List<Point> p = new List<Point>(n);

                // Parse the tower coordinates
                for (int i = 1; i <= n; i++)
                {
                    var tokens = inputLines[i].Split(' ');

                    if (tokens.Length != 2)
                    {
                        throw new Exception($"Invalid input at line {i + 1}. Expected 2 coordinates.");
                    }

                    p.Add(new Point { x = int.Parse(tokens[0]), y = int.Parse(tokens[1]) });
                }

                // Binary search for the minimum transmission power
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
                                            color[next] = 3 - color[cur]; // Alternate between 1 and 2
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

                // Write the result to OUTPUT.TXT
                using (StreamWriter outputFile = new StreamWriter(outputFilePath))
                {
                    // Write the power with precision of at least 10^-8
                    outputFile.WriteLine((Math.Sqrt(left) / 2).ToString("F9", CultureInfo.InvariantCulture));

                    // Write the frequencies for each tower
                    for (int i = 0; i < n; i++)
                    {
                        if (i > 0) outputFile.Write(" ");
                        outputFile.Write(ansColor[i]);
                    }
                    outputFile.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                Environment.Exit(1);  // Ensure non-zero exit code for failure
            }
        }
    }
}
