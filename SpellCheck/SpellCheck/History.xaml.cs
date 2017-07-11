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
    public partial class History : ContentPage
    {
        int count = 0;
        public History()
        {
            InitializeComponent();
        }

        private void getHistory(object sender, EventArgs e)
        {
            count++;

            ((Button)sender).Text = count.ToString();
        }
    }
}