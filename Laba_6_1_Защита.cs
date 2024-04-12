using System;
using static System.Formats.Asn1.AsnWriter;
using System.Text.RegularExpressions;
using System.Xml.Linq;

struct Diver
{
    private string Fam;
    private Jump[] jump;
    private double Score;

    public Diver(string Fam, Jump[] jump)
    {
        this.Fam = Fam;
        this.jump = jump;
        Score = 0;
        this.Score = CalcScore();
    }

    private double CalcScore()
    {
        double sum = 0;
        for (int i = 0; i < jump.Length; i++)
        {
            sum += jump[i].CalcScore();
        }
        return sum;
    }
    public string GetFam()
    {
        return Fam;
    }
    public double GetScore()
    {
        return Score;
    }
    public void Print()
    {
        Console.WriteLine($"{Fam}  {Score}");
    }
}

struct Jump
{
    private int[] scores;
    private double Coef;

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
class Program
{
    static void Main()
    {
        Diver[] divers = new Diver[6];
        divers[0] = new Diver("Иванов", new Jump[]
        {
            new Jump(new int[]{5, 4, 6, 3, 2, 5, 4}, 2.5),
            new Jump(new int[]{6, 6, 5, 4, 3, 6, 5}, 2.7),
            new Jump(new int[]{4, 5, 4, 6, 3, 5, 4}, 2.9),
            new Jump(new int[]{3, 4, 3, 5, 2, 4, 3}, 3.1)
        });
        divers[1] = new Diver("Поляков", new Jump[]
        {
            new Jump(new int[]{2, 4, 6, 3, 2, 5, 4}, 2.8),
            new Jump(new int[]{6, 6, 5, 4, 3, 6, 5}, 2.5),
            new Jump(new int[]{4, 5, 4, 6, 3, 5, 4}, 2.8),
            new Jump(new int[]{3, 4, 3, 5, 2, 4, 3}, 3.2)
        });

        divers[2] = new Diver("Смирнов", new Jump[]
        {
            new Jump(new int[]{2, 4, 6, 3, 2, 5, 4}, 2.5),
            new Jump(new int[]{6, 6, 5, 4, 3, 6, 5}, 2.8),
            new Jump(new int[]{4, 5, 4, 5, 3, 5, 4}, 2.6),
            new Jump(new int[]{3, 4, 3, 5, 2, 6, 3}, 3.3)
        });

        divers[3] = new Diver("Титов", new Jump[]
        {
            new Jump(new int[]{4, 4, 6, 3, 2, 5, 4}, 2.5),
            new Jump(new int[]{6, 2, 5, 4, 3, 6, 5}, 2.6),
            new Jump(new int[]{4, 5, 4, 6, 3, 5, 4}, 2.7),
            new Jump(new int[]{3, 6, 3, 5, 2, 4, 3}, 3.4)
        });

        divers[4] = new Diver("Гайкалов", new Jump[]
        {
            new Jump(new int[]{5, 4, 6, 3, 2, 5, 4}, 2.5),
            new Jump(new int[]{6, 6, 5, 4, 3, 6, 5}, 2.7),
            new Jump(new int[]{4, 5, 4, 6, 3, 5, 4}, 2.9),
            new Jump(new int[]{3, 4, 3, 5, 2, 4, 3}, 3.1)
        });
        divers[5] = new Diver("Ермаков", new Jump[]
{
            new Jump(new int[]{5, 4, 6, 3, 2, 5, 4}, 2.5),
            new Jump(new int[]{2, 5, 5, 4, 3, 6, 5}, 2.7),
            new Jump(new int[]{4, 5, 4, 6, 2, 5, 4}, 2.7),
            new Jump(new int[]{3, 4, 3, 5, 2, 4, 3}, 3.1)
});
        // Сортировка Шелла
        int n = divers.Length;
        for (int N = n / 2; N > 0; N /= 2)
        {
            for (int i = N; i < n; i++)
            {
                Diver temp = divers[i];
                int j;
                for (j = i; j >= N && divers[j - N].GetScore() < temp.GetScore(); j -=N)
                {
                    divers[j] = divers[j - N];
                }
                divers[j] = temp;
            }
        }
        Console.WriteLine(n);

        foreach (Diver a in divers)
        {
            a.Print();
        }
    }
}
