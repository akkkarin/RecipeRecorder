using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RecipeRecorder.Resources;
using RecipeRecorder.ViewModel.BasicModel;

namespace RecipeRecorder.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this._menusItems = new ObservableCollection<ItemViewModel>();
            this._myRecipeItems = new ObservableCollection<ItemViewModel>();
            this._myFavoriteItems = new ObservableCollection<ItemViewModel>();

        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>

        private ObservableCollection<ItemViewModel> _menusItems;
        public ObservableCollection<ItemViewModel> MenusItems
        {
            get { return _menusItems; }
            set
            {
                _menusItems = value;
                NotifyPropertyChanged("MenusItems");
            }
        }

        private ObservableCollection<ItemViewModel> _myRecipeItems;
        public ObservableCollection<ItemViewModel> MyRecipeItems
        {
            get { return _myRecipeItems; }
            set
            {
                _myRecipeItems = value;
                NotifyPropertyChanged("MyRecipeItems");
            }
        }

        private ObservableCollection<ItemViewModel> _myFavoriteItems;
        public ObservableCollection<ItemViewModel> MyFavoriteItems
        {
            get { return _myFavoriteItems; }
            set
            {
                _myFavoriteItems = value;
                NotifyPropertyChanged("MyFavoriteItems");
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            this._menusItems.Add(new ItemViewModel() { LineOne = "New Recipe" });
            this._menusItems.Add(new ItemViewModel() { LineOne = "My Recipes" });
            this._menusItems.Add(new ItemViewModel() { LineOne = "What's new" });
            this._menusItems.Add(new ItemViewModel() { LineOne = "About/Help" });

            this._myRecipeItems.Add(new ItemViewModel() { LineOne = "GONG BAO Chicken" });
            this._myRecipeItems.Add(new ItemViewModel() { LineOne = "Donpling" });
            this._myRecipeItems.Add(new ItemViewModel() { LineOne = "Noodles" });

            this._myFavoriteItems.Add(new ItemViewModel() { LineOne = "Dishes 1", LineTwo = "Person 1" });
            this._myFavoriteItems.Add(new ItemViewModel() { LineOne = "Dishes 2", LineTwo = "Person 2" });
            this._myFavoriteItems.Add(new ItemViewModel() { LineOne = "Dishes 3", LineTwo = "Person 3" });

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}