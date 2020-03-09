﻿using Newtonsoft.Json;
using Plugin.Connectivity;
using SUMATEAPPT2.Modelos;
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
    public partial class AsignarRol : ContentPage
    {
        public AsignarRol()
        {
            InitializeComponent();
        }


        private async void getEstados()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var uri = "http://192.168.90.165:55751/configuracion/getlistrolgpo";
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(uri);
                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        Console.WriteLine("----------------------------------------------_____:Here status 500");
                        break;
                    case System.Net.HttpStatusCode.OK:
                        Console.WriteLine("----------------------------------------------_____:Here status 200");
                        try
                        {
                            HttpContent content = response.Content;
                            PickerRoles.TextColor = Color.FromHex("#4E8F75");
                            PickerRoles.TitleColor = Color.FromHex("#4E8F75");
                            var json = await content.ReadAsStringAsync();
        
                            var result = JsonConvert.DeserializeObject<List<Roles>>(json);
                            PickerRoles.ItemsSource = result;
                           // PickerRoles.SelectedIndexChanged += PickerEstados_SelectedIndexChanged;
                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("", "" + ex.ToString(), "ok");
                            var x = ex.ToString();
                            return;
                        }
                        break;
                    case System.Net.HttpStatusCode.NotFound:

                        await DisplayAlert("error 404", "servidor no encontrado ", "ok");
                        break;
                }
            }

        }

    }
}