using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intermodular
{
    public class Familia
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string id_client { get; set; }

        public static Familia currentFamilia;
        public static List<Familia> clientFamilias;

        public Familia(string name)
        {
            this.name = name;
            this.id_client = Client.currentClient._id;
        }


        




    }
}
