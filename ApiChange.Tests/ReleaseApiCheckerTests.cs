using System;
using ApiChange.Api.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiChange.Tests
{
  [TestClass]
  public class ReleaseApiCheckerTests
  {
    [TestMethod]
    public void BreakLevelNone()
    {
      // Arrange
      var wApiChecker = ApiCheckerFactory.Instance.Create(new ReleaseVersionStrategy());
      var assembly1 = TestHelper.GetEmbededAssembly(@"Versions\V1.0.0.0\ApiChange.Test.dll");
      var assembly2 = TestHelper.GetEmbededAssembly(@"Versions\V1.0.0.0\ApiChange.Test.dll");
      
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
      var wApiChecker = ApiCheckerFactory.Instance.Create(new ReleaseVersionStrategy());
      var assembly1 = TestHelper.GetEmbededAssembly(@"Versions\V1.0.0.0\ApiChange.Test.dll");
      var assembly2 = TestHelper.GetEmbededAssembly(@"Versions\V1.1.0.0\ApiChange.Test.dll");
      
      // Act
      var wVerifyApiBreak = wApiChecker.VerifyApiBreak(assembly1, assembly2);

      // Assert
      Assert.AreEqual(ApiBreakLevel.Minor, wVerifyApiBreak.BreakLevel);
      Assert.AreEqual(new Version(1, 1, 0, 0), wVerifyApiBreak.ExpectedVersion);
      Assert.IsTrue(wVerifyApiBreak.IsVersionValid);
    }

    [TestMethod]
    public void BreakLevelMajor()
    {
      // Arrange
      var wApiChecker = ApiCheckerFactory.Instance.Create(new ReleaseVersionStrategy());
      var assembly1 = TestHelper.GetEmbededAssembly(@"Versions\V1.0.0.0\ApiChange.Test.dll");
      var assembly2 = TestHelper.GetEmbededAssembly(@"Versions\V2.0.0.0\ApiChange.Test.dll");
      
      // Act
      var wVerifyApiBreak = wApiChecker.VerifyApiBreak(assembly1, assembly2);

      // Assert
      Assert.AreEqual(ApiBreakLevel.Major, wVerifyApiBreak.BreakLevel);
      Assert.AreEqual(new Version(2, 0, 0, 0), wVerifyApiBreak.ExpectedVersion);
      Assert.IsTrue(wVerifyApiBreak.IsVersionValid);
    }

    [TestMethod]
    public void CurrentVersionNewerThanExpected()
    {
      // Arrange
      var wApiChecker = ApiCheckerFactory.Instance.Create(new ReleaseVersionStrategy());
      var assembly1 = TestHelper.GetEmbededAssembly(@"Versions\V1.0.0.0\ApiChange.Test.dll");
      var assembly21 = TestHelper.GetEmbededAssembly(@"Versions\V2.1.0.0\ApiChange.Test.dll");

      // Act
      var wVerifyApiBreak = wApiChecker.VerifyApiBreak(assembly1, assembly21);

      // Assert
      Assert.AreEqual(ApiBreakLevel.Major, wVerifyApiBreak.BreakLevel);
      Assert.AreEqual(new Version(2, 0, 0, 0), wVerifyApiBreak.ExpectedVersion);
      Assert.IsTrue(wVerifyApiBreak.IsVersionValid);
    }

    [TestMethod]
    public void MoveMethodToBaseClass()
    {
      // Arrange
      var wApiChecker = ApiCheckerFactory.Instance.Create(new ReleaseVersionStrategy());
      var assembly11 = TestHelper.GetEmbededAssembly(@"Versions\V1.1.0.0\ApiChange.Test.dll");
      var assembly12 = TestHelper.GetEmbededAssembly(@"Versions\V1.2.0.0\ApiChange.Test.dll");

      // Act
      var wVerifyApiBreak = wApiChecker.VerifyApiBreak(assembly11, assembly12);

      // Assert
      Assert.AreEqual(ApiBreakLevel.Minor, wVerifyApiBreak.BreakLevel);
      Assert.AreEqual(new Version(1, 2, 0, 0), wVerifyApiBreak.ExpectedVersion);
      Assert.IsTrue(wVerifyApiBreak.IsVersionValid);
    }

  }
}