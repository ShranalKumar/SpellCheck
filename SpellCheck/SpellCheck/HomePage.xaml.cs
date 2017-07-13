using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpellCheck
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private string _searchedText;
        public string SearchedText
        {
            get { return _searchedText; }
            set { _searchedText = value; }
        }

        public HomePage()
        {
            InitializeComponent();
        }

        async void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "233d11b1fba54760a24a2243eff1f6cc");

            var uri = "https://api.cognitive.microsoft.com/bing/v5.0/spellcheck/?text=" + searchBar.Text;

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                ResponseModel responseModel = JsonConvert.DeserializeObject<ResponseModel>(responseString);

                try
                {
                    correct.Text = responseModel.flaggedTokens[0].suggestions[0].suggestion;

                    
                }
                catch (Exception)
                {
                    correct.Text = searchBar.Text;
                }

                skmuspellchecktable NewWord = new skmuspellchecktable()
                {
                    word = correct.Text
                };

                await AzureManager.AzureManagerInstance.PostWord(NewWord);
            }            
        }
    }
}