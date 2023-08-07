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
                    cmd.Parameters.Add(new SqlParameter("@telefono", System.Data.SqlDbType.VarChar)).Value = pro.Telefono;
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


        public static dynamic BajaProvedor(int idProvedor)
        {


            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {

                try
                {
                    oConexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_baja_proveedor", oConexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.VarChar)).Value = idProvedor;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return "Se ah modificado el estado del proveedor";

                }
                catch (Exception e)
                {
                    return "Error al cambiar el estatus del proveedor, ponte en contacto con el guapo" + e.Message;
                    throw;
                }
                finally
                {
                    oConexion.Close();
                }
            }
        }

        public static dynamic MostarTodosProvedores()
        {

            List<Provedore> prove = new List<Provedore>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {

                try
                {
                    oConexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_mostar_proveedores_todos", oConexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Provedore pro = new Provedore()
                            {
                                idProveedor =Convert.ToInt32(dr["proovedoresId"]),
                                Nombre=dr["nombreProvedor"].ToString(),
                                Telefono = dr["telefono"].ToString(),
                                Estatus = Convert.ToInt32(dr["estatus"]),

                            };

                            prove.Add(pro);
                        }
                    }

                    if (prove.Count >0)
                    {
                        return prove;
                    }
                    else
                    {
                        return "Error en la consulta de provedores";
                    }

                }
                catch (Exception e)
                {
                    return "Error al consultar los proveedores, ponte en contacto con el guapo" + e.Message;
                    throw;
                }
                finally
                {
                    oConexion.Close();
                }
            }
        }

        public static dynamic MostarProvedoresActivos()
        {

            List<Provedore> prove = new List<Provedore>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {

                try
                {
                    oConexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_mostar_proveedores_activos", oConexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Provedore pro = new Provedore()
                            {
                                idProveedor =Convert.ToInt32(dr["proovedoresId"]),
                                Nombre=dr["nombreProvedor"].ToString(),
                                Telefono = dr["telefono"].ToString(),
                                Estatus = Convert.ToInt32(dr["estatus"]),

                            };

                            prove.Add(pro);
                        }
                    }

                    if (prove.Count >0)
                    {
                        return prove;
                    }
                    else
                    {
                        return "Error en la consulta de provedores";
                    }

                }
                catch (Exception e)
                {
                    return "Error al consultar los proveedores, ponte en contacto con el guapo " + e.Message;
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
