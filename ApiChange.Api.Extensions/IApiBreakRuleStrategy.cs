using System;
using ApiChange.Api.Introspection;

namespace ApiChange.Api.Extensions
{
  public interface IApiBreakRuleStrategy
  {
    ApiBreakLevel GetBreakLevel(AssemblyDiffCollection diffResult);
  }
}