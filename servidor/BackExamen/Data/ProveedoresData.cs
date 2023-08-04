using BackExamen.Data;
using serverTenis.Models;
using System.Data.SqlClient;
using tenis.Models;

namespace tenis.Data
{
    public class ProveedoresData
    {

        public static dynamic RegistrarProvedor(Provedor pro)
        {


            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {

                try
                {
                    oConexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_insertar_proveedor", oConexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@nombreP", System.Data.SqlDbType.VarChar)).Value = pro.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@materiPrima", System.Data.SqlDbType.VarChar)).Value = pro.MateriaPrima;
                    cmd.Parameters.Add(new SqlParameter("@costo", System.Data.SqlDbType.Float)).Value = pro.Costo;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return "proveedor insertado con exito";

                }
                catch (Exception e)
                {
                    return "Error al registrar un provedor hablale al programador, pinche imbecil: " + e.Message;
                    throw;
                }
                finally
                {
                    oConexion.Close();
                }
            }
        }


    }
}
