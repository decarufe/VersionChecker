using System;
using ApiChange.Api.Extensions;
using ApiChange.BuildTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mono.Cecil;
using Moq;

namespace ApiChange.Tests
{
  [TestClass]
  public class BuildTaskTest
  {
    private Mock<ILogger> _loggerMock;
    private Mock<IApiValidator> _apiValidatorMock;
    private Mock<IApiStatus> _apiStatusMock;
    private Mock<IApiCheckerFactory> _apiCheckerFactoryMock;

    [TestInitialize]
    public void TestInitialize()
    {
      _loggerMock = new Mock<ILogger>();
      _apiValidatorMock = new Mock<IApiValidator>();
      _apiStatusMock = new Mock<IApiStatus>();
      _apiCheckerFactoryMock = new Mock<IApiCheckerFactory>();
    }

    [TestMethod]
    public void Should_return_true_if_version_is_valid()
    {
      _apiStatusMock.SetupGet(x => x.IsVersionValid).Returns(true);
      _apiValidatorMock.Setup(x => x.ValidateApi(It.IsAny<AssemblyDefinition>(), It.IsAny<AssemblyDefinition>()))
                      .Returns(_apiStatusMock.Object);

      var verifyVersionTask = new VerifyVersionTask(_apiValidatorMock.Object, _loggerMock.Object, _apiCheckerFactoryMock.Object);

      bool execute = verifyVersionTask.Execute();
      
      Assert.IsTrue(execute);
    }

    [TestMethod]
    public void Should_return_false_if_version_is_invalid()
    {
      _apiStatusMock.SetupGet(x => x.IsVersionValid).Returns(false);
      _apiValidatorMock.Setup(x => x.ValidateApi(It.IsAny<AssemblyDefinition>(), It.IsAny<AssemblyDefinition>()))
                      .Returns(_apiStatusMock.Object);

      var verifyVersionTask = new VerifyVersionTask(_apiValidatorMock.Object, _loggerMock.Object, _apiCheckerFactoryMock.Object);

      bool execute = verifyVersionTask.Execute();
      
      Assert.IsFalse(execute);
    }

    [TestMethod]
    public void Should_use_beta_strategy_when_in_beta()
    {
      _apiStatusMock.SetupGet(x => x.IsVersionValid).Returns(false);
      _apiValidatorMock.Setup(x => x.ValidateApi(It.IsAny<AssemblyDefinition>(), It.IsAny<AssemblyDefinition>()))
                      .Returns(_apiStatusMock.Object);

      var verifyVersionTask = new VerifyVersionTask(_apiValidatorMock.Object, _loggerMock.Object, _apiCheckerFactoryMock.Object);
      verifyVersionTask.Beta = true;

      IApiChecker execute = verifyVersionTask.Checker;
      
      _apiCheckerFactoryMock.Verify(x => x.Create(It.IsAny<BetaVersionStrategy>(), It.IsAny<BetaApiBreakRuleStrategy>()));
    }

    [TestMethod]
    public void Should_use_release_strategy_when_not_in_beta()
    {
      _apiStatusMock.SetupGet(x => x.IsVersionValid).Returns(false);
      _apiValidatorMock.Setup(x => x.ValidateApi(It.IsAny<AssemblyDefinition>(), It.IsAny<AssemblyDefinition>()))
                      .Returns(_apiStatusMock.Object);

      var verifyVersionTask = new VerifyVersionTask(_apiValidatorMock.Object, _loggerMock.Object, _apiCheckerFactoryMock.Object);
      verifyVersionTask.Beta = false;

      IApiChecker execute = verifyVersionTask.Checker;
      
      _apiCheckerFactoryMock.Verify(x => x.Create(It.IsAny<ReleaseVersionStrategy>()));
    }

    [TestMethod]
    public void Test_main_VerifyVersionTask()
    {
      var _logger = new Mock<ILogger>();
      var verifyVersionTask = new VerifyVersionTask(_logger.Object);
      var assembly1 = TestHelper.GetEmbededAssembly(@"Versions\V1.0.0.0\ApiChange.Test.dll");
      var assembly21 = TestHelper.GetEmbededAssembly(@"Versions\V2.1.0.0\ApiChange.Test.dll");
      verifyVersionTask.ReleaseAssembly = assembly1;
      verifyVersionTask.TargetAssembly = assembly21;

      bool execute = verifyVersionTask.Execute();

      Assert.IsTrue(execute);
    }


  }
}
