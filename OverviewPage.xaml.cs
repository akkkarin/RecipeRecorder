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
    public partial class OverviewPage : PhoneApplicationPage
    {
        public OverviewPage()
        {
            InitializeComponent();
            DataContext = App.RecipeViewModel;
            debugger(App.RecipeViewModel.Ingredients.RecipeName);
        }
        private void debugger(string message)
        {
            MessageBoxResult result =
                    MessageBox.Show(message,
                    "Warning", MessageBoxButton.OK);
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e); 
        }

        private void FinishIcon_Click(object sender, EventArgs e)
        {

        }

        private void BackIcon_Click(object sender, EventArgs e)
        {

        }

        private void ExitIcon_Click(object sender, EventArgs e)
        {

        }
    }
}