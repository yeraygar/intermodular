using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intermodular
{
    public class Ticket
    {
        public string _id { get; set; }
        public string tipo_ticket { get; set; }
        public string id_user_que_abrio { get; set; }
        public string id_user_que_cerro { get; set; }
        public string id_client { get; set; }
        public string id_table { get; set; }
        public string name_table { get; set; }
        public int comensales { get; set; }
        public float total { get; set; }
        public DateTime date { get; set; }
        public Boolean cobrado { get; set; }

        public static Ticket currentTicket;
        public static List<Ticket> openTickets;
        public static List<Ticket> clientTickets;

        public Ticket(int comensales)
        {
            this.comensales = comensales;
            this.id_user_que_abrio = User.currentUser._id;
            this.id_table = Mesa.currentMesa._id;
            this.name_table = Mesa.currentMesa.name;
            this.cobrado = false;
            this.date = DateTime.Now;
            this.id_client = Client.currentClient._id;

        }
    }
}
