using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RecipeRecorder.Resources;
using RecipeRecorder.ViewModel.BasicModel;

namespace RecipeRecorder.ViewModel
{
    public class IngredientsModel : INotifyPropertyChanged
    {
        public IngredientsModel()
        {
            this._ingredientItems = new ObservableCollection<IngredientModel>();
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

        private ObservableCollection<IngredientModel> _ingredientItems;
        public ObservableCollection<IngredientModel> IngredientItems
        {
            get { return _ingredientItems; }
            set
            {
                _ingredientItems = value;
                NotifyPropertyChanged("IngredientItems");
            }
        }

        public void AddIngredientItem(string amount, string name)
        {
            this._ingredientItems.Add(new IngredientModel(amount, name));
        }

        public void DeleteIngredientItem(IngredientModel obj)
        {
            this._ingredientItems.Remove(obj);
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
