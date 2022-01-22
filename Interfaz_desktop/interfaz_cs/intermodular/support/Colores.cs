using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace intermodular
{
    public static class Colores
    {
        public static Brush azul = (Brush)(new BrushConverter().ConvertFrom("#48C9B0"));
        public static Brush blanco = Brushes.White;
        public static Brush blancoOscuro = Brushes.White;
        public static Brush oscuro = Brushes.DarkSlateGray;
        public static Brush gris = Brushes.DimGray;
        public static Brush grisClaro = (Brush)(new BrushConverter().ConvertFrom("#E0E0E0"));

    }
}
