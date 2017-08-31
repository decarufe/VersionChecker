using System;
using System.IO;
using System.Resources;
using System.Runtime.Remoting;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace ApiChange.Api.Extensions
{
  public class Logger : ILogger
  {
    private TaskLoggingHelper _loggingHelper;

    public Logger(IBuildEngine buildEngine, string taskName)
    {
      _loggingHelper = new TaskLoggingHelper(buildEngine, taskName);
    }

    public Logger(ITask taskInstance)
    {
      _loggingHelper = new TaskLoggingHelper(taskInstance);
    }

    public object GetLifetimeService()
    {
      return _loggingHelper.GetLifetimeService();
    }

    public object InitializeLifetimeService()
    {
      return _loggingHelper.InitializeLifetimeService();
    }

    public ObjRef CreateObjRef(Type requestedType)
    {
      return _loggingHelper.CreateObjRef(requestedType);
    }

    public string ExtractMessageCode(string message, out string messageWithoutCodePrefix)
    {
      return _loggingHelper.ExtractMessageCode(message, out messageWithoutCodePrefix);
    }

    public string FormatResourceString(string resourceName, params object[] args)
    {
      return _loggingHelper.FormatResourceString(resourceName, args);
    }

    public string FormatString(string unformatted, params object[] args)
    {
      return _loggingHelper.FormatString(unformatted, args);
    }

    public string GetResourceMessage(string resourceName)
    {
      return _loggingHelper.GetResourceMessage(resourceName);
    }

    public void LogMessage(string message, params object[] messageArgs)
    {
      _loggingHelper.LogMessage(message, messageArgs);
    }

    public void LogMessage(MessageImportance importance, string message, params object[] messageArgs)
    {
      _loggingHelper.LogMessage(importance, message, messageArgs);
    }

    public void LogMessageFromResources(string messageResourceName, params object[] messageArgs)
    {
      _loggingHelper.LogMessageFromResources(messageResourceName, messageArgs);
    }

    public void LogMessageFromResources(MessageImportance importance, string messageResourceName, params object[] messageArgs)
    {
      _loggingHelper.LogMessageFromResources(importance, messageResourceName, messageArgs);
    }

    public void LogExternalProjectStarted(string message, string helpKeyword, string projectFile, string targetNames)
    {
      _loggingHelper.LogExternalProjectStarted(message, helpKeyword, projectFile, targetNames);
    }

    public void LogExternalProjectFinished(string message, string helpKeyword, string projectFile, bool succeeded)
    {
      _loggingHelper.LogExternalProjectFinished(message, helpKeyword, projectFile, succeeded);
    }

    public void LogCommandLine(string commandLine)
    {
      _loggingHelper.LogCommandLine(commandLine);
    }

    public void LogCommandLine(MessageImportance importance, string commandLine)
    {
      _loggingHelper.LogCommandLine(importance, commandLine);
    }

    public void LogError(string message, params object[] messageArgs)
    {
      _loggingHelper.LogError(message, messageArgs);
    }

    public void LogError(string subcategory, string errorCode, string helpKeyword, string file, int lineNumber, int columnNumber,
                         int endLineNumber, int endColumnNumber, string message, params object[] messageArgs)
    {
      _loggingHelper.LogError(subcategory, errorCode, helpKeyword, file, lineNumber, columnNumber, endLineNumber, endColumnNumber, message, messageArgs);
    }

    public void LogErrorFromResources(string messageResourceName, params object[] messageArgs)
    {
      _loggingHelper.LogErrorFromResources(messageResourceName, messageArgs);
    }

    public void LogErrorFromResources(string subcategoryResourceName, string errorCode, string helpKeyword, string file,
                                      int lineNumber, int columnNumber, int endLineNumber, int endColumnNumber,
                                      string messageResourceName, params object[] messageArgs)
    {
      _loggingHelper.LogErrorFromResources(subcategoryResourceName, errorCode, helpKeyword, file, lineNumber, columnNumber, endLineNumber, endColumnNumber, messageResourceName, messageArgs);
    }

    public void LogErrorWithCodeFromResources(string messageResourceName, params object[] messageArgs)
    {
      _loggingHelper.LogErrorWithCodeFromResources(messageResourceName, messageArgs);
    }

    public void LogErrorWithCodeFromResources(string subcategoryResourceName, string file, int lineNumber, int columnNumber,
                                              int endLineNumber, int endColumnNumber, string messageResourceName,
                                              params object[] messageArgs)
    {
      _loggingHelper.LogErrorWithCodeFromResources(subcategoryResourceName, file, lineNumber, columnNumber, endLineNumber, endColumnNumber, messageResourceName, messageArgs);
    }

    public void LogErrorFromException(Exception exception)
    {
      _loggingHelper.LogErrorFromException(exception);
    }

    public void LogErrorFromException(Exception exception, bool showStackTrace)
    {
      _loggingHelper.LogErrorFromException(exception, showStackTrace);
    }

    public void LogErrorFromException(Exception exception, bool showStackTrace, bool showDetail, string file)
    {
      _loggingHelper.LogErrorFromException(exception, showStackTrace, showDetail, file);
    }

    public void LogWarning(string message, params object[] messageArgs)
    {
      _loggingHelper.LogWarning(message, messageArgs);
    }

    public void LogWarning(string subcategory, string warningCode, string helpKeyword, string file, int lineNumber,
                           int columnNumber, int endLineNumber, int endColumnNumber, string message, params object[] messageArgs)
    {
      _loggingHelper.LogWarning(subcategory, warningCode, helpKeyword, file, lineNumber, columnNumber, endLineNumber, endColumnNumber, message, messageArgs);
    }

    public void LogWarningFromResources(string messageResourceName, params object[] messageArgs)
    {
      _loggingHelper.LogWarningFromResources(messageResourceName, messageArgs);
    }

    public void LogWarningFromResources(string subcategoryResourceName, string warningCode, string helpKeyword, string file,
                                        int lineNumber, int columnNumber, int endLineNumber, int endColumnNumber,
                                        string messageResourceName, params object[] messageArgs)
    {
      _loggingHelper.LogWarningFromResources(subcategoryResourceName, warningCode, helpKeyword, file, lineNumber, columnNumber, endLineNumber, endColumnNumber, messageResourceName, messageArgs);
    }

    public void LogWarningWithCodeFromResources(string messageResourceName, params object[] messageArgs)
    {
      _loggingHelper.LogWarningWithCodeFromResources(messageResourceName, messageArgs);
    }

    public void LogWarningWithCodeFromResources(string subcategoryResourceName, string file, int lineNumber, int columnNumber,
                                                int endLineNumber, int endColumnNumber, string messageResourceName,
                                                params object[] messageArgs)
    {
      _loggingHelper.LogWarningWithCodeFromResources(subcategoryResourceName, file, lineNumber, columnNumber, endLineNumber, endColumnNumber, messageResourceName, messageArgs);
    }

    public void LogWarningFromException(Exception exception)
    {
      _loggingHelper.LogWarningFromException(exception);
    }

    public void LogWarningFromException(Exception exception, bool showStackTrace)
    {
      _loggingHelper.LogWarningFromException(exception, showStackTrace);
    }

    public bool LogMessagesFromFile(string fileName)
    {
      return _loggingHelper.LogMessagesFromFile(fileName);
    }

    public bool LogMessagesFromFile(string fileName, MessageImportance messageImportance)
    {
      return _loggingHelper.LogMessagesFromFile(fileName, messageImportance);
    }

    public bool LogMessagesFromStream(TextReader stream, MessageImportance messageImportance)
    {
      return _loggingHelper.LogMessagesFromStream(stream, messageImportance);
    }

    public bool LogMessageFromText(string lineOfText, MessageImportance messageImportance)
    {
      return _loggingHelper.LogMessageFromText(lineOfText, messageImportance);
    }

    public ResourceManager TaskResources
    {
      get { return _loggingHelper.TaskResources; }
      set { _loggingHelper.TaskResources = value; }
    }

    public string HelpKeywordPrefix
    {
      get { return _loggingHelper.HelpKeywordPrefix; }
      set { _loggingHelper.HelpKeywordPrefix = value; }
    }

    public bool HasLoggedErrors
    {
      get { return _loggingHelper.HasLoggedErrors; }
    }
  }
}