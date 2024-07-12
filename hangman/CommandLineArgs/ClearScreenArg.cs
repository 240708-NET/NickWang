/// <summary>
/// arg to disable terminal clearing 
/// </summary>
class ClearScreenArg : CommandLineArg
{
    public ClearScreenArg(string name) : base(name) { }

    /// <summary>
    /// set GameSettings.disableClear to true
    /// </summary>
    public override void Act()
    {
        GameSettings.disableClear = true;
    }

    /// <summary>
    /// does not require any additional parameter
    /// </summary>
    /// <param name="args"></param>
    /// <param name="index"></param>
    /// <exception cref="ParamsReadException"></exception>
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