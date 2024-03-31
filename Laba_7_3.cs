using System;

class Group
{
    protected string Name;
    protected Student[] Students;
    protected double AverageScore;

    public Group(string name, Student[] students)
    {
        Name = name;
        Students = students;
        CalculateAverageScore();
    }

    protected virtual void CalculateAverageScore()
    {
        double sum = 0;
        foreach (Student student in Students)
        {
            sum += student.GetAverageScore();
        }
        AverageScore = sum / Students.Length;
    }

    public void Print()
    {
        Console.WriteLine($"Группа: {Name}");
        Console.WriteLine($"Средний балл группы: {AverageScore:F1}");
        foreach (Student student in Students)
        {
            student.Print();
        }
    }
}

class GroupA : Group
{
    public GroupA(string name, Student[] students) : base(name, students) { }

    protected override void CalculateAverageScore()
    {
        double sum = 0;
        foreach (Student student in Students)
        {
            sum += student.GetAverageScore(); 
        }
        AverageScore = sum / Students.Length;
    }
}

class GroupB : Group
{
    public GroupB(string name, Student[] students) : base(name, students) { }

    protected override void CalculateAverageScore()
    {
        double sum = 0;
        foreach (Student student in Students)
        {
            sum += student.GetAverageScore();
        }
        AverageScore = sum / Students.Length;
    }
}

class GroupC : Group
{
    public GroupC(string name, Student[] students) : base(name, students) { }

    protected override void CalculateAverageScore()
    {
        double sum = 0;
        foreach (Student student in Students)
        {
            sum += student.GetAverageScore();
        }
        AverageScore = sum / Students.Length;
    }
}

class Student
{
    private string Name;
    private double[] ExamScores;
    private double AverageScore;

    public Student(string name, double[] examScores)
    {
        Name = name;
        ExamScores = examScores;
        CalculateAverageScore();
    }

    public double GetAverageScore()
    {
        return AverageScore;
    }

    private void CalculateAverageScore()
    {
        double sum = 0;
        foreach (double score in ExamScores)
        {
            sum += score;
        }
        AverageScore = sum / ExamScores.Length;
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

        Group groupA = new GroupA("Группа A", new Student[] { students[0], students[1], students[2] });
        Group groupB = new GroupB("Группа B", new Student[] { students[3], students[4], students[5] });
        Group groupC = new GroupC("Группа C", new Student[] { students[6], students[7], students[8] });

        Group[] allGroups = new Group[] { groupA, groupB, groupC };

        foreach (Group group in allGroups)
        {
            group.Print();
        }
    }
}