using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_DirecciónGeneral.Clases
{
    class Usuario
    {
        private Int32 idUsuario;
        private string usuario;
        private string contrasenia;
        


        public override string ToString()
        {
            return String.Format("idUsuario: {0} , usuario: {1}, contrasenia: {2} ",
                                 idUsuario, usuario, contrasenia);
        }


        public int Idusuario { get => idUsuario; set => idUsuario = value; }

        public string Nombreuser { get => usuario; set => usuario = value; }

        public string Contrasenia { get => contrasenia; set => contrasenia = value; }

    }
}
