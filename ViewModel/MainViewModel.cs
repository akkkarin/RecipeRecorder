using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RecipeRecorder.Resources;
using RecipeRecorder.ViewModel.BasicModel;
using RecipeRecorder.Model;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Data.Linq;

namespace RecipeRecorder.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this._menusItems = new ObservableCollection<ItemViewModel>();
            this._myRecipeItems = new ObservableCollection<RecipeViewModel>();
            this._myFavoriteItems = new ObservableCollection<RecipeViewModel>();
            this._localDB = new LocalRecipeModel(Resources.AppResources.DBConnectionString);
        }

        private LocalRecipeModel _localDB;

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>

        public void Load_Recipe()
        {
            this.MyRecipeItems.Clear();

            var recipes = from LocalRecipeItem rsp in this._localDB.Recipe
                          orderby rsp.InsertDate
                          select rsp;
            RecipeViewModel tr; 

            foreach (LocalRecipeItem recipe in recipes)
            {
                tr = new RecipeViewModel();

                tr.Ingredients = this.Get_Ingredients(recipe.RecipeId);
                tr.Steps = this.Get_Steps(recipe.RecipeId);

                this.MyRecipeItems.Add(tr); 
            }
        }

        private IngredientsViewModel Get_Ingredients(int rid) 
        {
            var ig = from IngredientItem in this._localDB.Ingredient
                          join MatchRecipeIngredientItem in this._localDB.MatchRecipeIngredient on IngredientItem.IngredientId equals MatchRecipeIngredientItem._iId
                          where MatchRecipeIngredientItem._rId == rid
                          select new
                          {
                              Amount = MatchRecipeIngredientItem.Amount,
                              IngredientName = IngredientItem.IngredientName
                          };
            IngredientsViewModel tis =new IngredientsViewModel();
            
            foreach(var x in ig)
            {
                tis.AddIngredientItem(x.Amount, x.IngredientName);
            }
            return tis;
        }

        private RecipeStepsViewModel Get_Steps(int rid) 
        {
            var st = from StepItem in this._localDB.Step
                     join MatchRecipeStepItem in this._localDB.MatchRecipeStep on StepItem.StepId equals MatchRecipeStepItem._sId
                     where MatchRecipeStepItem._rId == rid
                     select new
                     {
                         stepNum = "Step " + MatchRecipeStepItem.Order,
                         duration = StepItem.Duration,
                         description = StepItem.Description,
                         image = StepItem.Image
                     };

            RecipeStepsViewModel sps = new RecipeStepsViewModel();

            foreach (var x in st)
            {
                sps.AddStepItem(x.stepNum, x.description, x.image, x.duration);
            }
            return sps;
        }

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

        private ObservableCollection<RecipeViewModel> _myRecipeItems;
        public ObservableCollection<RecipeViewModel> MyRecipeItems
        {
            get { return _myRecipeItems; }
            set
            {
                _myRecipeItems = value;
                NotifyPropertyChanged("MyRecipeItems");
            }
        }

        private ObservableCollection<RecipeViewModel> _myFavoriteItems;
        public ObservableCollection<RecipeViewModel> MyFavoriteItems
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