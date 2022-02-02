using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intermodular
{
    public class Producto
    {
        public string _id { get; set; }
        public string name { get; set; }
        public int cantidad { get; set; }
        public float precio { get; set; }
        public float total { get; set; }
        public int stock { get; set; }
        public string id_familia { get; set; }
        public string id_client { get; set; }
        public string id_ticket { get; set; }

        public static Producto currentProduct;
        public static List<Producto> clientProducts;
        public static List<Producto> ticketProducts;
        public static List<Producto> familyProducts;

        ///<summary> Constructor para crear nuevos productos desde Admin</summary>
        public Producto(string name, int cantidad, float precio, int stock, string id_familia)
        {
            if (cantidad <= 0) cantidad = 1;
            if (stock < 0) stock = 0;
            this.name = name;
            this.cantidad = cantidad;
            this.precio = precio;
            this.stock = stock;
            this.id_client = Client.currentClient._id;
            this.id_familia = id_familia;
        }

        ///<summary> Constructor para almacenar los productos al ticar en una mesa</summary>
        public Producto(Producto p)
        {
            this.name = p.name;
            this.cantidad = p.cantidad;
            this.precio = p.precio;
            this.total = p.precio * p.cantidad;
            this.id_client = p.id_client;
            this.id_familia = p.id_familia;
            this.id_ticket = Ticket.currentTicket._id;
        }



    }
}
