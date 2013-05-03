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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TankControl.Class;
using System.Collections.ObjectModel;

namespace TankControl.View
{
    /// <summary>
    /// Interaction logic for ControlArea.xaml
    /// </summary>
    /// 

    public partial class ControlArea : UserControl
    {
        private ObservableCollection<TankControl.Recipe> recipelist;
        public ControlArea()
        {
            InitializeComponent();
            using (TankControlEntities tce = new TankControlEntities())
            {
                var query = from a in tce.Recipes
                            select new { a.id, a.name };

                if (this.recipelist == null)
                {
                    this.recipelist = new ObservableCollection<TankControl.Recipe>();
                }
                foreach (var recipe in query)
                {
                    recipelist.Add(new TankControl.Recipe() {id=recipe.id, name=recipe.name });
                    //DropdownRecipe.Items.Add(new List<object> { new { id = recipe.id, name = recipe.name } });
                    //DropdownRecipe.Items.Add(new RecipeDD(recipe.name,recipe.id));
                }

                DropdownRecipe.ItemsSource = recipelist;
            }
            //ListReceipe();
            StopProcess.IsEnabled = false;
        }

        public void Stop_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(DropdownRecipe.SelectedValue);
            Process.Singleton.FillupStop();
            StartProcess.IsEnabled = true;
            StopProcess.IsEnabled = false;

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Process.Singleton.FillupRun();
            StartProcess.IsEnabled = false;
            StopProcess.IsEnabled = true;
        }

        private void ListReceipe() {
            //for (int i = 1; i < 5; i++)
            //{
            //    TextBlock tb = new TextBlock();
            //    tb.Text = "Receipe "+i;
            //    tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            //    tb.Margin = new Thickness(5, 3, 0, 3);

            //    StackPanel sp = new StackPanel();
            //    sp.Orientation = Orientation.Horizontal;
            //    sp.Children.Add(tb);

            //    ListBoxItem lb = new ListBoxItem();
            //    lb.Content = sp;
            //    RecipeList.Items.Add(lb);
            //}
        }

        private void DropdownRecipe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var recipeId = DropdownRecipe.SelectedValue;

        }
    }
}
