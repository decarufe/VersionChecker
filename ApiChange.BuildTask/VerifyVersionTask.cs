using System.IO;
using ApiChange.Api.Extensions;
using ApiChange.Api.Introspection;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Mono.Cecil;
using ILogger = ApiChange.Api.Extensions.ILogger;
using Logger = ApiChange.Api.Extensions.Logger;

namespace ApiChange.BuildTask
{
  public class VerifyVersionTask : Task, IVerifyVersionTask
  {
    private readonly IAbout _about;
    private readonly ApiMessagebuilder _apiMessagebuilder;
    private bool? _beta;
    private IApiStatus mApiStatus;
    private AssemblyDefinition mReleaseAssembly;
    private string mReleaseAssemblyPathToCompare;

    private AssemblyDefinition mTargetAssembly;
    private string mTargetAssemblyPathToCompare;

    private IApiValidator mApiValidator;
    private IApiChecker mChecker;

    private ILogger _log;
    private readonly IApiCheckerFactory _apiCheckerFactory;

    public IApiStatus ApiStatus
    {
      get { return mApiStatus; }
    }

    public VerifyVersionTask() : this(null)
    {
    }

    public VerifyVersionTask(ILogger logger)
    {
      if (logger == null) _log = new Logger(this);
      else _log = logger;
      _about = new About();
      _apiMessagebuilder = new ApiMessagebuilder(this);
      _apiCheckerFactory = ApiCheckerFactory.Instance;
    }

    public VerifyVersionTask(IApiValidator apiValidator, ILogger logger, IApiCheckerFactory apiCheckerFactory)
    {
      _log = logger;
      _about = new About();
      _apiMessagebuilder = new ApiMessagebuilder(this);
      _apiCheckerFactory = apiCheckerFactory;
      mApiValidator = apiValidator;
    }

    public IApiValidator ApiValidator
    {
      get
      {
        if (mApiValidator == null)
          mApiValidator = new ApiValidator(_log, Checker, _apiMessagebuilder);
        return mApiValidator;
      }
    }

    #region Properties

    [Required]
    public string BuildConfiguration { get; set; }

    [Required]
    public string ReleaseAssemblyPathToCompare
    {
      get { return mReleaseAssemblyPathToCompare; }
      set
      {
        mReleaseAssemblyPathToCompare = value;
        mReleaseAssembly = AssemblyLoader.LoadCecilAssembly(value);
      }
    }

    [Required]
    public string TargetAssemblyPathToCompare
    {
      get { return mTargetAssemblyPathToCompare; }
      set
      {
        mTargetAssemblyPathToCompare = value;
        mTargetAssembly = AssemblyLoader.LoadCecilAssembly(value);
      }
    }

    public bool Beta
    {
      get { return _beta.HasValue && _beta.Value; }
      set
      {
        _beta = value;
      }
    }

    public AssemblyDefinition ReleaseAssembly
    {
      get { return mReleaseAssembly; }
      set { mReleaseAssembly = value; }
    }

    public AssemblyDefinition TargetAssembly
    {
      get { return mTargetAssembly; }
      set { mTargetAssembly = value; }
    }

    public IApiChecker Checker
    {
      get
      {
        if (Beta)
          return mChecker ?? (mChecker = _apiCheckerFactory.Create(new BetaVersionStrategy(), new BetaApiBreakRuleStrategy()));
        
        return mChecker ?? (mChecker = _apiCheckerFactory.Create(new ReleaseVersionStrategy()));
      }
    }

    #endregion

    #region Overrides of Task

    public override bool Execute()
    {
      try
      {
        string header = _about.CreateHeader();
        _log.LogMessage(MessageImportance.High, header);

        mApiStatus = ApiValidator.ValidateApi(ReleaseAssembly, TargetAssembly);

        return mApiStatus.IsVersionValid;
      }
      catch (MessageException ex)
      {
        _log.LogError(ex.Message);
        _log.LogMessage(MessageImportance.High, _apiMessagebuilder.BuildVersionMessage());

        if (_log.HasLoggedErrors && File.Exists(TargetAssemblyPathToCompare))
        {
          File.Delete(TargetAssemblyPathToCompare);
          return false;
        }
      }

      return false;
    }

    #endregion
  }
}