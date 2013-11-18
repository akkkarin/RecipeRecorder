using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeRecorder.ViewModel
{
    public class RecipeViewModel : INotifyPropertyChanged
    {
        private RecipeStepsViewModel _steps;
        private IngredientsViewModel _ingredients;

        public RecipeViewModel() 
        {
            this._ingredients = new IngredientsViewModel();
            this._steps = new RecipeStepsViewModel();
        }

        public RecipeStepsViewModel Steps
        {
            get
            {
                return _steps;
            }
            set
            {
                if (value != _steps)
                {
                    _steps = value;
                    NotifyPropertyChanged("Steps");
                }
            }
        }

        public IngredientsViewModel Ingredients
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
