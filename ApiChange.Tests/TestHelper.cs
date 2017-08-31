using System;
using System.Linq;
using System.Reflection;
using Mono.Cecil;

namespace ApiChange.Tests
{
  static internal class TestHelper
  {
    public static AssemblyDefinition GetEmbededAssembly(string path)
    {
      var segments = path.Replace('\\', '.').Split('.');
      var resourcePath = String.Join(".", segments.Select(s => Char.IsDigit(s[0]) ? "_" + s : s));
      var executingAssembly = Assembly.GetExecutingAssembly();
      string resourceName = executingAssembly.GetName().Name + "." + resourcePath;
      var file = executingAssembly.GetManifestResourceStream(resourceName);
      return AssemblyFactory.GetAssembly(file);
    }
  }
}