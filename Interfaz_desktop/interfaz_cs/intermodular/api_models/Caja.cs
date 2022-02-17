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
    class Caja
    {
        public string _id { get; set; }
        public DateTime fecha_apertura { get; set; }
        public DateTime fecha_cierre { get; set; }
        public bool cerrada { get; set; }
        public string id_client { get; set; }
        public float total { get; set; }

        public static Caja currentCaja;
        public static List<Caja> openCajas;
        public static List<Caja> allCajasClient;


        public Caja() { this.id_client = Client.currentClient._id; }

        public static async Task<bool> getClientCajas(string id)
        {
            string url = $"{Staticresources.urlHead}caja/client/{id}";
            HttpResponseMessage httpResponse = await Staticresources.httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                List<Caja> cajas = JsonSerializer.Deserialize<List<Caja>>(content);
                allCajasClient = cajas;
                return true;
            }
            else return false;
        }

        public static async Task<bool> getCajaById(string id)
        {
            string url = $"{Staticresources.urlHead}/caja/{id}";
            HttpResponseMessage httpResponse = await Staticresources.httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                Caja caja = JsonSerializer.Deserialize<Caja>(content);
                currentCaja = caja;
                return true;
            }
            return false;
        }

        public static async Task<bool> createCaja()
        {
            string url = $"{Staticresources.urlHead}/caja";

            JObject values = new JObject
            {
                { "id_client", Client.currentClient._id }
            };

            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PostAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                string result = await httpResponse.Content.ReadAsStringAsync();
                currentCaja = JsonSerializer.Deserialize<Caja>(result);
                return true;
            }
            return false;
        }

        public static async Task<bool> updateCaja(Caja caja)
        {
            string url = $"{Staticresources.urlHead}caja/{caja._id}";

            JObject values = new JObject
            {
                { "fecha_apertura", caja.fecha_apertura },
                { "fecha_cierre", caja.fecha_cierre },
                { "total", caja.total },
                { "cerrada", caja.cerrada },
                { "id_client", caja.id_client }

            };

            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PutAsync(url, content);

            return httpResponse.IsSuccessStatusCode;
        }

        public static async Task<bool> closeCaja()
        {
            string url = $"{Staticresources.urlHead}caja/{currentCaja._id}";

            JObject values = new JObject
            {
                { "fecha_cierre", DateTime.Now },
                { "cerrada", true },
                { "total", currentCaja.total },
            };

            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PutAsync(url, content);

            return httpResponse.IsSuccessStatusCode;
        }

        public static async Task<bool> deleteCaja(string id)
        {
            string url = $"{Staticresources.urlHead}caja/{id}";

            HttpResponseMessage httpResponse = await Staticresources.httpClient.DeleteAsync(url);

            if (httpResponse.IsSuccessStatusCode) return true;
            else return false;
        }

        public static async Task<bool> isCajaOpen()
        {
            string url = $"{Staticresources.urlHead}caja/client/{Client.currentClient._id}/open";
            HttpResponseMessage httpResponse = await Staticresources.httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                var res= JsonSerializer.Deserialize<List<Caja>>(content);
                if(res != null)
                {
                    List<Caja> cajas = res;
                    if (cajas.Count > 0)
                    {
                        openCajas = cajas;
                        currentCaja = cajas[0];
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
