using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaceuticaBack.dominio
{
    public class Suministro
    {
        public int IdSuministro { get; set; }
        public string Nombre { get; set; }
        public string VentaLibre { get; set; }
        public TipoSuministro TipoSuministro { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; }
        public Suministro()
        {

        }
        public Suministro(int idSuministro, string nSuministro, double precio)
        {
            IdSuministro = idSuministro;
            Nombre = nSuministro;
            Precio = precio;
        }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
