using System;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace TestReservationsCalendar.BuildingLink.Data
{
    public class OtherMonthSlot : Slot
    {
        public override Slot Copy()
        {
            Slot slot = new OtherMonthSlot();
            slot.CopyFrom(this);
            return slot;
        }

        public override void CopyFrom(Slot other)
        {
            if (other is OtherMonthSlot otherSlot)
            {
                base.CopyFrom(otherSlot);
            }
        }
    }
}