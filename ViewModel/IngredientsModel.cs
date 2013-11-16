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

        public void AddIngrediantItem(string amount, string name)
        {
            this._ingredientItems.Add(new IngredientModel(amount, name));
        }

        public void DeleteIngrediantItem(IngredientModel obj)
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
