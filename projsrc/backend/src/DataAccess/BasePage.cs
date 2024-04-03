namespace Datalayer;

public enum PageType {
    Basic,
    ProductPage,
    Article
}

public class BasePage
{

    public int id;
    public string Title;
    public string Author;
    public string Content;
    public PageType pageType;

    public BasePage(int id ,string title, string content, string author = "Anonymous", PageType pageType = PageType.Basic) {
        this.id = id
        Title = title;
        Author = author;
        Content = content;
        this.pageType = pageType;
    }
    public BasePage(int id, string title, string content, PageType pageType = PageType.Basic) {
        this.id = id;
        Title = title;
        Author = "Anonymous";
        Content = content;
        this.pageType = pageType;
    }
}

