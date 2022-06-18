using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Reporte{
        //Poder acceder a todos los metodos que componen a la clase
        //CD reporte
        private CD_Reporte objCapaDato = new CD_Reporte();

        public List<Reporte> Venta(string fechaInicio, string fechFin, string idTransaccion){
            //metodo que retorna el mismo metodo listar
            return objCapaDato.Venta(fechaInicio, fechFin, idTransaccion);
        }

        public DashBoard VerDashboard()
        {
            //metodo que retorna el mismo metodo listar
            return objCapaDato.VerDashboard();
        }
    }
}