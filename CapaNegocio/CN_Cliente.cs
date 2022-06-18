using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente objCapaDato = new CD_Cliente();

        public List<Cliente> Listar()
        {
            //metodo que retorna el mismo metodo listar
            return objCapaDato.Listar();
        }

        public int Registrar(Cliente obj, out string Mensaje)
        {

            Mensaje = String.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "Por favor ingrese un nombre de Cliente";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "Por favor ingrese un apellido";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "Por favor ingrese un correo";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                obj.Clave = CN_Recursos.ConvertirSHA256(obj.Clave);

                return objCapaDato.Registrar(obj, out Mensaje);

            }

            else
            {
                return 0;
            }


        }

        public bool CambiarClave(int id, string nuevaclave, out string Mensaje)
        {
            return objCapaDato.CambiarClave(id, nuevaclave, out Mensaje);
        }

        public bool ReestablecerClave(int id, string correo, out string Mensaje)
        {
            Mensaje = String.Empty;
            string nuevaclave = CN_Recursos.generarClave();
            bool resultado = objCapaDato.ReestablecerClave(id, CN_Recursos.ConvertirSHA256(nuevaclave), out Mensaje);

            if (resultado)
            {
                string asunto = "Contraseña Reestablecida";
                string mensaje_correo = "<h3>Su cuenta fue reestablecida correctamente</h3></br><p>Su contraseña para acceder ahora es: ¡clave!</p>";
                mensaje_correo = mensaje_correo.Replace("¡clave!", nuevaclave);

                bool respuesta = CN_Recursos.enviarCorreo(correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    return true;
                } else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "No se pudo reestablecer la contraseña";
                return false;
            }
           
        
        
        }

    }
}
