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
    public partial class Recipe : UserControl
    {
        private ObservableCollection<TankControl.Recipe> recipelist;
        public Recipe()
        {
            
            InitializeComponent();
            recipelist = new ObservableCollection<TankControl.Recipe>();
            using (TankControlEntities tce = new TankControlEntities())
            {

                var query = from a in tce.Recipes
                            select a;
                recipelist.Clear();
                foreach (var a in query)
                {
                    recipelist.Add(new TankControl.Recipe()
                    {
                        id = a.id,
                        name = a.name,
                        el1 = a.el1,
                        el2 = a.el2,
                        el3 = a.el3,
                        el4 = a.el4,
                        el5 = a.el5,
                        el6 = a.el6,
                        el7 = a.el7,
                        tol_el1 = a.tol_el1,
                        tol_el2 = a.tol_el2,
                        tol_el3 = a.tol_el3,
                        tol_el4 = a.tol_el4,
                        tol_el5 = a.tol_el5,
                        tol_el6 = a.tol_el6,
                        tol_el7 = a.tol_el7,
                        switch_el1 = a.switch_el1,
                        switch_el2 = a.switch_el2,
                        runtime = a.runtime
                    });
                }
                recipeListGridView.ItemsSource = recipelist;
            }

        }

        private void recipeListGridView_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
        {
            var updatedRow = (e.NewData as TankControl.Recipe);
            if (e.EditAction == Telerik.Windows.Controls.GridView.GridViewEditAction.Cancel)
            {
                this.recipeListGridView.Columns[1].IsVisible = false; //hide cancel button
                for (int i = 11; i < 20; i++)
                {
                    this.recipeListGridView.Columns[i].IsVisible = false;
                }
                return;
            }
            if (e.EditOperationType == Telerik.Windows.Controls.GridView.GridViewEditOperationType.Edit)
            {
                using (TankControlEntities tce = new TankControlEntities())
                {
                    var toUpdate = (from a in tce.Recipes
                                   where a.id == updatedRow.id
                                   select a).First();
                    toUpdate.el1 = updatedRow.el1;
                    toUpdate.el2 = updatedRow.el2;
                    toUpdate.el3 = updatedRow.el3;
                    toUpdate.el4 = updatedRow.el4;
                    toUpdate.el5 = updatedRow.el5;
                    toUpdate.el6 = updatedRow.el6;
                    toUpdate.el7 = updatedRow.el7;
                    tce.SaveChanges();
                }
            }
            if (e.EditOperationType == Telerik.Windows.Controls.GridView.GridViewEditOperationType.Insert)
            {
                using (TankControlEntities tce = new TankControlEntities())
                {
                    tce.Recipes.Add(updatedRow);
                    tce.SaveChanges();
                }
            }

            /*Hide Columns when the user has finished editing or adding item, based on its index*/
            this.recipeListGridView.Columns[1].IsVisible = false; //hide cancel button
            for (int i = 11; i < 20; i++)
            {
                this.recipeListGridView.Columns[i].IsVisible = false;
            }
        }

        private void recipeListGridView_Deleted(object sender, Telerik.Windows.Controls.GridViewDeletedEventArgs e)
        {
            var deleted = (e.Items.First() as TankControl.Recipe);
            using (TankControlEntities tce = new TankControlEntities())
            {
                var query = (from a in tce.Recipes
                             where a.id == deleted.id
                             select a).First();

                tce.Entry(query).State = System.Data.EntityState.Deleted;
                tce.SaveChanges();
            }
        }

        private void recipeListGridView_BeginningEdit(object sender, Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs e)
        {
            /*Show Columns when the user edit or add item, based on its index*/
            this.recipeListGridView.Columns[1].IsVisible = true; //show cancel button
            for (int i = 11; i < 20; i++)
            {
                this.recipeListGridView.Columns[i].IsVisible = true;
            }
        }

        private void recipeListGridView_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            var rowContent = (e.Row.DataContext as TankControl.Recipe);
            var elementLimit = Convert.ToSingle(rowContent.el1) + Convert.ToSingle(rowContent.el2) + Convert.ToSingle(rowContent.el3) + Convert.ToSingle(rowContent.el4) + Convert.ToSingle(rowContent.el5) + Convert.ToSingle(rowContent.el6) + Convert.ToSingle(rowContent.el7);
            if (elementLimit > 1000)
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "Element 1";
                validationResult.ErrorMessage = "The sum of element 1 to element 7 must not exceed 1000";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }

        }
    }
}
