using System;

struct Student
{
    private string Name;
    private double[] ExamScores;
    private double AverageScore;

    public Student(string name, double[] examScores)
    {
        Name = name;
        ExamScores = examScores;
        AverageScore = CalculateAverageScore();
    }
    public double SetAverageScore()
    {
        return AverageScore;
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
            new Student("Student 1 Group 1", new double[] {80, 75, 90, 85, 95}),
            new Student("Student 2 Group 1", new double[] {65, 70, 75, 80, 85}),
            new Student("Student 3 Group 1", new double[] {85, 90, 95, 88, 92}),
            new Student("Student 4 Group 2", new double[] {75, 72, 75, 84, 89}),
            new Student("Student 5 Group 2", new double[] {70, 92, 82, 76, 95}),
            new Student("Student 6 Group 2", new double[] {88, 84, 92, 79, 93}),
            new Student("Student 7 Group 3", new double[] {68, 76, 70, 85, 82}),
            new Student("Student 8 Group 3", new double[] {79, 83, 86, 85, 90}),
            new Student("Student 9 Group 3", new double[] {82, 78, 85, 89, 91}),
        };
        for (int i = 0; i < students.Length - 1; i++)
        {
            for (int j = 0; j < students.Length - i - 1; j++)
            {
                if (students[j].SetAverageScore() < students[j + 1].SetAverageScore())
                {
                    Student temp = students[j];
                    students[j] = students[j + 1];
                    students[j + 1] = temp;
                }
            }
        }
        foreach (Student a in students)
        {
            a.Print();
        }
    }
}