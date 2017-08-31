using Mono.Cecil;

namespace ApiChange.Api.Extensions
{
  public interface IApiChecker
  {
    IApiStatus VerifyApiBreak(AssemblyDefinition releaseAssemblyPath, AssemblyDefinition assemblyToVerify);
  }
}