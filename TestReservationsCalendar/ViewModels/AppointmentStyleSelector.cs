using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace TestReservationsCalendar.ViewModels
{
    public class AppointmentStyleSelector : OrientedAppointmentItemStyleSelector
    {
        //public Style SmallAppointmentStyle { get; set; }
        //public Style RegularAppointmentStyle { get; set; }
        //public Style BigAppointmentStyle { get; set; }
        //public override Style SelectStyle(object item, DependencyObject container, ViewDefinitionBase activeViewDefinition)
        //{
        //    var appointment = item as Appointment;
        //    if (appointment != null)
        //    {
        //        if (appointment.IsAllDayEvent)
        //        {
        //            return BigAppointmentStyle;
        //        }
        //        else if (appointment.Duration() <= new TimeSpan(1, 0, 0))
        //        {
        //            return SmallAppointmentStyle;
        //        }
        //        else if (appointment.Duration() <= new TimeSpan(2, 0, 0))
        //        {
        //            return RegularAppointmentStyle;
        //        }
        //        else if (appointment.Duration() > new TimeSpan(2, 0, 0))
        //        {
        //            return BigAppointmentStyle;
        //        }
        //    }

        //    return base.SelectStyle(item, container, activeViewDefinition);
        //}

    
            private Style level200HorizontalStyle;
            private Style level250HorizontalStyle;
            private Style level300HorizontalStyle;
            private Style defaultHorizontalStyle;

            private Style level200VerticalStyle;
            private Style level250VerticalStyle;
            private Style level300VerticalStyle;
            private Style defaultVerticalStyle;

            public Style DefaultHorizontalStyle
            {
                get
                {
                    return this.defaultHorizontalStyle;
                }
                set
                {
                    this.defaultHorizontalStyle = value;
                }
            }

            public Style Level200HorizontalStyle
            {
                get
                {
                    return this.level200HorizontalStyle;
                }
                set
                {
                    this.level200HorizontalStyle = value;
                }
            }

            public Style Level250HorizontalStyle
            {
                get
                {
                    return this.level250HorizontalStyle;
                }
                set
                {
                    this.level250HorizontalStyle = value;
                }
            }

            public Style Level300HorizontalStyle
            {
                get
                {
                    return this.level300HorizontalStyle;
                }
                set
                {
                    this.level300HorizontalStyle = value;
                }
            }

            public Style Level200VerticalStyle
            {
                get
                {
                    return this.level200VerticalStyle;
                }
                set
                {
                    this.level200VerticalStyle = value;
                }
            }

            public Style Level250VerticalStyle
            {
                get
                {
                    return this.level250VerticalStyle;
                }
                set
                {
                    this.level250VerticalStyle = value;
                }
            }

            public Style Level300VerticalStyle
            {
                get
                {
                    return this.level300VerticalStyle;
                }
                set
                {
                    this.level300VerticalStyle = value;
                }
            }

            public Style DefaultVerticalStyle
            {
                get
                {
                    return this.defaultVerticalStyle;
                }
                set
                {
                    this.defaultVerticalStyle = value;
                }
            }

            public override Style SelectStyle(object item, DependencyObject container, ViewDefinitionBase activeViewDefinition)
            {
                IAppointment appointment = item as IAppointment;
                if (appointment == null)
                {
                    return base.SelectStyle(item, container, activeViewDefinition);
                }

                IResource resource = appointment.Resources.OfType<IResource>().FirstOrDefault((IResource r) => r.ResourceType == "Level");
                if (resource != null)
                {
                    if (activeViewDefinition.GetOrientation() == Orientation.Horizontal)
                    {
                        switch (resource.ResourceName)
                        {
                            case "200": return this.Level200HorizontalStyle;
                            case "250": return this.Level250HorizontalStyle;
                            case "300": return this.Level300HorizontalStyle;
                            default: break;
                        }
                    }
                    else
                    {
                        switch (resource.ResourceName)
                        {
                            case "200": return this.Level200VerticalStyle;
                            case "250": return this.Level250VerticalStyle;
                            case "300": return this.Level300VerticalStyle;
                            default: break;
                        }
                    }
                }
                else
                {
                    if (activeViewDefinition.GetOrientation() == Orientation.Horizontal)
                    {
                        return this.DefaultHorizontalStyle;
                    }
                    return this.DefaultVerticalStyle;
                }
                return base.SelectStyle(item, container, activeViewDefinition);
            }
        }
}