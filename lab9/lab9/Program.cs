using System.Text.Json.Serialization;
using System.Xml.Serialization;
using lab9.serializers;
using ProtoBuf;

namespace lab9;

// 1 уровень
[Serializable]
[ProtoContract]
[ProtoInclude(10, typeof(PersonOfTheYear))]
[ProtoInclude(20, typeof(DiscoveryOfTheYear))]
public abstract class Person
{
    // публичные геттеры и сеттеры для сериализации
    [ProtoMember(1)] public string Name { get; set; }
    [ProtoMember(2)] public double Votes { get; set; }

    protected Person()
    {
    }

    protected Person(string name, int votes)
    {
        Name = name;
        Votes = votes;
    }

    public virtual void Print()
    {
        double n = Votes;
        Console.WriteLine("Фамилия {0}   \t {1} голосов", Name, Votes);
    }

    public double GetVotes()
    {
        return Votes;
    }
}

[Serializable]
[ProtoContract]
public class PersonOfTheYear : Person
{
    private static int _TotalVotes1;

    public PersonOfTheYear()
    {
    }

    public PersonOfTheYear(string surname, int number) : base(surname, number)
    {
        _TotalVotes1 += number;
    }

    public override void Print()
    {
        Console.WriteLine("Фамилия {0}   \t {1} голосов\t доля {2:F1}%", Name, Votes,
            ((Votes / (double)_TotalVotes1) * 100));
    }
}

[Serializable]
[ProtoContract]
public class DiscoveryOfTheYear : Person
{
    private static int _TotalVotes2;

    public DiscoveryOfTheYear()
    {
    }

    public DiscoveryOfTheYear(string name, int votes) : base(name, votes)
    {
        _TotalVotes2 += votes;
    }

    public override void Print()
    {
        Console.WriteLine("Открытие {0}   \t {1} голосов\t доля {2:F2}%", Name, Votes,
            ((Votes / (double)_TotalVotes2) * 100));
    }
}

// 2 уровень
[Serializable]
[ProtoContract]
public class Metres3 : Diver
{
    public Metres3()
    {
    }

    public Metres3(string fam, Jump[] jump) : base(fam, jump)
    {
        Discipline = "3 metres";
    }

    [JsonConstructor]
    public Metres3(string discipline, string fam, Jump[] jumps, double score) : base(discipline, fam, jumps, score)
    {
    }
}

[Serializable]
[ProtoContract]
public class Metres5 : Diver
{
    public Metres5()
    {
    }

    public Metres5(string fam, Jump[] jump) : base(fam, jump)
    {
        Discipline = "5 metres";
    }

    [JsonConstructor]
    public Metres5(string discipline, string fam, Jump[] jumps, double score) : base(discipline, fam, jumps, score)
    {
    }
}

[Serializable]
[ProtoContract]
[ProtoInclude(30, typeof(Metres3))]
[ProtoInclude(40, typeof(Metres5))]
[XmlInclude(typeof(Metres3))]
[XmlInclude(typeof(Metres5))]
[JsonDerivedType(typeof(Metres3), typeDiscriminator: "3")]
[JsonDerivedType(typeof(Metres5), typeDiscriminator: "5")]
public abstract class Diver
{
    [ProtoMember(1)] public string Discipline { get; set; }
    [ProtoMember(2)] public string Fam { get; set; }
    [ProtoMember(3)] public Jump[] Jumps { get; set; }
    [ProtoMember(4)] public double Score { get; set; }

    protected Diver()
    {
    }

    protected Diver(string fam, Jump[] jump)
    {
        Fam = fam;
        Jumps = jump;
        Score = CalcScore();
    }
    
    protected Diver(string discipline, string fam, Jump[] jumps, double score)
    {
        Discipline = discipline;
        Fam = fam;
        Jumps = jumps;
        Score = score;
    }

    private double CalcScore()
    {
        double sum = 0;
        for (int i = 0; i < Jumps.Length; i++)
        {
            sum += Jumps[i].CalcScore();
        }

        return sum;
    }

    public void Print()
    {
        Console.WriteLine($"{Discipline}\t{Fam}\t\t{Score}");
    }
}

[Serializable]
[ProtoContract]
public class Jump
{
    [ProtoMember(5)] public int[] scores { get; set; }
    [ProtoMember(6)] public double Coef { get; set; }

    public Jump()
    {
    }

    public Jump(int[] scores, double Coef)
    {
        this.scores = scores;
        this.Coef = Coef;
    }

