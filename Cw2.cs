using System.IO;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics.Tracing;
using System.Security.AccessControl;
using System.Globalization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

abstract class Task
{
    protected string text;
    public string Text
    {
        get => text;
        protected set => text = value;
    }
    public Task(string text)
    {
        this.text = text;
    }
}
class Task1 : Task
{

    public Task1(string text) : base(text) { }
    public override string ToString()
    {
        return SameLettersCount(text);
    }
    public string SameLettersCount(string text)
    {
        var uniqueLetters = new HashSet<char>();
        var sameLetters = new HashSet<char>();
        foreach (var letter in text)
        {
            if (!uniqueLetters.Contains(letter))
            {
                uniqueLetters.Add(letter);
            }
            else
            {
                sameLetters.Add(letter);
            }
        }
        return $"Повторяющиеся буквы: {string.Join(", ", sameLetters)}";
    }


}
class Program
{
    static void Main()
    {
        Task[] tasks = {
            new Task1("Привет мир"),
        };

        Console.WriteLine(tasks[0]);
        string path = @"C:\Users\m2303858\Desktop"; 
        string folderName = "Solution"; 
        path = Path.Combine(path, folderName);
        if (!Directory.Exists(path))    
        {
            Directory.CreateDirectory(path);
        }
        string fileName1 = "task_1.json"; 
        string fileName2 = "task_2.json";

        fileName1 = Path.Combine(path, fileName1);
        fileName2 = Path.Combine(path, fileName2);

        #region 
        if (!File.Exists(fileName1))
        {
            var filec = File.Create(fileName1);
            filec.Close();
        }
        #endregion

    }
}


