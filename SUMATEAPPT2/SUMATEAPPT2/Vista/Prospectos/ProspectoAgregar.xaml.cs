using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using SUMATEAPPT2.Modelos;
using SUMATEAPPT2.Modelos.JWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUMATEAPPT2.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProspectoAgregar : ContentPage
    {
        public ProspectoAgregar()
        {
            InitializeComponent();
            getEstados();
            getProductos();
        }
        private async void getColonias(string id_estado, string id_municipio)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var uri = "http://192.168.90.165:55751/utilidades/Colonias/?estado=" + id_estado + "&municipio=" + id_municipio + "";
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(uri);
                var client = new HttpClient();

                try
                {
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
                                PickerColonias.TextColor = Color.FromHex("#4E8F75");
                                PickerColonias.TitleColor = Color.FromHex("#4E8F75");
                                var json = await content.ReadAsStringAsync();


                                var result = JsonConvert.DeserializeObject<Estados>(json);
                                PickerColonias.ItemsSource = result.Table;
                                PickerColonias.IsVisible = true;
                                PickerColonias.SelectedIndexChanged += PickerColonias_SelectedIndexChanged;


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
                catch (Exception ex)
                {

                    await DisplayAlert("Error", "Intente en otro momento _ error: " + ex.ToString() + " _ ", "ok");
                }
              
            }
        }
        private async void getMunicipios(string id_estado)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var uri = "http://192.168.90.165:55751/utilidades/municipios/?estado=" + id_estado + "";

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(uri);
                var client = new HttpClient();
                

                try
                {
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
                                PickerMunicipios.TextColor = Color.FromHex("#4E8F75");
                                PickerMunicipios.TitleColor = Color.FromHex("#4E8F75");
                                var json = await content.ReadAsStringAsync();

                                var result = JsonConvert.DeserializeObject<Estados>(json);
                                PickerMunicipios.ItemsSource = result.Table;
                                // PickerEstados.SelectedIndexChanged += PickerEstados_SelectedIndexChanged;

                                PickerMunicipios.IsVisible = true;
                                PickerMunicipios.SelectedIndexChanged += PickerMunicipios_SelectedIndexChanged;

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
                catch (Exception ex)
                {

                    await DisplayAlert("Error", "Intente en otro momento _ error: " + ex.ToString() + " _ ", "ok");
                }

            }

        }
        private async void getProductos()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var uri = "http://192.168.90.165:55751/utilidades/productos";
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(uri);
                var client = new HttpClient();
               


                try
                {
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
                                string xjson = await content.ReadAsStringAsync();

                                PickerProductos.TextColor = Color.FromHex("#4E8F75");
                                PickerProductos.TitleColor = Color.FromHex("#4E8F75");

                                var result = JsonConvert.DeserializeObject<Estados>(xjson);
                                PickerProductos.ItemsSource = result.Table;
                                PickerProductos.SelectedIndexChanged += PickerProductos_SelectedIndexChanged;

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
                catch (Exception ex)
                {

                    await DisplayAlert("Error", "Intente en otro momento _ error: " + ex.ToString() + " _ ", "ok");
                }
            }
        }
        private async void getEstados()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var uri = "http://192.168.90.165:55751/utilidades/Estados";
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(uri);
                var client = new HttpClient();
               

                try
                {
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
                                PickerEstados.TextColor = Color.FromHex("#4E8F75");
                                PickerEstados.TitleColor = Color.FromHex("#4E8F75");
                                var json = await content.ReadAsStringAsync();
                                /*
                                var json = JsonConvert.DeserializeObject<Estados>(xjson);
                                var json_ = json.Table;foreach (var item in json_)
                                 {PickerEstados.Items.Add(item.ESTADO.ToString() + "_" +item.CLAVE_E);PickerEstados.SelectedIndex = item.CLAVE_E;}*/
                                // PickerEstados.SelectedIndexChanged += PickerEstados_SelectedIndexChanged;
                                var result = JsonConvert.DeserializeObject<Estados>(json);
                                PickerEstados.ItemsSource = result.Table;
                                PickerEstados.SelectedIndexChanged += PickerEstados_SelectedIndexChanged;
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
                catch (Exception ex)
                {

                    await DisplayAlert("Error", "Intente en otro momento _ error: " + ex.ToString() + " _ ", "ok");
                }
            }

        }
        private void PickerEstados_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            try
            {
                var i = (Table)PickerEstados.SelectedItem;
                var id = i.CLAVE_E.ToString();
                estados_id.Text = id.ToString();
                estado_.Text = i.ESTADO;
                getMunicipios(id);

            }
            catch (Exception ex)
            {
                  DisplayAlert("Error", "Intente en otro momento _ error: " + ex.ToString() + " _ ", "ok");
            }

        }
        private void PickerMunicipios_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            try
            {
                var i = (Table)PickerMunicipios.SelectedItem;
                var id_m = i.CLAVE_M.ToString();
                var id_e = estados_id.Text;
                municipio_id.Text = id_m.ToString();
                municipio_.Text = i.MUNICIPIO;
                getColonias(id_e, id_m);

            }
            catch (Exception ex)
            {
                  DisplayAlert("Error", "Intente en otro momento _ error: " + ex.ToString() + " _ ", "ok");
            }
        }
        private void PickerColonias_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {

            try
            {
                var i = (Table)PickerColonias.SelectedItem;
                var id = i.CLAVE_C.ToString();
                var id_cp = i.CP.ToString();
                cp_id.Text = id_cp;
                colonia_.Text = i.COLONIA;
                colonia_id.Text = id.ToString();


            }
            catch (Exception ex)
            {
                  DisplayAlert("Error", "Intente en otro momento _ error: " + ex.ToString() + " _ ", "ok");
            }

        }
        private void PickerProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var i = (Table)PickerProductos.SelectedItem;
                var id = i.CLAVE_P.ToString();
                producto_id.Text = id.ToString();
                producto_.Text = i.PRODUCTO;
                // getMunicipios(id);

            }
            catch (Exception ex)
            {
                  DisplayAlert("Error", "Intente en otro momento _ error: " + ex.ToString() + " _ ", "ok");
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            DateTime aDate = DateTime.Now;
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            HttpClient client = new HttpClient();
            var value_check = new Dictionary<string, string>
                         {
                            { "nombre", Nombre.Text},
                            { "fecha_visita_tentativa" , Fecha.Date.ToString("yyyy/MM/dd") },
                            { "calle" , Calle.Text },
                            { "numero" , Numero.Text },
                            { "telefono" , Telefono.Text },
                            { "actividad_negocio" , ActividadNegocio.Text },
                            { "fecha" , aDate.ToString("MM/dd/yyyy HH:mm") },
                            { "resolucion" , Resolucion.Text },
                            { "modo_enterado" , ModoEnterado.Text },
                            { "app" , AP.Text },
                            { "apm" , AM.Text },
                            { "cp" , cp_id.Text },
                            { "estado" ,estados_id.Text },
                            { "colonia" , colonia_id.Text },
                            { "municipio" , municipio_id.Text },                            
                            { "tipo_producto" , producto_.Text },
                            { "id_asesor" , "1" },
                            { "id_producto", producto_id.Text },
                            { "id_sucursal" , "1" },
                            {"index_prospecto", finalString },
                            { "direccion_full", colonia_.Text + " "+municipio_.Text+" "+estado_.Text }

                         };
           

            var content = new FormUrlEncodedContent(value_check);
          
            try
            {
                var response = await client.PostAsync("http://192.168.90.165:55751/promocion/InsertProspecto", content);

                switch (response.StatusCode)
                {
 
                    case System.Net.HttpStatusCode.OK:
                        await DisplayAlert("Prospecto Guardado ! ", "Cita agendada con éxito ", "Ok");
                        Application.Current.MainPage = new MainPage();

                        break;
 
                    default:
                        await DisplayAlert("Error! ", "Parece que se produjo un error. ", "Ok");

                        break;
                }


            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", "Intente en otro momento _ error: " + ex.ToString() + " _ ", "ok");
            }
        }
    }
}