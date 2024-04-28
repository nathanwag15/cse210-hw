using System;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers;
        numbers = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        string loop = "Yes";
        int sum = 0;
        int largestNumber = 0;
        while (loop == "Yes")
        {
            Console.Write("Enter number: ");
            string userInput = Console.ReadLine();
            int number = int.Parse(userInput);
            if (number == 0)
            {
                loop = "No";
            }
            else 
            {
                numbers.Add(number);
            }
            Console.WriteLine(numbers);
        } 
        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] > largestNumber)
            {
                largestNumber = numbers[i];
            }
            sum = sum + numbers[i];
            
        }
        int count = numbers.Count;
        float average = sum / count;
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {largestNumber}");
    }
}