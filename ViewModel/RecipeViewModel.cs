using RecipeRecorder.Model;
using RecipeRecorder.ViewModel.BasicModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private RecipeRecorder.Model.LocalRecipeModel.User _testUser;

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
            this._testUser = new LocalRecipeModel.User { UId = 1 };
        }
        
        public void SaveChangesToDB()
        {
            this._localDB.SubmitChanges();
        }

        public void SaveRecipe() 
        {
            RecipeRecorder.Model.LocalRecipeModel.LocalRecipe newRecipe =
            new RecipeRecorder.Model.LocalRecipeModel.LocalRecipe
                {
                    RecipeName = this._ingredients.RecipeName,
                    InsertDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    Category = this._ingredients.Category,
                    User = this._testUser
                };

            this._localDB.RecipeTable.InsertOnSubmit(newRecipe);
            this._localDB.SubmitChanges();
            this.SaveIngredients(newRecipe);
            this.SaveSteps(newRecipe);
        }

        private void SaveIngredients(RecipeRecorder.Model.LocalRecipeModel.LocalRecipe newRecipe)
        {
            RecipeRecorder.Model.LocalRecipeModel.Ingredient newIn;
            foreach(IngredientViewModel ivm in this._ingredients.IngredientItems)
            {
                newIn = new LocalRecipeModel.Ingredient
                {
                    IngredientName = ivm.IngredientName,
                    Category = "Default"
                };

                this._localDB.IngredientTable.InsertOnSubmit(newIn);
                this._localDB.SubmitChanges();

                this._localDB.MatchRITable.InsertOnSubmit(
                    new RecipeRecorder.Model.LocalRecipeModel.MatchRecipeIngredient 
                    { 
                        Amount = ivm.Amount,
                        Ingrediant = newIn,
                        Recipe = newRecipe
                    }
                );
                this._localDB.SubmitChanges();
            }
        }

        private void SaveSteps(RecipeRecorder.Model.LocalRecipeModel.LocalRecipe newRecipe) 
        {
            RecipeRecorder.Model.LocalRecipeModel.Step newSt;
            int i = 1;
            foreach(RecipeStepViewModel rsv in this._steps.RecipeStepItems)
            { 
                newSt = new LocalRecipeModel.Step 
                { 
                    Description=rsv.Description,
                    Duration=rsv.Duration,
                    Image = this.BufferFromImage(rsv.Image)
                };
                this._localDB.StepTable.InsertOnSubmit(newSt);
                this._localDB.SubmitChanges();

                this._localDB.MatchRSTable.InsertOnSubmit(
                    new RecipeRecorder.Model.LocalRecipeModel.MatchRecipeStep 
                    {
                        Step = newSt,
                        Order = i
                    }
                );
                this._localDB.SubmitChanges();

                i++;
            }
        }


        public string TestDB() 
        { 
            
        }

        private Binary BufferFromImage(BitmapImage imageSource)
        {
            byte[] data = null;
            using (MemoryStream stream = new MemoryStream())
            {
                Image image = new Image();
                image.Source = imageSource; 
                WriteableBitmap wBitmap = new WriteableBitmap(image, null); 
                wBitmap.SaveJpeg(stream, wBitmap.PixelWidth, wBitmap.PixelHeight, 0, 100);
                stream.Seek(0, SeekOrigin.Begin);
                data = stream.GetBuffer();
            } 
            return new Binary(data);
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
