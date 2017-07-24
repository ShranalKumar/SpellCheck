using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpellCheck
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        public Search()
        {
            InitializeComponent();
        }

        async void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            progress.IsVisible = true;
            List<skmuspellchecktable> spellCheckRows = await AzureManager.AzureManagerInstance.getAllRows();
            await progress.ProgressTo(1, 500, Easing.Linear);
            searchedWord.ItemsSource = spellCheckRows.Where(x => x.corrected.ToLower().Contains(searchBar.Text.ToLower()));
            progress.IsVisible = false;
            progress.ProgressTo(0, 0, Easing.Linear);
            clear.IsVisible = true;
        }

        void clearHistory(object sender, EventArgs e)
        {
            searchedWord.ItemsSource = "";
            searchBar.Text = "";
            DisplayAlert("Alert!", "Page has been cleared", "Okay");
            clear.IsVisible = false;
        }
    }
}