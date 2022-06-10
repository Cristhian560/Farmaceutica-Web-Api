using FarmaceuticaBack.acceso_a_datos.interfaces;
using FarmaceuticaBack.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaceuticaBack.acceso_a_datos.implementaciones
{
    class SuministroDao : ISuministroDao
    {
        private string CadenaConexion;
        private SqlConnection cnn;
        public SuministroDao()
        {
            CadenaConexion = HelperDao.Instancia().CadenaConeccion();
            cnn = new SqlConnection(CadenaConexion);
        }
        public DataTable Suministros()
        {
            SqlConnection cnn = new SqlConnection(CadenaConexion);
            SqlCommand cmd = new SqlCommand("SP_CONSULTAR_SUMINISTROS", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable tabla = new DataTable();

            cnn.Open();
            tabla.Load(cmd.ExecuteReader());
            cnn.Close();

            return tabla;
        }
        public bool Update(Suministro suministro)
        {
            SqlTransaction transaccion = null;
            bool flag = true;
            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();

                SqlCommand cmd = new SqlCommand("SP_UPDATE_SUMINISTRO", cnn, transaccion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_suministro", suministro.IdSuministro);
                cmd.Parameters.AddWithValue("@suministro", suministro.Nombre);
                cmd.Parameters.AddWithValue("@venta_libre", suministro.VentaLibre);
                cmd.Parameters.AddWithValue("@id_tipo_suministro", suministro.TipoSuministro.IdTipoSuministro);
                cmd.Parameters.AddWithValue("@pre_unitario", suministro.Precio);
                cmd.Parameters.AddWithValue("@descripcion", suministro.Descripcion);

                cmd.ExecuteNonQuery();

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                flag = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return flag;
        }
        public bool Delete(int id)
        {
            SqlTransaction transaccion = null;
            bool flag = true;
            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();

                SqlCommand cmd = new SqlCommand("SP_DELETE_SUMINISTRO", cnn, transaccion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_suministro", id);

                cmd.ExecuteNonQuery();

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                flag = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return flag;
        }
        public bool Insert(Suministro suministro)
        {
            SqlTransaction transaccion = null;
            bool flag = true;
            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();

                SqlCommand cmd = new SqlCommand("SP_INSERTAR_SUMINISTRO", cnn, transaccion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_suministro", suministro.IdSuministro);
                cmd.Parameters.AddWithValue("@suministro", suministro.Nombre);
                cmd.Parameters.AddWithValue("@venta_libre", suministro.VentaLibre);
                cmd.Parameters.AddWithValue("@id_tipo_suministro", suministro.TipoSuministro.IdTipoSuministro);
                cmd.Parameters.AddWithValue("@precio", suministro.Precio);
                cmd.Parameters.AddWithValue("@descripcion", suministro.Descripcion);

                cmd.ExecuteNonQuery();

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                flag = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return flag;
        }
        public DataTable TiposSuministros()
        {
            SqlCommand cmd = new SqlCommand("SP_CONSULTAR_TIPOS_SUMINISTRO", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable tabla = new DataTable();

            cnn.Open();
            tabla.Load(cmd.ExecuteReader());
            cnn.Close();

            return tabla;
        }
    }
}
