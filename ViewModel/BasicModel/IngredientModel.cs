using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace RecipeRecorder.ViewModel.BasicModel
{
    public class IngredientModel : INotifyPropertyChanged
    {

        public IngredientModel(string amount, string name)
        {
            this._amount = amount;
            this._ingredientName = name;
        }

        private string _amount;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if (value != _amount)
                {
                    _amount = value;
                    NotifyPropertyChanged("Amount");
                }
            }
        }

        private string _ingrediantName;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string IngrediantName
        {
            get
            {
                return _ingrediantName;
            }
            set
            {
                if (value != _ingrediantName)
                {
                    _ingrediantName = value;
                    NotifyPropertyChanged("IngrediantName");
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

        public string _ingredientName { get; set; }
    }
}
