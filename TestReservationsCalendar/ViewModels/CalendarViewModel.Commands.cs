using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;
using Telerik.Windows.Controls.RichTextBoxUI;
using Telerik.Windows.Controls.ScheduleView;
using TestReservationsCalendar.ViewModels;

namespace TestReservationsCalendar
{
    public partial class CalendarViewModel
    {
        public DelegateCommand PreviewShowCompassCommand { get; private set; }
        public DelegateCommand OpenDialogCommand { get; private set; }
        public DelegateCommand SetCategoryCommand { get; private set; }
        public DelegateCommand SetTimeMarkerCommand { get; private set; }
        public DelegateCommand SetTodayCommand { get; private set; }
        public DelegateCommand SetWorkWeekCommand { get; private set; }
        public DelegateCommand SetWeekViewCommand { get; private set; }
        public DelegateCommand DiscardCommand { get; set; }
        public DelegateCommand MenuOpenStateChangedCommand { get; private set; }
        public DelegateCommand SelectCalendarCommand { get; private set; }

        public CalendarViewModel()
        {
            InitializeProperties();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            PreviewShowCompassCommand = new DelegateCommand(OnPreviewShowCompassCommandExecuted);
            SelectCalendarCommand = new DelegateCommand(OnSelectedCalendarCommandExecuted);
            OpenDialogCommand = new DelegateCommand(OnOpenDialogCommandExecuted);
            SetCategoryCommand = new DelegateCommand(OnSetCategoryCommandExecuted, o => SelectedAppointment != null);
            SetTimeMarkerCommand = new DelegateCommand(OnSetTimeMarkerCommandExecuted, o => SelectedAppointment != null);
            SetTodayCommand = new DelegateCommand(OnSetTodayCommandExecuted);
            SetWorkWeekCommand = new DelegateCommand(OnSetWorkWeekCommandExecuted, o => ActiveViewDefinitionIndex != 2);
            SetWeekViewCommand = new DelegateCommand(OnSetWeekViewCommandExecuted, o => ActiveViewDefinitionIndex != 1);
            DiscardCommand = new DelegateCommand(OnDiscardCommandExecute);
            MenuOpenStateChangedCommand = new DelegateCommand(OnMenuOpenStateChangedCommandExecuted);
        }

        private void InitializeProperties()
        {
            _selectedResourceNames = new List<string>();
            ActiveViewDefinitionIndex = 3;
            Appointments = SampleContentService.GetCalendarAppointments();
            UpdateGroupFilter();
        }


        private void OnSelectedCalendarCommandExecuted(object parameter)
        {
            var args = parameter as SelectionChangedEventArgs;
            var radListBoxControl = args.OriginalSource as RadListBox;
            if (radListBoxControl != null)
            {
                var selectedItems = radListBoxControl.SelectedItems.Cast<CheckBoxItem>().ToList();
                _selectedResourceNames.Clear();
                foreach (var item in selectedItems)
                {
                    _selectedResourceNames.Add(item.Text);
                }
            }

            UpdateGroupFilter();
        }

        private void OnPreviewShowCompassCommandExecuted(object param)
        {
            var args = (PreviewShowCompassEventArgs)param;
            if (args.TargetGroup != null)
            {
                args.Compass.IsCenterIndicatorVisible = false;
                args.Compass.IsLeftIndicatorVisible = false;
                args.Compass.IsTopIndicatorVisible = false;
                args.Compass.IsRightIndicatorVisible = false;
                args.Compass.IsBottomIndicatorVisible = false;
            }
            else
            {
                args.Compass.IsCenterIndicatorVisible = CanDock(args.DraggedElement, DockPosition.Center);
                args.Compass.IsLeftIndicatorVisible = CanDock(args.DraggedElement, DockPosition.Left);
                args.Compass.IsTopIndicatorVisible = CanDock(args.DraggedElement, DockPosition.Top);
                args.Compass.IsRightIndicatorVisible = CanDock(args.DraggedElement, DockPosition.Right);
                args.Compass.IsBottomIndicatorVisible = CanDock(args.DraggedElement, DockPosition.Bottom);
            }

            args.Canceled = !(CompassNeedsToShow(args.Compass));
        }

        private void OnOpenDialogCommandExecuted(object obj)
        {
            RadWindow.Alert(
                new DialogParameters
                {
                    Content = string.Format("{0}'s command executed.", obj.ToString()),
                    Header = "Telerik"
                });
        }

