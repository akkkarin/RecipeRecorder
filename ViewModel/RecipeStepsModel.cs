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
        
        private string _ingredients = "";
        public string Ingredients
        {
            get
            {
                return _ingredients;
            }
            set
            {
                if (value != _ingredients)
                {
                    _ingredients = value;
                    NotifyPropertyChanged("Ingredients");
                }
            }
        }

        private string _recipeName = "";
        public string RecipeName
        {
            get
            {
                return _recipeName;
            }
            set
            {
                if (value != _recipeName)
                {
                    _recipeName = value;
                    NotifyPropertyChanged("RecipeName");
                }
            }
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
        public void AddStepItem(string step, string dest, string image, string duration)
        {
            this._recipeStepItems.Add(new RecipeStepModel(step, dest, image, duration));
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
