using System;

namespace ApiChange.Test
{
  public interface IInterface1
  {
    void InterfaceMethod();
    string InterfaceMemberProperty { get; set; }
    string InterfaceReadOnlyProperty { get; }
  }
}