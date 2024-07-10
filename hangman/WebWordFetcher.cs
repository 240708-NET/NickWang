using System.Text.RegularExpressions;

class WebWordFetcher : IWordFetcher
{
    private readonly string URL = "https://random-word.ryanrk.com/api/en/word/random";
    public string FetchWord()
    {
        try
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(URL).Result;
            if (response.IsSuccessStatusCode)
            {
                return GetStringFromHttpResponse(response);
            }
        }
        catch (System.Exception)
        {
        }

        Console.WriteLine("Unable to fetch data. Please enter a word.");
        return Utils.GetWordFromConsole();
    }

    private static string GetStringFromHttpResponse(HttpResponseMessage response)
    {
        string str = response.Content.ReadAsStringAsync().Result.ToString();
        return Regex.Replace(str, @"[^a-zA-Z]", "");
    }
}