using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;

namespace AnalyseSolutionFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            "Executing".Write();

            "Input path of solution:".Write();
            var rootPath = Util.Read();
            Run.ReadRecursive(rootPath);

            $"Number files on solution is fucking... {Run._qtdFiles.Sum()}".Write();
            $"Number Lines on solution if FUCKING... {Run._qtdLines.Sum()}".Write();
            Util.Wait();
        }

        static class Run
        {
            public static List<int> _qtdFiles = new List<int>();
            public static List<int> _qtdLines = new List<int>();

            public static void ReadRecursive(string rootPath)
            {
                Directory.GetDirectories(rootPath).ForEach(dir =>
                {
                    var files = Directory.GetFiles(dir, "*.cs");
                    _qtdFiles.Add(files.Length);
                    files.ForEach(f => _qtdLines.Add(File.ReadLines(f).Count(l => !string.IsNullOrEmpty(l))));

                    ReadRecursive(dir);
                });
            }
        }
    }
}
