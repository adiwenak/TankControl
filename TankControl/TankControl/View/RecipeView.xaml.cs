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
    /// Interaction logic for Recipe.xaml
    /// </summary>
    public partial class RecipeView : UserControl
    {
        private ObservableCollection<TankControl.Recipe> recipelist;
        private int startIndex = 12; 
        private int endIndex = 21;
        public RecipeView()
        {
            
            InitializeComponent();
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
                            tol_el1 = recipe.tol_el1,
                            tol_el2 = recipe.tol_el2,
                            tol_el3 = recipe.tol_el3,
                            tol_el4 = recipe.tol_el4,
                            tol_el5 = recipe.tol_el5,
                            tol_el6 = recipe.tol_el6,
                            tol_el7 = recipe.tol_el7,
                            switch_el1 = recipe.switch_el1,
                            switch_el2 = recipe.switch_el2,
                            runtime = recipe.runtime
                        });
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.InnerException.Message.ToString());
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
                ///*action when the user canceled editing or adding item, based on its index*/
                this.recipeListGridView.Columns[0].IsVisible = true; //show delete button
                this.recipeListGridView.Columns[1].IsVisible = false; //hide done button
                this.recipeListGridView.Columns[2].IsVisible = false; //hide cancel button
                errorText.Content = "";
                //for (int i = startIndex; i < endIndex; i++)
                //{
                //    this.recipeListGridView.Columns[i].IsVisible = false;
                //}
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
                        tce.SaveChanges();
                        errorText.Content = "";
                    }
                    catch (System.Data.EntityException ex)
                    {
                        //MessageBox.Show(ex.InnerException.Message.ToString());
                        MessageBox.Show("An error occured while performing update to the database. Please contact technician");
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
                    catch (System.Data.EntityException ex)
                    {
                        //MessageBox.Show(ex.InnerException.Message.ToString());
                        MessageBox.Show("An error occured while performing insert to the database. Please contact technician");
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

            ///*action when the user has finished editing or adding item, based on its index*/
            this.recipeListGridView.Columns[0].IsVisible = true; //show delete button
            this.recipeListGridView.Columns[1].IsVisible = false; //hide done button
            this.recipeListGridView.Columns[2].IsVisible = false; //hide cancel button
            //for (int i = startIndex; i < endIndex; i++)
            //{
            //    this.recipeListGridView.Columns[i].IsVisible = false;
            //}
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
                catch (System.Data.EntityException ex)
                {
                    //MessageBox.Show(ex.InnerException.Message.ToString());
                    MessageBox.Show("An error occured while performing delete to the database. Please contact technician");
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

        private void recipeListGridView_BeginningEdit(object sender, Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs e)
        {
            ///*action when the user edit or add item, based on its index*/
            this.recipeListGridView.Columns[0].IsVisible = false; //hide delete button
            this.recipeListGridView.Columns[1].IsVisible = true; //show done button
            this.recipeListGridView.Columns[2].IsVisible = true; //show cancel button
            for (int i = startIndex; i < endIndex; i++)
            {
                this.recipeListGridView.Columns[i].IsVisible = true;
            }
        }

        private void recipeListGridView_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            var rowContent = (e.Row.DataContext as TankControl.Recipe);
            var elementSum = 
                Convert.ToSingle(rowContent.el1) + 
                Convert.ToSingle(rowContent.el2) + 
                Convert.ToSingle(rowContent.el3) + 
                Convert.ToSingle(rowContent.el4) + 
                Convert.ToSingle(rowContent.el5) + 
                Convert.ToSingle(rowContent.el6) + 
                Convert.ToSingle(rowContent.el7);
            var elementLimit = 1000;
            //if (Convert.ToString(rowContent) == string.Empty)
            //{
            //    Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
            //    validationResult.PropertyName = "Error";
            //    validationResult.ErrorMessage = "At least one field must be filled";
            //    e.ValidationResults.Add(validationResult);
            //    e.IsValid = false;
            //}
            if (elementSum > elementLimit)
            {
                //Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                //validationResult.PropertyName = "Error";
                //validationResult.ErrorMessage = "The sum of element 1 to element 7 must not exceed 1000";
                //e.ValidationResults.Add(validationResult);
                errorText.Content = "The sum of element 1 to element 7 must not exceed " + elementLimit;
                e.IsValid = false;
            }
            
        }
    }
}
