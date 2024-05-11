using System;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.Design;
using System.IO;
using System.Numerics;


// Added the ability to pull scriptures from a scriptures.txt file and also only hide words that are already hidden. 
class Scripture
{
    private Reference _reference;

    private List<Word> _words = new List<Word>();


    public Scripture(Reference reference, List<Word> words)
    {
        _reference = reference;
        _words = words;
    }

    public void HideRandomWords(int numberToHide)
    {
        if (numberToHide >= 1 && numberToHide <= _words.Count)
        {
            // Create a list to store indices of words to hide
            List<int> indicesToHide = new List<int>();

            // Initialize random number generator
            Random random = new Random();

            // Generate random indices until we have enough to hide
            while (indicesToHide.Count < numberToHide)
            {
                int randomIndex = random.Next(0, _words.Count);
                while (_words[randomIndex].IsHidden())
                    {
                        randomIndex = random.Next(0, _words.Count);
                    }
                
                indicesToHide.Add(randomIndex);

            }

            // Hide words at selected indices
            foreach (int index in indicesToHide)
            {
                _words[index].Hide();
            }
        }
    }

    public string GetDisplayText()
    {
        string displayText = _reference.GetDisplayText();
        foreach (Word word in _words)
        {
            displayText += $" {word.GetDisplayText()}";
        }
        return displayText;
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false; // If any word is not hidden, return false
            }
        }
        return true; // If all words are hidden, return true
    }

}

class Word
{
    private string _text = "";
    private bool _isHidden = false;

    public Word(string text)
    {
        _text = text;
    }
    

    public void Hide()
    {
        char[] charArray = _text.ToCharArray();

        for (int i = 0; i < charArray.Length; i++)
        {
            charArray[i] = '_';
        } 

         _text = new string(charArray);

        
         _isHidden = true;
    }
    public void Show()
    {
        
        _isHidden = false;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        return _text;
    }


}

class Reference
{
    private string _book = "";
    private int _chapter;

    private int _verse;

    private int _endVerse;

    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
    }

    public Reference(string book, int chapter, int verse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = endVerse;
    }

    public string GetDisplayText()
    {

        string referenceString = $"{_book} {_chapter}:{_verse}";

        // If there's an end verse, append it to the reference string
        if (_endVerse > _verse)
        {
            referenceString += $"-{_endVerse}";
        }

        return referenceString;
    }

}

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptures = new List<Scripture>();
        List<Word> words = new List<Word>();
        string file = "scriptures.txt";
        string [] lines = System.IO.File.ReadAllLines(file);
        
        
        foreach (string line in lines)
        {
            string[] parts = line.Split("~~");
            string reference = parts[0];
            string[] referenceParts = reference.Split("~");
            string content = parts[1];
            string[] contentWords = content.Split(" ");
            foreach (string word in contentWords)
            {
                Word newWord = new Word(word);
                words.Add(newWord);
            }
            if (referenceParts.Length > 3) 
            {  
                string book = referenceParts[0];
                int chapter = int.Parse(referenceParts[1]);
                int verse = int.Parse(referenceParts[2]);
                int endVerse = int.Parse(referenceParts[3]);
                Reference newReference = new Reference(book, chapter, verse, endVerse);
                Scripture newScripture = new Scripture(newReference, words);
                scriptures.Add(newScripture);
            } else
            {
                string book = referenceParts[0];
                int chapter = int.Parse(referenceParts[1]);
                int verse = int.Parse(referenceParts[2]);
                Reference newReference = new Reference(book, chapter, verse);
                Scripture newScripture = new Scripture(newReference, words);
                scriptures.Add(newScripture);
            }          
        }

        Random rand = new Random();

        int randomIndex = rand.Next(0, scriptures.Count);
        Scripture randomScripture = scriptures[randomIndex];

        bool condition = true;
        while (condition)
        {   
            Console.WriteLine(randomScripture.GetDisplayText());
            Console.Write("\n\nPress enter to continue or type 'quit' to finish:");
            string answer = Console.ReadLine();

            if (answer == "quit")
            {
                condition = false;
            } 
            randomScripture.HideRandomWords(3);
            
        }        
    }
}