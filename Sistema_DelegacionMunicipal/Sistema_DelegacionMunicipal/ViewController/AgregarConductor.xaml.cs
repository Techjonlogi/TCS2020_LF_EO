using Sistema_DelegacionMunicipal.Interface;
using Sistema_DelegacionMunicipal.Modelo;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sistema_DelegacionMunicipal.ViewController
{
    /// <summary>
    /// Lógica de interacción para AgregarConductor.xaml
    /// </summary>
    public partial class AgregarConductor : UserControl
    {
        IConductor itActualizar;
        int idConductor = 0;

        public AgregarConductor(int idConductor, IConductor itActualizar)
        {
            InitializeComponent();
            this.idConductor = idConductor;
            if(idConductor > 0)
            {
                CargarConductores();
            }
            this.itActualizar = itActualizar;            
        }

        public void CargarConductores()
        {
            using(SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
            {
                Conductor conductorEdit = db.Conductor.Find(idConductor);
                txtApellidos.Text = conductorEdit.apellidos;
                txtLicencia.Text = conductorEdit.numLicencia;
                txtNacimiento.Text = conductorEdit.fechaNacimiento;
                txtNombre.Text = conductorEdit.nombre;
                txtTelefono.Text = conductorEdit.telefono;
                BtnAgregar.Content = "Actualizar";
               
            }
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellidos = txtApellidos.Text;
            string fechaNacimiento = txtNacimiento.Text;
            string numLicencia = txtLicencia.Text;
            string telefono = txtTelefono.Text;

            int idConductorAux = idConductor;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos) || string.IsNullOrEmpty(fechaNacimiento) || string.IsNullOrEmpty(numLicencia) || string.IsNullOrEmpty(telefono))
            {
                MessageBox.Show("Usuario y/o password Vacios...", "Error");
                return;
            }
            try
            {
                if (idConductor == 0)
                {
                    using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                    {
                        Conductor conductor = new Conductor
                        {
                            nombre = nombre,
                            apellidos = apellidos,
                            fechaNacimiento = fechaNacimiento,
                            numLicencia = numLicencia,
                            telefono = telefono
                        };
                        db.Conductor.Add(conductor);
                        db.SaveChanges();
                        MessageBox.Show("Agregado con éxito");
                        idConductorAux = idConductor;

                    }

                }
                else
                {
                    using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                    {
                        Conductor conductorEdit = db.Conductor.Find(idConductor);
                        conductorEdit.nombre = txtNombre.Text;
                        conductorEdit.apellidos = txtApellidos.Text;
                        conductorEdit.numLicencia = txtLicencia.Text;
                        conductorEdit.telefono = txtTelefono.Text;
                        conductorEdit.fechaNacimiento = txtNacimiento.Text;
                        db.Entry(conductorEdit).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Editado con éxito");
                        

                    }
                }
                this.Visibility = Visibility.Collapsed;
                this.itActualizar.Actualizar(idConductorAux);
            }
            catch
            {
                MessageBox.Show("Error, no se pudo agregar el conductor");

            }

        }
    }
}