    public double CalcScore()
    {
        for (int i = 0; i < scores.Length - 1; i++)
        {
            for (int j = 0; j < scores.Length - i - 1; j++)
            {
                if (scores[j] > scores[j + 1])
                {
                    int temp = scores[j];
                    scores[j] = scores[j + 1];
                    scores[j + 1] = temp;
                }
            }
        }

        double sum = 0;
        for (int i = 1; i < 6; i++)
        {
            sum += scores[i];
        }

        return sum * Coef;
    }
}




// 3 уровень
[Serializable]
[ProtoContract]
[ProtoInclude(50, typeof(GroupA))]
[ProtoInclude(60, typeof(GroupB))]
[ProtoInclude(70, typeof(GroupC))]
[XmlInclude(typeof(GroupA))]
[XmlInclude(typeof(GroupB))]
[XmlInclude(typeof(GroupC))]
[JsonDerivedType(typeof(GroupA), typeDiscriminator: "a")]
[JsonDerivedType(typeof(GroupB), typeDiscriminator: "b")]
[JsonDerivedType(typeof(GroupC), typeDiscriminator: "c")]
public abstract class Group
{
    [ProtoMember(1)]
    public string Name { get; set; }
    [ProtoMember(2)]
    public Student[] Students { get; set; }
    [ProtoMember(3)]
    public double AverageScore { get; set; }
    
    protected Group(){}

    protected Group(string name, Student[] students)
    {
        Name = name;
        Students = students;
        CalculateAverageScore();
    }

    public virtual void CalculateAverageScore()
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

[Serializable]
[ProtoContract]
public class GroupA : Group
{
    public GroupA(){}
    
    public GroupA(string name, Student[] students) : base(name, students) { }

    public override void CalculateAverageScore()
    {
        double sum = 0;
        foreach (Student student in Students)
        {
            sum += student.GetAverageScore(); 
        }
        AverageScore = sum / Students.Length;
    }
}

[Serializable]
[ProtoContract]
public class GroupB : Group
{
    public GroupB(){}
    
    public GroupB(string name, Student[] students) : base(name, students) { }

    public override void CalculateAverageScore()
    {
        double sum = 0;
        foreach (Student student in Students)
        {
            sum += student.GetAverageScore();
        }
        AverageScore = sum / Students.Length;
    }
}

[Serializable]
[ProtoContract]
public class GroupC : Group
{
    public GroupC(){}
    
    public GroupC(string name, Student[] students) : base(name, students) { }

    public override void CalculateAverageScore()
    {
        double sum = 0;
        foreach (Student student in Students)
        {
            sum += student.GetAverageScore();
        }
        AverageScore = sum / Students.Length;
    }
}

[Serializable]
[ProtoContract]
public class Student
{
    [ProtoMember(1)]
    public string Name { get; set; }
    [ProtoMember(2)]
    public double[] ExamScores { get; set; }
    [ProtoMember(3)]
    public double AverageScore { get; set; }
    
    public Student(){}

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




public class Program
{
    static void Sorts(Person[] a)
    {
        for (int i = 0; i < a.Length - 1; i++)
        {
            for (int j = i + 1; j < a.Length; j++)
            {
                if (a[j].GetVotes() > a[i].GetVotes())
                {
                    Person temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                }
            }
        }
    }

    // удаляем файл если он существует
    static void DeleteFileIfExist(string filename)
    {
        if (File.Exists(filename))
        {
            File.Delete(filename);
        }
    }

    // выводим инфу о массиве людей года (придерживамся DRY)
    static void PrintPersons(PersonOfTheYear[] persons)
    {
        foreach (PersonOfTheYear person in persons)
        {
            person.Print();
        }
    }

    // выводим инфу о массиве открытий года (DRY)
    static void PrintPersons(DiscoveryOfTheYear[] persons)
    {
        foreach (DiscoveryOfTheYear person in persons)
        {
            person.Print();
        }
    }

    // выводим информацию о дайверах
    static void PrintDivers(Diver[] divers)
    {
        foreach (Diver diver in divers)
        {
            diver.Print();
        }
    }

    // выводим информацию о группах
    static void PrintGroups(Group[] groups)
    {
        foreach (Group group in groups)
        {
            group.Print();
        }
    }

