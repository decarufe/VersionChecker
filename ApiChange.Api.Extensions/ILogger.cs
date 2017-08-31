using System;
using Microsoft.Build.Framework;

namespace ApiChange.Api.Extensions
{
  public interface ILogger
  {
    bool HasLoggedErrors { get; }
    void LogMessage(MessageImportance importance, string message, params object[] messageArgs);
    void LogError(string message, params object[] messageArgs);
    void LogErrorFromException(Exception exception, bool showStackTrace, bool showDetail, string file);
  }
}