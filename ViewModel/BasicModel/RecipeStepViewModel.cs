using RecipeRecorder.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RecipeRecorder.ViewModel.BasicModel
{
    public class RecipeStepViewModel : INotifyPropertyChanged
    {
        public RecipeStepViewModel() {
            this._stepNum = "";
            this._description = AppResources.DescriptionHandsup;
            this.Image = new Uri("/Images/edit.png", UriKind.RelativeOrAbsolute);
            this._duration = AppResources.DurationHandsup;
        }

        public RecipeStepViewModel(string dest, string image, string duration)
        {
            this._description = dest;
            this._image = new Uri(image, UriKind.RelativeOrAbsolute);
            this._duration = duration;
        }

        public RecipeStepViewModel(string stepNum, string dest, string image, string duration) 
        {
            this._stepNum = stepNum;
            this._description = dest;
            this._image = new Uri(image, UriKind.RelativeOrAbsolute);
            this._duration = duration;
        }

        private string _stepNum;
        public string StepNum
        {
            get
            {
                return _stepNum;
            }
            set
            {
                if (value != _stepNum)
                {
                    _stepNum = value;
                    NotifyPropertyChanged("StepNum");
                }
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        private Uri _image;
        public Uri Image
        {
            get
            {
                return _image;
            }
            set
            {
                if (value != _image)
                {
                    _image = value;
                    NotifyPropertyChanged("Image");
                }
            }
        }

        private string _duration;
        public string Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    NotifyPropertyChanged("Duration");
                }
            }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
