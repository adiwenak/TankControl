using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace TankControl.View
{
    /// <summary>
    /// Interaction logic for Reporting.xaml
    /// </summary>
    public partial class ReportingView : UserControl
    {
        private ObservableCollection<TankControl.History> reportlist = new ObservableCollection<TankControl.History>();
        public ReportingView()
        {
            InitializeComponent();
        }

        private void selectReportDateButton_Click(object sender, RoutedEventArgs e)
        {
            var startdate = StartDate.SelectedDate;
            var enddate = EndDate.SelectedDate;

            if (startdate == null || enddate == null)
            {
                ErrorText.Content = "Fill Start Date/End Date";
            }
            else if (startdate > enddate)
            {
                ErrorText.Content = "Start Date must be earlier than End Date";
            }
            else if (enddate < startdate)
            {
                ErrorText.Content = "End Date must be later than Start Date";
            }
            else
            {
                reportGridView.Visibility=System.Windows.Visibility.Visible;
                reportDataPager.Visibility = System.Windows.Visibility.Visible;
                /*Begin Query */
                using (TankControlEntities tce = new TankControlEntities())
                {
                    try
                    {
                        var query = from a in tce.Histories
                                    where a.date >= startdate
                                    where a.date <= enddate
                                    select a;

                        reportlist.Clear();
                        foreach (var history in query)
                        {
                            reportlist.Add(new TankControl.History(history.recipe_id,history.recipe_name)
                            {
                                id = history.id,
                                date = history.date,
                                el1 = history.el1,
                                el2 = history.el2,
                                el3 = history.el3,
                                el4 = history.el4,
                                el5 = history.el5,
                                el6 = history.el6,
                                el7 = history.el7,
                                total = history.total,
                                duration_el1 = history.duration_el1,
                                duration_el2 = history.duration_el2,
                                duration_el3 = history.duration_el3,
                                duration_el4 = history.duration_el4,
                                duration_el5 = history.duration_el5,
                                duration_el6 = history.duration_el6,
                                duration_el7 = history.duration_el7
                            });

                        }
                    }
                    catch (System.Data.EntityException)
                    {
                        MessageBox.Show("An error occured while generating data from database. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("An unknown error has occurred. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                    }
                    reportGridView.ItemsSource = reportlist;
                }
            }
        }

        /*
        Prevent the selection of Start Date that is later than End Date
        */
        private void StartDate_ParseDateTimeValue(object sender, Telerik.Windows.Controls.ParseDateTimeEventArgs args)
        {
            EndDate.DisplayDateStart = StartDate.SelectedDate;
        }

        /*
         Prevent the selection of End Date that is earlier Start Date 
         */
        private void EndDate_ParseDateTimeValue(object sender, Telerik.Windows.Controls.ParseDateTimeEventArgs args)
        {
            StartDate.DisplayDateEnd = EndDate.SelectedDate;
        }
    }
}
