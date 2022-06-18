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

        public int Registrar(Usuario obj, out string Mensaje)
        {

            Mensaje = String.Empty;

            if(string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "Por favor ingrese un nombre de usuario";
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

                string clave = CN_Recursos.generarClave();
                string asunto = "Creacion de cuenta";
                string mensaje_correo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Bienvenido a nuestro PastriesLand! Su clave es: _clave_</p>";
                mensaje_correo = mensaje_correo.Replace("_clave_", clave);

                bool respuesta = CN_Recursos.enviarCorreo(obj.Correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    obj.Clave = CN_Recursos.ConvertirSHA256(clave);

                    return objCapaDato.Registrar(obj, out Mensaje);
                } 
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }

              
            } 
            else{
                return 0;
            }
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = String.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "Por favor ingrese un nombre de usuario";
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
                return objCapaDato.Editar(obj, out Mensaje);
            } 
            
            else
            {
                return false;
            }

        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

        public bool CambiarClave(int idUsuario, string nuevaClave, out string Mensaje)
        {
            return objCapaDato.CambiarClave(idUsuario,nuevaClave, out Mensaje);
        }

        public bool ReestablecerClave(int idUsuario, string correo, out string Mensaje){

            Mensaje = String.Empty;
            string nuevaClave = CN_Recursos.generarClave();
            bool resultado = objCapaDato.ReestablecerClave(idUsuario,CN_Recursos.ConvertirSHA256(nuevaClave), out Mensaje);

            if (resultado){
                string asunto = "Constraseña temporal";
                string mensaje_correo = "<h3>Su contraseña fue reestablecida correctamente</h3></br><p>Bienvenido al sitio web de PastriesLand! Su clave es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaClave);

                bool respuesta = CN_Recursos.enviarCorreo(correo, asunto, mensaje_correo);
                
                if (respuesta){

                    return true;
                }
                else{
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else{
                Mensaje = "No se pudo reestablecer la contraseña";
                return false;
            }
        }
    }
}