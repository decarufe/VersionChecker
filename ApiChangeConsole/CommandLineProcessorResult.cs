namespace ApiChangeConsole
{
  enum Result
  {
    Success,
    Unknown
  }

  class CommandLineProcessorResult
  {
    private CommandLineProcessorResult(Result res, string message)
    {
      Result = res;
      Message = message;
    }

    public Result Result { get; private set; }
    public string Message { get; private set; }

    public static CommandLineProcessorResult FromSuccess()
    {
      return new CommandLineProcessorResult(Result.Success, string.Empty);
    }

    public static CommandLineProcessorResult FromFail(string format, params object[] args)
    {
      return new CommandLineProcessorResult(Result.Unknown, string.Format(format, args));
    }
  }
}
