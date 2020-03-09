using Plugin.Connectivity;
using SQLite;
using SUMATEAPPT2.Interfaces;
using SUMATEAPPT2.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SUMATEAPPT2
{
    public class SplashPage : ContentPage
    {

        public UserFS user;
        public UserDb UserDb;
        public SQLiteConnection conn;
        Image splashImage;
        public SplashPage()
        {
            //InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "icon_fs.png",
                WidthRequest = 100,
                HeightRequest = 100
            };
            AbsoluteLayout.SetLayoutFlags(splashImage,
               AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
             new Rectangle(1, 1, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.FromHex("#D9E6DF");
            this.Content = sub;

            CheckConnectivity();
        }

        public async void CheckConnectivity() {

            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Advertencia ! ", "ACTIVAR  CONSUMO DE DATOS O WIFI  PARA PODER CONTINUAR ", "Ok");
                //OnAppearing();
            }
        }




        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.FadeTo(1, 150, null);
            await splashImage.ScaleTo(1, 1000); //Time-consuming processes such as initialization
            await splashImage.ScaleTo(0.6, 1500, Easing.BounceOut);
            await splashImage.FadeTo(0, 270, null);
            // Application.Current.MainPage = new SignIn();

            //InitializeComponent();

            UserDb = new UserDb();
            var users = UserDb.GetMembers();
            var uCount = users.Count();
            int RowCount = 0;

            RowCount = Convert.ToInt32(uCount);
            if (RowCount > 0)
            {
                Application.Current.MainPage = new MainPage();                
            }
            else
            {
                Application.Current.MainPage = new SignIn();
            }


         
        }

    }
}
