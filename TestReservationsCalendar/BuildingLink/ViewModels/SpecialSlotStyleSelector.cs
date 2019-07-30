using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using TestReservationsCalendar.BuildingLink.Data;

namespace TestReservationsCalendar.BuildingLink.ViewModels
{
    public class SpecialSlotStyleSelector : ScheduleViewStyleSelector
    {
        public Style OtherMonthStyle { get; set; }


        public override Style SelectStyle(object item, DependencyObject container, ViewDefinitionBase activeViewDefinition)
        {
            return item is OtherMonthSlot
                ? OtherMonthStyle
                : base.SelectStyle(item, container, activeViewDefinition);
        }
    }
}