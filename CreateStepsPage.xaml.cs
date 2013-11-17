using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RecipeRecorder.Resources;
using RecipeRecorder.ViewModel.BasicModel;

namespace RecipeRecorder
{
    public partial class CreateStepsPage : PhoneApplicationPage
    {
        public CreateStepsPage()
        {
            InitializeComponent();
            DataContext = App.StepsViewModel;
        }

        private string RecipeName = "";
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string msg = "";
            if (NavigationContext.QueryString.TryGetValue("Rname", out msg))
            {
                this.RecipeName = msg;
                App.StepsViewModel.RecipeName = msg;
            }
        }

        private void DeleteStep_Click(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            RecipeStepViewModel obj = element.DataContext as RecipeStepViewModel;
            App.StepsViewModel.DeleteStepItem(obj);
            this.Resort_Step();
            this.Focus();
        }

        private void Resort_Step() {
            for (int i = 1; i < App.StepsViewModel.RecipeStepItems.Count+1; i++) {
                App.StepsViewModel.RecipeStepItems.ElementAt(i - 1).StepNum = "Step " + i;
            }
        }

        private void Empty_Step()
        {
            App.StepViewModel.Description = AppResources.DescriptionHandsup;
            App.StepViewModel.Duration = AppResources.DurationHandsup;
            App.StepViewModel.StepNum = "";
            App.StepViewModel.Image = new Uri("\\Images\\edit.png");
        }

        private void FinishIcon_Click(object sender, EventArgs e)
        { 
            
        }

        private void AddIcon_Click(object sender, EventArgs e)
        {
            string snum = "Step " + (App.StepsViewModel.RecipeStepItems.Count + 1);
            NavigationService.Navigate(new Uri("/CreateStepPage.xaml?Rname=" + this.RecipeName + "&SNum=" + snum, UriKind.Relative));
        }

        private void ExitIcon_Click(object sender, EventArgs e)
        { }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            RecipeStepViewModel obj = element.DataContext as RecipeStepViewModel;
            App.StepViewModel.Description=obj.Description;
            App.StepViewModel.Duration=obj.Duration;
            App.StepViewModel.Image=obj.Image;
            App.StepViewModel.StepNum=obj.StepNum;
            NavigationService.Navigate(new Uri("/CreateStepPage.xaml?Edit=1", UriKind.Relative));
        }

    }
}