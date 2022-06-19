using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;

    public class CN_Ubicacion
    {
        private CD_Ubicacion objCapaDato = new CD_Ubicacion();


        public List<Departamento> ObtenerDepartamento()
        {

            return objCapaDato.ObtenerDepartamento();
        }

        public List<Zona> ObtenerZona(string iddepartamento)
        {

            return objCapaDato.ObtenerZona(iddepartamento);
        }
    }
