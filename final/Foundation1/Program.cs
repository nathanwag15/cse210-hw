using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using System.Transactions;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();
       Video videoOne = new Video("I won at life", "The winner", "120");
       videoOne.addComment("Steve", "You totally lost bro");
       videoOne.addComment("Alfred", "Nah dude he totally won");
       videoOne.addComment("Alfredo", "You name is so similar to mine.");
       videos.Add(videoOne);
       Video videoTwo = new Video("I lost at life", "The loser", "420");
       videoTwo.addComment("Kevin", "You're such a winner.");
       videoTwo.addComment("Steve", "I agree such a better winner than The winner bro.");
       videoTwo.addComment("Lizzy", "Everyone is a winner tbh.");
       videos.Add(videoTwo);
       Video videoThree = new Video("You can't imagine what I just did", "The guy who tubes for you", "234");
       videoThree.addComment("Alfred", "I had no idea this was possible");
       videoThree.addComment("The Agree guy", "I agree");
       videoThree.addComment("Kelpy", "This is so fake bro");
       videos.Add(videoThree);

       foreach (Video video in videos)
       {
            Console.WriteLine($"\n\n'{video.GetTitle()}' by '{video.GetAuthor()}' ({video.GetLength()} seconds)");
            Console.WriteLine($"Comments: ({video.GetComments()})");
            foreach (Comment comment in video._comments)
            {
                Console.WriteLine($"> {comment.GetName()}: {comment.GetText()}");
            }
       }
    }
}

class Video
{
    private string _title = "";
    private string _author = "";
    private string _length = "";

    public List<Comment> _comments = new List<Comment>();

    public Video(string title, string author, string length)
    {
        _title = title;
        _author = author;
        _length = length;
    }

    public string GetTitle()
    {
        return _title;
    }

    public void SetTitle(string title)
    {
        _title = title;
    }

    public string GetAuthor()
    {
        return _author;
    }

    public void SetAuthor(string author)
    {
        _author = author;
    }

    public string GetLength()
    {
        return _length;
    }

    public void SetLength(string length)
    {
        _length = length;
    }

    public string GetComments()
    {
        string count = _comments.Count.ToString();
        return count;
    }

    public void addComment(string name, string text)
    {
        Comment comment = new Comment(name, text);
        _comments.Add(comment);
    }
}

class Comment
{
    private string _name = "";
    private string _text = "";

    public Comment(string name, string text)
    {
        _name = name;
        _text = text;
    }

    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public string GetText()
    {
        return _text;
    }

    public void SetText(string text)
    {
        _text = text;
    }


}