using Mono.Cecil;

namespace ApiChange.Api.Extensions
{
  public interface IVerifyVersionTask
  {
    IApiStatus ApiStatus { get; }
    AssemblyDefinition ReleaseAssembly { get; }
    AssemblyDefinition TargetAssembly { get; }
  }
}