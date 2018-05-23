using MonoSymbolicateHelper.Core;
using System;
using System.Collections.Generic;
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

namespace MonoSymbolicateHelper.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            ArchivePathTextBox.Text = Properties.Settings.Default.ArchivePath;
            CommandPathTextBox.Text = Properties.Settings.Default.CommandPath;
            PackageNameTextBox.Text = Properties.Settings.Default.PackageName;
            VersionCodeTextBox.Text = Properties.Settings.Default.VersionCode;
        }

        private void SymbolicateButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ArchivePath = ArchivePathTextBox.Text;
            Properties.Settings.Default.CommandPath = CommandPathTextBox.Text;
            Properties.Settings.Default.PackageName = PackageNameTextBox.Text;
            Properties.Settings.Default.VersionCode = VersionCodeTextBox.Text;

            Properties.Settings.Default.Save();

            var _helper = new SymbolicateHelper(ArchivePathTextBox.Text, CommandPathTextBox.Text);

            var symbolicatedTrace = _helper.Symbolicate(PackageNameTextBox.Text, VersionCodeTextBox.Text, InputTraceTextBox.Text);

            SymbolicatedTraceTextBox.Text = symbolicatedTrace.ToString();
        }
    }
}
