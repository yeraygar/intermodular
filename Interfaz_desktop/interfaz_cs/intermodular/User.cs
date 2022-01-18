
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace intermodular
{
    /// <summary>
    /// Clase con los atributos <b>name, email, _id</b> (strings los 3), 
    /// static List de User <b>allUsers</b> y static User <b>currentUser</b>
    /// Metodos: <b>getAllUsers, getUserById, createUser, updateUser, deleteUser,
    /// checkPass, checkUser</b>
    /// </summary>
    class User
    {
        /**IMPORTANTE
         *  Los atributos deben llamarse exactamente igual
         *  que los de Models>User.js de la API, 
         *  _id no aparece en el model porque dejaremos que
         *  se genere automaticamente en MongoDB, pero 
         *  necesitamos crear el atributo para recibirlo.
         */
        public string name { get; set; }
        public string email { get; set; }
        public string passw { get; set; }
        public string _id { get; set; } 
        public string id_client { get; set; }

        public static List<User> allUsers;

        public static User currentUser;

        public static User usuarioElegido; 

        public User(string name, string email, string passw, string id_client)
        {
            this.name = name;
            this.email = email;
            this.passw = passw;
            this.id_client = id_client;

        }

        public User() { }

        /// <summary>
        /// Async Static Method, carga todos los usuarios 
        /// desde la Api y los guarda en <b>User.allUsers</b> --   
        /// <return>Void</return>
        /// </summary>
        public static async Task getAllUsers()
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/users";

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
                List<User> allUsers = JsonSerializer.Deserialize<List<User>>(content);

                //No se puede retornar un valor, asi que lo guardamos en variable statica de clase User
                User.allUsers = allUsers;
            }
        }

        /// <summary>
        /// Async Static Method, le pasamos por parametro un 
        /// <b>string id</b>, carga el usuario correspondiente
        /// desde la Api y lo guarda en <b>User.currentUser</b> --   
        /// <return>Void</return>
        /// </summary>
        public static async Task getUserById(string id)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/users";
            url = url + "/" + id;

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
                User user = JsonSerializer.Deserialize<User>(content);

                //No se puede retornar un valor, asi que lo guardamos en variable statica de clase User
                User.currentUser = user;
            }
        }

        /// <summary>
        /// Async Static Method, le pasamos por parametros 
        /// <b>string name y string email</b> y lo crea en
        /// MongoDB con <b>_id</b> autogenerado
        /// <return>Void</return>
        /// </summary>
        public static async Task createUser(User user)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/users";

            //Creamos objeto tipo JSon
            var values = new JObject();
            values.Add("name", user.name);
            values.Add("email", user.email);
            values.Add("passw", user.passw);
            values.Add("id_client", user.id_client);

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
                var postResult = JsonSerializer.Deserialize<User>(result);

                Console.WriteLine($"Usuario creado correctamente\n\tname: {postResult.name},\n\temail: {postResult.email}");

            }
        }

        /// <summary>
        /// Async Static Method, le pasamos por parametros 
        /// <b>string id string name y string email</b> y lo
        ///  actualiza en MongoDB donde id = _id (unica)
        /// <return>Void</return>
        /// </summary>
        public static async Task updateUser(string id, User user)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/users";
            url = url + "/" + id;

            //Creamos objeto tipo JSon con los nuevos parametros
            var values = new JObject();
            values.Add("name", user.name);
            values.Add("email", user.email);
            values.Add("passw", user.passw);
            values.Add("id_client", user.id_client);

            //Creamos la peticion
            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            //Mandamos el JSon
            //var httpResponse = client.PostAsJsonAsync(url, values).Result; //Otra opcion sin await, no usar
            var httpResponse = await client.PutAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                //Guardamos la respuesta
                var result = await httpResponse.Content.ReadAsStringAsync();

                Console.WriteLine($"Usuario actualizado correctamente {result}");

            }
        }

        /// <summary>
        /// Async Static Method, le pasamos por parametros 
        /// <b>string id</b> y lo elimina en MongoDB
        ///  donde id = _id (unica)
        /// <returns>Void</returns>
        /// </summary>
        public static async Task deleteUser(string id)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/users";
            url = url + "/" + id;

            //Mandamos el JSon
            //var httpResponse = client.PostAsJsonAsync(url, values).Result; //Otra opcion sin await, no usar
            var httpResponse = await client.DeleteAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                //Guardamos la respuesta
                var result = await httpResponse.Content.ReadAsStringAsync();

                Console.WriteLine($"Usuario eliminado correctamente {result}");

            }
        }

        /// <summary>
        /// Busca al user en MongoDB donde _id = id y 
        /// evalua la contrasenya cifrada (pass).
        /// Actualiza User.currentUser
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pass"></param>
        /// <returns>True si los pass coinciden, False si no</returns>
        public static async Task<bool> checkPass (string id, string pass)
        {
            await getUserById(id);
            if (User.currentUser.passw.Equals(pass)) return true;
            else return false;
        }

        /// <summary>
        /// Busca al user en MongoDB donde _id = id,
        /// actualiza User.currentUser
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pass"></param>
        /// <returns>True si id existe en MongoDB, False si no</returns>
        public static async Task<bool> checkUser (string id)
        {
            await getUserById(id);
            if (User.currentUser._id != null) return true;
            else return false;
        }
    }

}

