using System.Windows;
using Telerik.Windows.Controls;
using TestReservationsCalendar.BuildingLink.Data;

namespace TestReservationsCalendar.BuildingLink.ViewModels
{
    public class ReservationStyleSelector : OrientedAppointmentItemStyleSelector
    {
        public Style RequestedStatusStyle { get; set; }

        public Style ApprovedStatusStyle { get; set; }

        public Style DeclinedStatusStyle { get; set; }

        public Style CanceledStatusStyle { get; set; }


        public override Style SelectStyle(object item, DependencyObject container, ViewDefinitionBase activeViewDefinition)
        {
            var reservation = item as AmenityReservation;
            if (reservation == null)
            {
                return base.SelectStyle(item, container, activeViewDefinition);
            }

            switch (reservation.Status)
            {
                case "Requested":
                    return RequestedStatusStyle;
                case "Approved":
                    return ApprovedStatusStyle;
                case "Declined":
                    return DeclinedStatusStyle;
                case "Canceled":
                    return CanceledStatusStyle;
            }

            return base.SelectStyle(item, container, activeViewDefinition);
        }
    }
}