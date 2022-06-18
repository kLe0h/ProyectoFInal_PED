using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Reporte{

        //Metodo para listar los elementos de la base de datos
        public List<Reporte> Venta(string fechaInicio, string fechFin, string idTransaccion){
            List<Reporte> lista = new List<Reporte>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)){
                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", oconexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechaInicio);
                    cmd.Parameters.AddWithValue("fechfin", fechFin);
                    cmd.Parameters.AddWithValue("idtransaccion", idTransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Ejecutar normalmente el query
                    oconexion.Open();

                    //Dar lectura al resultado del query
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Mientras lee los datos, se almacenaran en la lista
                        while (dr.Read()){
                            lista.Add(
                                new Reporte(){
                                    FechaVenta = dr["FechaVenta"].ToString(),
                                    Cliente = dr["Cliente"].ToString(),
                                    Producto = dr["Producto"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-PE")),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                    Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-PE")),
                                    IdTransaccion = dr["idTransaccion"].ToString()
                                });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Reporte>();
            }
            return lista;
        }

        public DashBoard VerDashboard()
        {
            DashBoard objeto = new DashBoard();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)){
                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Ejecutar normalmente el query
                    oconexion.Open();

                    //Dar lectura al resultado del query
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Mientras lee los datos, se almacenaran en la lista
                        while (dr.Read()){
                            objeto = new DashBoard(){
                                totalCliente = Convert.ToInt32(dr["totalCliente"]),
                                totalVenta = Convert.ToInt32(dr["totalVenta"]),
                                totalProducto = Convert.ToInt32(dr["totalProducto"]),
                            };
                        }
                    }
                }

            }
            catch (Exception ex){
                objeto = new DashBoard();
            }
            return objeto;
        }
    }
}