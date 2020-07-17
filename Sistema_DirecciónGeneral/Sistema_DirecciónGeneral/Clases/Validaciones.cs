using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sistema_DirecciónGeneral.Clases
{
    class Validaciones
    {
        public enum ResultadosValidacion
        {
            NombreValido,
            NombreInvalido,
            ApellidosValidos,
            ApellidosInvalidos,
            CorreoInvalido,
            CorreoValido,
            TelefonoValido,
            TelefonoInvalido
        }

        
        public ResultadosValidacion validarNombre(string nombre)
        {
            string patron = @"^[a-zA-Z' 'áéíóúÁÉÍÓÚñÑ]+$";
            if (Regex.IsMatch(nombre, patron))
            {
                return ResultadosValidacion.NombreValido;
            }
            return ResultadosValidacion.NombreInvalido;
        }

        public ResultadosValidacion validarApellidos(string apellidos)
        {
            string patron = @"^[a-zA-Z' 'áéíóúÁÉÍÓÚñÑ]+$";
            if (Regex.IsMatch(apellidos, patron))
            {
                return ResultadosValidacion.ApellidosValidos;
            }
            return ResultadosValidacion.ApellidosInvalidos;
        }

        public ResultadosValidacion validarCorreo(string correo)
        {
            string patron = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            if (Regex.IsMatch(correo, patron))
            {
                return ResultadosValidacion.CorreoValido;
            }
            return ResultadosValidacion.CorreoInvalido;
        }

        public ResultadosValidacion validarTelefono(string telefono)
        {
            string patron = @"^[0-9]*$";
            if (Regex.IsMatch(telefono, patron))
            {
                return ResultadosValidacion.TelefonoValido;
            }
            return ResultadosValidacion.TelefonoInvalido;
        }
    }
}
