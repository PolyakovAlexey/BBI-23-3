using System;

class Program
{
    struct PersonOfTheYear
    {
        private string Name;
        private double Votes;
        private double Percentage;

        public PersonOfTheYear(string _name, double _votes, double _percentage)
        {
            Name = _name;
            Votes = _votes;
            Percentage = _percentage;
        }
        public double GetVotes()
        {
            return Votes;
        }
        public void Print()
        {
            Console.WriteLine($"{Name}  {Votes}  {Percentage:F1}%");
        }
    }

    static void Main(string[] args)
    {
        string[] candidates = new string[5] { "Кандидат A", "Кандидат B", "Кандидат C", "Кандидат D", "Кандидат E" };
        double[] votes = new double[5] { 100, 80, 75, 120, 90 }; // Пример значений голосов
        double totalVotes = 0;

        for (int i = 0; i < votes.Length; i++)
        {
            totalVotes += votes[i];
        }

        PersonOfTheYear[] persons = new PersonOfTheYear[5];

        for (int i = 0; i < candidates.Length; i++)
        {
            double vote = votes[i];
            double percentage = vote / totalVotes * 100;

            persons[i] = new PersonOfTheYear(candidates[i], vote, percentage);
        }
        for (int i = 0; i < persons.Length - 1; i++)
        {
            for (int j = i + 1; j < persons.Length; j++)
            {
                if (persons[j].GetVotes() > persons[i].GetVotes())
                {
                    PersonOfTheYear temp = persons[i];
                    persons[i] = persons[j];
                    persons[j] = temp;

                }
            }
        }
        foreach (PersonOfTheYear a in persons)
        {
            a.Print();
        }
    }
}

