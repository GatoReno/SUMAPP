using SUMATEAPPT2.Interfaces;
using SUMATEAPPT2.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace SUMATEAPPT2.Vista.Visitas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisitaOffLine : ContentPage
    {
        public UserDb userDb;
        public IEnumerable<Cvisita> vi;
        public VisitaOffLine(int id)
        {
            InitializeComponent();

            var intx = id;
            userDb = new UserDb();
            var visita = userDb.GetMember_VisitaOffLine(intx).ToList();
            cliente.Text = visita[0].cliente ;
            Fecha.Text = visita[0].fecha;
            calle.Text = visita[0].calle +"  "+visita[0].numero + "  " + visita[0].colonia;
            ciudad.Text = visita[0].ciudad + " " + visita[0].estado;
            var lon = visita[0].longitud;
            var lat = visita[0].latitud;

            if (lon == null || lat == null)
            {
                ErrorMap.IsVisible = true;
                ErrorMap.Text = "Error al guardar ubicación, no hay coordenadas.";
            }
            else {
                var lonx = visita[0].longitud.Remove(0, 10);
                var latx = visita[0].latitud.Remove(0, 9);
                ErrorMap.IsVisible = false;
                callMap(lonx, latx, visita[0].cliente);
            }


            
        }

        public void callMap(string lon, string lat, string cliente) {


            Mapx.MoveToRegion(
           MapSpan.FromCenterAndRadius(
           new Position(Convert.ToDouble(lat), Convert.ToDouble(lon)), Distance.FromMiles(1)));


            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(Convert.ToDouble(lat), Convert.ToDouble(lon)),
                Label = "Ubicacion de cliente "+cliente,
                Address = "Direccion proporcionada",

            };
            Mapx.Pins.Add(pin);

        }
    }
}