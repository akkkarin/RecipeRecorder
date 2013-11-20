using RecipeRecorder.Model;
using RecipeRecorder.ViewModel.BasicModel;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Data.Linq;

namespace RecipeRecorder.ViewModel
{
    public class RecipeViewModel : INotifyPropertyChanged
    {
        private RecipeStepsViewModel _steps;
        private IngredientsViewModel _ingredients;
        private LocalRecipeModel _localDB;
        private UserItem _testUser;

        public RecipeViewModel() 
        {
            this._ingredients = new IngredientsViewModel();
            this._steps = new RecipeStepsViewModel();
        }
         
        public RecipeViewModel(string db)
        {
            this._ingredients = new IngredientsViewModel();
            this._steps = new RecipeStepsViewModel();
            this._localDB = new LocalRecipeModel(db);
            this._testUser = new UserItem { UId = 1 };
        }
        
        public void SaveChangesToDB()
        {
            this._localDB.SubmitChanges();
        }

        public void SaveRecipe() 
        {
            LocalRecipeItem newRecipe =new LocalRecipeItem
            {
                    RecipeName = this._ingredients.RecipeName,
                    InsertDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    Category = this._ingredients.Category,
                    User = this._testUser
            };
            this._localDB.Recipe.InsertOnSubmit(newRecipe);
            this._localDB.SubmitChanges();
            this.SaveIngredients(newRecipe);
            this.SaveSteps(newRecipe);
        }

        private void SaveIngredients(LocalRecipeItem newRecipe)
        {
            IngredientItem newIn;
            foreach(IngredientViewModel ivm in this._ingredients.IngredientItems)
            {
                newIn = new IngredientItem
                {
                    IngredientName = ivm.IngredientName,
                    Category = "Default"
                };

                this._localDB.Ingredient.InsertOnSubmit(newIn);
                this._localDB.SubmitChanges();

                this._localDB.MatchRecipeIngredient.InsertOnSubmit(
                    new MatchRecipeIngredientItem 
                    { 
                        Amount = ivm.Amount,
                        Ingrediant = newIn,
                        Recipe = newRecipe
                    }
                );
                this._localDB.SubmitChanges();
            }
        }

        private void SaveSteps(LocalRecipeItem newRecipe) 
        {
            StepItem newSt;
            int i = 1;
            foreach(RecipeStepViewModel rsv in this._steps.RecipeStepItems)
            { 
                newSt = new StepItem 
                { 
                    Description=rsv.Description,
                    Duration=rsv.Duration,
                    Image = rsv.Image.UriSource.OriginalString
                };
                this._localDB.Step.InsertOnSubmit(newSt);
                this._localDB.SubmitChanges();

                this._localDB.MatchRecipeStep.InsertOnSubmit(
                    new MatchRecipeStepItem 
                    {
                        Step = newSt,
                        Order = i,
                        Recipe = newRecipe
                    }
                );
                this._localDB.SubmitChanges();

                i++;
            }
        } 

        public string TestDB() 
        {
            // Specify the query for all to-do items in the database.
            var toDoItemsInDB = from LocalRecipeItem rsp in this._localDB.Recipe
                                select rsp.RecipeName;
            string r = "";
            foreach (string name in toDoItemsInDB)
            {
                r = r + name;
            }
            return r;
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
