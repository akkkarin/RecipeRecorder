using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RecipeRecorder.Resources;
using RecipeRecorder.ViewModel.BasicModel;
using Newtonsoft.Json;

namespace RecipeRecorder.ViewModel
{
    public class IngredientsViewModel : INotifyPropertyChanged
    {
        public IngredientsViewModel()
        {
            this._ingredientItems = new ObservableCollection<IngredientViewModel>();
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

        private ObservableCollection<IngredientViewModel> _ingredientItems;
        public ObservableCollection<IngredientViewModel> IngredientItems
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
            this._ingredientItems.Add(new IngredientViewModel(amount, name));
        }

        public void DeleteIngredientItem(IngredientViewModel obj)
        {
            this._ingredientItems.Remove(obj);
        }

        public string GetJsonContent() {
            string jsonContents = JsonConvert.SerializeObject(this._ingredientItems);
            return jsonContents;
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
