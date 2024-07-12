class ClearScreenArg : CommandLineArg
{
    public ClearScreenArg(string name) : base(name) { }

    public override void Act()
    {
        GameSettings.disableClear = true;
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
        return $"   --{flag} disable clearing the terminal.";
    }
}