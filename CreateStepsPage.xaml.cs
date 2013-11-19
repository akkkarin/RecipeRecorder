using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using RecipeRecorder.Resources;
using RecipeRecorder.ViewModel.BasicModel;
using System.Windows.Media.Imaging;

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
            this.Focus();
        } 

        private void Empty_Step()
        {
            App.StepViewModel.Description = AppResources.DescriptionHandsup;
            App.StepViewModel.Duration = AppResources.DurationHandsup;
            App.StepViewModel.StepNum = "";
            App.StepViewModel.Image = new BitmapImage(new Uri("/Images/edit.png", UriKind.RelativeOrAbsolute));
        }

        private void FinishIcon_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/OverviewPage.xaml", UriKind.Relative));
        }

        private void AddIcon_Click(object sender, EventArgs e)
        {
            this.Empty_Step(); 
            App.StepViewModel.StepNum = "Step " + (App.StepsViewModel.RecipeStepItems.Count + 1); 
            NavigationService.Navigate(new Uri("/CreateStepPage.xaml", UriKind.Relative));
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
            this.Focus();
        }

    }
}