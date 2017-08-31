using System;
using System.IO;
using System.Text;
using ApiChange.Api.Introspection.Diff;
using Microsoft.Build.Framework;
using Mono.Cecil;

namespace ApiChange.Api.Extensions
{
  public class ApiValidator : IApiValidator
  {
    private readonly ILogger _logger;
    private IApiChecker _checker;
    private ApiMessagebuilder _messageBuilder;

    public ApiValidator(ILogger logger, IApiChecker checker, ApiMessagebuilder messageBuilder)
    {
      _logger = logger;
      _checker = checker;
      _messageBuilder = messageBuilder;
    }

    public IApiStatus ValidateApi(AssemblyDefinition releaseAssembly, AssemblyDefinition targetAssembly)
    {
      IApiStatus apiStatus = null;
      try
      {
        apiStatus = Checker.VerifyApiBreak(releaseAssembly, targetAssembly);

        string wErrorMessage = _messageBuilder.BuildErrorMessage(apiStatus);

        if (!apiStatus.IsVersionValid)
        {
          throw new MessageException(wErrorMessage);
        }

        _logger.LogMessage(MessageImportance.High, _messageBuilder.BuildVersionMessage());
      }
      catch (Exception ex)
      {
        _logger.LogErrorFromException(ex, true, true, null);
        throw;
      }
      finally
      {
        if (apiStatus == null) _logger.LogMessage(MessageImportance.High, "No Changes");
        else DisplayChanges(apiStatus);
      }

      return apiStatus;
    }

    private void DisplayChanges(IApiStatus apiStatus)
    {
      var sb = new StringBuilder();
      using (TextWriter tw = new StringWriter(sb))
      {
        var wMessageArgs = new DiffPrinter(tw);
        wMessageArgs.Print(apiStatus.DiffResult);
      }
      _logger.LogMessage(MessageImportance.High, sb.ToString());
    }

    protected IApiChecker Checker
    {
      get { return _checker; }
    }


  }
}