using System;
using ApiChange.Api.Introspection;
using Mono.Cecil;

namespace ApiChange.Api.Extensions
{
  public interface IApiVersionStrategy
  {
    AssemblyDiffer GetAssemblyDiffer(AssemblyDefinition releaseAssemblyPath, AssemblyDefinition assemblyPathToVerify);
    Version GetExpectedVersion(Version releaseVersion, ApiBreakLevel breakLevel);
    bool IsVersionValid(Version releaseVersion, Version currentVersion, ApiBreakLevel breakLevel);
  }
}