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
        LaunchService launchService;

        public MainWindow()
        {
            InitializeComponent();
            langcm.ItemsSource = typeof(Locale).GetEnumValues();
            if (WokingPath)
            {
                launchService = new LaunchService(_leaugePath);
            }
            else
            {
                FileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "LeagueClient.exe .exe|*.exe";
                bool? result = fileDialog.ShowDialog();

                if (fileDialog.FileName.Split('\\').Last() != "LeagueClient.exe" && result == true)
                {
                    _leaugePath = fileDialog.FileName;
                }
            }
        }

        private bool WokingPath
        {
            get
            {
                bool work = false;
                if (File.Exists(_leaugePath)) return true;
                if (Registry.GetValue(_registryKey, _registryName, null) is string registryValue)
                {
                    _leaugePath = registryValue;
                    return true;
                }

                return work;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (langcm.SelectedIndex < 0)
                return;

            var s = launchService.Launch((Locale)langcm.SelectedIndex);
            if (s) state.Text = "Starting...";
        }
    }
}
