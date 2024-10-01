using System;
using Xunit;
using System.IO;
using Lab2;

public class LargestShipTests
{
    // Функція для запуску методу Main з тестовими файлами
    private void RunTest(string inputData, string expectedOutput)
    {
        // Записуємо тестовий вхід в файл INPUT.TXT
        File.WriteAllText("../../../../Lab2/INPUT.TXT", inputData);

        // Викликаємо основну програму
        LargestShip.Main(new string[0]);

        // Читаємо результат з OUTPUT.TXT
        string result = File.ReadAllText("../../../../Lab2/OUTPUT.TXT").Trim();

        // Порівнюємо результат з очікуваним значенням
        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void Test_SimpleScenario()
    {
        string inputData = "8 7 3\n1 1 2 2\n3 5 3 7\n4 2 4 3";
        string expectedOutput = "21";
        RunTest(inputData, expectedOutput);
    }

    [Fact]
    public void Test_OneShip()
    {
        string inputData = "5 5 1\n1 1 1 1";
        string expectedOutput = "15";  // Найбільший можливий корабель займає решту клітин
        RunTest(inputData, expectedOutput);
    }

    [Fact]
    public void Test_FullBlock()
    {
        string inputData = "3 3 1\n1 1 3 3";
        string expectedOutput = "0";  // Всі клітинки зайняті, тому новий корабель не можна розмістити
        RunTest(inputData, expectedOutput);
    }

    [Fact]
    public void Test_EmptyField()
    {
        string inputData = "4 4 0";
        string expectedOutput = "16";  // Немає кораблів, максимальний можливий корабель — 16 клітинок
        RunTest(inputData, expectedOutput);
    }

    [Fact]
    public void Test_MultipleSmallShips()
    {
        string inputData = "6 6 2\n1 1 2 2\n5 5 6 6";
        string expectedOutput = "9";  // Вільний простір у центрі дозволяє розмістити корабель розміром 12 клітинок
        RunTest(inputData, expectedOutput);
    }
}
