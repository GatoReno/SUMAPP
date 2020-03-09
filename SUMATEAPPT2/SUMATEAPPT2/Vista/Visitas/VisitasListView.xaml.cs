using Newtonsoft.Json;
using SUMATEAPPT2.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUMATEAPPT2.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisitasListView : ContentPage
    {
        public VisitasListView()
        {
            InitializeComponent();
            _ = GetVisitas();
        }
       // ObservableCollection<Visitas> results = new ObservableCollection<Visitas>();
        public async Task GetVisitas() {
           
                HttpClient client = new HttpClient();
                var uri = "http://192.168.90.165:55751/cartas/ListaVisitas";
               
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
                        var xjson = await content.ReadAsStringAsync();

                        var json_ = JsonConvert.DeserializeObject<List<VisitasMod>>(xjson);
                        ListVisitas.ItemsSource = json_;
                        Cator.IsVisible = false;



                        break;

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("", "" + ex.ToString(), "ok");
                Cator.IsVisible = false;

                CatorT.Text = "Ha habido un error";
                return;
            }


        }
        private async void ListVisitas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as VisitasMod;
            await Navigation.PushAsync(new VisitaInfo(Int32.Parse(content.id)));
        }
    }
}