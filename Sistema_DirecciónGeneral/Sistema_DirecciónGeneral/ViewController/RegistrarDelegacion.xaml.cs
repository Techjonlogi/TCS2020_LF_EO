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
    /// Lógica de interacción para RegistrarDelegacion.xaml
    /// </summary>
    public partial class RegistrarDelegacion : UserControl
    {
        IDelegacion itActualizar;
        int idDelegacion = 0;
        
        public RegistrarDelegacion(int idDelegacion, IDelegacion itActualizar)
        {
            InitializeComponent();            
            LlenarMunicipios();
            this.idDelegacion = idDelegacion;
            if(this.idDelegacion > 0)
            {
                CargarDelegacion();
            }
            this.itActualizar = itActualizar;

        }

        public void CargarDelegacion()
        {
            using(SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
            {
            Delegacion delegacionEdit = db.Delegacion.Find(idDelegacion);
            txt_alias.Text = delegacionEdit.nombre;
            txt_calle.Text = delegacionEdit.calle;
            txt_numero.Text = delegacionEdit.numero;
            txt_codigoPostal.Text = delegacionEdit.codigoPostal;
            txt_colonia.Text = delegacionEdit.colonia;
            txt_correo.Text = delegacionEdit.correo;
            txt_telefono.Text = delegacionEdit.telefono;
            cbMunicipio.SelectedIndex = delegacionEdit.idMunicipio;
            btn_registrarDelegacion.Content = "Actualizar";
            }
        }

        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LlenarMunicipios()
        {
            using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
            {
                var list = db.Municipio.ToList();
                if(list.Count() > 0)
                {
                    cbMunicipio.ItemsSource = list;
                    cbMunicipio.DisplayMemberPath = "municipio1";
                    cbMunicipio.SelectedValuePath = "idMunicipio";
                }
            }
        }

        private void btn_registrarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            string alias = txt_alias.Text;
            string calle = txt_calle.Text;
            string numero = txt_numero.Text;
            string codigoPostal = txt_codigoPostal.Text;
            int idMunicipio = cbMunicipio.SelectedIndex + 1;
            string colonia = txt_colonia.Text;
            string correo = txt_correo.Text;
            string telefono = txt_telefono.Text;

            int idDelAux = idDelegacion;

            if (string.IsNullOrEmpty(alias) || string.IsNullOrEmpty(calle) || string.IsNullOrEmpty(codigoPostal) || string.IsNullOrEmpty(codigoPostal) || string.IsNullOrEmpty(colonia) || string.IsNullOrEmpty(txt_numero.Text) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(telefono) || string.IsNullOrEmpty(cbMunicipio.Text))
            {
                MessageBox.Show("Campos Vacios...", "Error");
                return;
            }
            try
            {
                if(idDelegacion == 0)
                {
                    using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                    {
                        Delegacion delegacionNew = new Delegacion
                        {
                            nombre = alias,
                            calle = calle,
                            numero = numero,
                            colonia = colonia,
                            codigoPostal = codigoPostal,
                            idMunicipio = idMunicipio ,
                            telefono = telefono,
                            correo = correo
                        };
                        db.Delegacion.Add(delegacionNew);
                        db.SaveChanges();
                        MessageBox.Show("Delegación registrada con éxito");
                        idDelAux = delegacionNew.idDelegacion;
                    }

                }
                else
                {
                    using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                    {
                        Delegacion delegacionEdit = db.Delegacion.Find(idDelegacion);
                        delegacionEdit.nombre = txt_alias.Text;
                        delegacionEdit.calle = txt_calle.Text;
                        delegacionEdit.numero = txt_numero.Text;
                        delegacionEdit.colonia = txt_colonia.Text;
                        delegacionEdit.codigoPostal = txt_codigoPostal.Text;
                        delegacionEdit.idMunicipio = cbMunicipio.SelectedIndex;
                        delegacionEdit.colonia = txt_colonia.Text;
                        delegacionEdit.correo = txt_correo.Text;
                        delegacionEdit.telefono = txt_telefono.Text;
                        db.Entry(delegacionEdit).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Delegación editada con éxito");
                    }
                }
                this.Visibility = Visibility.Collapsed;
                this.itActualizar.Actualizar(idDelAux);
                
                
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
