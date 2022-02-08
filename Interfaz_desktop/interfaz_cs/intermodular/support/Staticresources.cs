using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace intermodular
{
    class Staticresources
    {
        public static string caja = "cerrada";
        //public static string id_client = "Ecosistema1";
        public static double width;
        public static double height;
        public static MainWindow mainWindow;
        public static bool isEditableTables = false;

        /// <summary>http://localhost:8081/api/</summary>
        public static string urlHead = "http://localhost:8081/api/";
        public static HttpClient httpClient = new HttpClient();

    }
}
