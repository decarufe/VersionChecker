using System;
using ApiChange.Api.Introspection;

namespace ApiChange.Api.Extensions
{
  public interface IApiStatus
  {
    ApiBreakLevel BreakLevel { get; }
    Version ReferenceVersion { get; }
    Version CurrentVersion { get; set; }
    Version ExpectedVersion { get; }
    AssemblyDiffCollection DiffResult { get; }
    bool IsVersionValid { get; }
  }
}