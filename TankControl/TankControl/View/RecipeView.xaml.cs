using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace TankControl.View
{
    /// <summary>
    /// Interaction logic for Recipe.xaml
    /// </summary>
    public partial class RecipeView : UserControl
    {
        private ObservableCollection<TankControl.Recipe> recipelist;
        private double elementLimit;
        public RecipeView()
        {
            InitializeComponent();
            elementLimit = TankControl.Properties.Settings.Default.Limit;
            recipelist = new ObservableCollection<TankControl.Recipe>();
            using (TankControlEntities tce = new TankControlEntities())
            {
                try
                {
                    var query = from a in tce.Recipes
                                select a;
                    recipelist.Clear();
                    foreach (var recipe in query)
                    {
                        recipelist.Add(new TankControl.Recipe()
                        {
                            id = recipe.id,
                            name = recipe.name,
                            el1 = recipe.el1,
                            el2 = recipe.el2,
                            el3 = recipe.el3,
                            el4 = recipe.el4,
                            el5 = recipe.el5,
                            el6 = recipe.el6,
                            el7 = recipe.el7,
                            //tol_el1 = recipe.tol_el1,
                            //tol_el2 = recipe.tol_el2,
                            //tol_el3 = recipe.tol_el3,
                            //tol_el4 = recipe.tol_el4,
                            //tol_el5 = recipe.tol_el5,
                            //tol_el6 = recipe.tol_el6,
                            //tol_el7 = recipe.tol_el7,
                            switch_el1 = recipe.switch_el1,
                            switch_el2 = recipe.switch_el2,
                            runtime = recipe.runtime
                        });
                    }
                }
                catch (System.Data.EntityException)
                {
                    MessageBox.Show("An error occured while performing update to the database. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
                catch (Exception)
                {
                    MessageBox.Show("An unknown error has occurred. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
                recipeListGridView.ItemsSource = recipelist;
            }

        }

        private void recipeListGridView_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
        {
            var updatedRow = (e.NewData as TankControl.Recipe);
            if (e.EditAction == Telerik.Windows.Controls.GridView.GridViewEditAction.Cancel)
            {
                /*action when the user canceled editing or adding item, based on its index*/
                this.recipeListGridView.Columns["columnDelete"].IsVisible = true; //show delete button
                this.recipeListGridView.Columns["columnDone"].IsVisible = false; //hide done button
                this.recipeListGridView.Columns["columnCancel"].IsVisible = false; //hide cancel button
                errorText.Content = "";
                return;
            }
            if (e.EditOperationType == Telerik.Windows.Controls.GridView.GridViewEditOperationType.Edit)
            {

                using (TankControlEntities tce = new TankControlEntities())
                {
                    try
                    {
                        var toUpdate = (from a in tce.Recipes
                                        where a.id == updatedRow.id
                                        select a).FirstOrDefault();
                        toUpdate.el1 = updatedRow.el1;
                        toUpdate.el2 = updatedRow.el2;
                        toUpdate.el3 = updatedRow.el3;
                        toUpdate.el4 = updatedRow.el4;
                        toUpdate.el5 = updatedRow.el5;
                        toUpdate.el6 = updatedRow.el6;
                        toUpdate.el7 = updatedRow.el7;
                        toUpdate.name = updatedRow.name;
                        toUpdate.switch_el1 = updatedRow.switch_el1;
                        toUpdate.switch_el2 = updatedRow.switch_el2;
                        toUpdate.runtime = updatedRow.runtime;
                        //toUpdate.tol_el1 = updatedRow.tol_el1;
                        //toUpdate.tol_el2 = updatedRow.tol_el2;
                        //toUpdate.tol_el3 = updatedRow.tol_el3;
                        //toUpdate.tol_el4 = updatedRow.tol_el4;
                        //toUpdate.tol_el5 = updatedRow.tol_el5;
                        //toUpdate.tol_el6 = updatedRow.tol_el6;
                        //toUpdate.tol_el7 = updatedRow.tol_el7;
                        tce.SaveChanges();
                        if (updatedRow is TankControl.Recipe)
                        {
                            if (checkElementLimit(updatedRow) == false)
                            {
                                e.Row.Background = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                e.Row.Background = new SolidColorBrush(Colors.White);
                            }
                        }
                        errorText.Content = "";
                    }
                    catch (System.Data.EntityException)
                    {
                        MessageBox.Show("An error occured while performing update to the database. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("An unknown error has occurred. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        tce.Recipes.Add(updatedRow);
                        tce.SaveChanges();
                        errorText.Content = "";
                    }
                    catch (System.Data.EntityException)
                    {
                        MessageBox.Show("An error occured while performing insert to the database. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("An unknown error has occurred. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                    }
                }
            }

            /*action when the user has finished editing or adding item, based on its index*/
            this.recipeListGridView.Columns["columnDelete"].IsVisible = true; //show delete button
            this.recipeListGridView.Columns["columnDone"].IsVisible = false; //hide done button
            this.recipeListGridView.Columns["columnCancel"].IsVisible = false; //hide cancel button
        }

        private void recipeListGridView_Deleted(object sender, Telerik.Windows.Controls.GridViewDeletedEventArgs e)
        {
            var deleted = (e.Items.First() as TankControl.Recipe);
            using (TankControlEntities tce = new TankControlEntities())
            {
                try
                {
                    var query = (from a in tce.Recipes
                                 where a.id == deleted.id
                                 select a).First();

                    tce.Entry(query).State = System.Data.EntityState.Deleted;
                    tce.SaveChanges();
                    errorText.Content = "";
                }
                catch (System.Data.EntityException)
                {
                    MessageBox.Show("An error occured while performing delete to the database. Please contact technician","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unknown error has occurred. Please contact technician" + ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
            }
        }

        private void recipeListGridView_BeginningEdit(object sender, Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs e)
        {
            ///*action when the user edit or add item, based on its index*/
            this.recipeListGridView.Columns["columnDelete"].IsVisible = false; //hide delete button
            this.recipeListGridView.Columns["columnDone"].IsVisible = true; //show done button
            this.recipeListGridView.Columns["columnCancel"].IsVisible = true; //show cancel button
        }

        // For use in validating element limit
        private void recipeListGridView_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            var rowContent = (e.Row.DataContext as TankControl.Recipe);
            if (checkElementLimit(rowContent) == false)
            {
                errorText.Content = "The sum of element 1 to element 7 must not exceed " + elementLimit;
                e.IsValid = false;
            }
            
        }

        private void recipeListGridView_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            
            var rowContent = (e.DataElement) as TankControl.Recipe;
            if (rowContent is TankControl.Recipe)
            {
               if (checkElementLimit(rowContent) == false)
               {
                   e.Row.Background = new SolidColorBrush(Colors.Red);
               }
                
            }
        }
    
        private bool checkElementLimit(TankControl.Recipe recipeElement)
        {
            var elementSum =
               Convert.ToSingle(recipeElement.el1) +
               Convert.ToSingle(recipeElement.el2) +
               Convert.ToSingle(recipeElement.el3) +
               Convert.ToSingle(recipeElement.el4) +
               Convert.ToSingle(recipeElement.el5) +
               Convert.ToSingle(recipeElement.el6) +
               Convert.ToSingle(recipeElement.el7);
            if (elementSum > elementLimit)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
