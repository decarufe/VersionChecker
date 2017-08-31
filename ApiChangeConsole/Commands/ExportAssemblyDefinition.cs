using System;
using System.IO;
using ApiChange.Api.Introspection;
using Mono.Cecil;

namespace ApiChangeConsole.Commands
{
  class ExportAssemblyDefinition : IConsoleCommand
  {
    private string mSourceFilePath;
    private string mTargetFilePath;

    #region Implementation of IConsoleCommand

    public bool ProcessArguments(string[] arguments)
    {
      if (arguments.Length != 2)
      {
        ConsoleHelpers.WriteError("The specified arguments for the Export command are invalid.");
        return false;
      }

      mSourceFilePath = arguments[0].ToLowerInvariant();
      mTargetFilePath = arguments[1].ToLowerInvariant();
      if ( mSourceFilePath == mTargetFilePath )
      {
        ConsoleHelpers.WriteError("The source file must be different from the target file.");
        return false;
      }

      if ( !File.Exists(mSourceFilePath) )
      {
        ConsoleHelpers.WriteError("The source file does not exist.");
        return false;
      }

      ConsoleHelpers.WriteLine("Exporting Assembly Definition{0}   Source: {1}{0}   Target: {2}{0}", Environment.NewLine,
                               mSourceFilePath, mTargetFilePath);

      return true;
    }

    public bool Execute()
    {
      AssemblyDefinition wAssemblyInfo;

      try
      {
        wAssemblyInfo = AssemblyLoader.LoadCecilAssembly(mSourceFilePath);
      }
      catch (Exception)
      {
        ConsoleHelpers.WriteError("Unable to load the source assembly: {0}", mSourceFilePath);
        return false;
      }

      try
      {
        using (var fileStream = File.Create(mTargetFilePath))
        {
          AssemblyFactory.SaveAssembly(wAssemblyInfo, fileStream);
        }
      }
      catch (Exception)
      {
        ConsoleHelpers.WriteError("Unable to generate the target file: {0}", mTargetFilePath);
      }

      ConsoleHelpers.WriteSuccess("The target file has been generated successfully.");

      return true;
    }

    public void ShowUsage()
    {
      
    }

    #endregion
  }
}
