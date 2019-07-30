using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using TestReservationsCalendar.BuildingLink.Data;

namespace TestReservationsCalendar.BuildingLink.ViewModels
{
    public partial class ReservationsCalendarViewModel : ViewModelBase
    {
        private ObservableCollection<AmenityReservation> _appointments;
        private DateTime _currentDate;


        public ObservableCollection<AmenityReservation> Appointments
        {
            get => _appointments;
            set
            {
                if (_appointments != value)
                {
                    _appointments = value;
                    OnPropertyChanged(() => Appointments);
                }
            }
        }

        public int VisibleDays => 42; //todo dynamic change;

        public DateTime CurrentDate
        {
            get => _currentDate;
            set
            {
                if (_currentDate != value)
                {
                    _currentDate = value;
                    SpecialSlots = GenerateSpecialSlots();
                    OnPropertyChanged(() => SpecialSlots);
                }
            }
        }

        public ObservableCollection<Slot> SpecialSlots { get; set; }


        public ReservationsCalendarViewModel()
        {
            Appointments = GetCalendarAppointments();

            CurrentDate = DateTime.Today;

            SpecialSlots = GenerateSpecialSlots();
        }


        private ObservableCollection<Slot> GenerateSpecialSlots()
        {
            var currentMonth = CurrentDate.Month;
            var currentYear = CurrentDate.Year;
            var previousMonthsSlot = new OtherMonthSlot
            {
                Start = DateTime.MinValue,
                End = new DateTime(currentYear, currentMonth, 1),
                IsReadOnly = true
            };

            var nextMonthsSlot = new OtherMonthSlot
            {
                Start = new DateTime(currentYear, currentMonth + 1, 1),
                End = DateTime.MaxValue,
                IsReadOnly = true
            };
            var result = new ObservableCollection<Slot>()
            {
                previousMonthsSlot, nextMonthsSlot
            };

            return result;
        }


        private static ObservableCollection<AmenityReservation> GetCalendarAppointments()
        {
            var appointments = new ObservableCollection<AmenityReservation>();

            string reservationsString;
            using (var sr = new StreamReader(@".\BuildingLink\Data\Database.json"))
            {
                reservationsString = sr.ReadToEnd();
            }
            var serializer = new JsonSerializer();
            var deserialized = serializer.Deserialize<AmenityReservation[]>(reservationsString).ToList();

            appointments.AddRange(deserialized);

            return appointments;
        }
    }
}