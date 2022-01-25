using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intermodular
{
    public class Mesa
    {
        public string name { get; set; }
        public int numero_mesa { get; set; }
        public bool status { get; set; }
        public string id_zone { get; set; }
        public int comensales { get; set; }
        public string id_user { get; set; }

        //falta public string List<Producto> cuenta {get; set;}


        public Mesa() { }

        public Mesa(string name, int numero_mesa, bool status, string id_zone, int comensales, string id_user)
        {
            this.name = name;
            this.numero_mesa = numero_mesa;
            this.status = status;
            this.comensales = comensales;
            this.id_zone = id_zone;
            this.id_user = id_user;
        }
    }
}
