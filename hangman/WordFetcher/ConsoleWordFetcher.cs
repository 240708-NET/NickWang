/// <summary>
/// fetch a word through CLI
/// </summary>
class ConsoleWordFetcher : WordFetcher
{
    public override string FetchWord()
    {
        return Utils.GetWordFromConsole();
    }
}