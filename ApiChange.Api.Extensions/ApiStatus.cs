using System;
using ApiChange.Api.Introspection;

namespace ApiChange.Api.Extensions
{
  class ApiStatus : IApiStatus
  {
    #region Implementation of IApiStatus

    public ApiBreakLevel BreakLevel { get; set; }
    public Version ReferenceVersion { get; set; }
    public Version CurrentVersion { get; set; }
    public Version ExpectedVersion { get; set; }
    public AssemblyDiffCollection DiffResult { get; set; }
    public bool IsVersionValid { get; set; }

    #endregion
  }
}
