﻿using Newtonsoft.Json;
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
        public ComunalInfoProspecto(int id)
        {
            InitializeComponent();
            idx.Text = id.ToString();
            _ = GetPreProspectoInfo(id);
            _ = GetVisita(id);
            _ = GetCartaJurada(id);
            _ = GetEvaluacionEco(id);
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
            await Navigation.PushAsync(new CartaJurada());
        }

        private async void Rolbtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartaJurada());
        }


        public async Task GetCartaJurada(int id)
        {

            HttpClient client = new HttpClient();
            var uri = "http://192.168.90.165:55751/cartas/GetCartaJuarda/?id=" + id;
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
            var uri = "http://192.168.90.165:55751/cartas/GetVisita/?id=" + id;
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


        public async Task GetEvaluacionEco(int id)
        {

            HttpClient client = new HttpClient();
            var uri = "http://192.168.90.165:55751/cartas/GetEdoEvaluacion/?id=" + id;
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


    }
}