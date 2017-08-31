using System;

namespace ApiChange.Api.Extensions
{
  public class MessageException : Exception
  {
    public MessageException(string message) : base(message)
    {
    }
  }
}