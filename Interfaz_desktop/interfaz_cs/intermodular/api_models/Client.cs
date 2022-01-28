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
        public string email { get; set; }
        public string passw { get; set; }

        /// <summary>cliente que ha iniciado sesion</summary>
        public static Client currentClient;

        public Client(string email, string passw)
        {
            this.email = email;
            this.passw = Encrypt.GetSHA256(passw);
        }

        /// <summary> Checkea si el correo existe o no en la BBDD </summary>
        /// <returns><b>TRUE</b> si existe<br></br>
        /// <b>FALSE</b> si no existe</returns>
        public static async Task<bool> checkEmailExists(string email)
        {
            string url = $"{Staticresources.urlHead}client/email/{email}";
            HttpResponseMessage httpResponse = await Staticresources.client.GetAsync(url);

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

            HttpResponseMessage httpResponse = await Staticresources.client.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                List<Client> cliente = JsonSerializer.Deserialize<List<Client>>(content);
                currentClient = cliente[0];
                return true;
            }
            else return false;

        }
    }
}
