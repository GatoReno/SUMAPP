using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SUMATEAPPT2.Modelos
{
    public class Cvisita
    {
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }
        public string documento { get; set; }
        public int sucursal { get; set; }
        public string cliente { get; set; }
        public int asesor { get; set; }
        public string fecha { get; set; }
        public int secuencia { get; set; }
        public string respuesta1 { get; set; }
        public string respuesta2 { get; set; }
        public string respuesta3 { get; set; }
        public string respuesta4 { get; set; }
        public string respuesta5 { get; set; }
        public string respuesta6 { get; set; }
        public string respuesta7 { get; set; }
        public string respuesta8 { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string latD { get; set; }
        public string lngD { get; set; }
        public string calle { get; set; }
        public string numero { get; set; }
        public string pais { get; set; }
        public string colonia { get; set; }
        public string municipio { get; set; }
        public string ciudad { get; set; }
        public string estado { get; set; }
        public string cp { get; set; }
        public string localidad { get; set; }
        public string referencia { get; set; }
        public string tipo { get; set; }


        public int id_carta { get; set; }
        public int id_sucursal { get; set; }
         public int id_cliente { get; set; }
         public int id_asesor { get; set; }
        public int id_documento { get; set; }
         public string tipo_carta { get; set; }
        public string lugar { get; set; }
         public string folio { get; set; }
        public string antiguedad_conocer { get; set; }
        public string monto { get; set; }
        public string actividad_negocio { get; set; }
        public string denominacion_institucion { get; set; }
        public string tipo_vehiculo { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string ann { get; set; }
        public string numero_serie { get; set; }
   
        public string respuesta9 { get; set; }
        public string respuesta10 { get; set; }
        public string respuesta11 { get; set; }
        public string respuesta12 { get; set; }
        public string respuesta13 { get; set; }
        public string nombre_acreeditado { get; set; }
        public string nombre_testigo { get; set; }
        public string visita_res1 { get; set; }
        public string visita_res_describe { get; set; }
        public string visita_res2 { get; set; }
        public string visita_res3 { get; set; }
        public string visita_res4 { get; set; }
        public string visita_res5 { get; set; }
        public string visita_res6 { get; set; }
        public string visita_res7 { get; set; }
        public string visita_res8 { get; set; }
        public string visita_describe { get; set; }
        public string foto_domicilio { get; set; }
  
        public string responsable_nombre { get; set; }
        public string responsable_lugar { get; set; }
        public string resposnable_fecha { get; set; }
        public string responsable_firma { get; set; }
 
        public string ubicacion { get; set; }
        public string observaciones { get; set; }
        public string img_visita { get; set; }
    }
}
