using BackExamen.Data;
using serverTenis.Models;
using System.Data.SqlClient;
using System.Globalization;
using tenis.Models;

namespace tenis.Data
{
    public class MateriaPrimaData
    {


        public static dynamic RegistrarMateriaPrima(MateriaPrima MP)
        {


            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {

                try
                {
                    int siExiste=0;
                    oConexion.Open();

                    dynamic provedores = ProveedoresData.MostarTodosProvedores();

                    foreach (Provedore prove in provedores) {
                        if (prove.idProveedor == MP.provedoresId && prove.Estatus == 1)
                        {
                            siExiste=1;
                            break;
                        }
                        else if (prove.idProveedor == MP.provedoresId && prove.Estatus == 0)
                        {
                            siExiste=2;
                            break;
                        }
                        else {
                            siExiste=3;
                        }
                    }

                    if (siExiste == 1)
                    {
                        SqlCommand cmd = new SqlCommand("sp_nueva_materiaPrima", oConexion);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@proovedoresId", System.Data.SqlDbType.Int)).Value = MP.provedoresId;
                        cmd.Parameters.Add(new SqlParameter("@nombreMateriaPrima", System.Data.SqlDbType.VarChar)).Value = MP.nombreMateriaPrima;
                        cmd.Parameters.Add(new SqlParameter("@costo", System.Data.SqlDbType.Float)).Value = MP.costo; 
                        cmd.Parameters.Add(new SqlParameter("@image_name", System.Data.SqlDbType.VarChar)).Value = MP.image_name;




                        // Agregar otros parámetros si es necesario

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return "Materia prima insertada con exito";
                    }
                    else if (siExiste == 2)
                    {
                        return "No se pudo agregar la materia prima porque el proveedor esta dado de baja, dalo de alta e intentalo nuevamente";
                    }
                    else { 
                        return "No se pudo agregar la materia prima porque el proveedor No existe";

                    }


                }
                catch (Exception e)
                {
                    return "Error al registrar la materia prima dile al leo: " + e.Message;
                    throw;
                }
                finally
                {
                    oConexion.Close();
                }
            }
        }


        public static dynamic MostarMateriaPrima()
        {

            List<MateriaPrimaE> mate = new List<MateriaPrimaE>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {

                try
                {
                    oConexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_mostrar_materiaPrima", oConexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MateriaPrimaE mp = new MateriaPrimaE()
                            {
                                materiaPrimaId =Convert.ToInt32(dr["materiaPrimaId"]),
                                provedoresId=Convert.ToInt32(dr["proovedoresId"]),
                                nombreMateriaPrima = dr["nombreMateriaPrima"].ToString(),
                                cantidadTotal = Convert.ToInt32(dr["cantidadTotal"]),
                                costo = Convert.ToDouble(dr["costo"]),
                                image_name = dr["image_name"].ToString()

                            };

                            mate.Add(mp);
                        }
                    }

                    if (mate.Count >0)
                    {
                        return mate;
                    }
                    else
                    {
                        return "Error en la consulta de la materia prima";
                    }

                }
                catch (Exception e)
                {
                    return "Error al consultar la materia prima, ponte en contacto con el guapo" + e.Message;
                    throw;
                }
                finally
                {
                    oConexion.Close();
                }
            }
        }


        public static dynamic MostarComprasMateriaPrima()
        {

            List<ComprasMateriaPrima> CMP = new List<ComprasMateriaPrima>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {

                try
                {
                    oConexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_mostrar_comprasMateriaPrima", oConexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ComprasMateriaPrima mp = new ComprasMateriaPrima()
                            {
                                compraMateriaPrimaId =Convert.ToInt32(dr["compraMateriaPrimaId"]),
                                materiaPrimaId =Convert.ToInt32(dr["materiaPrimaId"]),
                                cantidadCompra=Convert.ToInt32(dr["cantidadCompra"]),
                                pagoTotal = Convert.ToDouble(dr["pagoTotal"]),
                                fecha = Convert.ToDateTime(dr["fecha"]).ToString("dd-MM-yyyy")


                        };

                            CMP.Add(mp);
                        }
                    }

                    if (CMP.Count >0)
                    {
                        return CMP;
                    }
                    else
                    {
                        return "Error en la consulta de las compras de materia prima";
                    }

                }
                catch (Exception e)
                {
                    return "Error al consultar las compras de materia prima, ponte en contacto con el guapo" + e.Message;
                    throw;
                }
                finally
                {
                    oConexion.Close();
                }
            }
        }


        public static dynamic ComprarMateriaPrima(ComprasMateriaPrimaI CMPI)
        {


            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaCon))
            {

                try
                {
                    int siExiste = 0;
                    oConexion.Open();

                    dynamic MP = MostarMateriaPrima();

                    foreach (MateriaPrimaE MPE in MP)
                    {
                        if (MPE.materiaPrimaId == CMPI.materiaPrimaId)
                        {
                            siExiste=1;
                            break;
                        }
                        else
                        {
                            siExiste=2;
                        }
                    }

                    if (siExiste == 1)
                    {
                        SqlCommand cmd = new SqlCommand("sp_comprar_materiaPrima", oConexion);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@materiaPrimaId", System.Data.SqlDbType.Int)).Value = CMPI.materiaPrimaId;
                        cmd.Parameters.Add(new SqlParameter("@cantidadCompra", System.Data.SqlDbType.VarChar)).Value = CMPI.cantidadCompra;
                        cmd.Parameters.Add(new SqlParameter("@pagoTotal", System.Data.SqlDbType.Float)).Value = CMPI.pagoTotal;

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return "Compra de Materia prima realizada con exito con exito";
                    }
                    else
                    {
                        return "No se pudo comprar la materia prima porque no existe, da de alta la meteria prima";

                    }


                }
                catch (Exception e)
                {
                    return "Error al comprar la materia prima dile al guapo: " + e.Message;
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
