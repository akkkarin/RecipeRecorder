using RecipeRecorder.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RecipeRecorder.ViewModel
{
    public class RecipesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<RecipeViewModel> _recipes;
        private LocalRecipeModel _localDB;

        public RecipesViewModel() 
        {
            this._recipes = new ObservableCollection<RecipeViewModel>();
        }

        public ObservableCollection<RecipeViewModel> Recipes
        {
            get
            {
                return _recipes;
            }
            set
            {
                if (value != _recipes)
                {
                    _recipes = value;
                    NotifyPropertyChanged("Recipes");
                }
            }
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
