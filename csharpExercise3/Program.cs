// See https://aka.ms/new-console-template for more information

using HtmlAgilityPack;

class Program
{
    static async Task Main(string[] args)
    {
        // URL of the website
        string url = "https://www.timeanddate.com/worldclock/bulgaria/sofia";

        // Create HttpClient
        var web = new HtmlWeb();
        var doc = web.Load(url);

        var time = doc.DocumentNode.SelectSingleNode("//div[@id='qlook']//span[contains(@class, 'h1')]");
        var date = doc.DocumentNode.SelectSingleNode("//div[@id='qlook']//span[contains(@id, 'ctdat')]");
        
        Console.WriteLine(time.InnerHtml);
        Console.WriteLine(date.InnerHtml);
    }
}