        private void OnSelectedOutlookSectionCommandExecuted(object obj)
        {
            var args = obj as SelectionChangedEventArgs;
            var radListBoxControl = args.OriginalSource as RadListBox;
            if (radListBoxControl != null)
            {
                var selectedItems = radListBoxControl.SelectedItems.Cast<CheckBoxItem>().ToList();
                _selectedResourceNames.Clear();
                foreach (var item in selectedItems)
                {
                    _selectedResourceNames.Add(item.Text);
                }
            }

            UpdateGroupFilter();
        }


        public void OnSetCategoryCommandExecuted(object parameter)
        {
            CustomAppointment appointment = SelectedAppointment as CustomAppointment;
            Category newCategory = parameter as Category;
            IExceptionOccurrence exceptionToEdit = null;
            if (!(SelectedAppointment is Appointment))
            {
                appointment = (SelectedAppointment as Occurrence).Master as CustomAppointment;
                if (appointment.RecurrenceRule != null)
                {
                    exceptionToEdit = appointment.RecurrenceRule.Exceptions.SingleOrDefault(e => (e.Appointment as IOccurrence) == ((Occurrence)(SelectedAppointment)).Appointment);
                    if (exceptionToEdit != null)
                    {
                        appointment.RecurrenceRule.Exceptions.Remove(exceptionToEdit);
                        (exceptionToEdit.Appointment as CustomAppointment).Category = newCategory;
                        appointment.RecurrenceRule.Exceptions.Add(exceptionToEdit);
                    }
                }
            }

            CustomAppointment appointmentToEdit = (from app in Appointments where app.Equals(appointment) select app).FirstOrDefault();
            if (exceptionToEdit == null)
            {
                appointmentToEdit.Category = newCategory;
            }

            var index = Appointments.IndexOf(appointmentToEdit);
            Appointments.Remove(appointmentToEdit);
            Appointments.Insert(index, appointmentToEdit);
        }

        public void OnSetTimeMarkerCommandExecuted(object parameter)
        {
            CustomAppointment appointment = SelectedAppointment as CustomAppointment;
            TimeMarker newTimeMarker = parameter as TimeMarker;
            IExceptionOccurrence exceptionToEdit = null;
            if (!(SelectedAppointment is Appointment))
            {
                appointment = (SelectedAppointment as Occurrence).Master as CustomAppointment;
                if (appointment.RecurrenceRule != null)
                {
                    exceptionToEdit = appointment.RecurrenceRule.Exceptions.SingleOrDefault(e => (e.Appointment as IOccurrence) == ((Occurrence)(SelectedAppointment)).Appointment);
                    if (exceptionToEdit != null)
                    {
                        appointment.RecurrenceRule.Exceptions.Remove(exceptionToEdit);
                        (exceptionToEdit.Appointment as CustomAppointment).TimeMarker = newTimeMarker;
                        appointment.RecurrenceRule.Exceptions.Add(exceptionToEdit);
                    }
                }
            }

            CustomAppointment appointmentToEdit = (from app in Appointments where app.Equals(appointment) select app).FirstOrDefault();
            if (exceptionToEdit == null)
            {
                appointmentToEdit.TimeMarker = newTimeMarker;
            }

            var index = Appointments.IndexOf(appointmentToEdit);
            Appointments.Remove(appointmentToEdit);
            Appointments.Insert(index, appointmentToEdit);
        }

        public void OnSetTodayCommandExecuted(object parameter)
        {
            SelectedOutlookSection.SelectedItem = DateTime.Today;
        }

        public void OnSetWorkWeekCommandExecuted(object parameter)
        {
            ActiveViewDefinitionIndex = 2;
        }

        public void OnSetWeekViewCommandExecuted(object parameter)
        {
            ActiveViewDefinitionIndex = 1;
        }

        private void OnMenuOpenStateChangedCommandExecuted(object param)
        {
            var ribbon = param as RadRichTextBoxRibbonUI;
            if (ribbon.IsApplicationMenuOpen)
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
                {
                    DiscardCommand.Execute(null);
                }));
            }
        }

        private void OnDiscardCommandExecute(object obj)
        {
            // Execute any actions that should be triggered when the RadRichTextBoxRibbonUI's ApplicationMenuOpen is opened.
        }

    }
}
