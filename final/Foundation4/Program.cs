using System;
using System.IO.Pipes;

class Program
{
    static void Main(string[] args)
    {       

        RunningActivity runOne = new RunningActivity("12/12/2024", 30, 4.8);
        Console.WriteLine(runOne.GetActivitySummary());
        SwimmingActivity swimOne = new SwimmingActivity("12/12/2024", 20, 3.1);
        Console.WriteLine(swimOne.GetActivitySummary());
        BikingActivity bikeOne = new BikingActivity("12/12/2023", 30, 15);
        Console.WriteLine(bikeOne.GetActivitySummary());
        
    }
}

abstract class Activity
{
    protected string _date = "";
    protected double _minutes = 0.00;
    protected string _activity = "Activity";

    public Activity(string date, double minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public string GetActivitySummary()
    {
        return $"{_date} {_activity} ({_minutes} min) - Distance {GetDistance()} miles, Speed {GetSpeed()} mph, Pace: {GetPace()} min per mile";
    }

    public abstract double GetDistance();

    public double GetPace()
    {
        double pace = _minutes / GetDistance();        
        return Math.Round(pace, 2);
    }

    public virtual double GetSpeed()
    {
        double speed = 60 / GetPace();
        return Math.Round(speed, 2);
    }
}

class RunningActivity : Activity
{
    private double _distance = 0.00;
    public RunningActivity(string date, double minutes, double distance): base(date, minutes)
    {
        _distance = distance;
        _activity = "Running";
    }

    public override double GetDistance()
    {
        return _distance;
    }


}

class SwimmingActivity: Activity
{
    private double _numberOfLaps = 0.00;

    public SwimmingActivity(string date, double minutes, double numberOfLaps): base(date, minutes)
    {
        _numberOfLaps = numberOfLaps;
        _activity = "Swimming";
    }

    public override double GetDistance()
    {
        double distance = _numberOfLaps * 50;
        distance /= 1000;
        return Math.Round(distance, 3);;
    }

}

class BikingActivity: Activity
{
    private double _speed = 0.00;

    public BikingActivity(string date, double minutes, double speed): base(date, minutes)
    {
        _speed = speed;
        _activity = "Biking";
    }

    public override double GetSpeed()
    {
        return Math.Round(_speed, 2);
    }

    public override double GetDistance()
    {
        double distance = _speed /60;
        distance *= _minutes;
        return Math.Round(distance, 2);

    }
}