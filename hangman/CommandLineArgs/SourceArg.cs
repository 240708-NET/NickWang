/// <summary>
/// arg to select the source of target word 
/// </summary>
class SourceArg : CommandLineArg
{

    string source = "";

    public SourceArg(string flag) : base(flag) { }

    /// <summary>
    /// read 1 required argument
    /// </summary>
    /// <param name="args"></param>
    /// <param name="index"></param>
    /// <exception cref="ParamsCountException"></exception>
    public override void ConsumeParameters(string[] args, ref int index)
    {
        try
        {
            source = args[++index];
        }
        catch (System.IndexOutOfRangeException)
        {
            throw new ParamsCountException("Incorrect number of parameters.");
        }
        catch (System.Exception)
        {
            throw new ParamsCountException("Unknown error when reading parameters.");
        }
    }

    public override string Description()
    {
        return $"   --{flag} <param> defines the method of fetching the word used in hangman. Available params are \n       console - (default) promts the user for a word \n       web     - fetch a random word from https://random-word.ryanrk.com/api/en/word/random";
    }

    /// <summary>
    /// update GameSettings.wordFetcher according to arg
    /// </summary>
    /// <exception cref="UnrecognizedParamException"></exception>
    public override void Act()
    {
        if (source.ToLower() == "web")
        {
            GameSettings.wordFetcher = new WebWordFetcher();
            return;
        }

        if (source.ToLower() == "console")
        {
            return;
        }

        throw new UnrecognizedParamException("Unrecognized parameter.");

    }
}