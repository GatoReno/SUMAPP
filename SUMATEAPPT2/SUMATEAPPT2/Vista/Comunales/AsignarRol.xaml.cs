using Newtonsoft.Json;
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
        private string index_prospecto;

        public AsignarRol(string id,string name,string index)
        {
            InitializeComponent();
            _ = getRoles();
            id_user.Text = id;
            UserName.Text = name;
            index_prospecto = index;

        }

        private void PickerRoles_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            try
            {
                var i = (Roles)PickerRoles.SelectedItem;
                var id = i.id.ToString();
                id_rol.Text = id.ToString();
                name_rol.Text = i.rol;
                bntAddRol.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("", "_" + ex.ToString() + " _ ", "ok");
            }

        }
        private async Task getRoles()
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
                            PickerRoles.SelectedIndexChanged += PickerRoles_SelectedIndexChanged;
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

        private async void bntAddRol_Clicked(object sender, EventArgs e)
        {
            Cator.IsRunning = true;
            Cator.IsVisible = true;

            var uri = "http://192.168.90.165:55751/Configuracion/InsertRoleProspectoGpo";
            HttpClient client = new HttpClient();

            var value_check = new Dictionary<string, string>
                         {
                            { "id_rol_grupo" , id_rol.Text },
                            { "id_usuario" , id_user.Text },
                            { "index_prospecto" , index_prospecto } 
                         };

            var content = new FormUrlEncodedContent(value_check);
            var response = await client.PostAsync(uri, content);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.InternalServerError:
                    Console.WriteLine("----------------------------------------------_____:Here status 500");
                    break;

                case System.Net.HttpStatusCode.OK:
                    Console.WriteLine("----------------------------------------------_____:Here status 200");
                    try
                    {
                        HttpContent content_ = response.Content;                 
                        var json = await content_.ReadAsStringAsync();

                        var result = JsonConvert.DeserializeObject<RespSimple>(json);

                        if (result.resp == "Ya existente")
                        {
                            lbl1.Text = "El rol de este usuario ya existe, debió haber un error, de hacer falta reporte con sistemas";
                            //await Navigation.PopAsync();
                        } else if(result.resp == "OK")
                        {
                            await DisplayAlert("Exito",
                                  "Rol asignado a usuario con éxito", "ok");
                            await Navigation.PopAsync();
                        }
                        else {
                            await DisplayAlert("Posible Error",
                                "Pudo haber un error , por favor confirme en portal web", "ok");

                        }
                        
                        Cator.IsRunning = false;
                        Cator.IsVisible = false;

                    }
                    catch (Exception ex)
                    {                        
                        var x = ex.ToString();
                        await DisplayAlert("", "" + ex.ToString(), "ok");
                        Cator.IsRunning = false;
                        Cator.IsVisible = false;
                        return;
                    }
                    break;
            }

        }
    }
}