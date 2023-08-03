using BackExamen.Data;
using Newtonsoft.Json;
using serverTenis.Models;
using System.Data.SqlClient;


namespace serverTenis.Data
{
    public class LoginData
    {

        public static dynamic login(string contrasenia, string correo)
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {
                oConexion.Open();

                SqlCommand cmd = new SqlCommand("consultar_usuario", oConexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar)).Value = correo;
                cmd.Parameters.Add(new SqlParameter("@pass", System.Data.SqlDbType.VarChar)).Value = contrasenia;

                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Usuario user = new Usuario()
                            {
                                Nombre = dr["nombre"].ToString(),
                                Correo = dr["correo"].ToString(),
                                Contrasenia = dr["contrasenia"].ToString(),
                                IdRole =  Convert.ToInt32(dr["idRole"]),
                                Domicilio = new Domicilio()
                                {
                                    Estado = dr["estado"].ToString(),
                                    Municipio = dr["municipio"].ToString(),
                                    CodigoPostal = Convert.ToInt32(dr["codigoPostal"]),
                                    Colonia = dr["colonia"].ToString(),
                                    Calle = dr["calle"].ToString(),
                                    NumeroExt = Convert.ToInt32(dr["numeroExt"]),
                                    NumeroInt = dr.IsDBNull(dr.GetOrdinal("numeroInt")) ? (int?)null : dr.GetInt32(dr.GetOrdinal("numeroInt")),
                                    Referencia = dr["referencia"].ToString(),


                                },
                            };

                            usuarios.Add(user);
                        }
                    }

                    return usuarios;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error al iniciar sesion: " + e.Message);
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
