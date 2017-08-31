using System;

namespace ApiChangeConsole.Commands
{
  class ShowUsage : IConsoleCommand
  {
    #region Implementation of IConsoleCommand

    public bool ProcessArguments(string[] arguments)
    {
      return true;
    }

    public bool Execute()
    {
      ConsoleHelpers.WriteSuccess("USAGE: ApiCheck.exe [One of the following parameters]{0}", Environment.NewLine);

      foreach (var consoleCommandInfo in CommandLineProcessor.Instance.RegisteredCommands)
      {
        if (consoleCommandInfo.CommandType == typeof(ShowUsage))
          continue;

        var wCommand = Activator.CreateInstance(consoleCommandInfo.CommandType) as IConsoleCommand;
        if (wCommand != null)
          wCommand.ShowUsage();
      }
      return true;
    }

    void IConsoleCommand.ShowUsage()
    {
      
    }

    #endregion
  }
}
