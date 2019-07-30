using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace TestReservationsCalendar.Helpers
{
	public class OrientedGroupHeaderContentTemplateSelector : ScheduleViewDataTemplateSelector
	{
        public DataTemplate HorizontalTemplate { get; set; }

        public DataTemplate VerticalTemplate { get; set; }

        public DataTemplate HorizontalResourceTemplate { get; set; }

        public DataTemplate VerticalResourceTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container, ViewDefinitionBase activeViewDeifinition)
		{
			CollectionViewGroup cvg = item as CollectionViewGroup;
			if (cvg != null && cvg.Name is IResource)
			{
				if (activeViewDeifinition.GetOrientation() == Orientation.Vertical)
				{
					if (HorizontalResourceTemplate != null)
					{
						return HorizontalResourceTemplate;
					}
				}
				else
				{
					if (VerticalResourceTemplate != null)
					{
						return VerticalResourceTemplate;
					}
				}
			}

			if (cvg != null && cvg.Name is DateTime)
			{
				if (activeViewDeifinition.GetOrientation() == Orientation.Vertical)
				{
					if (HorizontalTemplate != null)
					{
						return HorizontalTemplate;
					}
				}
				else
				{
					if (VerticalTemplate != null)
					{
						return VerticalTemplate;
					}
				}
			}

			return base.SelectTemplate(item, container, activeViewDeifinition);
		}
	}
}
