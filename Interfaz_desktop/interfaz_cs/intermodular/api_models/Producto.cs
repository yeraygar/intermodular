using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
        public string comentario { get; set; }

        public static Producto currentProduct;
        public static Producto currentTicketLine;
        public static List<Producto> ticketLines;
        public static List<Producto> clientProducts;
        public static List<Producto> ticketProducts;
        public static List<Producto> familyProducts;

        ///<summary> Constructor para crear nuevos productos desde Admin</summary>
        public Producto(string name, float precio, int stock, string id_familia)
        {
            if (stock < 0) stock = 0;
            this.name = name;
            this.precio = precio;
            this.stock = stock;
            id_client = Client.currentClient._id;
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
            //this.id_ticket = Ticket.currentTicket._id;
            this.comentario = p.comentario;
        }
        public Producto()
        {

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
                { "precio", producto.precio },
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

        public static async Task<bool> updateProduct(Producto producto,string id)
        {
            string url = $"{Staticresources.urlHead}product/{id}";

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

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PutAsync(url, content);

            return httpResponse.IsSuccessStatusCode;
        }

        public static async Task<bool> deleteProduct(string id)
        {
            string url = $"{Staticresources.urlHead}product/{id}";

            HttpResponseMessage httpResponse = await Staticresources.httpClient.DeleteAsync(url);

            if (httpResponse.IsSuccessStatusCode) return true;
            else return false;
        }

        /*********************************************LINEA TICKETS************************************************/

        public static async Task getAllTicketLinesFromTicket(string id)
        {
            string url = $"{Staticresources.urlHead}ticket_line/ticket/{id}";
            var httpResponse = Staticresources.httpClient.GetAsync(url);
            await httpResponse;

            if (httpResponse.Result.IsSuccessStatusCode)
            {
                var content = await httpResponse.Result.Content.ReadAsStringAsync();
                List<Producto> allLines = JsonSerializer.Deserialize<List<Producto>>(content);

                ticketLines = allLines;
            }
        }

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
            bool retorno = false;
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
            HttpContent content;
            HttpResponseMessage httpResponse;
            if (ticketLines != null)
            {
                bool found = false;
                for (int x = 0; x < ticketLines.Count && !found; x++)
                {
                    if (ticketLines[x].name.Equals(producto.name))
                    {
                        Console.WriteLine(ticketLines[x].cantidad);
                        ticketLines[x].cantidad = ticketLines[x].cantidad + 1;
                        await updateLineTicket(ticketLines[x]);
                        Producto.currentTicketLine = ticketLines[x];
                        Producto.currentTicketLine.total += producto.precio; 
                        found = true;
                    }
                }
                
                if (!found)
                {
                    content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");
                    httpResponse = await Staticresources.httpClient.PostAsync(url, content);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = await httpResponse.Content.ReadAsStringAsync();
                        var postResult = JsonSerializer.Deserialize<Producto>(result);
                        currentTicketLine = postResult;
                        ticketLines.Add(currentTicketLine);
                        retorno = true;
                    }
                    else
                    {
                        retorno = false;
                    }

                }
            }
            else
            {
                content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");
                httpResponse = await Staticresources.httpClient.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    var postResult = JsonSerializer.Deserialize<Producto>(result);
                    currentTicketLine = postResult;
                    ticketLines = new List<Producto>();
                    ticketLines.Add(currentTicketLine);
                    retorno = true;
                }
                else
                {
                    retorno = false;
                }
            }

            return retorno;
        }

        public static async Task<bool> updateLineTicket(Producto producto)
        {
            string url = $"{Staticresources.urlHead}ticket_line/{producto._id}";

            JObject values = new JObject
            {
                { "name", producto.name },
                { "id_client", producto.id_client },
                { "cantidad", producto.cantidad },
                { "precio", producto.precio },
                { "total", producto.cantidad * producto.precio },
                { "stock", producto.stock },
                { "id_familia", producto.id_familia },
                {"comentario",producto.comentario }
            };

            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PutAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                string result = await httpResponse.Content.ReadAsStringAsync();
                /*currentProduct.name = producto.name;
                currentProduct.id_client = producto.id_client;
                currentProduct.cantidad = producto.cantidad;
                currentProduct.precio = producto.precio;
                currentProduct.total = producto.precio * producto.cantidad;
                currentProduct.stock = producto.stock;
                currentProduct.id_familia = producto.id_familia;
                currentProduct.comentario = producto.comentario;*/

                return true;
            }
            else return false;
        }

        public static async Task<bool> deleteTicketLine(string id)
        {
            string url = $"{Staticresources.urlHead}ticket_line/{id}";

            HttpResponseMessage httpResponse = await Staticresources.httpClient.DeleteAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                bool found = false;
                for(int x = 0; x < ticketLines.Count && !found; x++)
                {
                    if(ticketLines[x]._id.Equals(id))
                    {
                        found = true;
                        ticketLines.RemoveAt(x);
                    }
                }
                return true;
            }
            else return false;
        }

        public static async Task<bool> deleteAllFamilyProducts(string id_familia)
        {
            string url = $"{Staticresources.urlHead}product/family/{id_familia}";
            HttpResponseMessage httpResponse = await Staticresources.httpClient.DeleteAsync(url);
            return httpResponse.IsSuccessStatusCode;  //Esto ya retorna true o false.
        }

        public static async Task<bool> deleteAllTicketLinesFromTicket(string id_ticket)
        {
            string url = $"{Staticresources.urlHead}ticket_line/ticket/{id_ticket}";
            HttpResponseMessage httpResponse = await Staticresources.httpClient.DeleteAsync(url);
            if(httpResponse.IsSuccessStatusCode)
            {
                ticketLines = null;
                return true;
            }else
            {
                return false;
            }
        }



    }
}
