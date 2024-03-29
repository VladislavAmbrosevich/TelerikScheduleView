﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace TestReservationsCalendar
{
    public class OutlookSection : ViewModelBase
    {
        private object _selectedItem;

        public DelegateCommand Command { get; set; }

        public IEnumerable<object> Content { get; set; }

        public string Name { get; set; }

        public string IconPath { get; set; }

        public string MinimizedIconPath { get; set; }

        /// <summary>
        /// Gets or sets SelectedItem and notifies for changes
        /// </summary>
        public object SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(() => SelectedItem);
                }
            }
        }
    }
}