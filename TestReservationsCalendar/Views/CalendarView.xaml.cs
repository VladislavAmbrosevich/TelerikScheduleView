using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace TestReservationsCalendar
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : UserControl
    {
        public CalendarView()
        {
            InitializeComponent();
            IconSources.ChangeIconsSet(IconsSet.Modern);
        }
    }
}
