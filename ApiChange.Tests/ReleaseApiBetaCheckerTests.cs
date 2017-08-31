using System;
using System.Linq;
using System.Reflection;
using System.Resources;
using ApiChange.Api.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mono.Cecil;

namespace ApiChange.Tests
{
  [TestClass]
  public class ReleaseApiBetaCheckerTests
  {
    [TestMethod]
    public void BreakLevelNone()
    {
      // Arrange
      var wApiChecker = ApiCheckerFactory.Instance.Create(new BetaVersionStrategy());
      var assembly1 = TestHelper.GetEmbededAssembly(@"Versions\V0.1.0.0\ApiChange.Test.dll");
      var assembly2 = TestHelper.GetEmbededAssembly(@"Versions\V0.1.0.0\ApiChange.Test.dll");
      
      // Act
      var wVerifyApiBreak = wApiChecker.VerifyApiBreak(assembly1, assembly2);

      // Assert
      Assert.AreEqual(ApiBreakLevel.None, wVerifyApiBreak.BreakLevel);
      Assert.IsTrue(wVerifyApiBreak.IsVersionValid);
    }

    [TestMethod]
    public void BreakLevelMinor()
    {
      // Arrange
      var wApiChecker = ApiCheckerFactory.Instance.Create(new BetaVersionStrategy());
      var assembly1 = TestHelper.GetEmbededAssembly(@"Versions\V0.1.0.0\ApiChange.Test.dll");
      var assembly2 = TestHelper.GetEmbededAssembly(@"Versions\V0.1.1.0\ApiChange.Test.dll");
      
      // Act
      var wVerifyApiBreak = wApiChecker.VerifyApiBreak(assembly1, assembly2);

      // Assert
      Assert.AreEqual(ApiBreakLevel.Minor, wVerifyApiBreak.BreakLevel);
      Assert.AreEqual(new Version(0, 1, 1, 0), wVerifyApiBreak.ExpectedVersion);
      Assert.IsTrue(wVerifyApiBreak.IsVersionValid);
    }

    [TestMethod]
    public void BreakLevelMajor()
    {
      // Arrange
      var wApiChecker = ApiCheckerFactory.Instance.Create(new BetaVersionStrategy());
      var assembly1 = TestHelper.GetEmbededAssembly(@"Versions\V0.1.0.0\ApiChange.Test.dll");
      var assembly2 = TestHelper.GetEmbededAssembly(@"Versions\V0.2.0.0\ApiChange.Test.dll");

      // Act
      var wVerifyApiBreak = wApiChecker.VerifyApiBreak(assembly1, assembly2);

      // Assert
      Assert.AreEqual(ApiBreakLevel.Major, wVerifyApiBreak.BreakLevel);
      Assert.AreEqual(new Version(0, 2, 0, 0), wVerifyApiBreak.ExpectedVersion);
      Assert.IsTrue(wVerifyApiBreak.IsVersionValid);
    }

    [TestMethod]
    public void CurrentVersionNewerThanExpected()
    {
      // Arrange
      var wApiChecker = ApiCheckerFactory.Instance.Create(new BetaVersionStrategy());
      var assembly1 = TestHelper.GetEmbededAssembly(@"Versions\V0.1.0.0\ApiChange.Test.dll");
      var assembly21 = TestHelper.GetEmbededAssembly(@"Versions\V0.2.1.0\ApiChange.Test.dll");

      // Act
      var wVerifyApiBreak = wApiChecker.VerifyApiBreak(assembly1, assembly21);

      // Assert
      Assert.AreEqual(ApiBreakLevel.Major, wVerifyApiBreak.BreakLevel);
      Assert.AreEqual(new Version(0, 2, 0, 0), wVerifyApiBreak.ExpectedVersion);
      Assert.IsTrue(wVerifyApiBreak.IsVersionValid);
    }
  }
}