using System.Windows.Media;
using Telerik.Windows.Controls.ScheduleView;

namespace TestReservationsCalendar.ViewModels
{
    public class CustomAppointment : Appointment
    {
        public CustomAppointment()
        {
            BackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 169, 162, 0));
            LecturePart = "Lecture Part 1";
            PathData = string.Empty;
            PathWidth = 14;
            PathHeight = 16;
        }

        public Brush BackgroundBrush { get; set; }

        public string LecturePart { get; set; }

        public string PathData { get; set; }

        public int PathWidth { get; set; }

        public int PathHeight { get; set; }
    }
}