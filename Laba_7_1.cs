using System;

abstract class Person
{
    protected string Name;
    protected double Votes;

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

class PersonOfTheYear : Person
{
    private static int _TotalVotes1 = 0;
    public PersonOfTheYear(string surname, int number) : base(surname, number)
    {
        _TotalVotes1 += number;
    }
    public override void Print()
    {
        Console.WriteLine("Фамилия {0}   \t {1} голосов\t доля {2:F1}%", Name, Votes, ((Votes / (double)_TotalVotes1) * 100));
    }
}
    class DiscoveryOfTheYear : Person
    {

    private static int _TotalVotes2 = 0;
    public DiscoveryOfTheYear(string name, int votes) : base(name, votes)
    {
        _TotalVotes2 += votes;
    }

    public override void Print()
    {
        Console.WriteLine("Открытие {0}   \t {1} голосов\t доля {2:F2}%", Name, Votes, ((Votes / (double)_TotalVotes2) * 100));
    }
}

class Program
{
    static void Main()
    {
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

        Sorts(otkr);
        Sorts(candidate);
        Console.WriteLine("Человек года");
        for (int i = 0; i < 5; i++)
        {
            candidate[i].Print();
        }

        Console.WriteLine("");
        Console.WriteLine("Открытие года");
        for (int i = 0; i < 5; i++)
        {
            otkr[i].Print();
        }
        Console.ReadKey();
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
    }
}