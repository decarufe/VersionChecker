using System;
using System.IO;
using System.Text;
using ApiChange.Api.Extensions;
using ApiChange.Api.Introspection;
using ApiChange.Api.Introspection.Diff;

namespace ApiChangeConsole.Commands
{
  class ShowApiDifferences : IConsoleCommand
  {
    private string mOldFilePath;
    private string mNewFilePath;

    #region Implementation of IConsoleCommand

    public bool ProcessArguments(string[] arguments)
    {
      if ( arguments.Length != 2 )
      {
        ConsoleHelpers.WriteError("The specified arguments for the Diff command are invalid.");
        return false;
      }

      mOldFilePath = arguments[0];
      mNewFilePath = arguments[1];

      if ( !File.Exists(mOldFilePath) )
      {
        ConsoleHelpers.WriteError("The first assembly does not exist: {0}", mOldFilePath);
        return false;
      }
      
      if ( !File.Exists(mNewFilePath) )
      {
        ConsoleHelpers.WriteError("The second assembly does not exist: {0}", mNewFilePath);
        return false;
      }

      return true;
    }

    public bool Execute()
    {
      try
      {
        var wAssemblyDiff = new AssemblyDiffer(mOldFilePath, mNewFilePath);
        var wDiffResult = wAssemblyDiff.GenerateTypeDiff(QueryAggregator.PublicApiQueries);

        ConsoleHelpers.WriteLine("API Differences{0}" +
                                 "   Old Version: {1}, V{2}{0}" +
                                 "   New Version: {3}, V{4}{0}" +
                                 "-----------------------------{0}", 
                                 Environment.NewLine,
                                 mOldFilePath, wAssemblyDiff.AssemblyV1.Name.Version,
                                 mNewFilePath, wAssemblyDiff.AssemblyV2.Name.Version);

        var sb = new StringBuilder();
        using (TextWriter tw = new StringWriter(sb))
        {
          var wMessageArgs = new DiffPrinter(tw);
          wMessageArgs.Print(wDiffResult);
        }
        ConsoleHelpers.WriteLine(sb.ToString());
      }
      catch (Exception)
      {
        ConsoleHelpers.WriteError("An error occured while trying to find the differences between the two specified assemblies.");
      }

      return true;
    }

    public void ShowUsage()
    {
      ConsoleHelpers.WriteLine("  -diff [Previous Assembly Path] [Current Assembly Path]{0}" +
                               "   Show the differences between two assemblies.{0}", Environment.NewLine);
    }

    #endregion
  }
}
