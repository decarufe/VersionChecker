using System;
using ApiChange.Api.Introspection;
using Mono.Cecil;

namespace ApiChange.Api.Extensions
{
  class ApiChecker : IApiChecker
  {
    private readonly IApiVersionStrategy mVersionStrategy;
    private readonly IApiBreakRuleStrategy mBreakRuleStrategy;

    public ApiChecker(IApiVersionStrategy versionStrategy, IApiBreakRuleStrategy breakRuleStrategy)
    {
      mVersionStrategy = versionStrategy;
      mBreakRuleStrategy = breakRuleStrategy;
    }

    public IApiStatus VerifyApiBreak(AssemblyDefinition releaseAssemblyPath, AssemblyDefinition assemblyPathToVerify)
    {
      var wApiStatus = new ApiStatus();

      var wAssemblyDiffer = mVersionStrategy.GetAssemblyDiffer(releaseAssemblyPath, assemblyPathToVerify);
      wApiStatus.DiffResult = wAssemblyDiffer.GenerateTypeDiff(QueryAggregator.PublicApiQueries);
      wApiStatus.BreakLevel = GetBreakLevel(wApiStatus.DiffResult);
      wApiStatus.ReferenceVersion = wAssemblyDiffer.AssemblyV1.Name.Version;
      wApiStatus.CurrentVersion = wAssemblyDiffer.AssemblyV2.Name.Version;
      wAssemblyDiffer = new AssemblyDiffer(releaseAssemblyPath, assemblyPathToVerify);
      wApiStatus.ExpectedVersion = mVersionStrategy.GetExpectedVersion(wAssemblyDiffer.AssemblyV1.Name.Version, wApiStatus.BreakLevel);
      wApiStatus.IsVersionValid = IsVersionMatch(releaseAssemblyPath.Name.Version,
                                                 assemblyPathToVerify.Name.Version, wApiStatus.BreakLevel);

      return wApiStatus;
    }

    private bool IsVersionMatch(Version releaseVersion, Version currentVersion, ApiBreakLevel breakLevel)
    {
      return mVersionStrategy.IsVersionValid(releaseVersion, currentVersion, breakLevel);
    }

    private ApiBreakLevel GetBreakLevel(AssemblyDiffCollection diffResult)
    {
      return mBreakRuleStrategy.GetBreakLevel(diffResult);
    }
  }
}
