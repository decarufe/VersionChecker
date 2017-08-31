using Mono.Cecil;

namespace ApiChange.Api.Extensions
{
  public interface IApiValidator
  {
    IApiStatus ValidateApi(AssemblyDefinition releaseAssembly, AssemblyDefinition targetAssembly);
  }
}