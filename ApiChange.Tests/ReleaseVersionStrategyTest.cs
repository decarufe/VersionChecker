using System;
using ApiChange.Api.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiChange.Tests
{
  [TestClass]
  public class ReleaseVersionStrategyTest
  {
    [TestMethod]
    public void Should_be_false_if_versions_are_same_and_change_is_major()
    {
      // Arrange
      var versionStrategy = new ReleaseVersionStrategy();
      var releaseVersion = new Version(1, 0, 0, 0);
      var currentVersion = new Version(1, 0, 0, 0);

      // Act
      bool isVersionValid = versionStrategy.IsVersionValid(releaseVersion, currentVersion, ApiBreakLevel.Major);

      // Assert
      Assert.IsFalse(isVersionValid);
    }
  }
}