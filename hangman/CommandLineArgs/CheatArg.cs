class CheatArg : CommandLineArg
{
    public CheatArg(string flag) : base(flag) { }

    public override void Act()
    {
        GameSettings.enableCheats = true;
    }

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
        return $"   --{flag} enable cheats. Allows user to see the answer.";
    }
}