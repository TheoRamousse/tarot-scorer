using System;
using System.Collections.Generic;
using TarotScorer.ViewModels;
using TarotScorer.Views;
using Xamarin.Forms;

namespace TarotScorer
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {

        }
    }
}
