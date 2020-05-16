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
        private string nombre;
        private string apellidos;
        private string cargo;
        private Int32 idDelegación;
        


        public override string ToString()
        {
            return String.Format("idUsuario: {0} , usuario: {1}, contrasenia: {2}, nombre: {3}, apellidos: {4}, cargo: {5}, idDelegacion: {6} ",
                                 idUsuario, usuario, contrasenia, nombre, apellidos, cargo, idDelegación);
        }


        public int Idusuario { get => idUsuario; set => idUsuario = value; }

        public string Nombreuser { get => usuario; set => usuario = value; }

        public string Contrasenia { get => contrasenia; set => contrasenia = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Cargo { get => cargo; set => cargo = value; }
        public int IdDelegacion { get => idDelegación; set => idDelegación = value; }


    }
}
