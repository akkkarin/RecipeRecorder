using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace RecipeRecorder
{
    public partial class StartCreatingPage : PhoneApplicationPage
    {
        public StartCreatingPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (RecipiNameText.Text.Length > 0)
            {
                string Rname = RecipiNameText.Text;
                string Category = CategoryText.Text.Equals("") ? "PrivateKitchen" : CategoryText.Text;

                NavigationService.Navigate(new Uri("/CreateIngredientPage.xaml?Rname=" + Rname + "&Category=" + Category, UriKind.Relative));
            }
        }


    }
}