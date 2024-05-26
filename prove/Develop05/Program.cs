using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        goalManager.Start();
    }
}


class GoalManager 
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public GoalManager()
    {
    }

    public void Start()
    {
        bool condition = true;
        while (condition)
        {
            DisplayPlayerInfo();
            Console.Write("\nMenu Options: \n 1. Create New Goal \n 2. List Goals \n 3. Save Goals \n 4. Load Goals\n 5. Record Event \n 6. Quit");
            Console.WriteLine("\nWhat would you like to do?");
            string answer = Console.ReadLine();
            if (answer == "1")
            {
                CreateGoal();
            }
            else if (answer == "2")
            {
                ListGoalDetails();
            }
            else if (answer == "3")
            {
                SaveGoals();
            }
            else if (answer == "4")
            {
                
                LoadFromFile();
            }
            else if (answer == "5")
            {
                RecordEvent();
            }
            else if (answer == "6")
            {
                Console.WriteLine("Well that was fun bye!");
                condition = false;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        // I added a rank system to the wonderful game.
        if(_score >= 500 && _score <= 700)
        {
            Console.WriteLine("\nYou have reached the rank of captain goal guy");
        }
        else if (_score >= 700 && _score <= 1000)
        {
            Console.WriteLine("\nYou have reached the rank of sensei goal guy");
            
        }
        else if (_score >= 1000)
        {
            Console.WriteLine("\nYou have reached the rank of master goal guy");
        }
        Console.WriteLine($"\nYou have {_score} points.");
    }

    public void ListGoalNames()
    {
        Console.WriteLine("The Goals are: ");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i+1}. {_goals[i]._shortname}");
        }
    }

    public void ListGoalDetails()
    {
        Console.WriteLine("The Goals are: ");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i+1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine("What type of goal would you like to create? ");

        string answer = Console.ReadLine();
        if(answer == "1")
        {
            Console.WriteLine("What is the name of your goal?");
            string name = Console.ReadLine();
            Console.WriteLine("what is a short description of it?");
            string description = Console.ReadLine();
            Console.WriteLine("What are the number of points associated with this goal?");
            string points = Console.ReadLine();
            SimpleGoal goal = new SimpleGoal(name, description, int.Parse(points), false);
            _goals.Add(goal);
        } 
        if (answer == "2")
        {
            Console.WriteLine("What is the name of your goal?");
            string name = Console.ReadLine();
            Console.WriteLine("what is a short description of it?");
            string description = Console.ReadLine();
            Console.WriteLine("What are the number of points associated with this goal?");
            string points = Console.ReadLine();
            Goal goal = new EternalGoal(name, description, int.Parse(points));
            _goals.Add(goal);
        }   
        if (answer == "3")
        {
            Console.WriteLine("What is the name of your goal?");
            string name = Console.ReadLine();
            Console.WriteLine("what is a short description of it?");
            string description = Console.ReadLine();
            Console.WriteLine("What are the number of points associated with this goal?");
            string points = Console.ReadLine();
            Console.WriteLine("How many times does this goal need to be accomplished for a bonus?");
            string target = Console.ReadLine();
            Console.WriteLine("What is the bonus for accomplishing it that many times?");
            string bonus = Console.ReadLine();
            Goal goal = new ChecklistGoal(name, description, int.Parse(bonus), int.Parse(points), int.Parse(target));          _goals.Add(goal);
        }
    }

    public void RecordEvent()
    {
        ListGoalNames();
        Console.WriteLine("Which goal did you accomplish? ");
        string answer = Console.ReadLine();
        _goals[int.Parse(answer) - 1].RecordEvent();
        _score += _goals[int.Parse(answer) - 1 ]._points;
        Console.WriteLine($"You now have {_score} points");
    }

    public void SaveGoals()
    {
        Console.WriteLine("What is the filename?");
        string file = Console.ReadLine();
        Console.Write($"\nSaving to {file}... \n \n");
        using (StreamWriter outputFile = new StreamWriter(file))

        {
            outputFile.WriteLine(_score);
           foreach (Goal g in _goals)
           {
                if (g is SimpleGoal)
                {
                    outputFile.WriteLine($"SimpleGoal:{g.GetStringRepresentation()}");
                }
                else if (g is EternalGoal)
                {                    
                    outputFile.WriteLine($"EternalGoal:{g.GetStringRepresentation()}");
                }
                else if (g is ChecklistGoal)
                {
                    outputFile.WriteLine($"ChecklistGoal:{g.GetStringRepresentation()}");
                }
           }  
        }
    }
    
    public void LoadFromFile()
    {
        Console.WriteLine("What is the filename?");
        string file = Console.ReadLine();
        Console.Write($"\nLoading from {file}...");
        string [] lines = System.IO.File.ReadAllLines(file);
        _score = int.Parse(lines[0]);
        foreach (string line in lines.Skip(1))
        {
            
            string [] labelContentSplit = line.Split(":");
            string[] parts = labelContentSplit[1].Split("~~");
            if(labelContentSplit[0] == "SimpleGoal")
            {
                
                SimpleGoal goal = new SimpleGoal(parts[0], parts[1], int.Parse(parts[2]), bool.Parse(parts[3]));
                _goals.Add(goal);
            }
            else if (labelContentSplit[0] == "EternalGoal")
            {
                EternalGoal goal = new EternalGoal(parts[0], parts[1], int.Parse(parts[2]));
                _goals.Add(goal);
            }
            else if (labelContentSplit[0] == "ChecklistGoal")
            {
                string shortname = parts[0];
                string description = parts[1];
                int points = int.Parse(parts[2]);
                int bonus = int.Parse(parts[3]);
                int target = int.Parse(parts[4]);
                int amountCompleted = int.Parse(parts[5]);
                ChecklistGoal goal = new ChecklistGoal(shortname, description, bonus, points, target);
                goal._amountCompleted = amountCompleted;
                _goals.Add(goal);
            }

        }
    }

}

