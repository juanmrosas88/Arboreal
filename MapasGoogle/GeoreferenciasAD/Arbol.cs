using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GeoreferenciasAD
{
    public class Arbol
    {
        public String Descripcion { get; set; }
        public String Especie {get; set;} 
        public Double Latitud { get; set; }
        public Double Longitud { get; set; }

        public Arbol() { }

        public Arbol (String pDescripcion, String pEspecie, Double pLat, Double pLong)
        {
            this.Descripcion = pDescripcion;
            this.Especie = pEspecie;
            this.Latitud = pLat;
            this.Longitud = pLong;

        }


        

    }
}
