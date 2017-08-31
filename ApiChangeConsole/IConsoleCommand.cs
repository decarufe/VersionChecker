namespace ApiChangeConsole
{
  interface IConsoleCommand
  {
    bool ProcessArguments(string[] arguments);
    bool Execute();
    void ShowUsage();
  }
}
