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

                    SqlCommand cmd1 = new SqlCommand("sp_verificar_correo_existente", oConexion);
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd1.Parameters.Add(new SqlParameter("@correo", System.Data.SqlDbType.VarChar)).Value = usuario.Correo;

                    int correoExistente = (int)cmd1.ExecuteScalar();

                    if (correoExistente != 1)
                    {

                        SqlCommand cmd = new SqlCommand("sp_registrar_usuario", oConexion);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@estado", System.Data.SqlDbType.VarChar)).Value = usuario.Domicilio.Estado;
                        cmd.Parameters.Add(new SqlParameter("@municipio", System.Data.SqlDbType.VarChar)).Value = usuario.Domicilio.Municipio;
                        cmd.Parameters.Add(new SqlParameter("@codigoPostal", System.Data.SqlDbType.Int)).Value = usuario.Domicilio.CodigoPostal;
                        cmd.Parameters.Add(new SqlParameter("@colonia", System.Data.SqlDbType.VarChar)).Value = usuario.Domicilio.Colonia;
                        cmd.Parameters.Add(new SqlParameter("@calle", System.Data.SqlDbType.VarChar)).Value = usuario.Domicilio.Calle;
                        cmd.Parameters.Add(new SqlParameter("@numeroExt", System.Data.SqlDbType.Int)).Value = usuario.Domicilio.NumeroExt;

                        if (usuario.Domicilio.NumeroInt == null || usuario.Domicilio.NumeroInt == 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@numeroInt", System.Data.SqlDbType.Int)).Value = DBNull.Value;
                        }
                        else {
                            cmd.Parameters.Add(new SqlParameter("@numeroInt", System.Data.SqlDbType.Int)).Value = usuario.Domicilio.NumeroInt;
                        }

                        

                        cmd.Parameters.Add(new SqlParameter("@referencia", System.Data.SqlDbType.VarChar)).Value = usuario.Domicilio.Referencia;
                        cmd.Parameters.Add(new SqlParameter("@nombre", System.Data.SqlDbType.VarChar)).Value = usuario.Nombre;


                        cmd.Parameters.Add(new SqlParameter("@correo", System.Data.SqlDbType.VarChar)).Value = usuario.Correo;


                        cmd.Parameters.Add(new SqlParameter("@contrasenia", System.Data.SqlDbType.VarChar)).Value = usuario.Contrasenia;


                        if (usuario.Domicilio.NumeroInt == null || usuario.Domicilio.NumeroInt == 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@idRole", System.Data.SqlDbType.Int)).Value = 3;
                        }
                        else { 
                            cmd.Parameters.Add(new SqlParameter("@idRole", System.Data.SqlDbType.Int)).Value = usuario.IdRole;

                        }


                        int rowsAffected = cmd.ExecuteNonQuery();
                        return "Usuario insertado correctamente";
                    }
                    else {
                        return "Este correo ya esta en uso";
                    }

                }
                catch (Exception e)
                {
                    return "Error al Rgisttrar hablale al programador, pinche imbecil:" + e.Message;
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
