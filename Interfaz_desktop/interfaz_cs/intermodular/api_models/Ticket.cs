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

        //Crear ticket
        public static async Task<bool> createTicket(Ticket t)
        {
            string url = $"{Staticresources.urlHead}ticket";

            JObject values = new JObject
            {
                { "comensales", t.comensales },
                { "id_user_que_abrio", t.id_user_que_abrio },
                { "id_table", t.id_table},
                {"name_table",t.name_table },
                {"cobrado",t.cobrado },
                {"date",t.date },
                {"id_client",t.id_client }
            };

            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PostAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                string result = await httpResponse.Content.ReadAsStringAsync();
                currentTicket = JsonSerializer.Deserialize<Ticket>(result);
                Mesa.currentMesa.id_ticket = currentTicket._id;
                return true;
            }
            else return false;
        }

        //Actualizar Ticket
        public static async Task<bool> updateTicket(Ticket t)
        {
            string url = $"{Staticresources.urlHead}ticket/{t._id}";

            JObject values = new JObject
            {
                { "comensales", t.comensales },
                { "id_user_que_cerro", t.id_user_que_cerro },
                { "id_table", t.id_table},
                {"name_table",t.name_table },
                {"cobrado",t.cobrado },
                {"date",DateTime.Now }
            };

            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PutAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                string result = await httpResponse.Content.ReadAsStringAsync();
                return true;
            }
            else return false;


        }

        //Eliminar Ticket
        public static async Task<bool> deleteTicket()
        {
            string url = $"{Staticresources.urlHead}ticket/{currentTicket._id}";

            HttpResponseMessage httpResponse = await Staticresources.httpClient.DeleteAsync(url);

            if (httpResponse.IsSuccessStatusCode) return true;
            else return false;
        }

        //Obtener ticket
        public static async Task getTicket(String id_ticket)
        {
            string url = $"{Staticresources.urlHead}ticket/{id_ticket}";
            var httpResponse = Staticresources.httpClient.GetAsync(url);
            await httpResponse;

            if (httpResponse.Result.IsSuccessStatusCode)
            {
                var content = await httpResponse.Result.Content.ReadAsStringAsync();
                Ticket t = JsonSerializer.Deserialize<Ticket>(content);
                currentTicket = t;
            }
        }

        //Obtener tickets de un cliente
        public static async Task getTicketFromClient(String id_client)
        {
            string url = $"{Staticresources.urlHead}ticket/client/{id_client}";
            var httpResponse = Staticresources.httpClient.GetAsync(url);
            await httpResponse;

            if (httpResponse.Result.IsSuccessStatusCode)
            {
                var content = await httpResponse.Result.Content.ReadAsStringAsync();
                List<Ticket> t = JsonSerializer.Deserialize<List<Ticket>>(content);
                clientTickets = t;
            }
        }

        //Obtener tickets de una mesa
        public static async Task<bool> getTicketFromTable(String id_table)
        {
            string url = $"{Staticresources.urlHead}ticket/table/{id_table}/sin_cobrar";
            var httpResponse = Staticresources.httpClient.GetAsync(url);
            await httpResponse;

            if (httpResponse.Result.IsSuccessStatusCode)
            {
                var content = await httpResponse.Result.Content.ReadAsStringAsync();
                List<Ticket> t = JsonSerializer.Deserialize<List<Ticket>>(content);
                if(t.Count != 0)
                {
                    currentTicket = t[0];
                    return true;
                }
            }
            return false;
        }
    }
}
