using System;
using System.Linq;

class Metres3 : Diver
{
    public Metres3(string fam, Jump[] jumps) : base(fam, jumps)
    {
        _Discipline = "3 metres";
    }
}

class Metres5 : Diver
{
    public Metres5(string fam, Jump[] jumps) : base(fam, jumps)
    {
        _Discipline = "5 metres";
    }
}

public class Diver
{
    protected string _Discipline;
    protected string Fam;
    protected Jump[] Jumps;
    protected double Score;

    public Diver(string fam, Jump[] jumps)
    {
        Fam = fam;
        Jumps = jumps;
        Score = CalcScore();
    }

    public Diver(string fam, double score, double normalizer)
    {
        Fam = fam;
        Score = score * normalizer;
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

    public string GetFam()
    {
        return Fam;
    }

    public string GetDiscipline()
    {
        return _Discipline;
    }

    public double GetScore()
    {
        return Score;
    }

    public void Print()
    {
        Console.WriteLine($"{_Discipline}\t{Fam}\t\t{Score:F2}");
    }
}

public class Jump
{
    private int[] scores;
    private double Coef;

    public Jump(int[] scores, double coef)
    {
        this.scores = scores;
        this.Coef = coef;
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

class Program
{
    static void Main()
    {
        Diver[] divers3m = new Diver[3];
        divers3m[0] = new Metres3("Иванов", new Jump[]
        {
            new Jump(new int[] { 3, 4, 3, 5, 2, 4, 3 }, 3.1)
        });

        divers3m[1] = new Metres3("Смирнов", new Jump[]
        {
            new Jump(new int[] { 3, 4, 3, 5, 2, 6, 3 }, 3.3)
        });

        divers3m[2] = new Metres3("Гайкалов", new Jump[]
        {
            new Jump(new int[] { 3, 4, 3, 5, 2, 4, 3 }, 3.1)
        });

        Diver[] divers5m = new Diver[3];
        divers5m[0] = new Metres5("Поляков", new Jump[]
        {
            new Jump(new int[] { 3, 4, 3, 5, 2, 4, 3 }, 3.2)
        });

        divers5m[1] = new Metres5("Титов", new Jump[]
        {
            new Jump(new int[] { 3, 6, 3, 5, 2, 4, 3 }, 3.4)
        });

        divers5m[2] = new Metres5("Андреев", new Jump[]
        {
            new Jump(new int[] { 3, 4, 3, 5, 2, 4, 3 }, 3.1)
        });

        Console.WriteLine("Соревнования по прыжкам на 3 метра");
        SortDivers(divers3m);
        PrintDivers(divers3m);

        Console.WriteLine("\nСоревнования по прыжкам на 5 метров");
        SortDivers(divers5m);
        PrintDivers(divers5m);

        Diver[] allDivers = new Diver[divers3m.Length + divers5m.Length];

        for (int i = 0; i < divers3m.Length; i++)
        {
            allDivers[i] = new Diver(divers3m[i].GetFam(), divers3m[i].GetScore(), 0.5);
        }

        for (int i = 0; i < divers5m.Length; i++)
        {
            allDivers[i + divers3m.Length] = new Diver(divers5m[i].GetFam(), divers5m[i].GetScore(), 0.3);
        }

        SortDivers(allDivers);

        Console.WriteLine("\nОбщий список дайверов");
        PrintDivers(allDivers);
    }

    static void SortDivers(Diver[] divers)
    {
        for (int i = 0; i < divers.Length - 1; i++)
        {
            for (int j = 0; j < divers.Length - i - 1; j++)
            {
                if (divers[j].GetScore() < divers[j + 1].GetScore())
                {
                    Diver temp = divers[j];
                    divers[j] = divers[j + 1];
                    divers[j + 1] = temp;
                }
            }
        }
    }

    static void PrintDivers(Diver[] divers)
    {
        foreach (var diver in divers)
        {
            diver.Print();
        }
    }
}
