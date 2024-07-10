class ConsoleWordFetcher : IWordFetcher
{
    public string FetchWord()
    {
        return Utils.GetWordFromConsole();
    }
}