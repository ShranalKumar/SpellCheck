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
                List<wordsList> words = new List<wordsList>();

                try
                {
                    List<FlaggedTokens> flaggedTokens = responseModel.flaggedTokens;

                    if (flaggedTokens.Count != 0)
                    {
                        foreach (FlaggedTokens tokens in flaggedTokens)
                        {
                            var flagged = tokens.token;
                            List<Suggestions> suggestions = tokens.suggestions;

                            foreach (Suggestions sug in suggestions)
                            {
                                words.Add(new wordsList() { word = sug.suggestion });

                                skmuspellchecktable NewWord = new skmuspellchecktable() { word = flagged, corrected = sug.suggestion };
                                await AzureManager.AzureManagerInstance.PostWord(NewWord);
                            }
                        }
                    }
                    else { words.Add(new wordsList() { word = "No suggestions" }); }
                }
                catch (Exception)
                {
                    words.Add(new wordsList() { word = "There has been an error!" });
                }

                Suggesting.ItemsSource = words;               
            }            
        }
    }

    class wordsList
    {
        public string word { get; set; }
    }
}