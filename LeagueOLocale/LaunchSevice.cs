using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

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

        private string[] proc = { "LeagueClient", "RiotClientServices" };

        private void Close()
        {
            for (int i = 0; i < proc.Length; i++)
            {
                Process[] process = Process.GetProcessesByName(proc[i]);
                if (process.Length < 1) return;
                process[0].Kill();
            }
        }
    }
}
