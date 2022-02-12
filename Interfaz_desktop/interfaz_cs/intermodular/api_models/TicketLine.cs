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
        public TicketLine(string name, int cantidad, float precio, int stock, float total, string id_client, string id_familia, string id_ticket, string comentario)
        {
            this.name = name;
            this.cantidad = cantidad;
            this.precio = precio;
            this.stock = stock;
            this.total = total;
            this.id_client = id_client;
            this.id_familia = id_familia;
            this.id_ticket = id_ticket;
            this.comentario = comentario;
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
        public static async Task<TicketLine> createTicketLine(TicketLine ticketLine)
        {
            string url = $"{Staticresources.urlHead}ticket_line";

            //Creamos objeto tipo JSon
            var values = new JObject();
            values.Add("name", ticketLine.name);
          

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
    }
}
