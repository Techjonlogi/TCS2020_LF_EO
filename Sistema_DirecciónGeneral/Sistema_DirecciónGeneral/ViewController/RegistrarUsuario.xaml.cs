using Sistema_DirecciónGeneral.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
        /*
        private void Btn_RegistrarUsuario(object sender, RoutedEventArgs e)
        {
            string nombre = txt_alias.Text;
            string apellidos = txt_calle.Text
            string username = txt_codigoPostal.Text;          
            string contrasenia = txt_colonia.Text;
            string delegacion = txt_colonia.Text;
            string telefono = txt_telefono.Text;

            if (string.IsNullOrEmpty(alias) || string.IsNullOrEmpty(calle) || string.IsNullOrEmpty(codigoPostal) || string.IsNullOrEmpty(codigoPostal) || string.IsNullOrEmpty(colonia) || string.IsNullOrEmpty(txt_numero.Text) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(telefono) || string.IsNullOrEmpty(cbMunicipio.Text))
            {
                MessageBox.Show("Usuario y/o password Vacios...", "Error");            
                return;
            }
            try
            {
                //IQueryable query;
                using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                {
                    Delegacion delegacion = new Delegacion
                    {
                        nombre = alias,
                        calle = calle,
                        colonia = colonia,
                        codigoPostal = codigoPostal,
                        // Municipio = municipio,
                        telefono = telefono,
                        correo = correo
                    };
                    db.Delegacions.Add(delegacion);
                    db.SaveChanges();
                }
            }
            catch
            {
                MessageBox.Show("Error");

            }
        }
        */
                
    }
}
