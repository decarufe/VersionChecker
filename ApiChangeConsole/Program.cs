using System;
using System.Reflection;
using ApiChangeConsole.Commands;

namespace ApiChangeConsole
{
  class Program
  {
    static Program()
    {
      RegisterAvailableCommands();
    }

    private static void RegisterAvailableCommands()
    {
      //CommandLineProcessor.Instance.Register<ExportAssemblyDefinition>("export", "e");
      CommandLineProcessor.Instance.Register<ShowApiDifferences>("diff", "d");
      CommandLineProcessor.Instance.Register<CheckApi>("check", "c");
      CommandLineProcessor.Instance.Register<CheckVersion>("version", "v");
      CommandLineProcessor.Instance.Register<ShowUsage>("help", "?");
    }

    static void Main(string[] args)
    {
      CommandLineProcessor.Instance.Process(args.Length == 0 ? new[] {"-help"} : args);

      if (System.Diagnostics.Debugger.IsAttached)
      {
        Console.WriteLine("Press enter to continue");
        Console.ReadLine();
      }
    }

    /*
     [Test]
    public void DiffOneUiTest()
    {
      var wOldAssembly = AssemblyLoader.LoadCecilAssembly(@"E:\OneUI_Old\OneUI\bin\OneUi.Foundation.dll");
      var wNewAssembly = AssemblyLoader.LoadCecilAssembly(@"E:\OneUI\OneUI\bin\OneUi.Foundation.dll");

      FileStream fsOld = new FileStream(@"E:\temp\OneUi.FoundationOld.bin", FileMode.Create);
      FileStream fsNew = new FileStream(@"E:\temp\OneUi.Foundation.bin", FileMode.Create);

      AssemblyFactory.SaveAssembly(wOldAssembly, fsOld);
      AssemblyFactory.SaveAssembly(wNewAssembly, fsNew);

      fsOld.Close();
      fsNew.Close();

      AssemblyDefinition adOld;
      using (FileStream fs1 = File.Open(@"E:\temp\OneUi.FoundationOld.bin", FileMode.Open))
      {
        adOld = AssemblyFactory.GetAssembly(fs1);
      }

      AssemblyDefinition adNew;
      using (FileStream fs1 = File.Open(@"E:\temp\OneUi.Foundation.bin", FileMode.Open))
      {
        adNew = AssemblyFactory.GetAssembly(fs1);
      }

      AssemblyDiffer differ = new AssemblyDiffer(adOld, adNew);
      AssemblyDiffCollection diff = differ.GenerateTypeDiff(QueryAggregator.PublicApiQueries);
     
      
    }

    [Test]
    public void DiffOneUiTest2()
    {
      using (FileStream fs = File.Open(@"e:\temp\binSerializer.bin", FileMode.Open))
      {
        var t = AssemblyFactory.GetAssembly(fs);
      }
    }
     */
  }
}