abstract class Goal
{
    public string _shortname;
    protected string _description;
    public int _points;

    public Goal(string shortname, string description, int points)
    {
        _shortname = shortname;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent();

    public abstract bool IsComplete();

    public virtual string GetDetailsString()
    {
        if (IsComplete())
        {
            return($"[X] {_shortname} ({_description})");
        }
        return($"[ ] {_shortname} ({_description})");
    } 

    public abstract string GetStringRepresentation();
}

class SimpleGoal : Goal
{
    private bool _isComplete = false;
     public SimpleGoal(string shortname, string description, int points, bool isComplete)
        : base(shortname, description, points)
    {
        _isComplete = isComplete;
    }

    public override void RecordEvent()
    {
        _isComplete = true;
    }

    public override bool IsComplete()
    {
       return _isComplete;
    }

    public override string GetStringRepresentation()
    {
        return$"{_shortname}~~{_description}~~{_points}~~{_isComplete}";
    }

}

class EternalGoal : Goal
{
     public EternalGoal(string shortname, string description, int points)
        : base(shortname, description, points)
    {
    }
    public override void RecordEvent()
    {
        Console.WriteLine($"Congratulations you have recieved {_points} points");
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetStringRepresentation()
    {
        return$"{_shortname}~~{_description}~~{_points}";
    }
}

class ChecklistGoal : Goal
{
    public int _amountCompleted = 0;
    private int _target = 0;
    private int _bonus = 0;

    public ChecklistGoal(string shortname, string description, int bonus, int points, int target)
        : base(shortname, description, points)
    {
        _target = target;
        _bonus = bonus;
    }

    public override void RecordEvent()
    {
        _amountCompleted++;
        if(_amountCompleted == _target)
        {
            _points = _points + _bonus;
            Console.WriteLine($"Congratulations you have recieved {_points} points");
        }
        else
        {
            Console.WriteLine($"Congratulations you have recieved {_points} points");
        }
    }

    public override bool IsComplete()
    {
        if(_amountCompleted == _target)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override string GetStringRepresentation()
    {
        return$"{_shortname}~~{_description}~~{_points}~~{_bonus}~~{_target}~~{_amountCompleted}";
    }

    public  override string GetDetailsString()
    {
        if (IsComplete())
        {
            return($"[X] {_shortname} ({_description}) -- Currently completed: {_amountCompleted}/{_target}");
        }
        return($"[ ] {_shortname} ({_description}) -- Currently completed: {_amountCompleted}/{_target}");
    } 
}