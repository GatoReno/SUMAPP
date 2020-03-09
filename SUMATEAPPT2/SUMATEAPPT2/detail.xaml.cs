using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.LocalNotifications;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SUMATEAPPT2.Interfaces;
using SUMATEAPPT2.Modelos;
using SUMATEAPPT2.Modelos.JWS;
using SUMATEAPPT2.Vista;
using SUMATEAPPT2.Vista.Visitas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUMATEAPPT2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class detail : ContentPage
    {
        public UserDb userDb;
        public detail()
        {
            InitializeComponent();
            CheckConnectivity();
            userDb = new UserDb();
            var visits = userDb.GetMembers_visitas();
            if (visits.Count() > 0)
            {
                visitsView.ItemsSource = visits;
                ErrorVisitis.IsVisible = false;
            }
            else {
                ErrorVisitis.IsVisible = true;
                ErrorVisitis.Text = "No tienes visitas pendientes.";
            }
        }

        public   void btnNotification_Clicked(object sender, EventArgs e)
        {
              CrossLocalNotifications.Current.Show("Financiera", "Notifiacion Local");

        }
        public async void CheckConnectivity()
        {

            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Advertencia ! ", "ACTIVAR  CONSUMO DE DATOS O WIFI  PARA PODER CONTINUAR ", "Ok");
                //OnAppearing();
            }
        }
        private async void btnConnectivity_Clicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Advertencia ! ", "ACTIVAR  CONSUMO DE DATOS O WIFI  PARA PODER CONTINUAR ", "Ok");
                //OnAppearing();
            }
        }
        async void visitsView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var cv = e.Item as Cvisita;

            //await DisplayAlert("Item Tapped", "An item was tapped. "+ cv.cliente, "OK");
            //VisitaInfo
            await App.MasterD.Detail.Navigation.PushAsync(new VisitaOffLine(cv.id));
             //Deselect Item
            ((ListView)sender).SelectedItem = null;

        }

        // Connectivity();
        // getEstados();
        //getProductos();
        /*
  private async void getColonias(string id_estado,string id_municipio) {
      if (CrossConnectivity.Current.IsConnected)
      {
          var uri = "http://192.168.90.165:55751/utilidades/Colonias/?estado="+ id_estado + "&municipio=" + id_municipio+""; 
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
  private   async void getMunicipios(string id_estado) {
      if (CrossConnectivity.Current.IsConnected)
      {
          var uri = "http://192.168.90.165:55751/utilidades/municipios/?estado=" + id_estado   + "";

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
  private async void getProductos() {
      if (CrossConnectivity.Current.IsConnected)
      {
          var uri = "http://192.168.90.165:55751/utilidades/productos";
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
  }
  private async void  getEstados() {
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
        /*         var result = JsonConvert.DeserializeObject<Estados>(json);
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
      var id =  i.CLAVE_E.ToString();
      estados_id.Text = id.ToString(); 
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

     getColonias(id_e,id_m);

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
    colonia_id.Text = id.ToString();


}
catch (Exception ex)
{
    DisplayAlert("", "_" + ex.ToString() + " _ ", "ok");
} 

}
private void PickerProductos_SelectedIndexChanged(object sender, EventArgs e)
{
try
{
    var i = (Table)PickerProductos.SelectedItem;
    var id = i.CLAVE_P.ToString();
    producto_id.Text = id.ToString();
    // getMunicipios(id);

}
catch (Exception ex)
{
    DisplayAlert("", "_" + ex.ToString() + " _ ", "ok");
}
}*/
    }
}