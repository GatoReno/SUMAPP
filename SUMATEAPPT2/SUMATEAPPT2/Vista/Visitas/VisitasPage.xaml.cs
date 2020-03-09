using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUMATEAPPT2.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisitasPage : ContentPage
    {
        public VisitasPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;

            await App.MasterD.Detail.Navigation.PushAsync(new AgendarVisitaDomiciliaria());

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;

            await App.MasterD.Detail.Navigation.PushAsync(new VisitasListView());
        }



        /*
         * 
         *    App.MasterD.IsPresented = false;

            await App.MasterD.Detail.Navigation.PushAsync(new VisitasListView());
         * */
    }
}