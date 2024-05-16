using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualBasic;

class Activity
{
    protected string _name;
    protected string _description;
    protected int _duration;

    public void DisplayStartingMessage()
    {
        Console.WriteLine($"Welcome to the {_name}");
        Console.WriteLine($"\n {_description}");
        Console.Write("How long, in seconds would you like for your session? ");
        _duration = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(5);
        
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine("\nWell done!!");
        ShowSpinner(5);
        Console.WriteLine($"\n You have completed another {_duration} seconds of the {_name}.");
        ShowSpinner(5);
        Console.Clear();
    }

    public void ShowSpinner(int seconds)
    {
        
        // for (int i = seconds; i > 0; i--)
        // {
        //     Console.Write("|");
        //     Thread.Sleep(500);
        //     Console.Write("\b \b");
        //     Console.Write("/");
        // }
        List<string> animationStrings = new List<string>();
        animationStrings.Add("|");
        animationStrings.Add("/");
        animationStrings.Add("-");
        animationStrings.Add("\\");
        animationStrings.Add("|");
        animationStrings.Add("/");
        animationStrings.Add("-");
        animationStrings.Add("\\");

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(seconds);
        int i = 0;
        while (DateTime.Now < endTime)
        {
            string s = animationStrings[i];
            Console.Write(s);
            Thread.Sleep(1000);
            Console.Write("\b \b");
            i++;
            if (i >= animationStrings.Count)
            {
                i = 0;
            }
            
        }

    }

    public void ShowCountDown(int seconds)
    {
        
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }
}

class ListingActivity : Activity
{
    private int _count = 0;
    private List<string> _prompts = new List<string>();

    public ListingActivity()
    {
        _prompts = ["Who are people that you appreciate?", "What are personal strengths of yours?", "Who are people that you have helped this week?", "When have you felt the Holy Ghost this month?", "Who are some of your personal heroes?"];
        _name = "Listing Activity";
        _description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area";
    }

    public void GetRandomPrompt()
    {
        Random rand = new Random();

        int randomIndex = rand.Next(0, _prompts.Count);

        Console.WriteLine($" --- {_prompts[randomIndex]} ---");
    }

    public List<string> GetListFromUser()
    {

        ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);
        List<string> answers = new List<string>();

        

        Thread inputThread = new Thread(() =>
        {
            // List<string> answers = new List<string>();
            
            while (!resetEvent.Wait(0))
            {
                string item = Console.ReadLine();
                answers.Add(item);
            }
            resetEvent.Set();
        });

        inputThread.Start();


        System.Threading.Thread.Sleep(_duration * 1000);

        resetEvent.Set();


        inputThread.Join();

        return answers;
    }
    public void Run()
    {
        DisplayStartingMessage();
        Console.WriteLine("List as many responses you can to the following prompt:");
        GetRandomPrompt();
        Console.Write("You may begin in: ");
        ShowCountDown(9);
        Console.Write("\n");

        _count = GetListFromUser().Count();
        Console.WriteLine($"You listed {_count} items!");

        ShowCountDown(9);
        DisplayEndingMessage();

    }
}
class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        _name = "Breathing Activity";
        _description = "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }
    public void Run()
    {
        DisplayStartingMessage();
        Console.Write("\n");
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);
        while (DateTime.Now < endTime)
        {
            Console.Write("\n\nBreathe in...");
            ShowCountDown(5);
            Console.Write("\nNow breathe Out...");
            ShowCountDown(5);            
        }
        DisplayEndingMessage();

    }
}

class ReflectingActivity : Activity
{
    private List<string> _prompts= new List<string>();
    private List<string> _questions = new List<string>();

    public ReflectingActivity()
    {
        _description = "his activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
        _name = "Reflection Activity";
        _prompts = [
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult", 
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
        ];
        _questions = [
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?"
        ];
    }

    private string GetRandomPrompt()
    {
        Random rand = new Random();

        int randomIndex = rand.Next(0, _prompts.Count);

        return _prompts[randomIndex];
    }

    private string GetRandomQuestion()
    {
        Random rand = new Random();

        int randomIndex = rand.Next(0, _questions.Count);

        return _questions[randomIndex];
    }

    private void DisplayPrompt()
    {
        Console.WriteLine("Consider the following prompt:\n");
        Console.WriteLine($" --- {GetRandomPrompt()}2 --- ");
        Console.WriteLine("\n\nWhen you have something in mind, press enter to continue.");
        string answer = Console.ReadLine();
    }

    private void DisplayRandomQuestion()
    {
        Console.WriteLine($"\n{GetRandomQuestion()}");
        ShowSpinner(9);
    }

    public void Run()
    {
        DisplayStartingMessage();
        DisplayPrompt();

        Console.WriteLine("\nNow ponder on each of the following questions as they related to this experience.\n");
        Console.Write("You may begin in: ");
        ShowCountDown(8);
        Console.Write("\n");
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);
        while (DateTime.Now < endTime)
        {
            DisplayRandomQuestion();            
        }
        DisplayEndingMessage();
    }
}
class Program
{
    static void Main(string[] args)
    
    {
       bool condition = true;
       
       
       
        while (condition)
        {
            Console.Write("\nPlease select one of the following choices: \n 1. Breathing Activity \n 2. Reflection Activity \n 3. Listing Activity \n 4. Quit\n");
            Console.WriteLine("\nWhat would you like to do?");
            string answer = Console.ReadLine();

            if (answer == "1")
            {
                Console.Clear();
                BreathingActivity myBreathingActivity = new BreathingActivity();
                myBreathingActivity.Run();
            }

            else if (answer == "2")
            {
                Console.Clear();
                ReflectingActivity myReflectingActivity = new ReflectingActivity();
                myReflectingActivity.Run();
            }

            else if (answer == "3")
            {
                Console.Clear();
                ListingActivity myListingActivity = new ListingActivity();
                myListingActivity.Run();
            }

            else if (answer == "4")
            {
                Console.Clear();
                condition = false;
            }
        }
        
    }
}