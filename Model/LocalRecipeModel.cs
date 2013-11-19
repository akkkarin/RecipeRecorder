﻿using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace RecipeRecorder.Model
{
    public class LocalRecipeModel : DataContext
    {
        public LocalRecipeModel(string connectionString)
            : base(connectionString)
        { }
        public Table<LocalRecipe> RecipeTable;
        public Table<User> UserTable;
        public Table<Step> StepTable;
        public Table<Ingredient> IngredientTable;
        public Table<MatchRecipeIngredient> MatchRITable;
        public Table<MatchRecipeStep> MatchRSTable;

        [Table]
        public class LocalRecipe : INotifyPropertyChanged, INotifyPropertyChanging
        {
            private int _recipeId;
            [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
            public int RecipeId
            {
                get { return _recipeId; }
                set
                {
                    if (_recipeId != value)
                    {
                        NotifyPropertyChanging("RecipeId");
                        _recipeId = value;
                        NotifyPropertyChanged("RecipeId");
                    }
                }
            }

            private string _recipeName;
            [Column]
            public string RecipeName
            {
                get { return _recipeName; }
                set
                {
                    if (_recipeName != value)
                    {
                        NotifyPropertyChanging("RecipeName");
                        _recipeName = value;
                        NotifyPropertyChanged("RecipeName");
                    }
                }
            }
            private DateTime _insertDate;
            [Column]
            public DateTime InsertDate
            {
                get { return _insertDate; }
                set
                {
                    if (_insertDate != value)
                    {
                        NotifyPropertyChanging("InsertDate");
                        _insertDate = value;
                        NotifyPropertyChanged("InsertDate");
                    }
                }
            }
            private DateTime _modifyDate;
            [Column]
            public DateTime ModifyDate
            {
                get { return _modifyDate; }
                set
                {
                    if (_modifyDate != value)
                    {
                        NotifyPropertyChanging("ModifyDate");
                        _modifyDate = value;
                        NotifyPropertyChanged("ModifyDate");
                    }
                }
            }

            private string _category;
            [Column]
            public string Category
            {
                get { return _category; }
                set
                {
                    if (_category != value)
                    {
                        NotifyPropertyChanging("Category");
                        _category = value;
                        NotifyPropertyChanged("Category");
                    }
                }
            }

            internal int _userId;
            private EntityRef<User> _user;
            [Association(Storage = "_user", ThisKey = "_userId", OtherKey = "UId", IsForeignKey = true)]
            public User User
            {
                get { return _user.Entity; }
                set
                {
                    NotifyPropertyChanging("Category");
                    _user.Entity = value;

                    if (value != null)
                    {
                        _userId = value.UId;
                    }

                    NotifyPropertyChanging("Category");
                }
            }

            [Column(IsVersion = true)]
            private Binary _version;

            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            // Used to notify that a property changed
            private void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            #endregion

            #region INotifyPropertyChanging Members

            public event PropertyChangingEventHandler PropertyChanging;

            // Used to notify that a property is about to change
            private void NotifyPropertyChanging(string propertyName)
            {
                if (PropertyChanging != null)
                {
                    PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
                }
            }

            #endregion
        }

        [Table]
        public class User : INotifyPropertyChanged, INotifyPropertyChanging
        {
            private int _uId;
            [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
            public int UId
            {
                get { return _uId; }
                set
                {
                    if (_uId != value)
                    {
                        NotifyPropertyChanging("UId");
                        _uId = value;
                        NotifyPropertyChanged("UId");
                    }
                }
            }
            [Column(IsVersion = true)]
            private Binary _version;

            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            // Used to notify that a property changed
            private void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            #endregion

            #region INotifyPropertyChanging Members

            public event PropertyChangingEventHandler PropertyChanging;

            // Used to notify that a property is about to change
            private void NotifyPropertyChanging(string propertyName)
            {
                if (PropertyChanging != null)
                {
                    PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
                }
            }

            #endregion
        }

        [Table]
        public class Step : INotifyPropertyChanged, INotifyPropertyChanging
        {
            private int _stepId;
            [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
            public int StepId
            {
                get { return _stepId; }
                set
                {
                    if (_stepId != value)
                    {
                        NotifyPropertyChanging("StepId");
                        _stepId = value;
                        NotifyPropertyChanged("StepId");
                    }
                }
            }
            [Column(IsVersion = true)]
            private Binary _version;

            private string _duration;
            [Column]
            public string Duration
            {
                get { return _duration; }
                set
                {
                    if (_duration != value)
                    {
                        NotifyPropertyChanging("Duration");
                        _duration = value;
                        NotifyPropertyChanged("Duration");
                    }
                }
            }

            private string _description;
            [Column]
            public string Description
            {
                get { return _description; }
                set
                {
                    if (_description != value)
                    {
                        NotifyPropertyChanging("Description");
                        _description = value;
                        NotifyPropertyChanged("Description");
                    }
                }
            }

            private Binary _image;
            [Column]
            public Binary Image
            {
                get { return _image; }
                set
                {
                    if (_image != value)
                    {
                        NotifyPropertyChanging("Image");
                        _image = value;
                        NotifyPropertyChanged("Image");
                    }
                }
            }

            private bool _isCover;
            [Column]
            public bool IsCover
            {
                get { return _isCover; }
                set
                {
                    if (_isCover != value)
                    {
                        NotifyPropertyChanging("IsCover");
                        _isCover = value;
                        NotifyPropertyChanged("IsCover");
                    }
                }
            }

            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            // Used to notify that a property changed
            private void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            #endregion

            #region INotifyPropertyChanging Members

            public event PropertyChangingEventHandler PropertyChanging;

            // Used to notify that a property is about to change
            private void NotifyPropertyChanging(string propertyName)
            {
                if (PropertyChanging != null)
                {
                    PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
                }
            }

            #endregion
        }

        [Table]
        public class Ingredient : INotifyPropertyChanged, INotifyPropertyChanging
        {
            private int _ingredientId;
            [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
            public int IngredientId
            {
                get { return _ingredientId; }
                set
                {
                    if (_ingredientId != value)
                    {
                        NotifyPropertyChanging("IngredientId");
                        _ingredientId = value;
                        NotifyPropertyChanged("IngredientId");
                    }
                }
            }
            [Column(IsVersion = true)]
            private Binary _version;
              
            private string _ingredientName;
            [Column]
            public string IngredientName
            {
                get { return _ingredientName; }
                set
                {
                    if (_ingredientName != value)
                    {
                        NotifyPropertyChanging("IngredientName");
                        _ingredientName = value;
                        NotifyPropertyChanged("IngredientName");
                    }
                }
            }

            private string _category;
            [Column]
            public string Category
            {
                get { return _category; }
                set
                {
                    if (_category != value)
                    {
                        NotifyPropertyChanging("Category");
                        _category = value;
                        NotifyPropertyChanged("Category");
                    }
                }
            }

            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            // Used to notify that a property changed
            private void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            #endregion

            #region INotifyPropertyChanging Members

            public event PropertyChangingEventHandler PropertyChanging;

            // Used to notify that a property is about to change
            private void NotifyPropertyChanging(string propertyName)
            {
                if (PropertyChanging != null)
                {
                    PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
                }
            }

            #endregion
        }

        [Table]
        public class MatchRecipeIngredient : INotifyPropertyChanged, INotifyPropertyChanging
        {
            private int _matchRIId;
            [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
            public int MatchRIId
            {
                get { return _matchRIId; }
                set
                {
                    if (_matchRIId != value)
                    {
                        NotifyPropertyChanging("MatchRIId");
                        _matchRIId = value;
                        NotifyPropertyChanged("MatchRIId");
                    }
                }
            }
            [Column(IsVersion = true)]
            private Binary _version;

            private string _amount;
            [Column]
            public string Amount
            {
                get { return _amount; }
                set
                {
                    if (_amount != value)
                    {
                        NotifyPropertyChanging("Amount");
                        _amount = value;
                        NotifyPropertyChanged("Amount");
                    }
                }
            }

            internal int _rId;
            private EntityRef<LocalRecipe> _recipe;
            [Association(Storage = "_recipe", ThisKey = "_rId", OtherKey = "RecipeId", IsForeignKey = true)]
            public LocalRecipe Recipe
            {
                get { return _recipe.Entity; }
                set
                {
                    NotifyPropertyChanging("Recipe");
                    _recipe.Entity = value;

                    if (value != null)
                    {
                        _rId = value.RecipeId;
                    }

                    NotifyPropertyChanging("Recipe");
                }
            }

            internal int _iId;
            private EntityRef<Ingredient> _ingrediant;
            [Association(Storage = "_ingrediant", ThisKey = "_iId", OtherKey = "IngredientId", IsForeignKey = true)]
            public Ingredient Ingrediant
            {
                get { return _ingrediant.Entity; }
                set
                {
                    NotifyPropertyChanging("Ingrediant");
                    _ingrediant.Entity = value;

                    if (value != null)
                    {
                        _iId = value.IngredientId;
                    }

                    NotifyPropertyChanging("Ingrediant");
                }
            }

            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            // Used to notify that a property changed
            private void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            #endregion

            #region INotifyPropertyChanging Members

            public event PropertyChangingEventHandler PropertyChanging;

            // Used to notify that a property is about to change
            private void NotifyPropertyChanging(string propertyName)
            {
                if (PropertyChanging != null)
                {
                    PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
                }
            }

            #endregion
        }

        [Table]
        public class MatchRecipeStep : INotifyPropertyChanged, INotifyPropertyChanging
        {
            private int _matchRSId;
            [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
            public int MatchRSId
            {
                get { return _matchRSId; }
                set
                {
                    if (_matchRSId != value)
                    {
                        NotifyPropertyChanging("MatchRSId");
                        _matchRSId = value;
                        NotifyPropertyChanged("MatchRSId");
                    }
                }
            }

            private int _order;
            [Column]
            public int Order
            {
                get { return _order; }
                set
                {
                    if (_order != value)
                    {
                        NotifyPropertyChanging("Order");
                        _order = value;
                        NotifyPropertyChanged("Order");
                    }
                }
            }

            [Column(IsVersion = true)]
            private Binary _version;
              
            internal int _rId;
            private EntityRef<LocalRecipe> _recipe;
            [Association(Storage = "_recipe", ThisKey = "_rId", OtherKey = "RecipeId", IsForeignKey = true)]
            public LocalRecipe Recipe
            {
                get { return _recipe.Entity; }
                set
                {
                    NotifyPropertyChanging("Recipe");
                    _recipe.Entity = value;

                    if (value != null)
                    {
                        _rId = value.RecipeId;
                    }

                    NotifyPropertyChanging("Recipe");
                }
            }

            internal int _sId;
            private EntityRef<Step> _step;
            [Association(Storage = "_step", ThisKey = "_sId", OtherKey = "StepId", IsForeignKey = true)]
            public Step Step
            {
                get { return _step.Entity; }
                set
                {
                    NotifyPropertyChanging("Recipe");
                    _step.Entity = value;

                    if (value != null)
                    {
                        _sId = value.StepId;
                    }

                    NotifyPropertyChanging("Recipe");
                }
            }
            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            // Used to notify that a property changed
            private void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            #endregion

            #region INotifyPropertyChanging Members

            public event PropertyChangingEventHandler PropertyChanging;

            // Used to notify that a property is about to change
            private void NotifyPropertyChanging(string propertyName)
            {
                if (PropertyChanging != null)
                {
                    PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
                }
            }

            #endregion
        }

    }
}
