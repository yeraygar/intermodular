using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace intermodular.api_models
{
    class TicketLine
    {
        public string name { get; set; }
        public int cantidad { get; set; }
        public float precio { get; set; }
        public int stock { get; set; }
        public float total { get; set; }
        public string id_client { get; set; }
        public string id_familia { get; set; }
        public string id_ticket { get; set; }
        public string comentario { get; set; }

        public static List<TicketLine> LinesFromTicket;
        public static TicketLine currentTicketLine;

        public TicketLine()
        {

        }
        public TicketLine(Producto producto)
        {
            name = producto.name;
            cantidad = producto.cantidad;
            precio = producto.precio;
            stock = producto.stock;
            total = producto.total;
            id_client = producto.id_client;
            id_familia = producto.id_familia;
            id_ticket = producto.id_ticket;
        }

        //Obtener todas las líneas de un ticket
        public static async Task getAllTicketLinesFromTicket(string id)
        {
            string url = $"{Staticresources.urlHead}ticket_line/ticket/{id}";
            var httpResponse = Staticresources.httpClient.GetAsync(url);
            await httpResponse;

            if (httpResponse.Result.IsSuccessStatusCode)
            {
                var content = await httpResponse.Result.Content.ReadAsStringAsync();
                List<TicketLine> allLines = JsonSerializer.Deserialize<List<TicketLine>>(content);

                LinesFromTicket = allLines;
            }
        }

        //Obtener una línea de Ticket
         public static async Task getTicketLine(string id)
        {
            string url = $"{Staticresources.urlHead}ticket_line/{id}";
            var httpResponse = Staticresources.httpClient.GetAsync(url);
            await httpResponse;

            if (httpResponse.Result.IsSuccessStatusCode)
            {
                var content = await httpResponse.Result.Content.ReadAsStringAsync();
                TicketLine line = JsonSerializer.Deserialize<TicketLine>(content);
                currentTicketLine = line;
            }
        }

        //Crear Linea de producto
        public static async Task<TicketLine> createTicketLine(Producto p)
        {
            string url = $"{Staticresources.urlHead}ticket_line";

            //Creamos objeto tipo JSon
            var values = new JObject();
            values.Add("name", p.name);
            values.Add("cantidad", p.cantidad);
            values.Add("precio", p.precio);
            values.Add("stock", p.stock);
            values.Add("total", p.total);
            values.Add("id_client", p.id_client);
            values.Add("id_familia", p.id_familia);
            values.Add("id_ticket", p.id_ticket);
          

            //Creamos la peticion
            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            //Mandamos el JSon
            //var httpResponse = client.PostAsJsonAsync(url, values).Result; //Otra opcion sin await, no usar
            var httpResponse = await Staticresources.httpClient.PostAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                //Guardamos la respuesta
                var result = await httpResponse.Content.ReadAsStringAsync();

                //Leemos resultado del Body(Contenido) pero tambien podemos ver los Headers o las Cookies
                var postResult = JsonSerializer.Deserialize<TicketLine>(result);
                return postResult;
            }
            else return null;

        }

        //Update linea producto
        public static async Task<bool> updateTicketLine(string id,TicketLine p)
        {
            string url = $"{Staticresources.urlHead}ticket_line/{id}";

            //Creamos objeto tipo JSon con los nuevos parametros
            var values = new JObject();
            values.Add("name", p.name);
            values.Add("cantidad", p.cantidad);
            values.Add("precio", p.precio);
            values.Add("stock", p.stock);
            values.Add("total", p.total);
            values.Add("id_client", p.id_client);
            values.Add("id_familia", p.id_familia);
            values.Add("id_ticket", p.id_ticket);
            values.Add("comentario", p.comentario);


            //Creamos la peticion
            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            //Mandamos el JSon
            var httpResponse = await Staticresources.httpClient.PutAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                //Guardamos la respuesta
                var result = await httpResponse.Content.ReadAsStringAsync();
                return true;
            }
            else return false;
        }

        //Borrar linea de producto
        public static async Task<bool> deleteTicketLine(string id)
        {
            string url = $"{Staticresources.urlHead}ticket_line/{id}";

            //Mandamos el JSon
            //var httpResponse = client.PostAsJsonAsync(url, values).Result; //Otra opcion sin await, no usar
            var httpResponse = await Staticresources.httpClient.DeleteAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                //Guardamos la respuesta
                var result = await httpResponse.Content.ReadAsStringAsync();

                Console.WriteLine($"Linea de producto eliminada {result}");
                return true;

            }
            else return false;

        }
    }
}
