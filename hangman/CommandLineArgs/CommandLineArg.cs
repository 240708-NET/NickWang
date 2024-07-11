abstract class CommandLineArg
{
    protected string flag;

    public CommandLineArg(string flag)
    {
        this.flag = flag;
    }

    public abstract void ConsumeParameters(string[] args, ref int index);
    public abstract string Description();
    public abstract void Act();
}