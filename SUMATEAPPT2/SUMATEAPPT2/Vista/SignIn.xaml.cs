using Newtonsoft.Json;
using SQLite;
using SUMATEAPPT2.Interfaces;
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
    public partial class SignIn : ContentPage
    {

        public  UserFS user;
        public UserDb UserDb;
        public SQLiteConnection conn;
        public SignIn()
        {
            InitializeComponent();
        }



        private async void Button_Clicked(object sender, EventArgs e)
        {
            Cator.IsVisible = true;
            Loginbtn.IsVisible = false;
            var xn = User.Text;
            var xpass = Pass.Text;
            UserDb = new UserDb();
            var users = UserDb.GetMembers();
            var uCount = users.Count();
            int RowCount = 0;

            if (string.IsNullOrWhiteSpace(xn))
            {
                User.Focus();
            }
            else if (string.IsNullOrWhiteSpace(xpass))
            {
                Pass.Focus();
            }
            else {

                RowCount = Convert.ToInt32(uCount);
                if (RowCount > 0)
                {
                    Cator.IsVisible = false;
                    await DisplayAlert("Ya tenemos datos ! ", "YA EXISTE USUSARIO ", "Ok");
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    Errormsn.IsVisible = false;

                    var uri = "http://192.168.90.165:55751/Account/LoginAPP";
                    HttpClient client = new HttpClient();

                    var value_check = new Dictionary<string, string>
                         {
                            { "username", xn},
                            {"passw" , xpass }
                         };

                    try
                    {
                        var content = new FormUrlEncodedContent(value_check);
                        var response = await client.PostAsync(uri, content);


                        switch (response.StatusCode)
                        {
                            case (System.Net.HttpStatusCode.OK):

                                try
                                {
                                    HttpContent resp_content = response.Content;

                                    var json = await resp_content.ReadAsStringAsync();
                                    var userResult = JsonConvert.DeserializeObject<List<UserFS>>(json);
                                    if (userResult[0].Mensaje == "Error al Iniciar Sesión")
                                    {
                                        User.Focus();
                                        Pass.Focus();
                                        Errormsn.IsVisible = true;
                                        Errormsn.Text = "Usuario o contraseña invalidos";
                                    }
                                    else
                                    {
                                        var userFS = new UserFS();
                                        userFS.Nombre = xn;
                                        userFS.Email = userResult[0].Email;
                                        userFS.Id = userResult[0].Id;
                                        userFS.Sucursal = userResult[0].Sucursal;
                                        UserDb.AddMember(userFS);
                                        Application.Current.MainPage = new MainPage();

                                    }
                                }
                                catch (Exception ex)
                                {
                                    await DisplayAlert("", "" + ex.ToString(), "ok");
                                    var x = ex.ToString();
                                }

                                break;
                        }

                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", "Intente en otro momento _ error: " + ex.ToString() + " _ ", "ok");
                        Cator.IsVisible = false;
                        Pass.Focus();
                        Errormsn.IsVisible = true;
                         Errormsn.Text = "Ha habido un error";
                        return;
                    }
                    Cator.IsVisible = false;
                    Loginbtn.IsVisible = true;


                }

            }
        }
    }
}