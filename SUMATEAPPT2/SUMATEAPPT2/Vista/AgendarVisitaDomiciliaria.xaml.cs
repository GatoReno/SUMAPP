
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.LocalNotifications;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SUMATEAPPT2.Interfaces;
using SUMATEAPPT2.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Position = Xamarin.Forms.Maps.Position;

namespace SUMATEAPPT2.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendarVisitaDomiciliaria : ContentPage
    {
        private StreamImageSource imgstream;
        public Cvisita visitaOutLine;
        public UserDb visitasDb;
        //AIzaSyDUSWbri5yyzK1kh1lEj9fxX21WyM3Gaow   
        public AgendarVisitaDomiciliaria()
        {
            InitializeComponent();          
        }             
        protected override async void OnAppearing()
        {

            if (!CrossConnectivity.Current.IsConnected)
            {
            
                ErrorBtn.IsVisible = true;
                ErrorLbl.Text = "No cuentas con conexion a internet, puedes trabajar de todas formas";

            }
            if (!CrossGeolocator.IsSupported)
            {
                await DisplayAlert("Error", "Ha habido un error con el plugin", "ok");

            }


            var pos = await CrossGeolocator.Current.GetPositionAsync();

            Latit.Text = "Latitude: " + pos.Latitude.ToString();
            Longit.Text = "Longitude:" + pos.Longitude.ToString();

            Mapx.MoveToRegion(
            MapSpan.FromCenterAndRadius(
            new Position(pos.Latitude, pos.Longitude), Distance.FromMiles(1)));


            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(pos.Latitude, pos.Longitude),
                Label = "Mi ubicacion",
                Address = "aqui se encuentra usted",

            };
            Mapx.Pins.Add(pin);
       
        }
        private async void Agendarbtn_Clicked(object sender, EventArgs e)
        {
            DateTime aDate = DateTime.Now;
            if (!CrossConnectivity.Current.IsConnected)
            {
                if (Sign.IsBlank)
                {
                    Sign.Focus();
                    await DisplayAlert("Error ! ", "Por favor firme para continuar", "Ok");
                }
                else
                {                             
                    

                        Stream image = await Sign.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);

                        BinaryReader br = new BinaryReader(image);
                        Byte[] bytes = br.ReadBytes((Int32)image.Length);
                        string base64Str = Convert.ToBase64String(bytes, 0, bytes.Length);

            
                        visitaOutLine = new Cvisita();
                        visitasDb = new UserDb();
                        visitaOutLine.documento = "1111";
                        visitaOutLine.responsable_firma = base64Str;
                        visitaOutLine.asesor = 111;
                        visitaOutLine.observaciones = Observaciones.Text;
                        visitaOutLine.id_asesor = 1111;
                        visitaOutLine.id_sucursal = 111;
                        visitaOutLine.id_cliente = 111;
                        visitaOutLine.id_carta = 111;
                        visitaOutLine.cliente = NCliente.Text;
                        visitaOutLine.sucursal = 1111;
                        visitaOutLine.tipo = "0";
                        visitaOutLine.fecha = FechaVisita.Date.ToString("yyyy/MM/dd");
                        visitaOutLine.secuencia = 1;
                        visitaOutLine.observaciones = "0";
                        visitaOutLine.pais = "mexico";
                        visitaOutLine.colonia = "1";
                        visitaOutLine.municipio = "1";
                        visitaOutLine.ciudad = Municipio.Text;
                        visitaOutLine.estado = "d";
                        visitaOutLine.calle = Calle.Text;
                        visitaOutLine.latitud = Latit.Text;
                        visitaOutLine.latD = Latit.Text;
                        visitaOutLine.longitud = Longit.Text;
                        visitaOutLine.lngD = Longit.Text;
                        visitaOutLine.cp = "1";
                        visitaOutLine.numero = Num.Text;
                        visitaOutLine.numero_serie = "2";
                        visitaOutLine.tipo_carta = "11";
                        visitaOutLine.respuesta1 = "1";
                        visitaOutLine.respuesta12 = "1";
                        visitaOutLine.respuesta13 = "1";
                        visitaOutLine.respuesta10 = "1";
                        visitaOutLine.respuesta2 = "1";
                        visitaOutLine.respuesta11 = "1";
                        visitaOutLine.respuesta3 = "1";
                        visitaOutLine.respuesta4 = "1";
                        visitaOutLine.respuesta5 = "1";
                        visitaOutLine.respuesta6 = "1";
                        visitaOutLine.respuesta7 = "1";
                        visitaOutLine.respuesta8 = "1";
                        visitaOutLine.respuesta9 = "1";


                    try
                    {
                        visitasDb.AddMember_visitas(visitaOutLine);

                        await DisplayAlert("Exito ! ", " Solicitud y Frima guardada con éxito. ", "Ok");
                        CrossLocalNotifications.Current.Show("Datos guardados", "Los datos que guardaste fuera de linea se enviaran a la plataforma cuando cuentes con internet o datos moviles.");

                        Application.Current.MainPage = new MainPage();
                    }
                    catch (Exception ex)
                    {

                        await DisplayAlert("Error ", " Error : " + ex.ToString(), "Ok"); ;
                    }
                 
                }


            }



            if (Sign.IsBlank)
            {
                Sign.Focus();
                await DisplayAlert("Error ! ", "Por favor firme para continuar", "Ok"); 
            }
            else
            {
    
                Stream image = await Sign.GetImageStreamAsync
                    (SignaturePad.Forms.SignatureImageFormat.Png);
                //Sign.Clear();
                //Sign.IsVisible = false;


                BinaryReader br = new BinaryReader(image);
                Byte[] bytes = br.ReadBytes((Int32)image.Length);
                string base64Str = Convert.ToBase64String(bytes, 0, bytes.Length);

                var pos = await CrossGeolocator.Current.GetPositionAsync();

                HttpClient client = new HttpClient();

                // pos.Latitude 

                var value_check = new Dictionary<string, string>
                         {
                            {"id_documento" , "11111" },
                            { "id_sucursal" , "11111" },
                            { "id_cliente" ,  "11111" },
                            { "id_asesor" ,  "11111" },
                            { "cliente" ,  NCliente.Text },
                            { "sucursal" ,  "1" },
                            {"tipo_carta" ,  "11111" },
                            {"fecha" ,  FechaVisita.Date.ToString("yyyy/MM/dd") },
                            {"secuencia" , "1"},
                            {"respuesta1" , " "},
                            {"respuesta2" ,  " "},
                            {"respuesta3" ,  " "},
                            {"respuesta4" ,  " "},
                            {"respuesta5" ,  " "},
                            {"respuesta6" ,  " "},
                            {"respuesta7" ,  " "},
                            {"respuesta8" ,  " "},
                            {"respuesta9" ,  " "},
                            {"respuesta10" ,  " "},
                            {"respuesta11" ,  " "},
                            {"respuesta12" ,  " "},
                            {"respuesta13" ,  " "},
                            {"observaciones" , Observaciones.Text},             
                            {"pais", "Mexico"},
                            { "colonia_id", "111" },
                            { "municipio_id", "111" },
                            { "ciudad",  Municipio.Text },
                            { "estado", "111" },
                            { "cp_id", "111" },
                            { "numero", Num.Text },
                            { "calle", Calle.Text},
                            { "localidad", "localidad" },
                            { "referencia" , "referncia"},
                            { "tipo" , "tipo nuevo tipo test"},
                            {"latitud", pos.Latitude.ToString()  },
                            {"longitud", pos.Longitude.ToString()  },
                            {"latD", pos.Latitude.ToString() },
                            {"lngD",pos.Longitude.ToString()  },

                };
                var content = new FormUrlEncodedContent(value_check);
                var response = await client.PostAsync("http://192.168.90.165:55751/cartas/InsertVisitaApp", content);
                switch (response.StatusCode)
                {
                    case (System.Net.HttpStatusCode.OK):
                        if (response.IsSuccessStatusCode)
                        {

                            var xjson = await response.Content.ReadAsStringAsync();
                            var json = JsonConvert.DeserializeObject<Cvisita>(xjson);

                            var idx = json.id_carta;

 

                            await DisplayAlert("Exito ! ", " Exito agregando datos. ", "Ok");
                            Application.Current.MainPage = new MainPage();
                        }
                        else
                        {
                            await DisplayAlert("Error ! ", "Hubo un error en la insert de datos. ", "Ok");
                        }
                        break;
                    case (System.Net.HttpStatusCode.Forbidden):
                        await DisplayAlert("Error ! ", "Hubo un error en la insert de datos. ", "Ok");
                        break;

                }

                var value_check_firma = new Dictionary<string, string>
                         {
                            {"id_documento" , "11111" },
                            { "id_testigos" , "11111" },
                            { "nombre" ,  NCliente.Text },
                            { "app" ,  "Testing APP" },
                            { "apm" ,  "Testing APM" },
                            { "cargo" ,  "Testing CARGO" },
                            { "firma" ,  base64Str }

                };
                var content_firma = new FormUrlEncodedContent(value_check_firma);
                var response_firma = await client.PostAsync("http://192.168.90.165:55751/cartas/InsertFirma64", content_firma);
                switch (response_firma.StatusCode)
                {
                    case (System.Net.HttpStatusCode.OK):
                        if (response_firma.IsSuccessStatusCode)
                        {
                            await DisplayAlert("Exito ! ", " Frima agregada con éxito. ", "Ok");
                            Application.Current.MainPage = new MainPage();
                        }
                        else
                        {
                            await DisplayAlert("Error ! ", "Hubo un error en la insert de datos. ", "Ok");
                        }
                        break;
                    case (System.Net.HttpStatusCode.InternalServerError):
                        await DisplayAlert("Error ! ", "Hubo un error en el servidor, notifique al depo. de sistemas ", "Ok");
                        break;
                    case (System.Net.HttpStatusCode.Forbidden):
                        await DisplayAlert("Error ! ", "Hubo un error en la insert de datos. ", "Ok");
                        break;

                }

                

            }

            // cartas/InsertVisita

        }
        private async void btnCamara_Clicked(object sender, EventArgs e)
        {

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "Evidencia_vivienda.jpg"
            });
            if (file == null)
            return;
           // await DisplayAlert("File Location Error", "Error parece que hubo un problema con la camara, confirme espacio en memoria o notifique a sistemas", "OK");

            imgx.Source = ImageSource.FromStream(() => {    
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });

            
            // await App.MasterD.Detail.Navigation.PushAsync(new Photox());
        }
        private void ErrorBtn_Clicked(object sender, EventArgs e)
        {
            ErrorBtn.IsVisible = false;
            ErrorLbl.IsVisible = false;
        }
        private void signBtn_Clicked(object sender, EventArgs e)
        {
            signBtn.IsVisible = false;
            Sign.IsVisible = true;
            hidesignBtn.IsVisible = true;
        }
        private void hidesignBtn_Clicked(object sender, EventArgs e)
        {
            signBtn.IsVisible = true;
            Sign.IsVisible = false;
            hidesignBtn.IsVisible = false;
        }
    }

}
 