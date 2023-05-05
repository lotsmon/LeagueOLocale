
using Microsoft.Win32;
using System.Diagnostics;

namespace FixHotLaunch
{
    public class Program
    {
        private static string _leaguePath = "";
        private const string _registryKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall\Riot Game league_of_legends.live";
        private const string _registryName = "InstallLocation";

        public static void Main(string[] args)
        {
            if (Registry.GetValue(_registryKey, _registryName, null) is string registryValue)
            {
                _leaguePath = registryValue + "/" + "/" + "LeagueClient.exe";
            }

            Launch(_leaguePath);
            Console.ReadLine();
        }

        public static void Launch(string Path)
        {
            Close();

            while(Process.GetProcesses().Where(x => x.ProcessName == proc[0] || x.ProcessName == proc[1] && x is not null) is null)
            {
                Thread.Sleep(1);
            }
            Console.WriteLine("League Started.");

            var league = new Process();
            league.StartInfo.FileName = Path;
            league.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            league.Start();
        }

        private static List<string> proc = new List<string>{ "LeagueClient", "RiotClientServices" };

        private static void Close()
        {
            proc.ForEach(name => {
                Process.GetProcessesByName(name).ToList().ForEach(p => {
                    p.Kill();
                });
            });
        }
    }
}
