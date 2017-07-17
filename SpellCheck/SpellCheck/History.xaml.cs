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
            List<skmuspellchecktable> spellCheckRows = await AzureManager.AzureManagerInstance.getAllRows();
            WordList.ItemsSource = spellCheckRows.OrderByDescending(x => x.updatedAt);
        }

        void clearHistory(object sender, EventArgs e)
        {
            WordList.ItemsSource = "";
        }
    }
}