using System;
using Xunit;
using System.IO;
using Lab2;

public class LargestShipTests
{
    // ������� ��� ������� ������ Main � ��������� �������
    private void RunTest(string inputData, string expectedOutput)
    {
        // �������� �������� ���� � ���� INPUT.TXT
        File.WriteAllText("../../../../Lab2/INPUT.TXT", inputData);

        // ��������� ������� ��������
        LargestShip.Main(new string[0]);

        // ������ ��������� � OUTPUT.TXT
        string result = File.ReadAllText("../../../../Lab2/OUTPUT.TXT").Trim();

        // ��������� ��������� � ���������� ���������
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
        string expectedOutput = "15";  // ��������� �������� �������� ����� ����� �����
        RunTest(inputData, expectedOutput);
    }

    [Fact]
    public void Test_FullBlock()
    {
        string inputData = "3 3 1\n1 1 3 3";
        string expectedOutput = "0";  // �� ������� ������, ���� ����� �������� �� ����� ���������
        RunTest(inputData, expectedOutput);
    }

    [Fact]
    public void Test_EmptyField()
    {
        string inputData = "4 4 0";
        string expectedOutput = "16";  // ���� �������, ������������ �������� �������� � 16 �������
        RunTest(inputData, expectedOutput);
    }

    [Fact]
    public void Test_MultipleSmallShips()
    {
        string inputData = "6 6 2\n1 1 2 2\n5 5 6 6";
        string expectedOutput = "9";  // ³����� ������ � ����� �������� ��������� �������� ������� 12 �������
        RunTest(inputData, expectedOutput);
    }
}
