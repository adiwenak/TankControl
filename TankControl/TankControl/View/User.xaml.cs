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
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : UserControl
    {
        private ObservableCollection<TankControl.User> userlist;
        public User()
        {
            InitializeComponent();
            userlist = new ObservableCollection<TankControl.User>();
            using (TankControlEntities tce = new TankControlEntities())
            {
                var query = from a in tce.Users
                            select a;

                foreach (var user in query)
                {
                    userlist.Add(new TankControl.User()
                    {
                        id = user.id,
                        name = user.name,
                        auth_level = user.auth_level,
                        username = user.username
                    });
                }

                userListGridView.ItemsSource = userlist;
            }
        }

        private void userListGridView_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
        {
            var updatedRow = (e.NewData as TankControl.User);
            if (e.EditAction == Telerik.Windows.Controls.GridView.GridViewEditAction.Cancel)
            {
                ///*action when the user canceled editing or adding item, based on its index*/
                this.userListGridView.Columns[0].IsVisible = true; //show delete button
                this.userListGridView.Columns[1].IsVisible = false; //hide done button
                this.userListGridView.Columns[2].IsVisible = false; //hide cancel button
                return;
            }
            if (e.EditOperationType == Telerik.Windows.Controls.GridView.GridViewEditOperationType.Edit)
            {
                using (TankControlEntities tce = new TankControlEntities())
                {
                    var toUpdate = (from a in tce.Users
                                    where a.id == updatedRow.id
                                    select a).First();
                    toUpdate.name = updatedRow.name;
                    toUpdate.auth_level = updatedRow.auth_level;
                    toUpdate.username = updatedRow.username;
                    tce.SaveChanges();
                }
            }
            if (e.EditOperationType == Telerik.Windows.Controls.GridView.GridViewEditOperationType.Insert)
            {
                using (TankControlEntities tce = new TankControlEntities())
                {
                    tce.Users.Add(updatedRow);
                    try
                    {
                        tce.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        
                    }

                    
                }
            }

            ///*action when the user has finished editing or adding item, based on its index*/
            this.userListGridView.Columns[0].IsVisible = true; //show delete button
            this.userListGridView.Columns[1].IsVisible = false; //hide done button
            this.userListGridView.Columns[2].IsVisible = false; //hide cancel button
        }

        private void userListGridView_Deleted(object sender, Telerik.Windows.Controls.GridViewDeletedEventArgs e)
        {
            var deleted = (e.Items.First() as TankControl.User);
            using (TankControlEntities tce = new TankControlEntities())
            {
                var query = (from a in tce.Users
                             where a.id == deleted.id
                             select a).First();

                tce.Entry(query).State = System.Data.EntityState.Deleted;
                tce.SaveChanges();
            }
        }

        private void userListGridView_BeginningEdit(object sender, Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs e)
        {
            ///*action when the user edit or add item, based on its index*/
            this.userListGridView.Columns[0].IsVisible = false; //hide delete button
            this.userListGridView.Columns[1].IsVisible = true; //show done button
            this.userListGridView.Columns[2].IsVisible = true; //show cancel button
        }

        private void userListGridView_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {

        }
    }
}
