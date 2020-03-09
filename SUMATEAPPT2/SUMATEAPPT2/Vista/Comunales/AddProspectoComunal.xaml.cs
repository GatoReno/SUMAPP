using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SUMATEAPPT2.Modelos.JWS;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUMATEAPPT2.Vista.Comunales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProspectoComunal : ContentPage
    {
        public AddProspectoComunal(string id,int id_producto, string producto)
        {
            InitializeComponent();
            idx.Text = id;
            producto_.Text = producto;
            producto_id.Text = id_producto.ToString() ;
            getEstados();
            
        }
        private async void getColonias(string id_estado, string id_municipio)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var uri = "http://192.168.90.165:55751/utilidades/Colonias/?estado=" + id_estado + "&municipio=" + id_municipio + "";
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
        }
        private async void getMunicipios(string id_estado)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var uri = "http://192.168.90.165:55751/utilidades/municipios/?estado=" + id_estado + "";

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

        }   
        private async void getEstados()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var uri = "http://192.168.90.165:55751/utilidades/Estados";
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
                DisplayAlert("", "_" + ex.ToString() + " _ ", "ok");
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
                DisplayAlert("", "_" + ex.ToString() + " _ ", "ok");
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
                DisplayAlert("", "_" + ex.ToString() + " _ ", "ok");
            }

        }
        private async void Button_Clicked(object sender, EventArgs e)
        {

            DateTime aDate = DateTime.Now;
            var finalString = idx.Text;
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
            var response = await client.PostAsync("http://192.168.90.165:55751/promocion/InsertProspecto", content);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.Accepted:
                    break;
                case System.Net.HttpStatusCode.Ambiguous:
                    break;
                case System.Net.HttpStatusCode.BadGateway:
                    break;
                case System.Net.HttpStatusCode.BadRequest:
                    break;
                case System.Net.HttpStatusCode.Conflict:
                    break;
                case System.Net.HttpStatusCode.Continue:
                    break;
                case System.Net.HttpStatusCode.Created:
                    break;
                case System.Net.HttpStatusCode.ExpectationFailed:
                    break;
                case System.Net.HttpStatusCode.Forbidden:
                    break;
                case System.Net.HttpStatusCode.Found:
                    break;
                case System.Net.HttpStatusCode.GatewayTimeout:
                    break;
                case System.Net.HttpStatusCode.Gone:
                    break;
                case System.Net.HttpStatusCode.HttpVersionNotSupported:
                    break;
                case System.Net.HttpStatusCode.InternalServerError:
                    break;
                case System.Net.HttpStatusCode.LengthRequired:
                    break;
                case System.Net.HttpStatusCode.MethodNotAllowed:
                    break;
                case System.Net.HttpStatusCode.Moved:
                    break;

                case System.Net.HttpStatusCode.NoContent:
                    break;
                case System.Net.HttpStatusCode.NonAuthoritativeInformation:
                    break;
                case System.Net.HttpStatusCode.NotAcceptable:
                    break;
                case System.Net.HttpStatusCode.NotFound:
                    break;
                case System.Net.HttpStatusCode.NotImplemented:
                    break;
                case System.Net.HttpStatusCode.NotModified:
                    break;
                case System.Net.HttpStatusCode.OK:
                    await DisplayAlert("Prospecto Guardado ! ", "Cita agendada con éxito ", "Ok");
                    Application.Current.MainPage = new MainPage();

                    break;
                case System.Net.HttpStatusCode.PartialContent:
                    break;
                case System.Net.HttpStatusCode.PaymentRequired:
                    break;
                case System.Net.HttpStatusCode.PreconditionFailed:
                    break;
                case System.Net.HttpStatusCode.ProxyAuthenticationRequired:
                    break;

                case System.Net.HttpStatusCode.RedirectKeepVerb:
                    break;
                case System.Net.HttpStatusCode.RedirectMethod:
                    break;
                case System.Net.HttpStatusCode.RequestedRangeNotSatisfiable:
                    break;
                case System.Net.HttpStatusCode.RequestEntityTooLarge:
                    break;
                case System.Net.HttpStatusCode.RequestTimeout:
                    break;
                case System.Net.HttpStatusCode.RequestUriTooLong:
                    break;
                case System.Net.HttpStatusCode.ResetContent:
                    break;

                case System.Net.HttpStatusCode.ServiceUnavailable:
                    break;
                case System.Net.HttpStatusCode.SwitchingProtocols:
                    break;

                case System.Net.HttpStatusCode.Unauthorized:
                    break;
                case System.Net.HttpStatusCode.UnsupportedMediaType:
                    break;
                case System.Net.HttpStatusCode.Unused:
                    break;
                case System.Net.HttpStatusCode.UpgradeRequired:
                    break;
                case System.Net.HttpStatusCode.UseProxy:
                    break;
                default:
                    await DisplayAlert("Error! ", "Parece que se produjo un error. ", "Ok");

                    break;
            }
        }
    }
}