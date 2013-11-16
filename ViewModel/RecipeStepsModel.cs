using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RecipeRecorder.Resources;
using RecipeRecorder.ViewModel.BasicModel;

namespace RecipeRecorder.ViewModel
{
    public class RecipeStepsModel : INotifyPropertyChanged
    {
        public RecipeStepsModel()
        {
            this._recipeStepItems = new ObservableCollection<RecipeStepModel>();
        }
        private ObservableCollection<RecipeStepModel> _recipeStepItems;
        public ObservableCollection<RecipeStepModel> RecipeStepItems
        {
            get { return _recipeStepItems; }
            set
            {
                _recipeStepItems = value;
                NotifyPropertyChanged("RecipeStepItems");
            }
        }
        public void AddStepItem(string dest, string image, string duration)
        {
            this._recipeStepItems.Add(new RecipeStepModel(dest, image, duration));
        }

        public void DeleteStepItem(RecipeStepModel obj)
        {
            this._recipeStepItems.Remove(obj);
        }

        #region PropertyChangedEventHandler
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
