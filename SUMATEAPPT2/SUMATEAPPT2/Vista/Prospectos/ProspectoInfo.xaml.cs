using Newtonsoft.Json;
using SUMATEAPPT2.Modelos;
using SUMATEAPPT2.Vista.Comunales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUMATEAPPT2.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProspectoInfo : ContentPage
    {
        public string id_index;
        public ProspectoInfo(string id)
        {
            InitializeComponent();
            _ = GetInfoProspecto(id);
            id_index = id;
        }

        public async Task GetPreProspectos(string id) {

            HttpClient client = new HttpClient();
            var uri = " http://192.168.90.165:55751/utilidades/GetPreProspecto/?index_prospecto=" + id;
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
                        LPreProspecto.ItemsSource = json_;

                        LPreProspecto.IsVisible = true;

                        break;

                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("", "" + ex.ToString(), "ok");
                return;
            }



        }
        public async Task GetInfoProspecto(string id) {

            HttpClient client = new HttpClient();
            var uri = "http://192.168.90.165:55751/Promocion/GetListProspectosID/?index_prospecto=" + id; //"MFFXBIfz"
            Cator.IsVisible = true;
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
 
                        Nombre.Text = json_[0].nombre.ToString() +
                            " " + json_[0].app.ToString() + " " + json_[0].apm.ToString();
                        Direccion.Text = json_[0].direccion_full;
                        Tipo_producto.Text = json_[0].tipo_producto.ToString();
                        Fecha_tentativa.Text = json_[0].fecha_visita_tentativa;
                        Resolucion.Text = json_[0].resolucion.ToString();
                        Enterado.Text = json_[0].modo_enterado;//modo_enterado

                        var id_producto = json_[0].id_producto;
                        producto_.Text = id_producto.ToString();

                        var id_index = json_[0].index_prospecto;
                        index_.Text = id_index;
                        if (id_producto == 7059)
                        {
                            btnAddProspecto.IsVisible = true;
                            _ = GetPreProspectos(id_index);
                        }

                        break;

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("", "" + ex.ToString(), "ok");
                return;
            }

            Cator.IsVisible = false;
            CatorText.IsVisible = false;
        }
        private void Telefono_Clicked(object sender, EventArgs e)
        {

        }
        private async void btnAddProspecto_Clicked(object sender, EventArgs e)
        {
            var id = index_.Text;
            var id_producto = Int32.Parse(producto_.Text);
            var producto = Tipo_producto.Text;
            await App.MasterD.Detail.Navigation.PushAsync(new AddProspectoComunal(id,id_producto, producto));
        }
        private async void ProspectoList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
 
            var content_X = e.Item as CProspecto;
            var li = content_X.id_prospecto;
            await Navigation.PushAsync(new ComunalInfoProspecto(content_X.id_prospecto,content_X.index_prospecto));
        }
    }
}