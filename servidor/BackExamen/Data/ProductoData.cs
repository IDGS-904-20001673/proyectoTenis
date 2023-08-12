using BackExamen.Data;
using System.Data.SqlClient;
using System.Net;
using tenis.Models;

namespace tenis.Data
{
    public class ProductoData
    {
        public static dynamic RegistrarProductoNuevo(Producto P)
        {


            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {

                try
                {
                    oConexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_productoNuevo", oConexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@nombre", System.Data.SqlDbType.VarChar)).Value = P.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@precio", System.Data.SqlDbType.Float)).Value = P.Precio;
                    cmd.Parameters.Add(new SqlParameter("@descripccion", System.Data.SqlDbType.VarChar)).Value = P.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@image_name", System.Data.SqlDbType.VarChar)).Value = P.ImageName;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Se ha agregado el nuevo producto")
                    };


                }
                catch (Exception e)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("Error al registrar el nuevo producto")
                    };
                    throw;
                }
                finally
                {
                    oConexion.Close();
                }
            }
        }


        public static dynamic MostrarProductosActivos()
        {

            List<ProductoE> pro = new List<ProductoE>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {

                try
                {
                    oConexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_mostarProductosActivos", oConexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductoE PE = new ProductoE()
                            {
                                idProducto =Convert.ToInt32(dr["idProducto"]),
                                nombre=dr["nombre"].ToString(),
                                precio = Convert.ToDouble(dr["precio"]),
                                descripccion = dr["descripccion"].ToString(),
                                image_name = dr["image_name"].ToString(),
                                estatus = Convert.ToInt32(dr["estatus"])


                            };

                            pro.Add(PE);
                        }
                    }

                    if (pro.Count >0)
                    {
                        return pro;
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new StringContent("Error en la consulta de los productos")
                        };
                    }

                }
                catch (Exception e)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("Error al consultar los productos" + e.Message)
                    };
                    throw;
                }
                finally
                {
                    oConexion.Close();
                }
            }
        }

        public static dynamic MostrarProductosInActivos()
        {

            List<ProductoE> pro = new List<ProductoE>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {

                try
                {
                    oConexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_mostarProductosInactivos", oConexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductoE PE = new ProductoE()
                            {
                                idProducto =Convert.ToInt32(dr["idProducto"]),
                                nombre=dr["nombre"].ToString(),
                                precio = Convert.ToDouble(dr["precio"]),
                                descripccion = dr["descripccion"].ToString(),
                                image_name = dr["image_name"].ToString(),
                                estatus = Convert.ToInt32(dr["estatus"])


                            };

                            pro.Add(PE);
                        }
                    }

                    if (pro.Count >0)
                    {
                        return pro;
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent("Error en la consulta de los productos")
                        };
                    }

                }
                catch (Exception e)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("Error al consultar los productos" + e.Message)
                    };
                    throw;
                }
                finally
                {
                    oConexion.Close();
                }
            }
        }

        public static dynamic CambiarEstatusProducto(ProductoId id)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {
                try
                {
                    oConexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_estatus_producto", oConexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idProducto", System.Data.SqlDbType.Int)).Value = id.idProducto;

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent("Se ha cambiado el estatus de este producto")
                        };
                    }
                    else
                    {
                        
                        return new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent("No se pudo modificar el estatus de este producto")
                        };
                    }
                }
                catch (Exception e)
                {
         
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("Error al cambiar el estado del producto: " + e.Message)
                    };
                }
                finally
                {
                    oConexion.Close();
                }
            }
        }
    }

}