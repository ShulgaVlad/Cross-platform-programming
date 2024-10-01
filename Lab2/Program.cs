using System;
using System.IO;
using System.Collections.Generic;

namespace Lab2
{
    public class LargestShip
    {
        public static void Main(string[] args)
        {
            // Отримання поточного каталогу і встановлення шляху до файлів
            string inputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "INPUT.txt");
            string outputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "OUTPUT.TXT");

            // Читання вхідних даних з файлу
            var input = File.ReadAllLines(inputFilePath);

            // Розбираємо перший рядок: кількість рядків N, стовпців M та кількість кораблів K
            var firstLine = input[0].Split();
            int N = int.Parse(firstLine[0]);  // Кількість рядків на полі
            int M = int.Parse(firstLine[1]);  // Кількість стовпців на полі
            int K = int.Parse(firstLine[2]);  // Кількість вже розміщених кораблів

            // Ініціалізація поля. true означає, що клітинка зайнята або недоступна, false — вільна
            bool[,] grid = new bool[N, M];

            // Масиви для визначення всіх напрямків (включаючи діагональні), щоб позначати сусідні клітинки
            int[] dRow = { -1, -1, -1, 0, 0, 1, 1, 1 }; // Зміщення по рядках
            int[] dCol = { -1, 0, 1, -1, 1, -1, 0, 1 }; // Зміщення по стовпцях

            // Обробка розміщених кораблів
            for (int i = 1; i <= K; i++)
            {
                // Розбираємо координати кожного корабля
                var shipCoords = input[i].Split();
                int x1 = int.Parse(shipCoords[0]) - 1;  // Верхня ліва координата (рядок)
                int y1 = int.Parse(shipCoords[1]) - 1;  // Верхня ліва координата (стовпець)
                int x2 = int.Parse(shipCoords[2]) - 1;  // Нижня права координата (рядок)
                int y2 = int.Parse(shipCoords[3]) - 1;  // Нижня права координата (стовпець)

                // Позначаємо клітинки, де розташовані кораблі, як зайняті (true)
                for (int r = x1; r <= x2; r++)
                {
                    for (int c = y1; c <= y2; c++)
                    {
                        grid[r, c] = true;

                        // Маркуємо також всі сусідні клітинки (включно з діагональними) як зайняті
                        for (int d = 0; d < 8; d++)
                        {
                            int newRow = r + dRow[d];  // Нова координата рядка для сусідньої клітинки
                            int newCol = c + dCol[d];  // Нова координата стовпця для сусідньої клітинки
                                                       // Перевіряємо, чи не виходимо за межі поля
                            if (newRow >= 0 && newRow < N && newCol >= 0 && newCol < M)
                            {
                                grid[newRow, newCol] = true;  // Позначаємо як зайняту
                            }
                        }
                    }
                }
            }

            // Використовуємо динамічне програмування для пошуку найбільшого прямокутника вільних клітин
            int[,] height = new int[N, M];  // Висота стовпця в кожній клітинці
            int maxArea = 0;  // Максимальна площа прямокутника

            // Обчислюємо висоти стовпців і шукаємо максимальний прямокутник
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < M; c++)
                {
                    // Якщо клітинка вільна (не зайнята кораблем чи її околицями)
                    if (!grid[r, c])
                    {
                        // Визначаємо висоту стовпця в цій клітинці
                        height[r, c] = (r == 0) ? 1 : height[r - 1, c] + 1;  // Якщо це перший рядок — висота 1, інакше додаємо до попередньої висоти
                    }
                }

                // Обчислюємо найбільший прямокутник для поточного рядка як гістограми
                int[] currentRow = new int[M];  // Ініціалізуємо масив висот для поточного рядка
                for (int c = 0; c < M; c++)
                {
                    currentRow[c] = height[r, c];  // Заповнюємо висоти для гістограми
                }

                // Шукаємо найбільший прямокутник у гістограмі поточного рядка
                maxArea = Math.Max(maxArea, LargestRectangleInHistogram(currentRow));
            }

            // Записуємо результат у файл
            File.WriteAllText(outputFilePath, maxArea.ToString());
        }

        // Допоміжна функція для знаходження найбільшого прямокутника в гістограмі
        static int LargestRectangleInHistogram(int[] heights)
        {
            int maxArea = 0;  // Максимальна площа прямокутника
            Stack<int> stack = new Stack<int>();  // Стек для збереження індексів висот

            int width = heights.Length;  // Довжина поточного рядка

            // Проходимо по кожному елементу висот (та додатково розглядаємо кінець рядка)
            for (int i = 0; i <= width; i++)
            {
                int h = (i == width) ? 0 : heights[i];  // Висота або нуль, якщо кінець рядка

                // Якщо поточна висота менша за попередню, то обчислюємо площу прямокутника
                while (stack.Count > 0 && h < heights[stack.Peek()])
                {
                    int height = heights[stack.Pop()];  // Виймаємо попередню висоту зі стеку
                    int widthOfRectangle = (stack.Count == 0) ? i : i - stack.Peek() - 1;  // Визначаємо ширину прямокутника
                    maxArea = Math.Max(maxArea, height * widthOfRectangle);  // Обчислюємо площу і оновлюємо максимум
                }

                stack.Push(i);  // Додаємо індекс поточної висоти у стек
            }

            return maxArea;  // Повертаємо найбільшу площу
        }
    }
}