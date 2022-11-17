using System.Collections.Generic;
using System.Diagnostics;

namespace LeagueOLocale
{
    public class LaunchService
    {
        private string PathLol;

        public LaunchService(string pathLol)
        {
            PathLol = pathLol;
        }

        public bool Launch(Locale locale)
        {
            bool result = false;
            Close();

            var league = new Process();
            league.StartInfo.FileName = PathLol;
            league.StartInfo.Arguments = $" --locale={locale}";
            league.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            league.Start();
            result = league.Responding;

            return result;
        }

        private HashSet<string> proc = new HashSet<string> { "LeagueClient", "RiotClientServices" };

        private void Close()
        {
            foreach (var leagueProcessName in proc)
                foreach (var process in Process.GetProcesses())
                    if (process.ProcessName.ToLower().Contains(leagueProcessName.ToLower()))
                        process.Kill();
        }
    }
}
