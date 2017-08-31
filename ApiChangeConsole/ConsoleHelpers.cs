using System;

namespace ApiChangeConsole
{
  class ConsoleHelpers
  {
    public static void WriteError(string format, params object[] arguments)
    {
      var wColor = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(format, arguments);
      Console.ForegroundColor = wColor;
    }

    public static void WriteLine(string format, params object[] arguments)
    {
      Console.WriteLine(format, arguments);
    }

    public static void WriteSuccess(string format, params object[] arguments)
    {
      var wColor = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine(format, arguments);
      Console.ForegroundColor = wColor;
    }

    public static void WriteWarning(string format, params object[] arguments)
    {
      var wColor = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine(format, arguments);
      Console.ForegroundColor = wColor;
    }
  }
}
