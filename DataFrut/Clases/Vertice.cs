using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFrut.Clases
{
    public class Vertice
    {
        public int numeroVertice { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public int codigoSector { get; set; }
        public int numeroSector { get; set; }
        public int codigoBloque { get; set; }
        public int numeroBloque { get; set; }
        public List<GMap.NET.PointLatLng> points { get; set; }
        public Vertice()
        {

        }

        public Vertice(int nroVert, double lat, double lon)
        {
            numeroVertice = nroVert;
            latitud = lat;
            longitud = lon;
        }

    }
}
