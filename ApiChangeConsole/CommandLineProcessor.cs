using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiChangeConsole
{
  class ConsoleCommandInfo
  {
    public Type CommandType { get; set; }
    public string[] Aliases { get; set; }
  }

  class CommandLineProcessor
  {
    private static CommandLineProcessor _instance;
    private readonly List<ConsoleCommandInfo> mConsoleActions = new List<ConsoleCommandInfo>(); 

    public static CommandLineProcessor Instance
    {
      get { return _instance ?? (_instance = new CommandLineProcessor()); }
    }

    public IEnumerable<ConsoleCommandInfo> RegisteredCommands
    {
      get { return mConsoleActions; }
    }

    public void Register<T>(string name, params string[] aliases) where T : IConsoleCommand
    {
      mConsoleActions.Add(new ConsoleCommandInfo
                            {
                              CommandType = typeof (T),
                              Aliases = new[] { name }.Concat(aliases).ToArray()
                            });
    }

    public CommandLineProcessorResult Process(string[] arguments)
    {
      var wCommandInfo = GetCommandFromArguments(arguments);
      if (wCommandInfo == null)
        return CommandLineProcessorResult.FromFail("The command '{0}' is unknown.", arguments[0]);

      // process the arguments...
      var wCommand = Activator.CreateInstance(wCommandInfo.CommandType) as IConsoleCommand;
      if (wCommand == null)
        return CommandLineProcessorResult.FromFail("Unable to execute a command.");

      if (!wCommand.ProcessArguments(arguments.SubArray(1, arguments.Length - 1)))
        return CommandLineProcessorResult.FromFail("The specified arguments are not valid.");

      if (!wCommand.Execute())
        return CommandLineProcessorResult.FromFail("Unable to execute a command.");

      return CommandLineProcessorResult.FromSuccess();
    }

    private ConsoleCommandInfo GetCommandFromArguments(string[] arguments)
    {
      if (arguments.Length == 0)
        return null;

      string wAction = arguments[0].TrimStart(new[] {'-', '/'});
      return mConsoleActions.FirstOrDefault(p => p.Aliases.Contains(wAction));
    }
  }
}
