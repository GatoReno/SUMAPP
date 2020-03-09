using System;
using System.Collections.Generic;
using System.Text;

namespace SUMATEAPPT2.Modelos
{
 

    public class CProspecto
    {
        public int id_sucursal { get; set; }
        public int id_asesor { get; set; }
        public int id_producto { get; set; }

        public int id_prospecto { get; set; }
        public string index_prospecto { get; set; }
        public string fecha_visita_tentativa { get; set; }
        public string nombre { get; set; }
        public string app { get; set; }
        public string apm { get; set; }
        public string fecha { get; set; }
        public string telefono { get; set; }
        public string calle { get; set; }
        public string numero { get; set; }
        public string direccion_full { get; set; }
        public int colonia { get; set; }
        public int cp { get; set; }
        public int municipio { get; set; }
        public int estado { get; set; }
        public string actividad_negocio { get; set; }
        public string tipo_producto { get; set; }
        public string resolucion { get; set; }
        public string modo_enterado { get; set; }
    }

 
}
