using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;


//En esta capa podemos hacer varios filtros para el negocio
namespace CapaNegocio
{
    public class CN_usuarios
    {
        //Poder acceder a todos los metodos que componen a la clase
        //CD usuarios
        private CD_usuarios objCapaDato = new CD_usuarios();

        public List<Usuario> Listar()
        {
            //metodo que retorna el mismo metodo listar
            return objCapaDato.Listar();
        }
    }
}
