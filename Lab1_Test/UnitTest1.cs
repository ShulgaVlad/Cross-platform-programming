using Lab1;
using System;
using Xunit;

namespace Lab1_Test
{
    public class CubeTests
    {
        [Fact]
        public void CubeEquality_SameCubes_ReturnsTrue()
        {
            var cube1 = new Cube { front = 1, back = 2, top = 3, bottom = 4, left = 5, right = 6 };
            var cube2 = new Cube { front = 1, back = 2, top = 3, bottom = 4, left = 5, right = 6 };
            Assert.True(cube1 == cube2);
        }

        [Fact]
        public void CubeEquality_DifferentCubes_ReturnsFalse()
        {
            var cube1 = new Cube { front = 1, back = 2, top = 3, bottom = 4, left = 5, right = 6 };
            var cube2 = new Cube { front = 6, back = 5, top = 4, bottom = 3, left = 2, right = 1 };
            Assert.False(cube1 == cube2);
        }

        [Fact]
        public void RotateToTop_CorrectRotation_ReturnsExpected()
        {
            var cube = new Cube { front = 1, back = 2, top = 3, bottom = 4, left = 5, right = 6 };
            var rotated = cube.RotateToTop();
            Assert.Equal(4, rotated.front); // фронт повинен стати нижньою гранню
            Assert.Equal(3, rotated.back);  // зад повинен стати верхньою гранню
        }

        [Fact]
        public void RotateRight_CorrectRotation_ReturnsExpected()
        {
            var cube = new Cube { front = 1, back = 2, top = 3, bottom = 4, left = 5, right = 6 };
            var rotated = cube.RotateRight();
            Assert.Equal(5, rotated.front); // лівий стає переднім
            Assert.Equal(6, rotated.back);  // правий стає заднім
        }

        [Fact]
        public void RotateCW_CorrectRotation_ReturnsExpected()
        {
            var cube = new Cube { front = 1, back = 2, top = 3, bottom = 4, left = 5, right = 6 };
            var rotated = cube.RotateCW();
            Assert.Equal(5, rotated.top);   // лівий стає верхнім
            Assert.Equal(6, rotated.bottom);// правий стає нижнім
        }

        [Fact]
        public void TestCubesEqualWithRotation()
        {
            var cube1 = new Cube { front = 1, back = 2, top = 3, bottom = 4, left = 5, right = 6 };
            var cube2 = new Cube { front = 5, back = 6, top = 3, bottom = 4, left = 2, right = 1 };

            // Перевірка, що куби спочатку не рівні
            Assert.False(cube1 == cube2);

            // Повертаємо куб праворуч
            cube1 = cube1.RotateRight();
            // Тепер передній і задній повинні бути лівою і правою гранями відповідно
            Assert.Equal(5, cube1.front);
            Assert.Equal(6, cube1.back);

            // Тепер куби повинні бути рівні
            Assert.True(cube1 == cube2);
        }

        [Fact]
        public void TestCubesEqualWithMultipleRotations()
        {
            var cube1 = new Cube { front = 1, back = 2, top = 3, bottom = 4, left = 5, right = 6 };
            var cube2 = new Cube { front = 5, back = 6, top = 3, bottom = 4, left = 2, right = 1 };

            bool cubesMatch = false;

            // Пробуємо всі можливі оберти
            for (int i = 0; i < 4; i++)
            {
                cube1 = cube1.RotateToTop(); // Поворот до верхньої грані
                for (int j = 0; j < 4; j++)
                {
                    cube1 = cube1.RotateRight(); // Поворот праворуч
                    for (int k = 0; k < 4; k++)
                    {
                        cube1 = cube1.RotateCW(); // Поворот за годинниковою стрілкою
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

            Assert.True(cubesMatch); // Якщо знайдеться рівність після обертів, тест пройдено
        }
    }
}
