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
    public partial class Prospectos : ContentPage
    {
        internal string id;

        public Prospectos()
        {
            InitializeComponent();
        }


        private async void btnAgregarProspecto_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;

            await App.MasterD.Detail.Navigation.PushAsync(new ProspectoAgregar());
        }

        private async void btnVisita_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;

            await App.MasterD.Detail.Navigation.PushAsync(new AgendarVisitaDomiciliaria());
        }

        private async void btnListaProspecto_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;

            await App.MasterD.Detail.Navigation.PushAsync(new ProspectoListView());
        }
    }
}