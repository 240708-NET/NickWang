public class ArgumentException : Exception
{
    public ArgumentException() : base() { }
    public ArgumentException(string message) : base(message) { }
}

public class ArgsFormatException : ArgumentException
{
    public ArgsFormatException() : base() { }
    public ArgsFormatException(string message) : base(message) { }
}
public class ParamsReadException : ArgumentException
{
    public ParamsReadException() : base() { }
    public ParamsReadException(string message) : base(message) { }
}
public class ParamsCountException : ArgumentException
{
    public ParamsCountException() : base() { }
    public ParamsCountException(string message) : base(message) { }
}
public class UnrecognizedParamException : ArgumentException
{
    public UnrecognizedParamException() : base() { }
    public UnrecognizedParamException(string message) : base(message) { }
}
