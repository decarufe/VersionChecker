using System;
using ApiChange.Api.Extensions;
using ApiChange.BuildTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mono.Cecil;
using Moq;

namespace ApiChange.Tests
{
  [TestClass]
  public class BetaVersionStrategyTest
  {
    [TestMethod]
    public void Should_be_true_if_versions_are_same_and_change_is_major()
    {
      // Arrange
      var versionStrategy = new BetaVersionStrategy();
      var releaseVersion = new Version(1, 0, 0, 0);
      var currentVersion = new Version(1, 0, 0, 0);

      // Act
      bool isVersionValid = versionStrategy.IsVersionValid(releaseVersion, currentVersion, ApiBreakLevel.Major);

      // Assert
      Assert.IsTrue(isVersionValid);
    }

    [TestMethod]
    public void Major_change_in_beta_should_tigger_minor_version_change()
    {
      // Arrange
      var wApiChecker = ApiCheckerFactory.Instance.Create(new BetaVersionStrategy(), new BetaApiBreakRuleStrategy());
      var assembly1 = TestHelper.GetEmbededAssembly(@"Versions\V1.0.0.0\ApiChange.Test.dll");
      var assembly2 = TestHelper.GetEmbededAssembly(@"Versions\V2.0.0.0\ApiChange.Test.dll");

      // Act
      var wVerifyApiBreak = wApiChecker.VerifyApiBreak(assembly1, assembly2);

      // Assert
      Assert.AreEqual(ApiBreakLevel.Minor, wVerifyApiBreak.BreakLevel);
      Assert.AreEqual(new Version(1, 1, 0, 0), wVerifyApiBreak.ExpectedVersion);
      Assert.IsTrue(wVerifyApiBreak.IsVersionValid);
    }


  }
}
