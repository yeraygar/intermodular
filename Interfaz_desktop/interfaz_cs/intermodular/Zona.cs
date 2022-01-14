using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intermodular
{
    class Zona
    {
        private string nombreZona;
        private int numMesas;
        private readonly int MAXIMO_MESAS;
        private List<Mesa> mesasZona = new List<Mesa>();
        public static List<Zona> zonas = new List<Zona>();


        public List<Mesa> MesasZona
        {
            get
            {
                return mesasZona;
            }
            set
            {
                mesasZona = value;
            }
        }

        public string NombreZona
        {
            get 
            { 
                return nombreZona; 
            }
            set
            {
                nombreZona = value;
            }
        }

        public int NumMesas
        {
            get
            {
                return numMesas;
            }
            set
            {
                numMesas = value;
            }
        }

        public int maxMesas
        {
            get
            {
                return MAXIMO_MESAS;
            }
        }

        public Zona(string nombreZona, int numMesas, int max_mesas)
        {
            this.nombreZona = nombreZona;
            this.numMesas = numMesas;
            MAXIMO_MESAS = max_mesas;
            zonas.Add(this);
        }


        //Este método agregará mesas a la zona determinada
        public void addMesa(Mesa mesa) => MesasZona.Add(mesa);
    }
}
