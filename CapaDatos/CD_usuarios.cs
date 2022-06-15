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
    public class CD_usuarios
    {
        //Metodo para listar los elementos de la base de datos
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT IdUsuario, Nombres, Apellidos, Correo, Clave, Reestablecer, Activo from usuario";
                    
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    //Ejecutar normalmente el query
                    oconexion.Open();

                    //Dar lectura al resultado del query
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Mientras lee los datos, se almacenaran en la lista
                        while (dr.Read())
                        {
                            lista.Add(
                                new Usuario()
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    Nombres = dr["Nombres"].ToString(), 
                                    Apellidos = dr["Apellidos"].ToString(), 
                                    Correo = dr["Correo"].ToString(), 
                                    Clave = dr["Clave"].ToString(), 
                                    Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
                                    Activo = Convert.ToBoolean(dr["Activo"])

                                });
                        }
                    }
                }

            } catch (Exception ex)
            {
                string error = ex.Message;
                lista = new List<Usuario>();
            }
                return lista;    
        }
    }
}
