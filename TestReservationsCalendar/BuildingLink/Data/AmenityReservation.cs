using System;
using Telerik.Windows.Controls.ScheduleView;

namespace TestReservationsCalendar.BuildingLink.Data
{
    public class AmenityReservation : Appointment
    {
        public string Date { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string AmenityFullName { get; set; }

        public string AmenityName { get; set; }

        public string SharedFromBuildingName { get; set; }

        public string RequestedFor { get; set; }

        public string Status { get; set; }

        public string Comments { get; set; }

        public bool IsRecurrent { get; set; }
    }
}