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

        public RegistrarUsuario()
        {
            InitializeComponent();
            LlenarDelegacion();
        }
        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LlenarDelegacion()
        {
            SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities();
            var list = db.Delegacions.ToList();
            if (list.Count() > 0)
            {
                cb_delegacion.ItemsSource = list;
                cb_delegacion.DisplayMemberPath = "nombre";
                cb_delegacion.SelectedValue = "idDelegacion";
            }
        }

    }
        private void Btn_RegistrarUsuario(object sender, RoutedEventArgs e)
        {
           string nombre = txt_nombre.Text,
           string apellidos = txt_apellidos.Text,
           string username = txt_user.Text;          
           string contrasenia = txt_contrasenia.Text;
           //string delegacion = cb_contrasenia.Text;
           string cargo = cb_cargo.Text;

           if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(contrasenia) || string.IsNullOrEmpty(cargo))
           {
               MessageBox.Show("Usuario y/o password Vacios...", "Error");            
               return;
           }
           try
           {
               //IQueryable query;
               using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
               {
                   Usuario usuario = new Usuario
                   {
                       nombre = nombre,
                       apellidos = apellidos,
                       usuario1 = username,
                       //Delegacion = delegacion,                       
                       cargo = cargo
                   };
                   db.Usuarios.Add(usuario);
                   db.SaveChanges();
               }
           }
           catch
           {
               MessageBox.Show("Error");

           }
        }
        
}
