using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Office.Interop.OneNote;
using System.Reflection;
using Application = Microsoft.Office.Interop.OneNote.Application;
using Window = System.Windows.Window;
using OneNoteVanilla5.WPF.ViewModel;
using MahApps.Metro.Controls;

namespace OneNoteVanilla5.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private Application _onenoteApplication;
        public MainWindow(Application onenoteApplication)
        {
            _onenoteApplication = onenoteApplication;
            this.DataContext = new MainViewModel();
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Deactivated(object sender, System.EventArgs e)
        {
            Activate();
        }
    }
}