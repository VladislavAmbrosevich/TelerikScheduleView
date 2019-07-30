using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;
using Telerik.Windows.Controls.ScheduleView;
using TestReservationsCalendar.BuildingLink.Data;
using TestReservationsCalendar.ViewModels;

namespace TestReservationsCalendar.BuildingLink.ViewModels
{
    public partial class ReservationsCalendarViewModel : ViewModelBase
    {
        //private ObservableCollection<OutlookSection> _outlookSections;
        //private OutlookSection _selectedOutlookSection;
        //private IOccurrence _selectedAppointment;
        private ObservableCollection<AmenityReservation> _appointments;
        //private int _activeViewDefinitionIndex;
        //private List<string> _selectedResourceNames;


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


        public ReservationsCalendarViewModel()
        {
            Appointments = GetCalendarAppointments();
        }


        private static ObservableCollection<AmenityReservation> GetCalendarAppointments()
        {
            //var firstDayOfThisWeek = DateTime.Now.Date.AddDays(-(int) DateTime.Now.Date.DayOfWeek);
            var appointments = new ObservableCollection<AmenityReservation>();

            var reservationsString = String.Empty;
            using (var sr = new StreamReader(@".\BuildingLink\Data\Database.json"))
            {
                reservationsString = sr.ReadToEnd();
            }
            var serializer = new JsonSerializer();
            var deserialized = serializer.Deserialize<AmenityReservation[]>(reservationsString).ToList();

            //deserialized.Select(a => appointments.Add(a));

            appointments.AddRange(deserialized);

            //    appointments.Add(new AmenityReservation
            //    {
            //        Start = DateTime.Parse("2019-06-25T00:00:00Z"),
            //        End = DateTime.Parse("2019-06-25T05:00:00Z"),
            //        Date = "6/25/19",
            //        StartTime = "12:00 AM",
            //        EndTime = "5:00 AM",
            //        AmenityFullName = "Test Amenity",
            //        SharedFromBuildingName = null,
            //        RequestedFor = "Apt. C67 - captain howdy",
            //        Status = "Requested",
            //        Comments = "",
            //        IsRecurrent = true
            //    });
            //appointments.Add(new AmenityReservation
            //{
            //    Start = DateTime.Parse("2019-06-25T00:00:00Z") ,
            //    End = DateTime.Parse("2019-06-26T00:00:00Z"),
            //    Date = "6/25/19 12:00 AM - 6/26/19 12:00 AM",
            //    StartTime = "All Day",
            //    EndTime = "All Day",
            //    AmenityFullName = "ytyy",
            //    SharedFromBuildingName = null,
            //    RequestedFor = "Unit SM-ZZ - Xxx000 Xxx000",
            //    Status = "Declined",
            //    Comments = "",
            //    IsRecurrent = true
            //});

            return appointments;

        }
        //public IOccurrence SelectedAppointment
            //{
            //    get => _selectedAppointment;

            //    set
            //    {
            //        if (_selectedAppointment != value)
            //        {
            //            _selectedAppointment = value;
            //            SetCategoryCommand.InvalidateCanExecute();
            //            SetTimeMarkerCommand.InvalidateCanExecute();
            //            OnPropertyChanged(() => SelectedAppointment);
            //        }
            //    }
            //}



            //public OutlookSection SelectedOutlookSection
            //{
            //    get
            //    {
            //        if (_selectedOutlookSection == null)
            //        {
            //            _selectedOutlookSection = OutlookSections.FirstOrDefault();
            //            _selectedOutlookSection.Command = new DelegateCommand(OnSelectedOutlookSectionCommandExecuted);
            //        }
            //        return _selectedOutlookSection;
            //    }

            //    set
            //    {
            //        if (_selectedOutlookSection != value)
            //        {
            //            _selectedOutlookSection = value;


            //            OnPropertyChanged(() => SelectedOutlookSection);
            //        }
            //    }
            //}

            /// <summary>
            /// Gets or sets OutlookSections and notifies for changes
            /// </summary>
            //public ObservableCollection<OutlookSection> OutlookSections
            //{
            //    get
            //    {
            //        if (_outlookSections == null)
            //        {
            //            _outlookSections = SampleContentService.GetOutlookSections();
            //        }

            //        return _outlookSections;
            //    }
            //}

            //private void InvalidateGotoViewDefinitionCommands()
            //{
            //    if (SetWeekViewCommand != null && SetWorkWeekCommand != null)
            //    {
            //        SetWeekViewCommand.InvalidateCanExecute();
            //        SetWorkWeekCommand.InvalidateCanExecute();
            //    }
            //}

            //private void UpdateGroupFilter()
            //{
            //    GroupFilter = new Func<object, bool>(GroupFilterFunc);
            //}

            //private bool GroupFilterFunc(object groupName)
            //{
            //    IResource resource = groupName as IResource;

            //    return resource == null ? true : _selectedResourceNames.Contains(resource.ResourceName, StringComparer.OrdinalIgnoreCase);
            //}

            //private Enums.PaneType GetPaneType(RadPane pane)
            //{
            //    return ConditionalDockingHelper.GetPaneType(pane);
            //}

            ///// <summary>
            ///// Determines if the Docking's Top, Bottom, Left and Right compasses should be shown for the Dragged Pane
            ///// </summary>
            //private bool CanDock(RadPane paneToDock, DockPosition position)
            //{
            //    var paneToDockType = GetPaneType(paneToDock);
            //    switch (paneToDockType)
            //    {
            //        case Enums.PaneType.Normal:
            //            return true;
            //        case Enums.PaneType.Restricted:
            //            return position != DockPosition.Center && position != DockPosition.Top && position != DockPosition.Bottom;
            //        default:
            //            return false;
            //    }
            //}

            //private bool CanDock(object dragged, DockPosition position)
            //{
            //    var splitContainer = dragged as RadSplitContainer;

            //    return !splitContainer.EnumeratePanes().Any(p => !CanDock(p, position));
            //}

            //private bool IsExistingGroup(string groupName)
            //{
            //    foreach (var group in GroupDescriptions)
            //    {
            //        if (group is ResourceGroupDescription)
            //        {
            //            if ((group as ResourceGroupDescription).ResourceType == groupName)
            //            {
            //                return true;
            //            }
            //        }
            //    }

            //    return false;
            //}

            //private static bool CompassNeedsToShow(Compass compass)
            //{
            //    return compass.IsLeftIndicatorVisible
            //        || compass.IsTopIndicatorVisible
            //        || compass.IsRightIndicatorVisible
            //        || compass.IsBottomIndicatorVisible
            //        || compass.IsCenterIndicatorVisible;
            //}
        }
}
