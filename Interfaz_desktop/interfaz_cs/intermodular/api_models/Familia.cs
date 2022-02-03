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

        public static async Task<bool> clientFamilies()
        {
            string url = $"{Staticresources.urlHead}family/client/{Client.currentClient._id}";
            HttpResponseMessage httpResponse = await Staticresources.httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                List<Familia> familias = JsonSerializer.Deserialize<List<Familia>>(content);
                clientFamilias = familias;
                return true;
            }
            else return false;
        }

        public static async Task<bool> getFamilyById(string id)
        {
            string url = $"{Staticresources.urlHead}/family/{id}";
            HttpResponseMessage httpResponse = await Staticresources.httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                Familia familia = JsonSerializer.Deserialize<Familia>(content);
                currentFamilia = familia;
                return true;
            }
            return false;
        }

        public static async Task<bool> createFamily(Familia familia)
        {
            string url = $"{Staticresources.urlHead}/family";

            JObject values = new JObject
            {
                { "name", familia.name },
                { "id_client", familia.id_client }
            };

            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PostAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                string result = await httpResponse.Content.ReadAsStringAsync();
                currentFamilia = JsonSerializer.Deserialize<Familia>(result);
                return true;
            }
            return false;
        }

        public static async Task<bool> updateFamilia(Familia familia)
        {
            string url = $"{Staticresources.urlHead}family/{currentFamilia._id}";

            JObject values = new JObject
            {
                { "name", familia.name},
                { "id_client", familia.id_client }
            };

            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PostAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                string result = await httpResponse.Content.ReadAsStringAsync();
                currentFamilia.name = familia.name;
                return true;
            }
            else return false;
        }

        public static async Task<bool> deleteClient(string id) 
        {
            string url = $"{Staticresources.urlHead}family/{id}";

            HttpResponseMessage httpResponse = await Staticresources.httpClient.DeleteAsync(url);

            if (httpResponse.IsSuccessStatusCode) return true;
            else return false;
        }
    }
}
