namespace Datalayer;

public class BasePage
{
    public string Title;
    public string Author;
    public string Content;

    public BasePage(string title, string content, string author = "Anonymous")
    {
        Title = title;
        Author = author;
        Content = content;
    }
}
