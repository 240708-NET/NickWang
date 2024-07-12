using System.Text.RegularExpressions;

/// <summary>
/// fetches a word through a web API
/// </summary>
class WebWordFetcher : WordFetcher
{
    private static readonly string URL = "https://random-word.ryanrk.com/api/en/word/random";

    /// <summary>
    /// fetch a word from the provided API and retrieve the data
    /// if api call fails, defaults back to fetch word through CLI 
    /// </summary>
    /// <returns></returns>
    public override string FetchWord()
    {
        try
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(URL).Result;
            if (response.IsSuccessStatusCode)
            {
                //retrieves the target word from http response
                return GetStringFromHttpResponse(response);
            }
        }
        catch (System.Exception)
        {
        }

        //if call fails, default back to CLI 
        Console.WriteLine("Unable to fetch data. Please enter a word.");
        return Utils.GetWordFromConsole();

    }

    /// <summary>
    /// retrieve the target word from http response
    /// the response body for this URL contains just the single word.
    /// this method may not work with a different URL. 
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private static string GetStringFromHttpResponse(HttpResponseMessage response)
    {
        //some sort of json integration would prob be good here. but this response body is so simple
        //that it is prob not worth the work and the imports.
        string str = response.Content.ReadAsStringAsync().Result.ToString();
        //simply returns string of all the letters in the response. May not work with different URL.
        return Regex.Replace(str, @"[^a-zA-Z]", "");
    }
}