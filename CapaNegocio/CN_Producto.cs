using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;



namespace CapaNegocio
{
    public class CN_Producto
    {
        private CD_Producto objCapaDato = new CD_Producto();

        public List<Producto> Listar()
        {
            //metodo que retorna el mismo metodo listar
            return objCapaDato.Listar();
        }

        public int Registrar(Producto obj, out string Mensaje)
        {

            Mensaje = String.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "Por favor ingrese un nombre";
            }

            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "Por favor ingrese una descripción";
            }

            else if(obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Por favor ingrese una categoría";
            }

            else if (obj.Precio == 0)
            {
                Mensaje = "Por favor ingrese un precio";
            }

            else if (obj.Stock == 0)
            {
                Mensaje = "Por favor ingresar stock";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = String.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "Por favor ingrese un nombre";
            }

            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "Por favor ingrese una descripción";
            }

            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Por favor ingrese una categoría";
            }

            else if (obj.Precio == 0)
            {
                Mensaje = "Por favor ingrese un precio";
            }

            else if (obj.Stock == 0)
            {
                Mensaje = "Por favor ingresar stock";
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

        public bool GuardarDatosImagen(Producto obj, out string Mensaje)
        {
            return objCapaDato.GuardarDatosImagen(obj, out Mensaje);
        }

    }
}
