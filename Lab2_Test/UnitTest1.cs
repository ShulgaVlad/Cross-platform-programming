using System;
using Xunit;
using System.IO;
using Lab2;

public class LargestShipTests
{
    int testnum = 0;
    // ������� ��� ������� ������ Main � ��������� �������
    private void RunTest(string inputData, string expectedOutput)
    {
        // �������� �������� ���� � ���� INPUT.TXT
        File.WriteAllText("../../../../Lab2/INPUT.txt", inputData);

        // ��������� ������� ��������
        LargestShip.Main(new string[0]);

        // ������ ��������� � OUTPUT.TXT
        string result = File.ReadAllText("../../../../Lab2/OUTPUT.txt").Trim();

        // ��������� ��������� � ���������� ���������
        Assert.Equal(expectedOutput, result);
        Console.WriteLine($"Test number {testnum} \nExpexted result: {expectedOutput} \nActual result: {result}");
    }

    [Fact]
    public void Test_SimpleScenario()
    {
        testnum = 2;
        string inputData = "8 7 3\n1 1 2 2\n3 5 3 7\n4 2 4 3";
        string expectedOutput = "21";
        RunTest(inputData, expectedOutput);
    }

    [Fact]
    public void Test_OneShip()
    {
        testnum = 4;
        string inputData = "5 5 1\n1 1 1 1";
        string expectedOutput = "15";  // ��������� �������� �������� ����� ����� �����
        RunTest(inputData, expectedOutput);
    }

    [Fact]
    public void Test_FullBlock()
    {
        testnum = 1;
        string inputData = "3 3 1\n1 1 3 3";
        string expectedOutput = "0";  // �� ������� ������, ���� ����� �������� �� ����� ���������
        RunTest(inputData, expectedOutput);
    }

    [Fact]
    public void Test_EmptyField()
    {
        testnum = 3;
        string inputData = "4 4 0";
        string expectedOutput = "16";  // ���� �������, ������������ �������� �������� � 16 �������
        RunTest(inputData, expectedOutput);
    }

    [Fact]
    public void Test_MultipleSmallShips()
    {
        testnum = 5;
        string inputData = "6 6 2\n1 1 2 2\n5 5 6 6";
        string expectedOutput = "9";  // ³����� ������ � ����� �������� ��������� �������� ������� 12 �������
        RunTest(inputData, expectedOutput);
    }
}
