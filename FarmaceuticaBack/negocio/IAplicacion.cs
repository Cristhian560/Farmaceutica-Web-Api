using FarmaceuticaBack.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaceuticaBack.negocio
{
    public interface IAplicacion
    {
        List<Suministro> Suministros();
        List<TipoSuministro> TipoSuministros();
        bool Delete(int id);
        bool Update(Suministro suministro);
        bool Insert(Suministro suministro);
    }
}
