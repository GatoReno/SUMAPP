using Newtonsoft.Json;
using SUMATEAPPT2.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUMATEAPPT2.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisitaInfo : ContentPage
    {
        public VisitaInfo(int id)
        {
            InitializeComponent();
 
            _ = GetVisitaInfo(id);
            Mapx.MoveToRegion(
               MapSpan.FromCenterAndRadius(
                   new Position(19.054598, -98.191248), Distance.FromMiles(1)));
        }

        public async Task GetVisitaInfo(int id) {

            HttpClient client = new HttpClient();

            try
            {
                var uri = "http://192.168.90.165:55751/cartas/CVisita/" + id;
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


 


                        var json_ = JsonConvert.DeserializeObject<List<Cvisita>>(xjson);



                        Calle.Text = json_[0].calle.ToString();
                        Colonia.Text = json_[0].colonia.ToString();
                        Municipio.Text = json_[0].municipio.ToString();
                        Ciudad.Text = "Puebla";
                        Estado.Text = "Puebla";
                        CP.Text = json_[0].cp.ToString();
                        Fecha.Text = json_[0].fecha.ToString();

                       


                        break;

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("", "" + ex.ToString(), "ok");
               
                return;
            }
        }
    }
}