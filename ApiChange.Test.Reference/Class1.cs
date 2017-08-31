using System;
using System.Linq;

namespace ApiChange.Test
{
    public class Class1
    {
      private object _privateMember;
      internal object _internalMember;
      protected object _protectedMember;
      public object _publicMember;

      public void PublicMethod()
      {
        _publicMember = new object();
      }

      internal void InternalMethod()
      {
        _internalMember = new object();
      }

      protected void ProtectedMethod()
      {
        _protectedMember = new object();
      }

      private void PrivateMethod()
      {
        _privateMember = new object();
      }
    }
}