    public static void Main(string[] args)
    {
        // 1 уровень
        string directoryBinary = "C:/Users/Алексей/Desktop/lab9/lab9/files/binary/";
        string directoryJson = "C:/Users/Алексей/Desktop/lab9/lab9/files/json/";
        string directoryXml = "C:/Users/Алексей/Desktop/lab9/lab9/files/xml/";

        PersonOfTheYear[] candidate = new PersonOfTheYear[5];
        candidate[0] = new PersonOfTheYear("Кандидат A", 100);
        candidate[1] = new PersonOfTheYear("Кандидат B", 80);
        candidate[2] = new PersonOfTheYear("Кандидат C", 75);
        candidate[3] = new PersonOfTheYear("Кандидат D", 120);
        candidate[4] = new PersonOfTheYear("Кандидат E", 90);

        DiscoveryOfTheYear[] otkr = new DiscoveryOfTheYear[5];
        otkr[0] = new DiscoveryOfTheYear("Новый вид энергии ", 50);
        otkr[1] = new DiscoveryOfTheYear("Новое вещество    ", 75);
        otkr[2] = new DiscoveryOfTheYear("ИИ                ", 25);
        otkr[3] = new DiscoveryOfTheYear("Лекарство от рака ", 110);
        otkr[4] = new DiscoveryOfTheYear("Новый остров      ", 65);

        // сортим
        Sorts(otkr);
        Sorts(candidate);

        string filenameBinary1 = "person_of_the_year.bin";
        string filenameJson1 = "person_of_the_year.json";
        string filenameXml1 = "person_of_the_year.xml";
        DeleteFileIfExist(directoryBinary + filenameBinary1);
        DeleteFileIfExist(directoryXml + filenameXml1);
        DeleteFileIfExist(directoryJson + filenameJson1);

        // создаем объекты для сериализации
        MyBinarySerializer myBinarySerializer = new MyBinarySerializer();
        MyJsonSerializer myJsonSerializer = new MyJsonSerializer();
        MyXmlSerializer myXmlSerializer = new MyXmlSerializer();

        // сериализуем массивы с людьми года в соответсвующие форматы и файлы
        myBinarySerializer.Write(candidate, directoryBinary + filenameBinary1);
        myJsonSerializer.Write(candidate, directoryJson + filenameJson1);
        myXmlSerializer.Write(candidate, directoryXml + filenameXml1);

        // десериализуем объекты из файлов и выводим инфу на экран
        Console.WriteLine("binary");
        Console.WriteLine("Человек года");
        PrintPersons(myBinarySerializer.Read<PersonOfTheYear[]>(directoryBinary + filenameBinary1));
        Console.WriteLine("json");
        Console.WriteLine("Человек года");
        PrintPersons(myJsonSerializer.Read<PersonOfTheYear[]>(directoryJson + filenameJson1));
        Console.WriteLine("xml");
        Console.WriteLine("Человек года");
        PrintPersons(myXmlSerializer.Read<PersonOfTheYear[]>(directoryXml + filenameXml1));

        string filenameBinary2 = "discovery_of_the_year.bin";
        string filenameJson2 = "discovery_of_the_year.json";
        string filenameXml2 = "discovery_of_the_year.xml";
        DeleteFileIfExist(directoryBinary + filenameBinary2);
        DeleteFileIfExist(directoryXml + filenameXml2);
        DeleteFileIfExist(directoryJson + filenameJson2);

        // сериализуем массивы с открытиями года в соответсвующие форматы и файлы
        myBinarySerializer.Write(otkr, directoryBinary + filenameBinary2);
        myJsonSerializer.Write(otkr, directoryJson + filenameJson2);
        myXmlSerializer.Write(otkr, directoryXml + filenameXml2);

        // десериализуем объекты из файлов и выводим инфу на экран
        Console.WriteLine("binary");
        Console.WriteLine("Открытие года");
        PrintPersons(myBinarySerializer.Read<DiscoveryOfTheYear[]>(directoryBinary + filenameBinary2));
        Console.WriteLine("json");
        Console.WriteLine("Открытие года");
        PrintPersons(myJsonSerializer.Read<DiscoveryOfTheYear[]>(directoryJson + filenameJson2));
        Console.WriteLine("xml");
        Console.WriteLine("Открытие года");
        PrintPersons(myXmlSerializer.Read<DiscoveryOfTheYear[]>(directoryXml + filenameXml2));


        // 2 уровень
        Diver[] divers = new Diver[5];
        divers[0] = new Metres3("Иванов", new Jump[]
        {
            new Jump(new int[] { 5, 4, 6, 3, 2, 5, 4 }, 2.5),
            new Jump(new int[] { 6, 6, 5, 4, 3, 6, 5 }, 2.7),
            new Jump(new int[] { 4, 5, 4, 6, 3, 5, 4 }, 2.9),
            new Jump(new int[] { 3, 4, 3, 5, 2, 4, 3 }, 3.1)
        });

        divers[1] = new Metres5("Поляков", new Jump[]
        {
            new Jump(new int[] { 2, 4, 6, 3, 2, 5, 4 }, 2.8),
            new Jump(new int[] { 6, 6, 5, 4, 3, 6, 5 }, 2.5),
            new Jump(new int[] { 4, 5, 4, 6, 3, 5, 4 }, 2.8),
            new Jump(new int[] { 3, 4, 3, 5, 2, 4, 3 }, 3.2)
        });

        divers[2] = new Metres3("Смирнов", new Jump[]
        {
            new Jump(new int[] { 2, 4, 6, 3, 2, 5, 4 }, 2.5),
            new Jump(new int[] { 6, 6, 5, 4, 3, 6, 5 }, 2.8),
            new Jump(new int[] { 4, 5, 4, 5, 3, 5, 4 }, 2.6),
            new Jump(new int[] { 3, 4, 3, 5, 2, 6, 3 }, 3.3)
        });

        divers[3] = new Metres5("Титов", new Jump[]
        {
            new Jump(new int[] { 4, 4, 6, 3, 2, 5, 4 }, 2.5),
            new Jump(new int[] { 6, 2, 5, 4, 3, 6, 5 }, 2.6),
            new Jump(new int[] { 4, 5, 4, 6, 3, 5, 4 }, 2.7),
            new Jump(new int[] { 3, 6, 3, 5, 2, 4, 3 }, 3.4)
        });

        divers[4] = new Metres3("Гайкалов", new Jump[]
        {
            new Jump(new int[] { 5, 4, 6, 3, 2, 5, 4 }, 2.5),
            new Jump(new int[] { 6, 6, 4, 4, 3, 6, 5 }, 2.7),
            new Jump(new int[] { 4, 5, 4, 6, 3, 5, 4 }, 2.9),
            new Jump(new int[] { 3, 4, 3, 5, 2, 4, 3 }, 3.1)
        });

        for (int i = 0; i < divers.Length - 1; i++)
        {
            for (int j = 0; j < divers.Length - i - 1; j++)
            {
                if (divers[j].Score < divers[j + 1].Score)
                {
                    Diver temp = divers[j];
                    divers[j] = divers[j + 1];
                    divers[j + 1] = temp;
                }
            }
        }

        string filenameBinary3 = "divers.bin";
        string filenameJson3 = "divers.json";
        string filenameXml3 = "divers.xml";
        DeleteFileIfExist(directoryBinary + filenameBinary3);
        DeleteFileIfExist(directoryXml + filenameXml3);
        DeleteFileIfExist(directoryJson + filenameJson3);

        // сериализуем массивы с драйверами в соответсвующие форматы и файлы
        myBinarySerializer.Write(divers, directoryBinary + filenameBinary3);
        myJsonSerializer.Write(divers, directoryJson + filenameJson3);
        myXmlSerializer.Write(divers, directoryXml + filenameXml3);

        // десериализуем объекты из файлов и выводим инфу на экран
        Console.WriteLine("binary");
        Console.WriteLine("дисциплина\tфамилия\t\tбалл");
        PrintDivers(myBinarySerializer.Read<Diver[]>(directoryBinary + filenameBinary3));

        Console.WriteLine("json");
        Console.WriteLine("дисциплина\tфамилия\t\tбалл");
        PrintDivers(myJsonSerializer.Read<Diver[]>(directoryJson + filenameJson3));

        Console.WriteLine("xml");
        Console.WriteLine("дисциплина\tфамилия\t\tбалл");
        PrintDivers(myXmlSerializer.Read<Diver[]>(directoryXml + filenameXml3));
        
        
        
        
        // 3 уровень
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
        
        string filenameBinary4 = "groups.bin";
        string filenameJson4 = "groups.json";
        string filenameXml4 = "groups.xml";
        DeleteFileIfExist(directoryBinary + filenameBinary4);
        DeleteFileIfExist(directoryXml + filenameXml4);
        DeleteFileIfExist(directoryJson + filenameJson4);
        
        // сериализуем массивы с группами в соответсвующие форматы и файлы
        myBinarySerializer.Write(allGroups, directoryBinary + filenameBinary4);
        myJsonSerializer.Write(allGroups, directoryJson + filenameJson4);
        myXmlSerializer.Write(allGroups, directoryXml + filenameXml4);

        // десериализуем объекты из файлов и выводим инфу на экран
        Console.WriteLine("binary");
        PrintGroups(myBinarySerializer.Read<Group[]>(directoryBinary + filenameBinary4));
        
        Console.WriteLine("json");
        PrintGroups(myJsonSerializer.Read<Group[]>(directoryJson + filenameJson4));
        
        Console.WriteLine("binary");
        PrintGroups(myXmlSerializer.Read<Group[]>(directoryXml + filenameXml4));
        
    }
}