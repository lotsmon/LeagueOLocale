using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace LeagueOLocale
{
    public class LaunchService
    {
        public string PathLol = "";

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

        private List<string> proc = new List<string> { "LeagueClient", "RiotClientServices" };

        private void Close()
        {
            proc.ForEach(name => {
                Process.GetProcessesByName(name).ToList().ForEach(p => {
                    p.Kill(); 
                });
            });
        }
    }
}
