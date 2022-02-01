using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text.Json;


namespace intermodular
{
    public class Mesa
    {
        public string _id { get; set; }
        public string name { get; set; }
        public bool status { get; set; }
        public string id_zone { get; set; }
        public int comensales { get; set; }
        public int comensalesMax { get; set; }
        public string id_user { get; set; }
        public int num_row { get; set; }
        public int num_column { get; set; }


        //falta public string List<Producto> cuenta {get; set;}

        public static List<Mesa> currentZoneTables;


        public Mesa() { }

        public Mesa(string name, bool status, string id_zone, int comensalesMax)
        {
            this.name = name;
            this.status = status;
            this.comensalesMax = comensalesMax;
            this.id_zone = id_zone;
        }


        /// <summary>
        /// Async Static Method, le pasamos por parametros 
        /// <b>string name y string email</b> y lo crea en
        /// MongoDB con <b>_id</b> autogenerado
        /// <return>Void</return>
        /// </summary>
        public static async Task<Mesa> createTable (Mesa mesa)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/tables";

            //Creamos objeto tipo JSon
            var values = new JObject();
            values.Add("name", mesa.name);
            values.Add("status", mesa.status);
            values.Add("comensalesMax", mesa.comensalesMax);
            values.Add("id_zone", mesa.id_zone);
            values.Add("num_row", mesa.num_row);
            values.Add("num_column", mesa.num_column);

            //Creamos la peticion
            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            //Mandamos el JSon
            //var httpResponse = client.PostAsJsonAsync(url, values).Result; //Otra opcion sin await, no usar
            var httpResponse = await client.PostAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                //Guardamos la respuesta
                var result = await httpResponse.Content.ReadAsStringAsync();

                //Leemos resultado del Body(Contenido) pero tambien podemos ver los Headers o las Cookies
                var postResult = JsonSerializer.Deserialize<Mesa>(result);
                return postResult;

            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Async Static Method, le pasamos por parametros 
        /// <b>string id string name y string email</b> y lo
        ///  actualiza en MongoDB donde id = _id (unica)
        /// <return>Void</return>
        /// </summary>
        public static async Task<bool> updateTable(string id, Mesa mesa) 
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/tables";
            url = url + "/" + id;

            //Creamos objeto tipo JSon con los nuevos parametros
            var values = new JObject();
            values.Add("name", mesa.name);
            values.Add("status", mesa.status);
            values.Add("comensales", mesa.comensales);
            values.Add("comensalesMax", mesa.comensalesMax);
            values.Add("id_zone", mesa.id_zone);
            values.Add("num_row", mesa.num_row);
            values.Add("num_column", mesa.num_column);
            values.Add("id_user", mesa.id_user);

        //Creamos la peticion
        HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            //Mandamos el JSon
            var httpResponse = await client.PutAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                //Guardamos la respuesta
                var result = await httpResponse.Content.ReadAsStringAsync();
                return true;

                Console.WriteLine($"Usuario actualizado correctamente {result}");

            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Async Static Method, le pasamos por parametros 
        /// <b>string id</b> y lo elimina en MongoDB
        ///  donde id = _id (unica)
        /// <returns>Void</returns>
        /// </summary>
        public static async Task deleteTable(string id)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/tables";
            url = url + "/" + id;

            //Mandamos el JSon
            //var httpResponse = client.PostAsJsonAsync(url, values).Result; //Otra opcion sin await, no usar
            var httpResponse = await client.DeleteAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                //Guardamos la respuesta
                var result = await httpResponse.Content.ReadAsStringAsync();

            }
        }


        /********** Funciones relacionadas con el cliente *************/

        /// <summary>
        /// Async Static Method, carga todos los usuarios de <b>CLIENTE</b>
        /// desde la Api y los guarda en <b>User.usuariosDeCliente</b> --   
        /// <return>Void</return>
        /// </summary>
        public static async Task getZoneTables(String id_zona)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/tables/zone";
            url = url + "/" + id_zona;

            //Hacemos la peticion
            var httpResponse = client.GetAsync(url);

            //Tareas que podemos hacer mientras se hace la peticion,
            // Si no necesitamos hacer nada mientras se puede hacer del tiron
            // deteniendo el hilo principal:
            // var httpResponse = await client.GetAsync(url);
            Console.WriteLine("peticion en curso");

            //Detenemos el hilo principal hasta que recibamos la respuesta
            await httpResponse;

            //ambos Return true si la peticion se ha realizado correctamente.
            Console.WriteLine($"Peticion realizada con exito? : {httpResponse.Result.IsSuccessStatusCode}");

            if (httpResponse.Result.IsSuccessStatusCode)
            {
                //Esto tambien asincrono por si el contenido es muy grande (leer respuesta), detiene hilo principal
                var content = await httpResponse.Result.Content.ReadAsStringAsync();

                //Deserializamos el Json y guardamos en una lista de User
                List<Mesa> listaRes = JsonSerializer.Deserialize<List<Mesa>>(content);

                currentZoneTables = listaRes;
            }
        }

        public static async Task removeZoneTables(string id_zona)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/tables/zone";
            url = url + "/" + id_zona;

            //Hacemos la peticion
            var httpResponse = await client.DeleteAsync(url);


            if (httpResponse.IsSuccessStatusCode)
            {
                //Esto tambien asincrono por si el contenido es muy grande (leer respuesta), detiene hilo principal
                var result = await httpResponse.Content.ReadAsStringAsync();
            }
        }


    }

}


