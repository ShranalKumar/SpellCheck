using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
//using System.Net.WebUtility;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
            var queryString = searchBar.Text;
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "233d11b1fba54760a24a2243eff1f6cc");

            var uri = "https://api.cognitive.microsoft.com/bing/v5.0/spellcheck/?" + queryString;

            HttpResponseMessage response;

            byte[] byteData = Encoding.UTF8.GetBytes("{body}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                response = await client.PostAsync(uri, content);
            }
            correct.Text = response.ToString();
        }
    }
}