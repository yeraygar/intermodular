using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace intermodular
{
    public class Client
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string passw { get; set; }

        /// <summary>cliente que ha iniciado sesion</summary>
        public static Client currentClient;

        public Client(string name, string email, string passw)
        {
            this.name = name;
            this.email = email;
            this.passw = Encrypt.GetSHA256(passw);
        }

        /// <summary> Checkea si el correo existe o no en la BBDD </summary>
        /// <returns><b>TRUE</b> si existe<br></br>
        /// <b>FALSE</b> si no existe</returns>
        public static async Task<bool> checkEmailExists(string email)
        {
            string url = $"{Staticresources.urlHead}client/email/{email}";
            HttpResponseMessage httpResponse = await Staticresources.httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<bool>(content);
            }
            else throw new HttpRequestException("Respuesta fallida en checkEmailExists");
        }

        /// <summary> Valida en la BBDD, cifra el passw antes de la peticion
        /// <br></br> guarda el <b>_id</b> en public static Client.currentClient</summary>
        /// <returns><b>TRUE</b> email y passw correcto<br></br>
        /// <b>FALSE</b> email o passw incorrecto </returns>
        public static async Task<bool> validateClient(string email, string passw)
        {
            string passwCifrado = Encrypt.GetSHA256(passw);
            string url = $"{Staticresources.urlHead}client/validate/{email}/{passwCifrado}";

            HttpResponseMessage httpResponse = await Staticresources.httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                List<Client> cliente = JsonSerializer.Deserialize<List<Client>>(content);
                currentClient = cliente[0];
                return true;
            }
            else return false;

        }

        /// <summary> Crea Cliente y lo asigna a Client.currentClient </summary>
        /// <returns><b>TRUE</b> si creacion correcta<br></br>
        /// <b>FALSE</b> si se produce un fallo al crear</returns>
        public static async Task<bool> createClient(Client client)
        {
            string url = $"{Staticresources.urlHead}client";

            JObject values = new JObject
            {
                { "name", client.name },
                { "email", client.email },
                { "passw", client.passw }
            };

            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PostAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                string result = await httpResponse.Content.ReadAsStringAsync();
                currentClient = JsonSerializer.Deserialize<Client>(result);
                return true;
            }
            else return false;
        }

        /// <summary> Actualiza el <b>currentClient</b> </summary>
        /// <returns><b>TRUE</b> si actualizacion correcta<br></br>
        /// <b>FALSE</b> si se produce un fallo al actualizar</returns>
        public static async Task<bool> updateClient(Client client)
        {
            string url = $"{Staticresources.urlHead}client/{currentClient._id}";

            JObject values = new JObject
            {
                { "name", client.name },
                { "email", client.email },
                { "passw", client.passw }
            };

            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await Staticresources.httpClient.PostAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                string result = await httpResponse.Content.ReadAsStringAsync();
                currentClient.email = client.email;
                currentClient.passw = client.passw;
                return true;
            }
            else return false;


        }

        /// <summary> Elimina <b>currentClient</b> </summary>
        /// <returns><b>TRUE</b> si borrado correcta<br></br>
        /// <b>FALSE</b> si se produce un fallo al borrar</returns>
        public static async Task<bool> deleteClient()
        {
            string url = $"{Staticresources.urlHead}client/{currentClient._id}";

            HttpResponseMessage httpResponse = await Staticresources.httpClient.DeleteAsync(url);

            if (httpResponse.IsSuccessStatusCode) return true;
            else return false;
        }
    }
}
