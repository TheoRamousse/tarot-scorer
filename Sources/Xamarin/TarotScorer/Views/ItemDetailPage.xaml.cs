using System.ComponentModel;
using TarotScorer.ViewModels;
using Xamarin.Forms;

namespace TarotScorer.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}