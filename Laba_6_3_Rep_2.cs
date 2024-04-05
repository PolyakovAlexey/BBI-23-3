using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;

struct Group
{
    private string GroupName;
    private Student[] Students;
    private double AverageScore;

    public Group(string groupName, Student[] students)
    {
        GroupName = groupName;
        Students = students;
        AverageScore = 0;
        CalculateAverageScore();
    }

    public void PrintStudents()
    {
        Console.WriteLine($"Группа: {GroupName}");
        foreach (Student student in Students)
        {
            student.Print();
        }
    }
    public void CalculateAverageScore()
    {
        double sum = 0;
        foreach (Student student in Students)
        {
            sum += student.AverageScore;
        }
        AverageScore = sum / Students.Length;
    }

    public void Print()
    {
        Console.WriteLine($"Группа: {GroupName}");
        Console.WriteLine($"Средний балл группы: {AverageScore:F1}");
        foreach (Student student in Students)
        {
            student.Print();
        }
    }
    public static void SortGroup(Group[] students)
    {
        for (int i = 0; i < students.Length - 1; i++)
        {
            for (int j = 0; j < students.Length - i - 1; j++)
            {
                if (students[j].AverageScore < students[j + 1].AverageScore)
                {
                    var temp = students[j];
                    students[j] = students[j + 1];
                    students[j + 1] = temp;
                }
            }
        }
    }
}
struct Student
{
    private string Name;
    private double[] ExamScores;
    public double AverageScore { get; private set; }

    public Student(string name, double[] examScores)
    {
        Name = name;
        ExamScores = examScores;
        AverageScore = 0;
        AverageScore = CalculateAverageScore();
    }

    private double CalculateAverageScore()
    {
        double sum = 0;
        for (int i = 0; i < ExamScores.Length; i++)
        {
            sum += ExamScores[i];
        }
        return sum / ExamScores.Length;
    }
    public double GetAverageScore()
    {
        return AverageScore;
    }
    public void Print()
    {
        Console.WriteLine($"Студент: {Name}, Средний балл: {AverageScore:F1}");
    }
}


class Program
{
    static void Main()
    {
        Student[] students = new Student[9]
        {
            new Student("Студент 1 Группа 1", new double[] {80, 75, 90, 85, 95}),
            new Student("Студент 2 Группа 1", new double[] {65, 70, 75, 80, 85}),
            new Student("Студент 3 Группа 1", new double[] {85, 90, 95, 88, 92}),
            new Student("Студент 1 Группа 2", new double[] {75, 72, 75, 84, 89}),
            new Student("Студент 2 Группа 2", new double[] {70, 92, 82, 76, 95}),
            new Student("Студент 3 Группа 2", new double[] {88, 84, 92, 79, 93}),
            new Student("Студент 1 Группа 3", new double[] {68, 76, 70, 85, 82}),
            new Student("Студент 2 Группа 3", new double[] {79, 83, 86, 85, 90}),
            new Student("Студент 3 Группа 3", new double[] {82, 78, 85, 89, 91}),
        };

        Group group1 = new Group("Группа 1", new Student[] { students[0], students[1], students[2] });
        Group group2 = new Group("Группа 2", new Student[] { students[3], students[4], students[5] });
        Group group3 = new Group("Группа 3", new Student[] { students[6], students[7], students[8] });

        Group[] allGroups = new Group[] { group1, group2, group3 };
        Group.SortGroup(allGroups);
        foreach (Group group in allGroups)
        {
            group.Print();
        }
    }
}