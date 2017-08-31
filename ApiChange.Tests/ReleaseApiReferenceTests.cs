using System;
using ApiChange.Api.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mono.Cecil;

namespace ApiChange.Tests
{
  [TestClass]
  public class ReleaseApiReferenceTests
  {
    [TestMethod]
    public void MajorBreakWhenRemoveType()
    {
      // Arrange
      var wApiChecker = ApiCheckerFactory.Instance.Create(new ReleaseVersionStrategy());
      var assembly1 = TestHelper.GetEmbededAssembly(@"Versions\Reference\ApiChange.Test.dll");
      var assembly2 = TestHelper.GetEmbededAssembly(@"Versions\Reference.RemoveType\ApiChange.Test.dll");
      
      // Act
      var wVerifyApiBreak = wApiChecker.VerifyApiBreak(assembly1, assembly2);

      // Assert
      AssertBreakLevel(wVerifyApiBreak, ApiBreakLevel.Major);
      AssertVersion(assembly1, assembly2, wVerifyApiBreak);
    }

    [TestMethod]
    public void MajorBreakWhenRemoveEnumValue()
    {
      // Arrange
      var wApiChecker = ApiCheckerFactory.Instance.Create(new ReleaseVersionStrategy());
      var assembly1 = TestHelper.GetEmbededAssembly(@"Versions\Reference\ApiChange.Test.dll");
      var assembly2 = TestHelper.GetEmbededAssembly(@"Versions\Reference.RemoveEnumValue\ApiChange.Test.dll");
      
      // Act
      var wVerifyApiBreak = wApiChecker.VerifyApiBreak(assembly1, assembly2);

      // Assert
      AssertBreakLevel(wVerifyApiBreak, ApiBreakLevel.Major);
      AssertVersion(assembly1, assembly2, wVerifyApiBreak);
    }

    private static void AssertBreakLevel(IApiStatus wVerifyApiBreak, ApiBreakLevel apiBreakLevel)
    {
      Assert.AreEqual(apiBreakLevel, wVerifyApiBreak.BreakLevel);
    }

    private static void AssertVersion(AssemblyDefinition assembly1, AssemblyDefinition assembly2, IApiStatus wVerifyApiBreak)
    {
      var reference = assembly1.Name.Version;
      var current = assembly2.Name.Version;
      var expected = wVerifyApiBreak.ExpectedVersion;
      string message = String.Format("Ref Version: {0}, Current Version: {1}, Expected Version: {2}", reference, current,
                                     expected);
      Console.WriteLine(message);
      Assert.IsTrue(wVerifyApiBreak.IsVersionValid, message);
    }
  }
}