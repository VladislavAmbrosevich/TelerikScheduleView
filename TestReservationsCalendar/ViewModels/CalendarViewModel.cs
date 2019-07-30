using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;
using Telerik.Windows.Controls.ScheduleView;
using TestReservationsCalendar.ViewModels;

namespace TestReservationsCalendar
{
    public partial class CalendarViewModel : ViewModelBase
    {
        private ObservableCollection<OutlookSection> _outlookSections;
        private OutlookSection _selectedOutlookSection;
        private IOccurrence _selectedAppointment;
        private ObservableCollection<CustomAppointment> _appointments;
        private int _activeViewDefinitionIndex;
        private Func<object, bool> _groupFilter;
        private GroupDescriptionCollection _groupDescriptions;
        private List<string> _selectedResourceNames;

        /// <summary>
        /// Gets the GroupDescriptions
        /// </summary>
        public GroupDescriptionCollection GroupDescriptions
        {
            get
            {
                if (_groupDescriptions == null)
                {
                    ResourceGroupDescription resourceGroupDescription = new ResourceGroupDescription();
                    resourceGroupDescription.ResourceType = "CalendarType";
                    _groupDescriptions = new GroupDescriptionCollection() { resourceGroupDescription, new DateGroupDescription() };
                }

                return _groupDescriptions;
            }
        }

        /// <summary>
        /// Gets or sets GroupFilter and notifies for changes
        /// </summary>
        public Func<object, bool> GroupFilter
        {
            get => _groupFilter;

            set
            {

                _groupFilter = value;
                OnPropertyChanged(() => GroupFilter);
            }
        }

        /// <summary>
        /// Gets or sets Appointments and notifies for changes
        /// </summary>
        public ObservableCollection<CustomAppointment> Appointments
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

        /// <summary>
        /// Gets or sets SelectedAppointment and notifies for changes
        /// </summary>
        public IOccurrence SelectedAppointment
        {
            get => _selectedAppointment;

            set
            {
                if (_selectedAppointment != value)
                {
                    _selectedAppointment = value;
                    SetCategoryCommand.InvalidateCanExecute();
                    SetTimeMarkerCommand.InvalidateCanExecute();
                    OnPropertyChanged(() => SelectedAppointment);
                }
            }
        }

        /// <summary>
        /// Gets or sets ActiveViewDefinitionIndex and notifies for changes
        /// </summary>
        public int ActiveViewDefinitionIndex
        {
            get => _activeViewDefinitionIndex;

            set
            {
                if (_activeViewDefinitionIndex != value)
                {
                    _activeViewDefinitionIndex = value;
                    InvalidateGotoViewDefinitionCommands();
                    OnPropertyChanged(() => ActiveViewDefinitionIndex);
                }
            }
        }

        /// <summary>
        /// Gets or sets SelectedOutlookSection and notifies for changes
        /// </summary>
        public OutlookSection SelectedOutlookSection
        {
            get
            {
                if (_selectedOutlookSection == null)
                {
                    _selectedOutlookSection = OutlookSections.FirstOrDefault();
                    _selectedOutlookSection.Command = new DelegateCommand(OnSelectedOutlookSectionCommandExecuted);
                }
                return _selectedOutlookSection;
            }

            set
            {
                if (_selectedOutlookSection != value)
                {
                    _selectedOutlookSection = value;


                    OnPropertyChanged(() => SelectedOutlookSection);
                }
            }
        }

        /// <summary>
        /// Gets or sets OutlookSections and notifies for changes
        /// </summary>
        public ObservableCollection<OutlookSection> OutlookSections
        {
            get
            {
                if (_outlookSections == null)
                {
                    _outlookSections = SampleContentService.GetOutlookSections();
                }

                return _outlookSections;
            }
        }

        private void InvalidateGotoViewDefinitionCommands()
        {
            if (SetWeekViewCommand != null && SetWorkWeekCommand != null)
            {
                SetWeekViewCommand.InvalidateCanExecute();
                SetWorkWeekCommand.InvalidateCanExecute();
            }
        }

        private void UpdateGroupFilter()
        {
            GroupFilter = new Func<object, bool>(GroupFilterFunc);
        }

        private bool GroupFilterFunc(object groupName)
        {
            IResource resource = groupName as IResource;

            return resource == null ? true : _selectedResourceNames.Contains(resource.ResourceName, StringComparer.OrdinalIgnoreCase);
        }

        private Enums.PaneType GetPaneType(RadPane pane)
        {
            return ConditionalDockingHelper.GetPaneType(pane);
        }

        /// <summary>
        /// Determines if the Docking's Top, Bottom, Left and Right compasses should be shown for the Dragged Pane
        /// </summary>
        private bool CanDock(RadPane paneToDock, DockPosition position)
        {
            var paneToDockType = GetPaneType(paneToDock);
            switch (paneToDockType)
            {
                case Enums.PaneType.Normal:
                    return true;
                case Enums.PaneType.Restricted:
                    return position != DockPosition.Center && position != DockPosition.Top && position != DockPosition.Bottom;
                default:
                    return false;
            }
        }

        private bool CanDock(object dragged, DockPosition position)
        {
            var splitContainer = dragged as RadSplitContainer;

            return !splitContainer.EnumeratePanes().Any(p => !CanDock(p, position));
        }

        private bool IsExistingGroup(string groupName)
        {
            foreach (var group in GroupDescriptions)
            {
                if (group is ResourceGroupDescription)
                {
                    if ((group as ResourceGroupDescription).ResourceType == groupName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool CompassNeedsToShow(Compass compass)
        {
            return compass.IsLeftIndicatorVisible
                || compass.IsTopIndicatorVisible
                || compass.IsRightIndicatorVisible
                || compass.IsBottomIndicatorVisible
                || compass.IsCenterIndicatorVisible;
        }
    }
}
