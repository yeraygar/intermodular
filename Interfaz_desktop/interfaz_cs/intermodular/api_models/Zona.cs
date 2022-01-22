﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace intermodular
{
    public class Zona
    {
        private string _id_client;
        private string _zone_name;
        private int _num_tables;
        private bool _zone_status;
        private Mesa[] _tables;
        private string id;
        public static List<Zona> allZonas = new List<Zona>();
        public static Zona zonaBuscada;

        public string id_client
        {
            get
            {
                return _id_client;
            }
            set
            {
                _id_client = value;
            }
        }
        public string zone_name
        {
            get
            {
                return _zone_name;
            }
            set
            {
                _zone_name = value;
            }
        }
        public int num_tables
        {
            get
            {
                return _num_tables;
            }
            set
            {
                _num_tables = value;
            }
        }
        public bool zone_status
        {
            get
            {
                return _zone_status;
            }
            set
            {
                _zone_status = value;
            }
        }
        public Mesa[] tables
        {
            get
            {
                return _tables;
            }
            set
            {
                _tables = value;
            }
        }

        public string _id
        {
            get
            {
                return id;
            }
            set
            {
                if(id == null)
                {
                    id = value;
                }
            }
        }



        public Zona(string id_client,string zone_name, int num_tables,bool zone_status)
        {
            this._id_client = id_client;
            this._zone_name = zone_name;
            this._num_tables = num_tables;
            this._zone_status = zone_status;
        }
        
        public static async Task getAllZones()
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/zones";

            //Hacemos la petición
            var httpResponse = client.GetAsync(url);
            await httpResponse;

            if (httpResponse.Result.IsSuccessStatusCode)
            {
                //Esto tambien asincrono por si el contenido es muy grande (leer respuesta), detiene hilo principal
                var content = await httpResponse.Result.Content.ReadAsStringAsync();

                //Deserializamos el Json y guardamos en una lista de User
                allZonas = JsonSerializer.Deserialize<List<Zona>>(content);
            }
        }

        public static async Task getZoneById(string id)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/zones";
            url = url + "/" + id;

            //Hacemos la peticion
            var httpResponse = client.GetAsync(url);

            //Esperamos a tener la respuesta http
            await httpResponse;

            if (httpResponse.Result.IsSuccessStatusCode)
            {
                //Esto tambien asincrono por si el contenido es muy grande (leer respuesta), detiene hilo principal
                var content = await httpResponse.Result.Content.ReadAsStringAsync();

                //Deserializamos el Json y guardamos en una lista de User
                Zona zona = JsonSerializer.Deserialize<Zona>(content);

                zonaBuscada = zona;

            }
        }

        public static async Task<Zona> createZone(Zona zona)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/zones";

            //Creamos objeto tipo JSon
            var values = new JObject();
            values.Add("id_client", zona.id_client);
            values.Add("zone_name", zona.zone_name);
            values.Add("num_tables", zona.num_tables);
            values.Add("zone_status", zona.zone_status);
            //values.Add("tables", zona.mesasZona.ToString());
           

            //Creamos la peticion
            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            //Mandamos el JSon
            var httpResponse = await client.PostAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                //Guardamos la respuesta
                var result = await httpResponse.Content.ReadAsStringAsync();

                //Leemos resultado del Body(Contenido) pero tambien podemos ver los Headers o las Cookies
                var postResult = JsonSerializer.Deserialize<Zona>(result);
                allZonas.Add(postResult);
                return postResult;
            }
            return null;
        }

        public static async Task<bool> updateZona(string id, Zona zona)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/zones";
            url = url + "/" + id;

            //Creamos objeto tipo JSon con los nuevos parametros
            var values = new JObject();
            values.Add("id_client", zona.id_client);
            values.Add("zone_name", zona.zone_name);
            values.Add("num_tables", zona.num_tables);
            values.Add("zone_status", zona.zone_status);
            values.Add("tables", zona.tables.ToString());


            //Creamos la peticion
            HttpContent content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/json");

            //Mandamos el JSon
            var httpResponse = await client.PutAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                //Guardamos la respuesta
                var result = await httpResponse.Content.ReadAsStringAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> deleteZone(string id)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:8081/api/zones";
            url = url + "/" + id;

            //Mandamos el JSon
            var httpResponse = await client.DeleteAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                //Guardamos la respuesta
                var result = await httpResponse.Content.ReadAsStringAsync();
                return true;
            }else
            {
                return false;
            }
        }

    }

}
