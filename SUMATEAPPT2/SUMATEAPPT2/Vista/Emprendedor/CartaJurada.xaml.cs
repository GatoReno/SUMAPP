using Newtonsoft.Json;
using SUMATEAPPT2.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUMATEAPPT2.Vista.Emprendedor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartaJurada : ContentPage
    {
        public CartaJurada(int id)
        {
            InitializeComponent();
            _ = GetPreProspectoInfo(id);
        }

        private void NoAplica_Clicked(object sender, EventArgs e)
        {

        }

        private async void AddCartabtn_Clicked(object sender, EventArgs e)
        {

            HttpClient client = new HttpClient();

            // pos.Latitude 

            var value_check = new Dictionary<string, string>
                         {
 
                            { "id_cliente" , "11111" },
                            { "id_sucursal" , "11111" },
                            { "monto", "11111" },
                            { "actividad_negocio", "11111" },
                            { "cliente" ,  nombre.Text },
                            { "fecha", DateTime.Now.ToString() },
                            { "sucursal" ,  "1" },
                            {"tipo_carta" ,  "No APLICA CARTA JURADA" }
                        };
            var content = new FormUrlEncodedContent(value_check);
            var response = await client.PostAsync("http://192.168.90.165:55751/cartas/InsertVisitaApp", content);
        }


        public async Task GetPreProspectoInfo(int id)
        {

            HttpClient client = new HttpClient();
            var uri = "http://192.168.90.165:55751/utilidades/GetPreProspectoID/?id=" + id;
            try
            {
                var response = await client.GetAsync(uri);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        Console.WriteLine("----------------------------------------------_____:Here status 500");


                        break;


                    case System.Net.HttpStatusCode.OK:
                        Console.WriteLine("----------------------------------------------_____:Here status 200");

                        HttpContent content = response.Content;
                        string xjson = await content.ReadAsStringAsync();

                        var json_ = JsonConvert.DeserializeObject<List<CProspecto>>(xjson);
                         
                        //telefono.Text = json_[0].telefono;
                        nombre.Text = "" + json_[0].nombre + " " + json_[0].app + " " + json_[0].apm;

                        grupo.Text = json_[0].index_prospecto;
                       // direccion.Text = "" + json_[0].calle + " " + json_[0].numero + " " + json_[0].direccion_full;
                       // fecha_visita_tentativa.Text = json_[0].fecha_visita_tentativa;
                       // actividad_negocio.Text = json_[0].actividad_negocio;
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