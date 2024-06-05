using System;
using System.ComponentModel;
using System.Reflection;


class Program
{
    static void Main(string[] args)
    {
       OutdoorEvent eventOne = new OutdoorEvent("Bridge Tour", "Tour the London Bridge", "10/13/2023", "6:10am", new Address("246 Oak Circle", "London", "England", "UK"), "Chance of rain and wind.");
       
       Console.WriteLine(eventOne.getStandardInfo());
       Console.WriteLine("\n");
       Console.WriteLine(eventOne.GetFullReceptionDetails());
       Console.WriteLine("\n");
       Console.WriteLine(eventOne.getShortDescription());
       Console.WriteLine("==============");
       Console.WriteLine("\n");

       LectureEvent eventTwo = new LectureEvent("Object Oriented Programming", "Inheritance", "1/1/2023", "6:00am", new Address("123 Elm St", "New York City", "NY", "USA"), "Bob the Builder", "100");

       Console.WriteLine(eventTwo.getStandardInfo());
       Console.WriteLine("\n");
       Console.WriteLine(eventTwo.GetFullLectureDetails());
       Console.WriteLine("\n");
       Console.WriteLine(eventTwo.getShortDescription());
       Console.WriteLine("==============");
       Console.WriteLine("\n");

       ReceptionEvent eventThree = new ReceptionEvent("Graduation", "MSD 321 Graduation Party", "6/1/2023", "7:00pm", new Address("246 Oak Circle", "London", "England", "UK"), "grad@msd321.com"); 
       
       Console.WriteLine(eventThree.getStandardInfo());
       Console.WriteLine("\n");
       Console.WriteLine(eventThree.GetFullReceptionDetails());
       Console.WriteLine("\n");
       Console.WriteLine(eventThree.getShortDescription());
       Console.WriteLine("==============");
    }
}

class Address
{
    private string _streetAddress = "";
    private string _city = "";
    private string _state = "";
    private string _country = "";

    public Address(string streetAddress, string city, string state, string country)
    {
        _streetAddress = streetAddress;
        _city = city;
        _state = state;
        _country = country;
    }

    public bool IsUSA()
    {
        if(_country == "USA")
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public string GetAddress()
    {
        return($"{_streetAddress}\n{_city}, {_state}, {_country}");
    }
}

class Event
{
    protected string _title = "";
    protected string _description = "";
    protected string _date = "";
    protected string _time = "";
    protected Address _address;

    protected string _type = "";

    public Event(string title, string description, string date, string time, Address address)
    {
        _title = title;
        _description = description;
        _date = date;
        _time = time;
        _address = address;
        _type = "Event";
    }

    public string getStandardInfo()
    {
        return $"{_title} - {_description}\n{_date} @ {_time}\n{_address.GetAddress()}";
    }

    public string getShortDescription()
    {
        return $"{_type} - {_title} - {_date}";
    }
}

class ReceptionEvent : Event
{
    private string _email = "";
    public ReceptionEvent(string title, string description, string date, string time, Address address, string email ): base(title, description, date, time, address)
    {
        _email = email;
        _type = "Reception";
    }

    public string GetFullReceptionDetails()
    {
        return $"Type: {_type}\n{getStandardInfo()}\nEmail: {_email}";
    }
}

class LectureEvent : Event
{
    private string _speaker = "";
    private string _capacity = "";

    public LectureEvent(string title, string description, string date, string time, Address address, string speaker, string capacity): base(title, description, date, time, address)
    {
        _capacity = capacity;
        _speaker = speaker;
        _type = "Lecture";
    }

    public string GetFullLectureDetails()
    {
        return $"Type: {_type}\n{getStandardInfo()}\nSpeaker: {_speaker}\nCapacity: {_capacity}";
    }
}

class OutdoorEvent : Event
{
    private string _weather = "";
    
    public OutdoorEvent(string title, string description, string date, string time, Address address, string weather ): base(title, description, date, time, address)
    {
        _weather = weather;
        _type = "Outdoors";
    }

    public string GetFullReceptionDetails()
    {
        return $"Type: {_type}\n{getStandardInfo()}\nWeather: {_weather}";
    }
}

