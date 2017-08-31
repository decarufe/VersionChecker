using System;

namespace ApiChange.Api.Extensions
{
  public class ApiCheckerFactory : IApiCheckerFactory
  {
    private static ApiCheckerFactory _apiCheckerFactory;

    public static ApiCheckerFactory Instance
    {
      get { return _apiCheckerFactory ?? (_apiCheckerFactory = new ApiCheckerFactory()); }
      set { _apiCheckerFactory = value; }
    }

    public IApiChecker Create(IApiVersionStrategy versionStrategy, IApiBreakRuleStrategy breakRuleStrategy)
    {
      return new ApiChecker(versionStrategy, breakRuleStrategy);
    }

    public IApiChecker Create(IApiVersionStrategy versionStrategy)
    {
      return Create(versionStrategy, new DefaultApiBreakRuleStrategy());
    }
  }
}
