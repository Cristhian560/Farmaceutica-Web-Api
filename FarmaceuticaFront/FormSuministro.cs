//using PyFarmaceutica.dominio;
//using PyFarmaceutica.servicios.implementaciones;
//using PyFarmaceutica.servicios.interfaces;
using FarmaceuticaBack.dominio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyFarmaceutica.Presentacion
{
    public partial class FormSuministro : Form
    {
        //private IServiceSuministro serviceSuministro;
        List<Suministro> lista_suministros;
        public FormSuministro()
        {
            InitializeComponent();
            lista_suministros = new List<Suministro>();
            //serviceSuministro = new ServiceSuministro();
            // CargarlistaDB();
        }
        private async Task CargarlistaDB()
        {
            //lista_suministros = serviceSuministro.Suministros();
            string url = "https://localhost:44301/api/Suministros/suministros";
            HttpClient cliente = new HttpClient();
            var result = await cliente.GetAsync(url);

            var bodyJSON = await result.Content.ReadAsStringAsync();
            lista_suministros = JsonConvert.DeserializeObject<List<Suministro>>(bodyJSON);

        }
        private async void FormSuministro_Load(object sender, EventArgs e)
        {
            await CargarlistaDB();
            RecargarGrilla();
            rbtYes.Checked = true;
            btnGuardarCambios.Enabled = false;
            btnCancelar.Enabled = false;
            await CargarCombo();
            
        }

        private async Task CargarCombo()
        {
            string url = "https://localhost:44301/api/Suministros/TiposSuministros";
            HttpClient cliente = new HttpClient();
            var result = await cliente.GetAsync(url);

            var bodyJSON = await result.Content.ReadAsStringAsync();
            List<TipoSuministro> lista_tipoSuministros = JsonConvert.DeserializeObject<List<TipoSuministro>>(bodyJSON);


            cboTipo.DataSource = lista_tipoSuministros;
            cboTipo.ValueMember = "IdTipoSuministro";
            cboTipo.DisplayMember = "NombreTipoSuministro";
        }

        private void dgvSuministros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvSuministros.CurrentCell.ColumnIndex == 1)
            //{
            //    lista_suministros.RemoveAt(dgvSuministros.CurrentRow.Index);
            //    dgvSuministros.Rows.Remove(dgvSuministros.CurrentRow);
            //}
        }
        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ComprobarCampos(txtCodigo.Text) || !ComprobarCampos(txtPrecio.Text))
            {
                MessageBox.Show("EL CAMPO CODIGO O PRECIO NO PUEDE ESTAR VACIO!!");
            }
            else
            {
                foreach (Suministro item in lista_suministros)
                {
                    if (item.IdSuministro == Convert.ToInt32(txtCodigo.Text))
                    {
                        MessageBox.Show("El CODIGO INGRESADO YA EXISTE");
                        return;
                    }
                }
                var s = ConstruirObjeto();
                lista_suministros.Add(s);
                string url = "https://localhost:44301/api/Suministros/InsertarSuministro";
                HttpClient cliente = new HttpClient();
                string json = JsonConvert.SerializeObject(s);
                StringContent content = new StringContent(json, Encoding.UTF8,"application/json");
                await cliente.PostAsync(url, content);

                MessageBox.Show("EL SUMINISTRO SE AGREGO CORRECTAMENTE");

                LimpiarCampos();
                //CargarlistaDB();
                RecargarGrilla();
                //if (serviceSuministro.Insert(s))
                //{
                //    MessageBox.Show("EL SUMINISTRO SE AGREGO CORRECTAMENTE");
                //    LimpiarCampos();
                //    CargarlistaDB();
                //    CargarGrilla();
                //}
                //else
                //{
                //    MessageBox.Show("ERROR AL AGRAGAR SUMINISTRO");
                //}
            }
        }

        private bool ComprobarCampos(string campo)
        {
            if(!(campo == ""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void RecargarGrilla()
        {
            dgvSuministros.Rows.Clear();
            foreach (Suministro s in lista_suministros)
            {
                dgvSuministros.Rows.Add(new object[] { s.IdSuministro, s.Nombre, s.TipoSuministro.IdTipoSuministro, s.TipoSuministro.NombreTipoSuministro, s.Descripcion, s.VentaLibre, s.Precio });
            }
        }
        private async void dgvSuministros_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSuministros.CurrentCell.ColumnIndex == 8)
            {
                await BorrarFila();
            }
            if (dgvSuministros.CurrentCell.ColumnIndex == 7)
            {
                EditarFila();
            }
        }
        private void EditarFila()
        {
            txtCodigo.Text = dgvSuministros.CurrentRow.Cells[0].Value.ToString();
            txtSuministro.Text= dgvSuministros.CurrentRow.Cells[1].Value.ToString();
            txtDescripcion.Text = dgvSuministros.CurrentRow.Cells[4].Value.ToString();
            txtPrecio.Text = dgvSuministros.CurrentRow.Cells[6].Value.ToString();
            if (dgvSuministros.CurrentRow.Cells[5].Value.ToString()=="S")
            {
                rbtYes.Checked = true;
            }
            else
            {
                rbtNo.Checked = true;
            }
            Habilitar(false);
        }
        private async Task BorrarFila()
        {
            if (MessageBox.Show("BORRAR EL SUMINISTRO : " + dgvSuministros.CurrentRow.Cells[1].Value.ToString() + " ?", "ELIMINAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                
                string url = "https://localhost:44301/api/Suministros";
                
                HttpClient cliente = new HttpClient();
                await cliente.DeleteAsync($"{url}/{Convert.ToInt32(dgvSuministros.CurrentRow.Cells[0].Value)}");
                


                //
                //
                //VUELVE A HACER LA CONSULTA A LA BASE DE DATOS
                //await CargarlistaDB();
                //
                //



                //
                //O SOLO TRABAJAR CON LA LISTA QUE SE CARGO EN EL LOAD
                //
                int i = 0;
                foreach (Suministro item in lista_suministros)
                {
                    if(item.IdSuministro== Convert.ToInt32(dgvSuministros.CurrentRow.Cells[0].Value))
                    {
                        lista_suministros.RemoveAt(i);
                        RecargarGrilla();
                        return;
                    }
                    i++;
                }
            }
        }
        private Suministro ConstruirObjeto()
        {
            Suministro suministro = new Suministro();
            suministro.IdSuministro = Convert.ToInt32(txtCodigo.Text);
            suministro.Nombre = txtSuministro.Text;
            suministro.Descripcion = txtDescripcion.Text;
            suministro.Precio = Convert.ToDouble(txtPrecio.Text);
            //{
            //    IdSuministro = Convert.ToInt32(txtCodigo.Text),
            //    Nombre = txtSuministro.Text,
            //    Descripcion = txtDescripcion.Text,
            //    Precio = Convert.ToDouble(txtPrecio)
            //};
            if (rbtYes.Checked)
            {
                suministro.VentaLibre = "S";
            }
            else
            {
                suministro.VentaLibre = "N";
            }
            TipoSuministro tipo_suministro = (TipoSuministro)cboTipo.SelectedItem;
            suministro.TipoSuministro = tipo_suministro;

            return suministro;
        }
        private void LimpiarCampos()
        {
            txtCodigo.Text = "";
            txtSuministro.Text = "";
            txtPrecio.Text = "";
            txtDescripcion.Text = "";
        }
        private void Habilitar(bool x)
        {
            txtCodigo.Enabled = x;
            dgvSuministros.Enabled = x;
            btnGuardarCambios.Enabled = !x;
            btnAgregar.Enabled = x;
            btnCancelar.Enabled = !x;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (X(txtCodigo.Text))
            {
                
            }
            else
            {
                MessageBox.Show("CODIGO INCORRECTO");
                txtCodigo.Text = "";
            }
        }
        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            if (X(txtPrecio.Text))
            {

            }
            else
            {
                MessageBox.Show("PRECIO INCORRECTO");
                txtPrecio.Text = "";
            }
        }
        private bool X(string campo)
        {
            bool b = false;
            if (campo!="")
            {
                for (int i = 0; i < campo.Length; i++)
                {
                    if (Char.IsDigit(campo, i))
                    {
                        b = true;
                    }
                    else
                    {
                        b = false;
                    }
                }
            }
            else
            {
                b = true;
            }
            return b;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Habilitar(true);
            LimpiarCampos();
        }
        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text != "")
            {
                string h = txtFiltro.Text.ToLower();
                dgvSuministros.Rows.Clear();
                foreach (Suministro s in lista_suministros)
                {
                    bool x = s.Nombre.ToLower().Contains(h);
                    if (x)
                    {
                        dgvSuministros.Rows.Add(new object[] { s.IdSuministro, s.Nombre, s.TipoSuministro.IdTipoSuministro, s.TipoSuministro.NombreTipoSuministro, s.Descripcion, s.VentaLibre, s.Precio });
                    }
                }
            }
            else
            {
                dgvSuministros.Rows.Clear();
                foreach (Suministro s in lista_suministros)
                {
                    dgvSuministros.Rows.Add(new object[] { s.IdSuministro, s.Nombre, s.TipoSuministro.IdTipoSuministro, s.TipoSuministro.NombreTipoSuministro, s.Descripcion, s.VentaLibre, s.Precio });
                }
            }
        }
        private async void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (ComprobarCampos(txtPrecio.Text))
            {
                dgvSuministros.Enabled = true;
                try
                {
                    var s = ConstruirObjeto();
                    lista_suministros.Add(s);
                    string url = "https://localhost:44301/api/Suministros/ActualizarSuministro";
                    HttpClient cliente = new HttpClient();
                    string json = JsonConvert.SerializeObject(s);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    await cliente.PutAsync(url, content);
                    MessageBox.Show("LOS CAMBIOS HAN SIDO GUARDADOS");
                }
                catch (Exception)
                {
                    MessageBox.Show("ERROR AL GUARDAR CAMBIOS");
                }
                //if (serviceSuministro.Update(ConstruirObjeto()))
                //{
                //    MessageBox.Show("LOS CAMBIOS HAN SIDO GUARDADOS");
                //}
                //else
                //{
                //    MessageBox.Show("ERROR AL GUARDAR CAMBIOS");
                //}
                //dgvSuministros.Rows.Clear();
                await CargarlistaDB();
                RecargarGrilla();
                Habilitar(true);
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("EL CAMPO PRECIO NO PUEDE ESTAR VACIO");
            }
        }
    }
}
