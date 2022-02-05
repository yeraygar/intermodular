using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json.Linq;
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
        public static Producto currentTicketLine;
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

        public static async Task<bool> getClientProducts(string id)
        {
            string url = $"{Staticresources.urlHead}product/client/{id}";
            HttpResponseMessage httpResponse = await Staticresources.httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                List<Producto> productos = JsonSerializer.Deserialize<List<Producto>>(content);
                clientProducts = productos;
                return true;
            }
            else return false;
        }

        public static async Task<bool> getFamilyProducts(string id)
        {
            string url = $"{Staticresources.urlHead}product/family/{id}";
            HttpResponseMessage httpResponse = await Staticresources.httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                List<Producto> productos = JsonSerializer.Deserialize<List<Producto>>(content);
                familyProducts = productos;
                return true;
            }
            else return false;
        }

        public static async Task<bool> getProductById(string id)
        {
            string url = $"{Staticresources.urlHead}/product/{id}";
            HttpResponseMessage httpResponse = await Staticresources.httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                Producto producto = JsonSerializer.Deserialize<Producto>(content);
                currentProduct = producto;
                return true;
            }
            return false;
        }

        public static async Task<bool> createProduct(Producto producto)
        {
            string url = $"{Staticresources.urlHead}/product";

            JObject values = new JObject
            {
                { "name", producto.name },
                { "id_client", producto.id_client },
                { "cantidad", producto.cantidad },
                { "precio", producto.precio },
                { "total", producto.cantidad * producto.precio },
                { "stock", producto.stock },
                { "id_familia", producto.id_familia }
            };

            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PostAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                string result = await httpResponse.Content.ReadAsStringAsync();
                currentProduct = JsonSerializer.Deserialize<Producto>(result);
                return true;
            }
            return false;
        }

        public static async Task<bool> updateProduct(Producto producto)
        {
            string url = $"{Staticresources.urlHead}product/{currentProduct._id}";

            JObject values = new JObject
            {
                { "name", producto.name },
                { "id_client", producto.id_client },
                { "cantidad", producto.cantidad },
                { "precio", producto.precio },
                { "total", producto.cantidad * producto.precio },
                { "stock", producto.stock },
                { "id_familia", producto.id_familia }
            };

            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PostAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                string result = await httpResponse.Content.ReadAsStringAsync();
                currentProduct.name = producto.name;
                currentProduct.id_client = producto.id_client;
                currentProduct.cantidad = producto.cantidad;
                currentProduct.precio = producto.precio;
                currentProduct.total = producto.precio * producto.cantidad;
                currentProduct.stock = producto.stock;
                currentProduct.id_familia = producto.id_familia;

                return true;
            }
            else return false;
        }

        public static async Task<bool> deleteProduct(string id)
        {
            string url = $"{Staticresources.urlHead}product/{id}";

            HttpResponseMessage httpResponse = await Staticresources.httpClient.DeleteAsync(url);

            if (httpResponse.IsSuccessStatusCode) return true;
            else return false;
        }

    /*********************************************LINEA TICKETS************************************************/

        public static async Task<bool> getTicketProducts(string id)
        {
            string url = $"{Staticresources.urlHead}ticket_line/ticket/{id}";
            HttpResponseMessage httpResponse = await Staticresources.httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                List<Producto> productos = JsonSerializer.Deserialize<List<Producto>>(content);
                ticketProducts = productos;
                return true;
            }
            else return false;
        }

        public static async Task<bool> getLineTicketById(string id)
        {
            string url = $"{Staticresources.urlHead}/ticket_line/{id}";
            HttpResponseMessage httpResponse = await Staticresources.httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                Producto producto = JsonSerializer.Deserialize<Producto>(content);
                currentTicketLine = producto;
                return true;
            }
            return false;
        }

        public static async Task<bool> createLineTicket(Producto producto)
        {
            string url = $"{Staticresources.urlHead}/ticket_line";

            JObject values = new JObject
            {
                { "name", producto.name },
                { "id_client", producto.id_client },
                { "id_ticket", Ticket.currentTicket._id },
                { "cantidad", producto.cantidad },
                { "precio", producto.precio },
                { "total", producto.cantidad * producto.precio },
                { "stock", producto.stock },
                { "id_familia", producto.id_familia }
            };

            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PostAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                string result = await httpResponse.Content.ReadAsStringAsync();
                currentTicketLine = JsonSerializer.Deserialize<Producto>(result);
                return true;
            }
            return false;
        }

        public static async Task<bool> updateLineTicket(Producto producto)
        {
            string url = $"{Staticresources.urlHead}ticket_line/{currentTicketLine._id}";

            JObject values = new JObject
            {
                { "name", producto.name },
                { "id_client", producto.id_client },
                { "cantidad", producto.cantidad },
                { "precio", producto.precio },
                { "total", producto.cantidad * producto.precio },
                { "stock", producto.stock },
                { "id_familia", producto.id_familia }
            };

            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PostAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                string result = await httpResponse.Content.ReadAsStringAsync();
                currentProduct.name = producto.name;
                currentProduct.id_client = producto.id_client;
                currentProduct.cantidad = producto.cantidad;
                currentProduct.precio = producto.precio;
                currentProduct.total = producto.precio * producto.cantidad;
                currentProduct.stock = producto.stock;
                currentProduct.id_familia = producto.id_familia;

                return true;
            }
            else return false;
        }

        public static async Task<bool> deleteTicketLine(string id)
        {
            string url = $"{Staticresources.urlHead}ticket_line/{id}";

            HttpResponseMessage httpResponse = await Staticresources.httpClient.DeleteAsync(url);

            if (httpResponse.IsSuccessStatusCode) return true;
            else return false;
        }





    }
}
