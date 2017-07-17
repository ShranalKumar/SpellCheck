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
            List<skmuspellchecktable> spellCheckRows = await AzureManager.AzureManagerInstance.getAllRows();
            searchedWord.ItemsSource = spellCheckRows.Where(x => x.corrected.ToLower().Contains(searchBar.Text.ToLower()));
        }

        void clearHistory(object sender, EventArgs e)
        {
            searchedWord.ItemsSource = "";
        }
    }
}