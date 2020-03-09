using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SUMATEAPPT2.Modelos.JWS
{
    public class Table
    {
        [JsonProperty(PropertyName = "CLAVE_E")]
        public int CLAVE_E { get; set; }
       
        [JsonProperty(PropertyName = "CLAVE_S")]
        public int CLAVE_S { get; set; }
        [JsonProperty(PropertyName = "CLAVE_M")]
        public int CLAVE_M { get; set; }
        [JsonProperty(PropertyName = "CLAVE_C")]
        public int CLAVE_C { get; set; }
        [JsonProperty(PropertyName = "CLAVE_P")]
        public int CLAVE_P { get; set; }

        [JsonProperty(PropertyName = "ESTADO")]
        public string ESTADO { get; set; }
        [JsonProperty(PropertyName = "CP")]
        public string CP { get; set; }
        [JsonProperty(PropertyName = "PRODUCTO")]
        public string PRODUCTO { get; set; }
        [JsonProperty(PropertyName = "MUNICIPIO")]
        public string MUNICIPIO { get; set; }
        [JsonProperty(PropertyName = "SUUCRSAL")]
        public string SUUCRSAL { get; set; }
        [JsonProperty(PropertyName = "COLONIA")]
        public string COLONIA { get; set; }
        

    }

    public class Estados
    {
        public List<Table> Table { get; set; }
    }
}
