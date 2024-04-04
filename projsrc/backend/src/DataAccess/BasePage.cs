namespace Datalayer;

public enum PageType {
    Basic,
    ProductPage,
    Article
}

public class BasePage
{
    public int id;
    public int AuthorId;
    public int contentId;
    public PageType pageType;

    public BasePage(int id, int contentId, int authorid = 0, PageType pageType = PageType.Basic) {
        this.id = id;
        AuthorId = authorid;
        this.contentId = contentId;
        this.pageType = pageType;
    }
    public BasePage(int id, int contentId, PageType pageType = PageType.Basic) {
        this.id = id;
        AuthorId = 0;
        this.contentId = contentId;
        this.pageType = pageType;
    }
}

