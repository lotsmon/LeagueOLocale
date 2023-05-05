using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeagueOLocale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _leaugePath = @"C:\Riot Games\League of Legends\LeagueClient.exe";
        private const string _registryKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall\Riot Game league_of_legends.live";
        private const string _registryName = "InstallLocation";
        private LaunchService launchService = new LaunchService();

        public MainWindow()
        {
            InitializeComponent();
            langcm.ItemsSource = typeof(Locale).GetEnumValues();
            if(WokingPath is not null)
            {
                launchService.PathLol = _leaugePath;
            }
        }

        private string WokingPath
        {
            get
            {
                string str = "";
                if (Registry.GetValue(_registryKey, _registryName, null) is string registryValue)
                {
                    str = registryValue + "/" + "/" + "LeagueClient.exe";
                }

                _leaugePath = str;
                return str;
            }
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {
            if (langcm.SelectedIndex < 0)
                return;

            var s = launchService.Launch((Locale)langcm.SelectedIndex);
        }
    }
}
