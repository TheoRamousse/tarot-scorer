using System;
using System.Collections.Generic;
using System.ComponentModel;
using TarotScorer.Models;
using TarotScorer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TarotScorer.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}