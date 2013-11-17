using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RecipeRecorder.Resources;
using RecipeRecorder.ViewModel.BasicModel;

namespace RecipeRecorder.ViewModel
{
    public class RecipeStepsViewModel : INotifyPropertyChanged
    {
        public RecipeStepsViewModel()
        {
            this._recipeStepItems = new ObservableCollection<RecipeStepViewModel>();
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

        private ObservableCollection<RecipeStepViewModel> _recipeStepItems;
        public ObservableCollection<RecipeStepViewModel> RecipeStepItems
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
            this._recipeStepItems.Add(new RecipeStepViewModel(step, dest, image, duration));
        }

        public void AddStepItem(RecipeStepViewModel obj)
        {
            this._recipeStepItems.Add(obj);
        }

        public void EditStepItem(RecipeStepViewModel obj) 
        {
            string[] tmp = obj.StepNum.Split(' ');
            int index = Convert.ToInt32(tmp[1]) - 1;
            this._recipeStepItems.RemoveAt(index);
            this._recipeStepItems.Insert(index, obj);
        }

        public void DeleteStepItem(RecipeStepViewModel obj)
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
