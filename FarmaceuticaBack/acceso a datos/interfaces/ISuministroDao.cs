
using FarmaceuticaBack.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaceuticaBack.acceso_a_datos.interfaces
{
    public interface ISuministroDao
    {
        DataTable Suministros();
        //DataTable Suministros();
        DataTable TiposSuministros();
        bool Insert(Suministro suministro);
        bool Delete(int id);
        bool Update(Suministro suministro);
    }
}
