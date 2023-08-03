using BackExamen.Data;
using serverTenis.Models;
using System.Data.SqlClient;

namespace tenis.Data
{
    public class RegistrarseData
    {

        public static dynamic Registrar(Usuario usuario)
        {
          

            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {
              
                try
                {
                    oConexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_registrar_usuario", oConexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@estado", System.Data.SqlDbType.VarChar)).Value = usuario.Domicilio.Estado;
                    cmd.Parameters.Add(new SqlParameter("@municipio", System.Data.SqlDbType.VarChar)).Value = usuario.Domicilio.Municipio;
                    cmd.Parameters.Add(new SqlParameter("@codigoPostal", System.Data.SqlDbType.Int)).Value = usuario.Domicilio.CodigoPostal;
                    cmd.Parameters.Add(new SqlParameter("@colonia", System.Data.SqlDbType.VarChar)).Value = usuario.Domicilio.Colonia;
                    cmd.Parameters.Add(new SqlParameter("@calle", System.Data.SqlDbType.VarChar)).Value = usuario.Domicilio.Calle;
                    cmd.Parameters.Add(new SqlParameter("@numeroExt", System.Data.SqlDbType.Int)).Value = usuario.Domicilio.NumeroExt;
                    cmd.Parameters.Add(new SqlParameter("@numeroInt", System.Data.SqlDbType.Int)).Value = usuario.Domicilio.NumeroInt;
                    cmd.Parameters.Add(new SqlParameter("@referencia", System.Data.SqlDbType.VarChar)).Value = usuario.Domicilio.Referencia;
                    cmd.Parameters.Add(new SqlParameter("@nombre", System.Data.SqlDbType.VarChar)).Value = usuario.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@correo", System.Data.SqlDbType.VarChar)).Value = usuario.Correo;
                    cmd.Parameters.Add(new SqlParameter("@contrasenia", System.Data.SqlDbType.VarChar)).Value = usuario.Contrasenia;
                    cmd.Parameters.Add(new SqlParameter("@idRole", System.Data.SqlDbType.Int)).Value = usuario.IdRole;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error al Rgisttrar un usuario: " + e.Message);
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
