namespace ApiChange.Api.Extensions
{
  public class ApiMessagebuilder
  {
    private IVerifyVersionTask _verifyVersionTask;

    public ApiMessagebuilder(IVerifyVersionTask verifyVersionTask)
    {
      _verifyVersionTask = verifyVersionTask;
    }

    public string BuildErrorMessage(IApiStatus apiStatus)
    {
      string wMessage;
      if (apiStatus.BreakLevel == ApiBreakLevel.Major)
      {
        wMessage = BuildVersionMessage()
                   + " Major version changes."
                   + " You must edit your AssemblyInfo file to change"
                   + " the major version or rollback your changes.";
      }
      else if (apiStatus.BreakLevel == ApiBreakLevel.Minor)
      {
        wMessage = BuildVersionMessage()
                   + " Minor version changes."
                   + " You must edit your AssemblyInfo file to change"
                   + " the minor version or rollback your changes.";
      }
      else
      {
        wMessage = BuildVersionMessage()
                   + " No breaking changes."
                   + " There is a version mismatch.";
      }
      return wMessage;
    }

    public string BuildVersionMessage()
    {
      string wMessage = "Release: " + _verifyVersionTask.ReleaseAssembly.Name.Version.ToString(3) +
                        " Current: " + _verifyVersionTask.TargetAssembly.Name.Version.ToString(3);
      if (_verifyVersionTask.ApiStatus != null) wMessage += " Expected: " + _verifyVersionTask.ApiStatus.ExpectedVersion.ToString(3);
      return wMessage;
    }
  }
}