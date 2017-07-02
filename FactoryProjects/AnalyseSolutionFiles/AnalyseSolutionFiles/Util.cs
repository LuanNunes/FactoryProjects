using System;

namespace AnalyseSolutionFiles
{
    public static class Util
    {
        public static void Write(this string str) => Console.WriteLine(str);

        public static void Wait() => Console.ReadLine();

        public static string Read() => Console.ReadLine();
    }
}
