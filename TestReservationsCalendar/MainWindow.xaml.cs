using Telerik.Windows.Controls;

namespace TestReservationsCalendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RadRibbonWindow
    {
        static MainWindow()
        {
            StyleManager.ApplicationTheme = new Office2013Theme();
            IsWindowsThemeEnabled = false;
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
