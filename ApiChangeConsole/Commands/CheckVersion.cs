using System;
using System.IO;
using ApiChange.Api.Extensions;
using ApiChange.Api.Introspection;
using Mono.Cecil;

namespace ApiChangeConsole.Commands
{
  class CheckVersion : IConsoleCommand
  {
    private string mOldFilePath;
    private string mNewFilePath;

    #region Implementation of IConsoleCommand

    public bool ProcessArguments(string[] arguments)
    {
      if (arguments.Length != 2)
      {
        ConsoleHelpers.WriteError("The specified arguments for the CheckVersion command are invalid.");
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
      ConsoleHelpers.WriteLine("API Version Check{0}   Previous Assembly: {1}{0}   Current Assembly:  {2}{0}", Environment.NewLine,
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

      ConsoleHelpers.WriteLine("Change Status:    {0}", wRes.BreakLevel == ApiBreakLevel.Major ? "MAJOR" : wRes.BreakLevel == ApiBreakLevel.Minor ? "MINOR" : "NONE");
      ConsoleHelpers.WriteLine("Previous Version: {0}", wRes.ReferenceVersion.ToString(3));
      ConsoleHelpers.WriteLine("Current Version:  {0}", wRes.CurrentVersion.ToString(3));
      ConsoleHelpers.WriteLine("Expected Version: {0}", wRes.ExpectedVersion.ToString(3));

      var wVersion1 = new Version(wRes.CurrentVersion.Major, wRes.CurrentVersion.Minor, wRes.CurrentVersion.Build);
      var wVersion2 = new Version(wRes.ExpectedVersion.Major, wRes.ExpectedVersion.Minor, wRes.ExpectedVersion.Build);
      if (wVersion1 != wVersion2)
        ConsoleHelpers.WriteWarning("The current version is not matching the expected one.", wRes.ExpectedVersion.ToString(3));
      else
        ConsoleHelpers.WriteSuccess("The current version is matching the expected one.");

      return true;
    }

    public void ShowUsage()
    {
      ConsoleHelpers.WriteLine("  -version [Previous Assembly Path] [Current Assembly Path]{0}" +
                               "   Shows the new expected version based on the API changes.{0}", Environment.NewLine);
    }

    #endregion
  }
}
