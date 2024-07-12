/// <summary>
/// arg to enable display the answer to user
/// </summary>
class CheatArg : CommandLineArg
{
    public CheatArg(string flag) : base(flag) { }

    /// <summary>
    /// set GameSettings.enableCheats to true
    /// </summary>
    public override void Act()
    {
        GameSettings.enableCheats = true;
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
        return $"   --{flag} enable cheats. Allows user to see the answer.";
    }
}