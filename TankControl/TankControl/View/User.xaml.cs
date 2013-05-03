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
        private List<AuthenticationList> authenticationlist;

        public User()
        {
            InitializeComponent();
            /*For binding in radGridViewComboBox Column
             *For example if the data from database is 1 then the combobox column will display
             *administrator
             */
            authenticationlist = new List<AuthenticationList>()
            {
                new AuthenticationList(){AuthenticationLevel = 1, AuthenticationName = "Administrator"},
                new AuthenticationList(){AuthenticationLevel = 2, AuthenticationName = "Operator"}
            };
            Telerik.Windows.Controls.GridViewComboBoxColumn column = new Telerik.Windows.Controls.GridViewComboBoxColumn();
            this.userListGridView.Columns.Add(column);
            ((Telerik.Windows.Controls.GridViewComboBoxColumn)this.userListGridView.Columns["columnAuthLevel"]).ItemsSource = authenticationlist;

            /*Begin Query*/
            userlist = new ObservableCollection<TankControl.User>();
                using (TankControlEntities tce = new TankControlEntities())
                {
                    try{
                        var query = from a in tce.Users
                                    orderby a.auth_level ascending
                                    select a;
                        foreach (var user in query)
                        {
                            userlist.Add(new TankControl.User()
                            {
                                id = user.id,
                                name = user.name,
                                auth_level = user.auth_level,
                                username = user.username,
                                password = TankControl.Class.AES.DecryptAES(user.password)
                            });
                        }
                    }
                    catch (System.Data.EntityException)
                    {
                        MessageBox.Show("An error occured while retrieving data from database. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("An unknown error has occurred. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                    }

                    userListGridView.ItemsSource = userlist;
                }
            
            
        }

        private void userListGridView_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
        {
            var updatedRow = (e.NewData as TankControl.User);
            string error = "";
            if (e.EditAction == Telerik.Windows.Controls.GridView.GridViewEditAction.Cancel)
            {
                ///*action when the user canceled editing or adding item, based on its index*/
                this.userListGridView.Columns["columnDelete"].IsVisible = true; //show delete button
                this.userListGridView.Columns["columnDone"].IsVisible = false; //hide done button
                this.userListGridView.Columns["columnCancel"].IsVisible = false; //hide cancel button
                this.userListGridView.Columns["columnAuthLevel"].IsVisible = true; //hide authorization level column
                errorText.Content = "";
                return;
            }
            if (e.EditOperationType == Telerik.Windows.Controls.GridView.GridViewEditOperationType.Edit)
            {
                using (TankControlEntities tce = new TankControlEntities())
                {
                    try
                    {
                        var toUpdate = (from a in tce.Users
                                        where a.id == updatedRow.id
                                        select a).First();
                        toUpdate.name = updatedRow.name;
                        toUpdate.auth_level = updatedRow.auth_level;
                        //toUpdate.username = updatedRow.username;
                        toUpdate.password = TankControl.Class.AES.EncryptAES(updatedRow.password);
                        tce.SaveChanges();
                        errorText.Content = "";
                    }
                    catch (System.Data.EntityException)
                    {
                        //MessageBox.Show(ex.InnerException.Message.ToString());
                        MessageBox.Show("An error occured while performing update to the database. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(ex.InnerException.Message.ToString());
                        MessageBox.Show("An unknown error has occurred. Please contact technician","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                    }
                }
            }
            if (e.EditOperationType == Telerik.Windows.Controls.GridView.GridViewEditOperationType.Insert)
            {
                
                using (TankControlEntities tce = new TankControlEntities())
                {
                    
                    try
                    {
                        /*the value stored in temp are only for the purpose of the grid display
                         *by not storing the original updatedrow values, the password value
                         *that will be displayed would be in AES format after insertion,
                         *the grid needs to be refreshed/reloaded in order for the correct display 
                         *value to appear. Storing the value in temp then restoring the default values
                         *of updated row eliminate the need to refresh/reload the grid.
                         */
                        var temp = updatedRow.password;
                        updatedRow.auth_level = 2;
                        updatedRow.password = TankControl.Class.AES.EncryptAES(updatedRow.password);
                        tce.Users.Add(updatedRow);
                        tce.SaveChanges();
                        updatedRow.password = temp;
                        this.userListGridView.Columns["columnAuthLevel"].IsVisible = true; //hide column authorization
                        errorText.Content = "";
                    }
                    catch (System.Data.EntityException)
                    {
                        MessageBox.Show("An error occured while performing insert to the database. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                        {
                            error = "Username sudah diambil";
                            SystemError errorMessage = new SystemError();
                            errorMessage.errorLevel = 1;
                            errorMessage.errorDesc = error;
                            tce.SystemErrors.Add(errorMessage);
                            Application.Current.Shutdown();
                        }
                        else
                        {
                            MessageBox.Show("An unknown error has occurred. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            Application.Current.Shutdown();
                        }
                    }

                    
                }
            }

            this.userListGridView.Columns["columnDelete"].IsVisible = true; //show delete button
            this.userListGridView.Columns["columnDone"].IsVisible = false; //hide done button
            this.userListGridView.Columns["columnCancel"].IsVisible = false; //hide cancel button
            this.userListGridView.Columns["columnAuthLevel"].IsVisible = true; //hide column authorization
           
        }

        private void userListGridView_Deleted(object sender, Telerik.Windows.Controls.GridViewDeletedEventArgs e)
        {
            var deleted = (e.Items.First() as TankControl.User);
            using (TankControlEntities tce = new TankControlEntities())
            {
                try
                {
                    var query = (from a in tce.Users
                                 where a.id == deleted.id
                                 select a).First();

                    tce.Entry(query).State = System.Data.EntityState.Deleted;
                    tce.SaveChanges();
                    errorText.Content = "";
                }
                catch (System.Data.EntityException)
                {
                    MessageBox.Show("An error occured while performing delete to the database. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
                catch (Exception)
                {
                    //MessageBox.Show(ex.InnerException.Message.ToString());
                    MessageBox.Show("An unknown error has occurred. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
            }
        }

        private void userListGridView_BeginningEdit(object sender, Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs e)
        {
            ///*action when the user edit or add item, based on its index*/
            this.userListGridView.Columns["columnDelete"].IsVisible = false; //hide delete button
            this.userListGridView.Columns["columnDone"].IsVisible = true; //show done button
            this.userListGridView.Columns["columnCancel"].IsVisible = true; //show cancel button
        }


        private void userListGridView_CellValidating(object sender, Telerik.Windows.Controls.GridViewCellValidatingEventArgs e)
        {
            if (e.Cell.Column.UniqueName == "columnUsername")
            {
                using (TankControlEntities tce = new TankControlEntities())
                {
                    try
                    {
                        var query = (from a in tce.Users
                                     where a.username == e.NewValue
                                     select a).FirstOrDefault();
                        if (query != null)
                        {
                            errorText.Content = "Username already taken";
                            e.IsValid = false;
                        }
                    }
                    catch (System.Data.EntityException)
                    {
                        MessageBox.Show("An error occured while performing query to the database. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("An unknown error has occurred. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                    }

                }
            }

        }

        private void userListGridView_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            /* check if the current user is an administrator, if yes prevent deletion */
            var deletecheck = (e.Items.First() as TankControl.User);
            if (deletecheck.auth_level == 1)
            {
                e.Cancel = true;
                errorText.Content = "Cannot delete administrator";
            }
        }

        private void userListGridView_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            this.userListGridView.Columns["columnAuthLevel"].IsVisible = false; //hide authorization level column

        }
    }

    public class AuthenticationList
    {
        public int AuthenticationLevel { get; set; }
        public string AuthenticationName { get; set; }
    }
}
