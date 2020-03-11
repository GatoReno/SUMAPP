using Newtonsoft.Json;
using SUMATEAPPT2.Modelos;
using SUMATEAPPT2.Vista.Emprendedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUMATEAPPT2.Vista.Comunales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComunalInfoProspecto : ContentPage
    {
        private string index_prospecto;
        public ComunalInfoProspecto(int id,string index_pros)
        {
            InitializeComponent();
            idx.Text = id.ToString();
            _ = GetPreProspectoInfo(id);
            _ = GetVisita(id);
            _ = GetCartaJurada(id);
            _ = GetEvaluacionEco(id);
            _ = GetRolGrupo(id);
            index_prospecto = index_pros;
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
                        telefono.Text = json_[0].telefono;
                        nombre.Text = "" + json_[0].nombre + " " + json_[0].app + " " + json_[0].apm;
                        direccion.Text = "" + json_[0].calle + " " + json_[0].numero + " " + json_[0].direccion_full;
                        fecha_visita_tentativa.Text = json_[0].fecha_visita_tentativa;
                        actividad_negocio.Text = json_[0].actividad_negocio;
                        break;

                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("", "" + ex.ToString(), "ok");
                return;
            }



        }

        private async void Visitabtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgendarVisitaDomiciliaria());
        }

        private async void btnEvEco_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EvaluacionEcoComunal());
        }

        private async void btnCartaJurada_Clicked(object sender, EventArgs e)
        {
            var id = idx.Text;
            var idx_ = Int32.Parse(id);
            await Navigation.PushAsync(new CartaJurada(idx_));
        }

        private async void Rolbtn_Clicked(object sender, EventArgs e)
        {
            var id = idx.Text;
            var name = nombre.Text;
            var index_ = index_prospecto;
            await Navigation.PushAsync(new AsignarRol(id,name,index_));
        }


        public async Task GetCartaJurada(int id)
        {
      
            try
            {


                HttpClient client = new HttpClient();
                var uric = "http://192.168.90.165:55751/Cartas/GetCartaJurada/" + id;


                var response = await client.GetAsync(uric);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        await DisplayAlert("Error 500", "Intente mas tarde"  , "ok");
                        Console.WriteLine("----------------------------------------------_____:Here status 500");


                        break;


                    case System.Net.HttpStatusCode.OK:
                        Console.WriteLine("----------------------------------------------_____:Here status 200");

                        HttpContent content = response.Content;
                        string xjson = await content.ReadAsStringAsync();

                       // var json_ = JsonConvert.DeserializeObject<List<CProspecto>>(xjson);
                        if (xjson == "PENDIENTE")
                        {
                            Visita_.Text = "Visita Pendiente";
                        }
                        break;

                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("", "" + ex.ToString(), "ok");
                return;
            }
 
        }


        public async Task GetVisita(int id)
        {

            HttpClient client = new HttpClient();
            var urix = "http://192.168.90.165:55751/cartas/GetVisita/" + id;
           
                var response = await client.GetAsync(urix);
                
           
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.InternalServerError:
                    await DisplayAlert("Error 500", "Intente mas tarde", "ok");
                    Console.WriteLine("----------------------------------------------_____:Here status 500");


                    break;


                case System.Net.HttpStatusCode.OK:
                    Console.WriteLine("----------------------------------------------_____:Here status 200");

                    HttpContent content = response.Content;
                    string xjson = await content.ReadAsStringAsync();
                    if (xjson == "PENDIENTE")
                    {
                        Visita_.Text = "Visita Pendiente";
                    }

                    break;

            }

        }


        public async Task GetEvaluacionEco(int id)
        {

           
            try
            {


                HttpClient client = new HttpClient();
                var urie = "http://192.168.90.165:55751/cartas/GetEdoEvaluacion/" + id;
                var response = await client.GetAsync(urie);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        await DisplayAlert("Error 500", "Intente mas tarde", "ok");
                        Console.WriteLine("----------------------------------------------_____:Here status 500");


                        break;


                    case System.Net.HttpStatusCode.OK:
                        Console.WriteLine("----------------------------------------------_____:Here status 200");

                        HttpContent content = response.Content;
                        string xjson = await content.ReadAsStringAsync();

                        if (xjson == "PENDIENTE")
                        {
                            Evaluacion_.Text = "Evaluacion economica Pendiente";
                        }
                        break;

                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("", "" + ex.ToString(), "ok");
                return;
            }

        }


        public async Task GetRolGrupo(int id)
        {

            HttpClient client = new HttpClient();
            var uri = "http://192.168.90.165:55751/configuracion/getprospectorol/" + id;
            try
            {
                var response = await client.GetAsync(uri);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        await DisplayAlert("Error 500", "Intente mas tarde", "ok");
                        Console.WriteLine("----------------------------------------------_____:Here status 500");


                        break;


                    case System.Net.HttpStatusCode.OK:
                        Console.WriteLine("----------------------------------------------_____:Here status 200");

                        HttpContent content = response.Content; //Roles
                        string xjson = await content.ReadAsStringAsync();
                        var json_ = JsonConvert.DeserializeObject<List<Roles>>(xjson);

                        if (json_[0].nombre.Length > 0)
                        {
                            Role_.Text = "Rol en el grupo : "+json_[0].nombre;
                        }
                        else {

                            Role_.Text = "Rol Pendiente";
                        }


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