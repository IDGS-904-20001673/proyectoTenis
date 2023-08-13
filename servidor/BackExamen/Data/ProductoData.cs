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


        public static dynamic RegistrarPuntosProducto(ProductoDetaPuntos P)
        {


            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {

                try
                {
                    oConexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_ProductoDetalle", oConexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idProducto", System.Data.SqlDbType.Int)).Value = P.idProducto;
                    cmd.Parameters.Add(new SqlParameter("@punto", System.Data.SqlDbType.Float)).Value = P.punto;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Se ha agregado el punto")
                    };


                }
                catch (Exception e)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("Error al registrar el nuevo punto del producto")
                    };
                    throw;
                }
                finally
                {
                    oConexion.Close();
                }
            }
        }


        public static dynamic MostrarDetalleProductos(ProductoId ProID)
        {

            List<ProductoDetaPuntosIds> products = new List<ProductoDetaPuntosIds>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {

                try
                {
                    int activo = 0;
                    oConexion.Open();

                    dynamic ProAct = MostrarProductosActivos();
                    foreach (ProductoE PE in ProAct)
                    {
                        if (PE.idProducto == ProID.idProducto)
                        {
                            activo=1;
                            break;
                        }
                        else
                        {
                            activo=2;
                        }
                    }


                    if (activo == 1)
                    {
                        SqlCommand cmd = new SqlCommand("sp_mostrar_puntosProducto", oConexion);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@idProducto", System.Data.SqlDbType.Int)).Value = ProID.idProducto;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                ProductoDetaPuntosIds PDPI = new ProductoDetaPuntosIds()
                                {
                                    idProductoDetalle =Convert.ToInt32(dr["ProductoDetalleID"]),
                                    idProducto=Convert.ToInt32(dr["idProducto"]),
                                    punto = Convert.ToDouble(dr["punto"]),
                                    cantidad = Convert.ToInt32(dr["cantidad_STOCK"]),


                                };

                                products.Add(PDPI);
                            }
                        }
                        if (products.Count >0)
                        {
                            return products;
                        }
                        else
                        {
                            return new HttpResponseMessage(HttpStatusCode.NotFound)
                            {
                                Content = new StringContent("Error en la consulta del detalle del producto")
                            };
                        }

                    }
                    else {
                        return new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent("No se pudo consultar la infromacion porque el producto esta dado de baja, dalo de alta e intenta nuevamente")
                        };
                    }

                 

                }
                catch (Exception e)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("Error al consultar el detalle del producto" + e.Message)
                    };
                    throw;
                }
                finally
                {
                    oConexion.Close();
                }
            }
        }


        public static dynamic RegistrarDetalleMateriaDelProductoPorPunto(ProductoMateriaDetallePuntos PMDP)
        {


            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {

                try
                {
  
                    oConexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_DetalleMateriaProducto", oConexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idProductoDetalle", System.Data.SqlDbType.Int)).Value = PMDP.idProductoDetalle;
                    cmd.Parameters.Add(new SqlParameter("@materiaPrimaId", System.Data.SqlDbType.Int)).Value = PMDP.materiaPrimaId;
                    cmd.Parameters.Add(new SqlParameter("@cantidadUsoMateria", System.Data.SqlDbType.Float)).Value = PMDP.cantidadUsoMateria;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Se ha agregado la materia prima a este punto")
                    };


                }
                catch (Exception e)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("Error al registrar la materia prima para este punto"+e.Message)
                    };
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