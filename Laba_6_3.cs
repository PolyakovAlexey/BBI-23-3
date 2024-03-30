using System;

class Program
{
    struct Results
    {
        private string Name;
        private double[] ExamScores;

        public Results(string Names, double[] examScores)
        {
            Name = Names;
            ExamScores = examScores;
        }

        private double Score()
        {
            double sum = 0;
            for (int i = 0; i < ExamScores.Length; i++)
            {
                sum += ExamScores[i];
            }
            return sum / ExamScores.Length;
        }

        private static void SortScore(Results[] groups)
        {
            for (int i = 0; i < groups.Length - 1; i++)
            {
                for (int j = 0; j < groups.Length - i - 1; j++)
                {
                    if (groups[j + 1].Score() > groups[j].Score())
                    {
                        Results temp = groups[j];
                        groups[j] = groups[j + 1];
                        groups[j + 1] = temp;
                    }
                }
            }
        }

        public static void ShowResults(Results[] groups)
        {
            SortScore(groups);

            Console.WriteLine("Группа     Средний балл");
            for (int i = 0; i < groups.Length; i++)
            {
                Console.WriteLine(groups[i].Name + "       " + groups[i].Score());
            }
        }
    }

    static void Main()
    {
        Results[] groups = new Results[5]
        {
            new Results("Группа 1", new double[] {80, 75, 90, 85, 95}),
            new Results("Группа 2", new double[] {65, 70, 75, 80, 85}),
            new Results("Группа 3", new double[] {85, 90, 95, 88, 92}),
            new Results("Группа 4", new double[] {75, 72, 75, 84, 89}),
            new Results("Группа 5", new double[] {70, 92, 82, 76, 95}),
        };

        Results.ShowResults(groups);
    }
}