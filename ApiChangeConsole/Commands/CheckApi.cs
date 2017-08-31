using System;
using System.IO;
using ApiChange.Api.Extensions;
using ApiChange.Api.Introspection;
using ApiChange.Api.Introspection.Diff;
using Mono.Cecil;

namespace ApiChangeConsole.Commands
{
  class CheckApi : IConsoleCommand
  {
    private string mOldFilePath;
    private string mNewFilePath;

    #region Implementation of IConsoleCommand

    public bool ProcessArguments(string[] arguments)
    {
      if (arguments.Length != 2)
      {
        ConsoleHelpers.WriteError("The specified arguments for the CheckApi command are invalid.");
        return false;
      }

      mOldFilePath = arguments[0];
      mNewFilePath = arguments[1];

      if (!File.Exists(mOldFilePath))
      {
        ConsoleHelpers.WriteError("The first assembly does not exist: {0}", mOldFilePath);
        return false;
      }

      if (!File.Exists(mNewFilePath))
      {
        ConsoleHelpers.WriteError("The second assembly does not exist: {0}", mNewFilePath);
        return false;
      }

      return true;
    }

    public bool Execute()
    {
      ConsoleHelpers.WriteLine("API Check{0}   Old Version: {1}{0}   New Version: {2}", Environment.NewLine,
                               mOldFilePath, mNewFilePath);

      try
      {
        AssemblyFactory.GetAssembly(mOldFilePath);
      }
      catch (Exception)
      {
        ConsoleHelpers.WriteError("Unable to load the first assembly: {0}", mOldFilePath);
        return false;
      }

      try
      {
        AssemblyFactory.GetAssembly(mNewFilePath);
      }
      catch (Exception)
      {
        ConsoleHelpers.WriteError("Unable to load the second assembly: {0}", mNewFilePath);
        return false;
      }

      var wApiChecker = ApiCheckerFactory.Instance.Create(new ReleaseVersionStrategy());
      var wOldAssembly = AssemblyLoader.LoadCecilAssembly(mOldFilePath);
      var wNewAssembly = AssemblyLoader.LoadCecilAssembly(mNewFilePath);
      var wRes = wApiChecker.VerifyApiBreak(wOldAssembly, wNewAssembly);

      switch (wRes.BreakLevel)
      {
        case ApiBreakLevel.None:
          ConsoleHelpers.WriteSuccess("   Result:      No API change");
          break;
        case ApiBreakLevel.Minor:
          ConsoleHelpers.WriteWarning("   Result:      Minor API change");
          break;
        case ApiBreakLevel.Major:
          ConsoleHelpers.WriteWarning("   Result:      Major API change");
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      ConsoleHelpers.WriteLine("");

      var wPrinter = new DiffPrinter();
      wPrinter.Print(wRes.DiffResult);

      
      return true;
    }

    public void ShowUsage()
    {
      ConsoleHelpers.WriteLine("  -check [Previous Assembly Path] [Current Assembly Path]{0}" +
                               "   Checks if a new major or minor version is required{0}", Environment.NewLine);
    }

    #endregion
  }
}
