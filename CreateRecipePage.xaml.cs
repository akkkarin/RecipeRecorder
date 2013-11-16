using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RecipeRecorder.ViewModel;
using RecipeRecorder.ViewModel.BasicModel;

namespace RecipeRecorder
{
    public partial class CreateRecipePage : PhoneApplicationPage
    {
        public CreateRecipePage()
        {
            InitializeComponent();
            DataContext = App.IngredientViewModel;
        }

        private string RecipeName = "";
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string msg = "";
            if (NavigationContext.QueryString.TryGetValue("rname", out msg))
                this.RecipeName = msg;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (AmountText.Text.Length > 0 && IngrediantText.Text.Length > 0)
            {
                string amount = AmountText.Text;
                string name = IngrediantText.Text;

                App.IngredientViewModel.AddIngrediantItem(amount, name);

                this.Focus();
                AmountText.Text = "";
                IngrediantText.Text = "";

            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            IngredientModel obj = element.DataContext as IngredientModel;
            App.IngredientViewModel.DeleteIngrediantItem(obj);
            this.Focus();
        }
        private void Confrim_Click(object sender, RoutedEventArgs e)
        {
            //hihihihihihihihihi
        }
        private void Quit_Click(object sender, RoutedEventArgs e) { }

    }
}