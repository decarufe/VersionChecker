namespace ApiChange.Api.Extensions
{
  public interface IApiCheckerFactory
  {
    IApiChecker Create(IApiVersionStrategy versionStrategy, IApiBreakRuleStrategy breakRuleStrategy);
    IApiChecker Create(IApiVersionStrategy versionStrategy);
  }
}