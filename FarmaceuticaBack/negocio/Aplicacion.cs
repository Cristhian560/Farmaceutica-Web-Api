using FarmaceuticaBack.acceso_a_datos.implementaciones;
using FarmaceuticaBack.acceso_a_datos.interfaces;
using FarmaceuticaBack.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaceuticaBack.negocio
{
    public class Aplicacion : IAplicacion
    {
        private ISuministroDao suministroDao;
        public Aplicacion()
        {
            suministroDao = new SuministroDao();
        }
        public bool Update(Suministro suministro)
        {
            return suministroDao.Update(suministro);
        }
        public bool Delete(int id)
        {
            return suministroDao.Delete(id);
        }
        public bool Insert(Suministro suministro)
        {
            return suministroDao.Insert(suministro);
        }
        public List<Suministro> Suministros()
        {
            List<Suministro> lista_suministros = new List<Suministro>();
            foreach (DataRow item in suministroDao.Suministros().Rows)
            {
                Suministro suministro = new Suministro()
                {
                    IdSuministro = Convert.ToInt32(item.ItemArray[0]),
                    Nombre = item.ItemArray[1].ToString(),
                    VentaLibre = item.ItemArray[2].ToString(),

                    Precio = Convert.ToDouble(item.ItemArray[4]),
                    Descripcion = item.ItemArray[5].ToString()
                };
                TipoSuministro ts = new TipoSuministro()
                {
                    IdTipoSuministro = Convert.ToInt32(item.ItemArray[6]),
                    NombreTipoSuministro = item.ItemArray[7].ToString(),
                };
                suministro.TipoSuministro = ts;
                lista_suministros.Add(suministro);
            }
            return lista_suministros;
        }
        public List<TipoSuministro> TipoSuministros()
        {
            List<TipoSuministro> lista_tipo_suministro = new List<TipoSuministro>();
            foreach (DataRow item in suministroDao.TiposSuministros().Rows)
            {
                TipoSuministro tipo_suministro = new TipoSuministro()
                {
                    IdTipoSuministro = Convert.ToInt32(item.ItemArray[0]),
                    NombreTipoSuministro = item.ItemArray[1].ToString()
                };
                lista_tipo_suministro.Add(tipo_suministro);
            }
            return lista_tipo_suministro;
        }
        
    }
}