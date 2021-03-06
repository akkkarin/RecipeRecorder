﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using RecipeRecorder.ViewModel.BasicModel;
using System.Windows.Media;
using RecipeRecorder.Resources;
using Microsoft.Phone.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging; 

namespace RecipeRecorder
{
    public partial class CreateStepPage : PhoneApplicationPage
    {
        private PhotoChooserTask photoChooserTask;
        private CameraCaptureTask cameraCaptureTask;
        bool isEdit;
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
                BitmapImage bmp = new  BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                App.StepViewModel.Image = bmp;  
            }
        }

        private void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                MessageBox.Show(e.ChosenPhoto.Length.ToString()); 
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                App.StepViewModel.Image = bmp;  
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string msg = "";

            if (NavigationContext.QueryString.TryGetValue("Edit", out msg) && msg.Equals("1"))
            {
                this.isEdit = true;
            }
            else 
            {
                this.isEdit = false;
            }
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
        
        private void Update_Focus(){
            object focusObj = FocusManager.GetFocusedElement();
            if (focusObj != null && focusObj is TextBox)
            {
                var binding = (focusObj as TextBox).GetBindingExpression(TextBox.TextProperty);
                binding.UpdateSource();
            }
        }
        private void debugger(string message)
        {
            MessageBoxResult result =
                    MessageBox.Show(message,
                    "Warning", MessageBoxButton.OK);
        }

        private void FinishIcon_Click(object sender, EventArgs e)
        {
            
            if (!this.Is_Null())
            {
                this.Update_Focus();
                RecipeStepViewModel temp = this.Clone();
      
                if (this.isEdit) 
                {
                    App.StepsViewModel.EditStepItem(temp);
                }
                else
                {
                    App.StepsViewModel.AddStepItem(temp);
                }
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
            App.StepViewModel.Image = new BitmapImage(new Uri("/Images/edit.png", UriKind.RelativeOrAbsolute));
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