using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace intermodular
{
    class ProdImage
    {
        public string _id { get; set; }
        public Image img { get; set; }

        public ProdImage(string _id,Image img)
        {
            this._id = _id;
            this.img = img;
        }
        public static async Task createProdImage(ProdImage prod)
        {
            string url = $"{Staticresources.urlHead}tables";

            //Creamos objeto tipo JSon
            var values = new JObject();
            values.Add("_id", prod._id);
            values.Add("img", prod.img.ToString());

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
                var postResult = JsonSerializer.Deserialize<Mesa>(result);
            }

        }
    }
}
