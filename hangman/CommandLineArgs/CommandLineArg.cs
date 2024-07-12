/// <summary>
/// defines a command line argument
/// </summary>
abstract class CommandLineArg
{
    //the flag to invoke this arg. e.g "--help"
    protected string flag;

    public CommandLineArg(string flag)
    {
        this.flag = flag;
    }

    //read all necessarry parameters for this arg
    public abstract void ConsumeParameters(string[] args, ref int index);
    //returns a description string when --help is called
    public abstract string Description();
    //program behavior is modified in this method
    public abstract void Act();
}