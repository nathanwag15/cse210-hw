using System;
using System.ComponentModel.Design;
using System.IO;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;


// For my Creative way to exceed Requirements I added a Workout Time into each Journal Entry that way the user can log how much work out time they performed. I also added a AverageWorkoutTime method to the Journal Class to allow for the user to find out their average workout time per entry. 
public class Entry
{

    public string _date ="";   
    public string _promptText = "";
    public string _content = "";
    public string _workoutTime = "";

    public Entry(string date, string content, string prompt, string workoutTime)
    {
        _date = date;
        _content = content;
        _promptText = prompt;
        _workoutTime = workoutTime;
    }
    public void Display()
    {
        Console.Write($"\nDate: {_date} - Prompt: {_promptText} \n{_content}\nWorkout Time: {_workoutTime}");
    }

    
    
}

class Journal
{
    public List<Entry> _entries= new List<Entry>();



    public void AddEntry()
    {
        string date = DateTime.Now.ToShortDateString();

        PromptGenerator prompt = new PromptGenerator();
        string selectedPrompt = prompt.GetRandomPrompt();

        Console.WriteLine(selectedPrompt + " ");
        string content = Console.ReadLine();

        Console.WriteLine("How many minutes did you work out today? ");
        string workoutTime = Console.ReadLine();

        Entry newEntry = new Entry(date, content, selectedPrompt, workoutTime);
        _entries.Add(newEntry);
    }

    public void AverageWorkoutTime()
    {
        double average = 0.00;
        int total = 0;
        foreach (Entry entry in _entries)
        {
            total = total + int.Parse(entry._workoutTime);
        }

        average = total / _entries.Count;
        Console.WriteLine($"\nYour average is {average} minutes");
    }

    public void DisplayAll()
    {
       foreach (Entry entry in _entries)
       {
            entry.Display();
       } 
    }

    public void SaveToFile()
    {
        Console.WriteLine("What is the filename?");
        string file = Console.ReadLine();
        Console.Write($"\nSaving to {file}...");
        using (StreamWriter outputFile = new StreamWriter(file))

        {
           foreach (Entry e in _entries)
           {
                outputFile.WriteLine($"{e._date}~~{e._promptText}~~{e._content}~~{e._workoutTime}");
           }  
        }
    }

    public void LoadFromFile()
    {
        Console.WriteLine("What is the filename?");
        string file = Console.ReadLine();
        Console.Write($"\nLoading from {file}...");
        string [] lines = System.IO.File.ReadAllLines(file);
        foreach (string line in lines)
        {
            string[] parts = line.Split("~~");
            string date = parts[0];
            string prompt = parts[1];
            string content = parts[2];
            string workoutTime = parts[3];

            Entry newEntry = new Entry(date, content, prompt, workoutTime);

            _entries.Add(newEntry);
            newEntry.Display();

        }

    }
}

class PromptGenerator
{
    public List<string> _prompts = new List<string> {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public string GetRandomPrompt()
    {
        Random random = new Random();
        int randomIndex = random.Next(0, _prompts.Count);
        return _prompts[randomIndex];
    }
}

class Program
{
    
    static void Main(string[] args)
    {   
        bool condition = true;
        Journal myJournal = new Journal();
        while (condition)
        {
            Console.Write("\nPlease select one of the following choices: \n 1. Write \n 2. Display \n 3. Load \n 4. Save\n 5. Check my Workout Average \n 6. Quit");
            Console.WriteLine("\nWhat would you like to do?");
            string answer = Console.ReadLine();

            if (answer == "1")
            {
                myJournal.AddEntry();
            }

            else if (answer == "2")
            {
                myJournal.DisplayAll();
            }

            else if (answer == "3")
            {
                myJournal.LoadFromFile();
            }

            else if (answer == "4")
            {
                myJournal.SaveToFile();
            }
            else if (answer == "5")
            {
                myJournal.AverageWorkoutTime();
            }
            else if (answer == "6")
            {
                Console.WriteLine("Well that was fun bye!");
                condition = false;
            }
            else 
            {
                Console.WriteLine("That was not a valid entry \n");
            }
        }
        
    }
}