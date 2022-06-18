using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Reporte{
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