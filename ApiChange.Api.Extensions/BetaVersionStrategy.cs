using System;
using ApiChange.Api.Introspection;
using Mono.Cecil;

namespace ApiChange.Api.Extensions
{
  public class BetaVersionStrategy : IApiVersionStrategy
  {
    public AssemblyDiffer GetAssemblyDiffer(AssemblyDefinition releaseAssemblyPath, AssemblyDefinition assemblyPathToVerify)
    {
      return new AssemblyDiffer(releaseAssemblyPath, assemblyPathToVerify);
    }

    public Version GetExpectedVersion(Version releaseVersion, ApiBreakLevel breakLevel)
    {
      Version expectedVersion = default(Version);


      if (releaseVersion.Major == 0)
      {
        switch (breakLevel)
        {
          case ApiBreakLevel.None:
            expectedVersion = new Version(releaseVersion.Major, releaseVersion.Minor, releaseVersion.Build, releaseVersion.Revision + 1);
            break;
          case ApiBreakLevel.Minor:
            expectedVersion = new Version(releaseVersion.Major, releaseVersion.Minor, releaseVersion.Build + 1, 0);
            break;
          case ApiBreakLevel.Major:
            expectedVersion = new Version(releaseVersion.Major, releaseVersion.Minor + 1, 0, 0);
            break;
        }
        
      }
      else
      {
        switch (breakLevel)
        {
          case ApiBreakLevel.None:
            expectedVersion = new Version(releaseVersion.Major, releaseVersion.Minor, releaseVersion.Build + 1, 0);
            break;
          case ApiBreakLevel.Minor:
          case ApiBreakLevel.Major:
            expectedVersion = new Version(releaseVersion.Major, releaseVersion.Minor + 1, 0, 0);
            break;
        }
      }
      return expectedVersion;
    }

    public bool IsVersionValid(Version releaseVersion, Version currentVersion, ApiBreakLevel breakLevel)
    {
      var expectedVersion = releaseVersion;

      return CompareVersion(currentVersion, expectedVersion) >= 0;
    }

    public int CompareVersion(Version v1, Version v2)
    {
      if (v2 == null)
        return 1;

      if (v1.Major != v2.Major)
        if (v1.Major > v2.Major)
          return 2;
        else
          return -2;

      if (v1.Minor != v2.Minor)
        if (v1.Minor > v2.Minor)
          return 1;
        else
          return -1;

      return 0;
    }
  }
}