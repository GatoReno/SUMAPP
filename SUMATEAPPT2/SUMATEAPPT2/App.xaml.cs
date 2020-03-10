using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.LocalNotifications;
using SQLite;
using SUMATEAPPT2.Interfaces;
using SUMATEAPPT2.Modelos;
using SUMATEAPPT2.Test;
using SUMATEAPPT2.Vista;
using SUMATEAPPT2.Vista.Comunales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUMATEAPPT2
{
    public partial class App : Application
    {
        public UserDb userDb;
        public static MasterDetailPage MasterD { get; set; }
        public App()
        {
            InitializeComponent();

            //MainPage = new SignIn();
          
            MainPage =  //new EvEcoClienteEmprendedor();
                        new SplashPage();
                       //new NavigationPage(new SplashPage());

           // new TPage();
        }

        

        protected async   override void OnStart()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                userDb = new UserDb();
                var visits = userDb.GetMembers_visitas();
                var nv = visits.Count();

                HttpClient client = new HttpClient();

                if (nv > 0)
                {
                    foreach (var visoff in visits)
                    {
                        var lnx = visoff.longitud;
                        var lty = visoff.latitud;

                        if (lnx != null || lty != null)
                        {
                          

                            try
                            {
                                var value_check = new Dictionary<string, string>
                         {
                            {"id_documento" , visoff.id_documento.ToString() },
                            { "id_sucursal" , visoff.id_sucursal.ToString() },
                            { "id_cliente" ,  visoff.id_cliente.ToString() },
                            { "id_asesor" ,  visoff.id_asesor.ToString() },
                            { "cliente" , visoff.cliente.ToString() },
                            { "sucursal" ,  visoff.sucursal.ToString() },
                            {"tipo_carta" ,  "tipo guardada sin datos moviles" },
                            {"fecha" ,  visoff.fecha.ToString() },
                            {"secuencia" , " 1 " },
                            {"respuesta1" , " " },
                            {"respuesta2" ,  " " },
                            {"respuesta3" ,  " " },
                            {"respuesta4" , " " },
                            {"respuesta5" ,  " " },
                            {"respuesta6" ,  " " },
                            {"respuesta7" ,  " " },
                            {"respuesta8" ,  " " },
                            {"respuesta9" ,  " " },
                            {"observaciones" , visoff.observaciones.ToString() },
                            {"pais", "Mexico"},
                            { "colonia_id"," 1 " },
                            { "municipio_id"," 1 " },
                            { "ciudad",  visoff.ciudad.ToString() },
                            { "estado", "2" },
                            { "cp_id", visoff.cp.ToString() },
                            { "numero", visoff.numero.ToString() },
                            { "calle", visoff.calle.ToString() },
                            { "localidad", "localidad" },
                            { "referencia" , "referncia"},
                            { "tipo" , "tipo guardada sin datos moviles"},
                            {"latitud", visoff.longitud.ToString() },
                            {"longitud", visoff.latitud.ToString() },
                            {"latD", visoff.latitud.ToString() },
                            {"lngD", visoff.longitud.ToString() },

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


                                            Console.WriteLine(" --------------------------------------- Exito !   Exito agregando datos. ");
                                            Application.Current.MainPage = new MainPage();
                                        }
                                        else
                                        {
                                            Console.WriteLine(" --------------------------------------- Error !  Hubo un error en la insert de datos.  Ok");
                                        }
                                        break;
                                    case (System.Net.HttpStatusCode.Forbidden):
                                        Console.WriteLine(" --------------------------------------- Error 403  Hubo un error en la insert de datos.  Ok");
                                        break;


                                    case (System.Net.HttpStatusCode.NotFound):
                                        CrossLocalNotifications.Current.Show("Sumate Actualizado", "Ha habido un problema con el servidor por favor confirme sus datos guardados");
                                        break;

                                }
                            }
                            catch (Exception)
                            {

                                CrossLocalNotifications.Current.Show("Error", "No se encuentra el servidor contacte a sistema");
                            }

                 

                            try
                            {
                                var value_check_firma = new Dictionary<string, string>
                                     {
                                        {"id_documento" , "11111" },
                                        { "id_testigos" , "11111" },
                                        { "nombre" ,  visoff.cliente },
                                        { "app" ,  "Testing APP" },
                                        { "apm" ,  "Testing APM" },
                                        { "cargo" ,  "Testing CARGO" },
                                        { "firma" ,  visoff.responsable_firma }

                                      };
                                var content_firma = new FormUrlEncodedContent(value_check_firma);
                                var response_firma = await client.PostAsync("http://192.168.90.165:55751/cartas/InsertFirma64", content_firma);
                                switch (response_firma.StatusCode)
                                {
                                    case (System.Net.HttpStatusCode.OK):
                                        if (response_firma.IsSuccessStatusCode)
                                        {
                                            userDb.DeleteMember_visitas(visoff.id);
                                            CrossLocalNotifications.Current.Show("Sumate Actualizado", "Los datos que guardaste fuera de linea ya han sido enviados a platadorma");
                                            Console.WriteLine(" --------------------------------------- Exito !   visita guardada FIRMA off line ya insertada en servidor / agregando datos. ");

                                        }
                                        else
                                        {
                                            Console.WriteLine(" --------------------------------------- Error !  Hubo un error en la insert de datos.  Ok");
                                        }
                                        break;
                                    case (System.Net.HttpStatusCode.InternalServerError):
                                        Console.WriteLine(" --------------------------------------- Error 500 !  Hubo un error en la insert de datos.  Ok");
                                        break;
                                    case (System.Net.HttpStatusCode.Forbidden):
                                        Console.WriteLine(" --------------------------------------- Error 403 !  Hubo un error en la insert de datos.  Ok");
                                        break;

                                }


                            }
                            catch (Exception)
                            {

                                CrossLocalNotifications.Current.Show("Error", "No se encuentra el servidor contacte a sistema");
                            }
                        }
                        else {


                            CrossLocalNotifications.Current.Show("Error", "Una de tus visitas no cuenta con la ubicación");
                        }

                    }
                }

            }
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
