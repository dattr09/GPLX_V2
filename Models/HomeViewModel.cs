public class HomeViewModel
{
    public List<Article> Articles { get; set; }
    public List<Article> TrafficFineLinks { get; set; }
}

public class Article
{
    public string Title { get; set; }
    public string Url { get; set; }
}
