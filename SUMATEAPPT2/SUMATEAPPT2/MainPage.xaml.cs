using SUMATEAPPT2.Vista;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SUMATEAPPT2
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnClick(object sender, EventArgs e)

        {
            await DisplayAlert("mi cabecera","mi mensaje","ok");
             Application.Current.MainPage = new NavigationPage(new Page1());
        }
    }
}
