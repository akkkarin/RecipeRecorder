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
using System.Windows.Media;
using RecipeRecorder.Resources;

namespace RecipeRecorder
{
    public partial class CreateStepPage : PhoneApplicationPage
    {
        public CreateStepPage()
        {
            InitializeComponent();
            DataContext = App.StepViewModel;
        }
        
        private string RecipeName = "";

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string msg = "";
            if (NavigationContext.QueryString.TryGetValue("Rname", out msg))
            {
                this.RecipeName = msg;
                App.StepViewModel.RecipeName = msg;
            }
            if (NavigationContext.QueryString.TryGetValue("Ingredients", out msg))
            { 
                App.StepViewModel.Ingredients = msg; 
            }
            App.StepViewModel.AddStepItem("Step1","","","");
        }

        private void Text_GotFocus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == StepDuration)
            {
                if (StepDuration.Text.Equals(AppResources.DurationHandsup))
                {
                    StepDuration.Text = "";
                    SolidColorBrush Brush1 = new SolidColorBrush();
                    Brush1.Color = Colors.Red;
                    StepDuration.Foreground = Brush1;
                }
            }
            else {
                if (StepDescription.Text.Equals(AppResources.DescriptionHandsup))
                {
                    StepDescription.Text = "";
                    SolidColorBrush Brush1 = new SolidColorBrush();
                    Brush1.Color = Colors.Red;
                    StepDescription.Foreground = Brush1;
                }
                
            }
            
        }

        private void Text_LostFocus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == StepDuration)
            {
                if (StepDuration.Text == String.Empty)
                {
                    StepDuration.Text = AppResources.DurationHandsup;
                    SolidColorBrush Brush2 = new SolidColorBrush();
                    Brush2.Color = Colors.Gray;
                    StepDuration.Foreground = Brush2;
                }
            }
            else {
                if (StepDescription.Text == String.Empty)
                {
                    StepDescription.Text = AppResources.DescriptionHandsup;
                    SolidColorBrush Brush2 = new SolidColorBrush();
                    Brush2.Color = Colors.Gray;
                    StepDescription.Foreground = Brush2;
                }
                
            }
        }
         

    }
}