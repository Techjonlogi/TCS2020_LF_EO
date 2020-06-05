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
    /// Lógica de interacción para RegistrarDelegacion.xaml
    /// </summary>
    public partial class RegistrarDelegacion : UserControl
    {
        public RegistrarDelegacion()
        {
            InitializeComponent();
            LlenarMunicipios();
        }

        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LlenarMunicipios()
        {
            SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities();
            var list = db.Municipio.ToList();
            if(list.Count() > 0)
            {
                cbMunicipio.ItemsSource = list;
                cbMunicipio.DisplayMemberPath = "municipio1";
                cbMunicipio.SelectedValuePath = "idMunicipio";
            }
        }

     


        private void btn_registrarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            string alias = txt_alias.Text;
            string calle = txt_calle.Text + " " + txt_numero;
            string codigoPostal = txt_codigoPostal.Text;
            int idMunicipio = cbMunicipio.SelectedIndex + 1;
            string colonia = txt_colonia.Text;
            string correo = txt_correo.Text;
            string telefono = txt_telefono.Text;

            if (string.IsNullOrEmpty(alias) || string.IsNullOrEmpty(calle) || string.IsNullOrEmpty(codigoPostal) || string.IsNullOrEmpty(codigoPostal) || string.IsNullOrEmpty(colonia) || string.IsNullOrEmpty(txt_numero.Text) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(telefono) || string.IsNullOrEmpty(cbMunicipio.Text))
            {
                MessageBox.Show("Campos Vacios...", "Error");
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
                        idMunicipio = idMunicipio ,
                        telefono = telefono,
                        correo = correo
                    };
                    db.Delegacion.Add(delegacion);
                    db.SaveChanges();
                    MessageBox.Show("Delegación registrada con éxito");
                }
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
