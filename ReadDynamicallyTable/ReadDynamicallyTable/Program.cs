using System.Diagnostics;
using System.Threading;
using DataAccessLayer;

namespace ReadDynamicallyTable
{
    public class Program
    {
        public static void Main(string[] args)
        {
            "Hello everyone! Let's go start the validation proccess".Write();
            Util.BlankLine();
            Thread.Sleep(2000);
            var limitTops = new int[] { 1000, 10000, 100000, 200000, 250000 };
            
            //ValidateWithReflection(limitTops);
            ValidateWithDictionary(limitTops);

            Util.Wait();
        }

        private static void ValidateWithReflection(int[] limitTops)
        {
            foreach (var limitTop in limitTops)
            {
                var stopWatch = Stopwatch.StartNew();
                $"============= ANALYSE TOP {limitTop} REGISTERS =============".Write();
                var repository = new StagingRepository();
                var configurations = repository.GetFileConfiguration(6);
                var records = repository.GetRecords(configurations, limitTop);
                var critics = repository.ValidateRecords(records, configurations);

                if (critics.Count > 0)
                    foreach (var critic in critics)
                        critic.Description.Write();

                if (critics.Count <= 0)
                    "All is good!".Write();

                Util.BlankLine();
                stopWatch.Stop();
                $"TOTAL TIME -> {stopWatch.Elapsed}".Write();
                Util.BlankLine();
            }
        }

        private static void ValidateWithDictionary(int[] limitTops)
        {
            foreach (var limitTop in limitTops)
            {
                var stopWatch = Stopwatch.StartNew();
                $"============= ANALYSE TOP {limitTop} REGISTERS =============".Write();
                var repository = new StagingRepository_Dictionary();
                var configurations = repository.GetFileConfiguration(6);
                var records = repository.GetRecords(configurations, limitTop);
                var critics = repository.ValidateRecords(records, configurations);

                if (critics.Count > 0)
                    foreach (var critic in critics)
                        critic.Description.Write();

                if (critics.Count <= 0)
                    "All is good!".Write();

                Util.BlankLine();
                stopWatch.Stop();
                $"TOTAL TIME -> {stopWatch.Elapsed}".Write();
                Util.BlankLine();
            }
        }
    }
}
