using System;
using System.IO;
using Xunit;
using Lab3;

public class ProgramTests
{
    private string inputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "INPUT.txt");
    private string outputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "OUTPUT.txt");

    [Fact]
    public void TestDist2_CalculatesCorrectDistance()
    {
        // Arrange
        Program.Point p1 = new Program.Point { x = 0, y = 0 };
        Program.Point p2 = new Program.Point { x = 3, y = 4 };

        // Act
        int result = Program.dist2(p1, p2);

        // Assert
        Assert.Equal(25, result);
    }

    [Fact]
    public void TestNoTowers_ShouldOutputZeroPower()
    {
        // Arrange
        File.WriteAllLines(inputFilePath, new string[]
        {
        "0" // ���� ���
        });

        // Act
        Program.Main();

        // Assert
        string[] output = File.ReadAllLines(outputFilePath);

        // ����������, �� ��������� ������� ����
        Assert.Equal("0.000000000", output[0]); // ������� ���������
        Assert.Equal(string.Empty, output[1]);   // ���� ������
    }


    [Fact]
    public void TestTwoDistantTowers_ShouldOutputCorrectPower()
    {
        // Arrange
        File.WriteAllLines(inputFilePath, new string[]
        {
            "2",
            "0 0",
            "10000 10000"
        });

        // Act
        Program.Main();

        // Assert
        string[] output = File.ReadAllLines(outputFilePath);
        Assert.NotEqual("0.000000000", output[0]);
        Assert.Equal("1 2", output[1]); // �� ��� ����� ��� �������
    }

    [Fact]
    public void TestCloseTowers_ShouldOutputCorrectColors()
    {
        // Arrange
        File.WriteAllLines(inputFilePath, new string[]
        {
            "3",
            "0 0",
            "0 3",
            "3 0"
        });

        // Act
        Program.Main();

        // Assert
        string[] output = File.ReadAllLines(outputFilePath);
        Assert.NotEqual("0.000000000", output[0]);
        Assert.Equal("1 2 2", output[1]); // ������ ��� �������� ������� 1, ���� ����� ������� 2
    }

    [Fact]
    public void TestSymmetricalTowers_ShouldOutputCorrectColors()
    {
        // Arrange
        File.WriteAllLines(inputFilePath, new string[]
        {
            "4",
            "0 0",
            "0 5",
            "5 0",
            "5 5"
        });

        // Act
        Program.Main();

        // Assert
        string[] output = File.ReadAllLines(outputFilePath);
        Assert.NotEqual("0.000000000", output[0]);
        Assert.Equal("1 2 2 1", output[1]); // ����������� ����� ������������� �������� �������
    }
}
