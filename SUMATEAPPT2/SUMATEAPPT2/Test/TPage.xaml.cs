using Newtonsoft.Json;
using Plugin.Geolocator;
using SUMATEAPPT2.Modelos.JWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUMATEAPPT2.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TPage : ContentPage
    {
        public TPage()
        {
            InitializeComponent();
            FillPicker();
            //btnLocation_Clicked();
        }
        void Picker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var i = (Estados)PickerEstados.SelectedItem;
            DisplayAlert("Barrio", i.Table[0].CLAVE_E.ToString(), "Aceptar");
        }
        private async void FillPicker()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            HttpResponseMessage responseMessage = new HttpResponseMessage();

            httpRequestMessage.Headers.Add("accept", "application/json");
            httpRequestMessage.Method = HttpMethod.Get;
            httpRequestMessage.RequestUri = new Uri("http://192.168.90.165:55751/utilidades/estados");

            responseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = responseMessage.Content;
                var json = await content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<Estados>>(json);

                PickerEstados.ItemsSource = result;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        /*async void btnLocation_Clicked()
        {
            try
            {
                var location = await CrossGeolocator.Current.GetPositionAsync();

                if (location != null)
                {
                    var lon = "Latitude: " + location.Latitude.ToString();
                    var lat = "Longitude:" + location.Longitude.ToString();

                    string x = lon.ToString() + "_" + lat.ToString();

                    lbl_st_1.Text = x;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Faild", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Faild", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }
        }*/
    }
}