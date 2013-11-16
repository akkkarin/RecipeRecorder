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
    public partial class CreateIngredientPage : PhoneApplicationPage
    {
        public CreateIngredientPage()
        {
            InitializeComponent();
            DataContext = App.IngredientViewModel;
        }

        private string RecipeName = "";
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string msg = "";
            if (NavigationContext.QueryString.TryGetValue("Rname", out msg))
            { 
                this.RecipeName = msg;
                App.IngredientViewModel.RecipeName = msg;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (AmountText.Text.Length > 0 && IngredientText.Text.Length > 0)
            {
                string amount = AmountText.Text;
                string name = IngredientText.Text;

                App.IngredientViewModel.AddIngredientItem(amount, name);

                this.Focus();
                AmountText.Text = "";
                IngredientText.Text = "";

            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            IngredientModel obj = element.DataContext as IngredientModel;
            App.IngredientViewModel.DeleteIngredientItem(obj);
            this.Focus();
        }

        private void ConfirmIcon_Click(object sender, EventArgs e)
        {
            string ingredients = App.IngredientViewModel.GetJsonContent();
            NavigationService.Navigate(new Uri("/CreateStepPage.xaml?Rname=" + this.RecipeName + "&Ingredients=" + ingredients, UriKind.Relative));
        }

        private void ExitIcon_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        } 

    }
}