class HelpArg : CommandLineArg
{
    public HelpArg(string flag) : base(flag) { }

    public override void ConsumeParameters(string[] args, ref int index)
    {
        try
        {

        }
        catch (System.Exception)
        {
            throw new ParamsReadException("Unknown error when reading parameters.");
        }
    }

    public override string Description()
    {
        return $"   --{flag} list the available args";
    }


    public override void Act()
    {
        Console.WriteLine("usage: hangman [<flag> [params]]");
        foreach (var arg in GameSettings.args)
        {
            Console.WriteLine(arg.Value.Description());
        }
        Environment.Exit(0);
    }
}