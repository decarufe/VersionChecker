using System;
using System.Reflection;

namespace ApiChange.Api.Extensions
{
  public class About : IAbout
  {
    public string CreateHeader()
    {
      Assembly wAssembly = Assembly.GetExecutingAssembly();
      string wProductName = GetAttributeValue<AssemblyProductAttribute>(wAssembly).Product;
      string wVersion = GetAttributeValue<AssemblyFileVersionAttribute>(wAssembly).Version;
      string wDisplayVersion = new Version(wVersion).ToString(2);
      string wCopyright = GetAttributeValue<AssemblyCopyrightAttribute>(wAssembly).Copyright;

      string wInfo = String.Format("{0} {1} {2}", wProductName, wDisplayVersion, wCopyright);
      return wInfo;
    }

    private static TAttribute GetAttributeValue<TAttribute>(Assembly assembly)
    {
      object[] wCustomAttributes = assembly.GetCustomAttributes(typeof (TAttribute), true);
      if (wCustomAttributes.Length != 1) throw new InvalidOperationException();

      return (TAttribute) wCustomAttributes[0];
    }
  }
}