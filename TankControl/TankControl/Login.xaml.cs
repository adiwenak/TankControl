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
using System.Windows.Shapes;
using System.Security.Cryptography;

namespace TankControl
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            string user = username.Text;
            string pass =TankControl.Class.AES.EncryptAES(password.Password);

            using (TankControlEntities tce = new TankControlEntities())
            {
                try
                {
                    var query = (from a in tce.Users
                                 where a.username == user
                                 where a.password == pass
                                 select a).FirstOrDefault();
                    if (query == null)
                    {
                        errorText.Content = "Invalid username/password";
                    }
                    else if (query != null)
                    {
                        Application.Current.Properties["userAuthenticationLevel"] = query.auth_level; //set the current user authentication level to a "global" property/variable, so that other parts of the application can access it.
                        TankControl.MainWindow openwindow = new TankControl.MainWindow();
                        openwindow.Show();
                        this.Close();
                    }
                    else
                    {
                        errorText.Content = "An unknown error has occurred. Please contact technician";
                    }
                }
                catch (System.Data.EntityException ex)
                {
                    //MessageBox.Show(ex.InnerException.Message.ToString());
                    MessageBox.Show("An error occured while logging in to the aplication. Please contact technician");
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.InnerException.Message.ToString());
                    MessageBox.Show("An unknown error has occurred. Please contact technician");
                    Application.Current.Shutdown();
                }
            }

        }
    }
}
