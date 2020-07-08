using Sistema_DirecciónGeneral.Interfaces;
using Sistema_DirecciónGeneral.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sistema_DirecciónGeneral.ViewController
{
    /// <summary>
    /// Lógica de interacción para RegistrarUsuario.xaml
    /// </summary>
    public partial class RegistrarUsuario : UserControl
    {
        IUsuario itActualizar;
        int idUsuario = 0;

        public RegistrarUsuario(int idUsuario, IUsuario itActualizar)
        {
            InitializeComponent();
            LlenarDelegacion();
            this.idUsuario = idUsuario;
            if (this.idUsuario > 0)
            {
                CargarUsuarios();
            }
            this.itActualizar = itActualizar;

        }

        public void CargarUsuarios()
        {
            using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
            {

                Usuario userEdit = db.Usuario.Find(idUsuario);
                txt_nombre.Text = userEdit.nombre;
                txt_apellidos.Text = userEdit.apellidos;
                txt_user.Text = userEdit.usuario1;
                txt_contrasenia.Text = userEdit.contrasenia;
                cb_cargo.Text = userEdit.cargo;
                cb_delegacion.SelectedIndex = userEdit.idDelegación - 1;
                btn_RegistrarUsuario.Content = "Actualizar";
            }
        }


        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LlenarDelegacion()
        {
            SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities();
            var list = db.Delegacion.ToList();
            if (list.Count() > 0)
            {
                cb_delegacion.ItemsSource = list;
                cb_delegacion.DisplayMemberPath = "nombre";
                cb_delegacion.SelectedValue = "idDelegacion";
            }
        }



        private void btn_RegistrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txt_nombre.Text;
            string apellidos = txt_apellidos.Text;
            string username = txt_user.Text;
            string contrasenia = txt_contrasenia.Text;
            int idDelegacion = cb_delegacion.SelectedIndex + 1;
            string cargo = cb_cargo.Text;

            int idUserAux = idUsuario;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(contrasenia) || string.IsNullOrEmpty(cargo))
            {
                MessageBox.Show("Campos Vacios...", "Error");
                return;
            }
            try
            {
                if (idUsuario == 0)
                {
                    using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                    {
                        Usuario usuarioNew = new Usuario
                        {
                            nombre = nombre,
                            apellidos = apellidos,
                            usuario1 = username,
                            contrasenia = contrasenia,
                            idDelegación = idDelegacion,
                            cargo = cargo
                        };
                        db.Usuario.Add(usuarioNew);
                        db.SaveChanges();
                        MessageBox.Show("Usuario registrado con éxito");
                        idUserAux = usuarioNew.idUsuario;
                    }
                }
                else
                {
                    using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                    {
                        Usuario userEdit = db.Usuario.Find(idUsuario);
                        userEdit.nombre = txt_nombre.Text;
                        userEdit.apellidos = txt_apellidos.Text;
                        userEdit.usuario1 = txt_user.Text;
                        userEdit.contrasenia = txt_contrasenia.Text;
                        userEdit.cargo = cb_cargo.Text;
                        userEdit.idDelegación = cb_delegacion.SelectedIndex + 1;
                        db.Entry(userEdit).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Usuario editada con éxito");
                    }
                }
                    this.Visibility = Visibility.Collapsed;
                    this.itActualizar.Actualizar(idUserAux);
            }


            catch
            {
                MessageBox.Show("Error");

            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
        
        
}
