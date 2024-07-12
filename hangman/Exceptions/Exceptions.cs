/// <summary>
/// base class exception for exception related to args reading
/// </summary>
public class ArgumentException : Exception
{
    public ArgumentException() : base() { }
    public ArgumentException(string message) : base(message) { }
}

/// <summary>
/// thrown when expecting an arg flag but didnt get a flag
/// possibly thrown in other cases
/// </summary>
public class ArgsFormatException : ArgumentException
{
    public ArgsFormatException() : base() { }
    public ArgsFormatException(string message) : base(message) { }
}

/// <summary>
/// generall exception when reading additional parameters for args
/// </summary>
public class ParamsReadException : ArgumentException
{
    public ParamsReadException() : base() { }
    public ParamsReadException(string message) : base(message) { }
}

/// <summary>
/// thrown when fewer parameters are provided than expected
/// </summary>
public class ParamsCountException : ArgumentException
{
    public ParamsCountException() : base() { }
    public ParamsCountException(string message) : base(message) { }
}

/// <summary>
/// thrown when an unrecongnized parameter is read
/// </summary>
public class UnrecognizedParamException : ArgumentException
{
    public UnrecognizedParamException() : base() { }
    public UnrecognizedParamException(string message) : base(message) { }
}
