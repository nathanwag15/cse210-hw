using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {   
        Random randomGenerator = new Random();
        int number = randomGenerator.Next(1, 101);
        int userGuess = 0;
        string match = "No";
        do
        {
            Console.Write("What is your guess? ");
            string userInput = Console.ReadLine();
            userGuess = int.Parse(userInput);
            
            if (userGuess < number)
            {   
                Console.WriteLine("Higher");
            }
            else if (userGuess > number)
            {
                Console.WriteLine("Lower");
            }
            else if (userGuess == number)
            {
                Console.WriteLine("You guessed right!!");
                match = "Yes";
            }   

        } while (match == "No");

    }
}