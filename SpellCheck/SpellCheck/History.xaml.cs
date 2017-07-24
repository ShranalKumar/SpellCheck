using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpellCheck
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class History : ContentPage
    {
        public History()
        {
            InitializeComponent();
        }

        async void getHistory(object sender, EventArgs e)
        {
            progress.IsVisible = true;
            List<skmuspellchecktable> spellCheckRows = await AzureManager.AzureManagerInstance.getAllRows();
            await progress.ProgressTo(1, 500, Easing.Linear);
            WordList.ItemsSource = spellCheckRows.OrderByDescending(x => x.updatedAt);
            progress.IsVisible = false;
            clear.IsVisible = true;
            progress.ProgressTo(0, 0, Easing.Linear);
        }

        void clearHistory(object sender, EventArgs e)
        {
            WordList.ItemsSource = "";
            DisplayAlert("Alert!", "Page has been cleared", "Okay");
            clear.IsVisible = false;
        }
    }
}