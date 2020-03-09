using SQLite;
using SUMATEAPPT2.Interfaces;
using SUMATEAPPT2.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUMATEAPPT2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class master : ContentPage
    {

        public UserFS user;
        public UserDb UserDb;
        private UserDb UserDbM;
        public SQLiteConnection conn;


        public master()
        {
            InitializeComponent();
        }

        //btnCerrarSesion_Clicked
        private void btnCerrarSesion_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            UserDbM = new UserDb();
            UserDbM.DropTbMember();
            Application.Current.MainPage = new SignIn();
        }
        private async  void btnPrimera_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            
            await App.MasterD.Detail.Navigation.PushAsync(new Prospectos());
        }

        private async void btnVisita_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;

            await App.MasterD.Detail.Navigation.PushAsync(new VisitasPage());
        }
    }
} 