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
using Microsoft.Phone.Tasks; 

namespace RecipeRecorder
{
    public partial class CreateStepPage : PhoneApplicationPage
    {
        private PhotoChooserTask photoChooserTask;
        private CameraCaptureTask cameraCaptureTask;
        public CreateStepPage()
        {
            InitializeComponent();
            DataContext = App.StepViewModel;
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed += new EventHandler<PhotoResult>(cameraCaptureTask_Completed);
        }

        private void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                
                string uri = e.OriginalFileName;
                MessageBoxResult result =
                MessageBox.Show(uri,
                "MessageBox Example", MessageBoxButton.OK);
                App.StepViewModel.Image = new Uri(uri, UriKind.RelativeOrAbsolute);
            }
        }

        private void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                MessageBox.Show(e.ChosenPhoto.Length.ToString());

                //Code to display the photo on the page in an image control named myImage.
                //System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                //bmp.SetSource(e.ChosenPhoto);
                //myImage.Source = bmp;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string msg = "";
            if (NavigationContext.QueryString.TryGetValue("SNum", out msg))
            { 
                App.StepViewModel.StepNum = msg;
            }
            this.Empty_Step(); 
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
        
        private bool Is_Null() {
            return (StepDescription.Text == AppResources.DescriptionHandsup || StepDuration.Text == AppResources.DurationHandsup);
        }

        private RecipeStepViewModel Clone() {
            RecipeStepViewModel temp = new RecipeStepViewModel();
            temp.Description = App.StepViewModel.Description;
            temp.Duration = App.StepViewModel.Duration;
            temp.Image = App.StepViewModel.Image;
            temp.StepNum = App.StepViewModel.StepNum;

            return temp;
        }

        private void FinishIcon_Click(object sender, EventArgs e)
        {
            this.Focus();
            if (!this.Is_Null())
            {
                RecipeStepViewModel temp = this.Clone(); 

                App.StepsViewModel.AddStepItem(temp);
                this.Empty_Step();
                NavigationService.GoBack();
            }
            else {
                MessageBoxResult result =
                MessageBox.Show("Please finish this step",
                "Warning", MessageBoxButton.OK);
            }
        }

        private void ExitIcon_Click(object sender, EventArgs e)
        {
            this.Empty_Step();
            NavigationService.GoBack();
        }

        private void Empty_Step() {
            App.StepViewModel.Description = AppResources.DescriptionHandsup;
            App.StepViewModel.Duration = AppResources.DurationHandsup;
            App.StepViewModel.StepNum = "";
            App.StepViewModel.Image = new Uri("/Images/edit.png", UriKind.RelativeOrAbsolute);
        } 
        
        private void StepImage_Click(object sender, RoutedEventArgs e)
        {
            IAsyncResult result = Microsoft.Xna.Framework.GamerServices.Guide.BeginShowMessageBox(
             "Image source",
             "Where do you want to get your image?",
             new string[] { "Camera", "Albums" },
             0,
             Microsoft.Xna.Framework.GamerServices.MessageBoxIcon.Alert,
             null,
             null);
            result.AsyncWaitHandle.WaitOne();

            int? choice = Microsoft.Xna.Framework.GamerServices.Guide.EndShowMessageBox(result);
            if (choice.HasValue)
            {
                if (choice.Value == 0)
                {
                    this.cameraCaptureTask.Show();
                }
                else 
                {
                    this.photoChooserTask.Show();
                }
            }
        }
    }
}