using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using SUMATEAPPT2.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUMATEAPPT2.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProspectoListView : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        public ProspectoListView()
        {
            InitializeComponent();
            _ =  GetProspectos();         
        }
        private async void ProspectoList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content_X = e.Item as Prospectos_Call;
        
            await Navigation.PushAsync(new ProspectoInfo (content_X.index_prospecto));

        }
        public async Task GetProspectos() {
            HttpClient client = new HttpClient();
             

            var uri = "http://192.168.90.165:55751/utilidades/GetPreProspectoApp/";

            Cator.IsVisible = true;


            try
            {

                var response = await client.GetAsync(uri);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        Console.WriteLine("----------------------------------------------_____:Here status 500");

                        //xlabel.Text = "error 500";
                        // Cator.IsVisible = false;
                        break;


                    case System.Net.HttpStatusCode.OK:
                        Console.WriteLine("----------------------------------------------_____:Here status 200");

                        // ylabel.Text = "Ultimas noticas de proyectos";
                        HttpContent content = response.Content;
                        string xjson = await content.ReadAsStringAsync();

                        var json_ = JsonConvert.DeserializeObject<List<Prospectos_Call>>(xjson);              
                        ProspectoList.ItemsSource = json_;
                        break;


                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Intente en otro momento _ error: " + ex.ToString() + " _ ", "ok");
                Cator.IsVisible = false;

                CatorText.Text = "Ha habido un error";
                return;
            }

            Cator.IsVisible = false;
            CatorText.IsVisible = false;





        }
    }
}