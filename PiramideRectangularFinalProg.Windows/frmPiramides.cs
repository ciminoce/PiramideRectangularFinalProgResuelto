using PiramideRectangularFinalProg.Datos;
using PiramideRectangularFinalProg.Entidades;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace PiramideRectangularFinalProg.Windows
{
    public partial class frmPiramides : Form
    {
        private RepositorioDePiramides? repositorio;
        private int cantidadRegistros;
        private List<PiramideRegular>? lista;
        public frmPiramides()
        {
            InitializeComponent();
            repositorio = new RepositorioDePiramides();
        }


        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmPiramideAE frm = new frmPiramideAE() { Text = "Agregar Piramide" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) return;
            PiramideRegular? piramide = frm.GetPiramide();
            try
            {
                if (!repositorio!.ExistePiramide(piramide!))
                {
                    repositorio.AgregarPiramide(piramide!);
                    DataGridViewRow r = ConstruirFila(dgvDatos);
                    SetearFila(r, piramide!);
                    AgregarFila(r, dgvDatos);
                    MessageBox.Show("Registro agregado", "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {

                    MessageBox.Show("Registro existente!!!", "Error",
        MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Algún error!!!", "Error",
    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void AgregarFila(DataGridViewRow r, DataGridView dgv)
        {
            dgv.Rows.Add(r);
        }

        public void LimpiarGrilla(DataGridView grid)
        {
            grid.Rows.Clear();
        }
        public DataGridViewRow ConstruirFila(DataGridView grid)
        {
            var r = new DataGridViewRow();
            r.CreateCells(grid);
            return r;
        }

        public void SetearFila(DataGridViewRow r, PiramideRegular obj)
        {
            r.Cells[0].Value = obj.LadoBase;
            r.Cells[1].Value = obj.Altura;
            r.Cells[2].Value = obj.Material;
            r.Cells[3].Value = obj.GetVolumen().ToString("N2");
            r.Cells[4].Value = obj.GetArea().ToString("N2");

            r.Tag = obj;
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            DataGridViewRow r = dgvDatos.SelectedRows[0];
            PiramideRegular piramide = (PiramideRegular)r.Tag!;
            DialogResult dr = MessageBox.Show("¿Desea borrar la piramide?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            try
            {
                repositorio!.EliminarPiramide(piramide);
                EliminarFila(r, dgvDatos);
                cantidadRegistros = repositorio!.GetCantidad();
                MessageBox.Show("Registro agregado", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            catch (Exception)
            {

                MessageBox.Show("Algún error!!!", "Error",
    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void EliminarFila(DataGridViewRow r, DataGridView grid)
        {
            grid.Rows.Remove(r);
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            DataGridViewRow r = dgvDatos.SelectedRows[0];
            PiramideRegular? piramide = (PiramideRegular)r.Tag!;
            frmPiramideAE frm = new frmPiramideAE() { Text = "Editar Piramide" };
            frm.SetPiramide(piramide);
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            try
            {
                piramide = frm.GetPiramide();
                SetearFila(r, piramide);
                MessageBox.Show("Registro editado", "Mensaje",
    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {

                MessageBox.Show("Algún error!!!", "Error",
MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void MostrarDatosGrilla()
        {
            LimpiarGrilla(dgvDatos);
            foreach (var item in lista!)
            {
                var r = ConstruirFila(dgvDatos);
                SetearFila(r, item);
                AgregarFila(r, dgvDatos);
            }
        }

        private void CargarComboMateriales(ref ToolStripComboBox cbo)
        {
            var lista = Enum.GetValues(typeof(Material));
            foreach (var item in lista)
            {
                cbo.Items.Add(item);
            }
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.SelectedIndex = 0;
        }

        private void tsCboBordes_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void área09ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lista = repositorio!.OrdenarAsc();
            MostrarDatosGrilla();
        }

        private void área90ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lista = repositorio!.OrdenarDesc();
            MostrarDatosGrilla();

        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            lista = repositorio!.GetPiramides();
            cantidadRegistros = repositorio.GetCantidad();
            MostrarDatosGrilla();
        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
            repositorio!.GuardarDatos();
            MessageBox.Show("Fin del Programa", "Mensaje",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Exit();
        }

        private void frmPiramides_Load(object sender, EventArgs e)
        {
            CargarComboMateriales(ref tcboMateriales);
            cantidadRegistros = repositorio!.GetCantidad();
            if (cantidadRegistros > 0)
            {
                lista = repositorio.GetPiramides();
                MostrarDatosGrilla();
                MostrarCantidadRegistros();
            }

        }

        private void MostrarCantidadRegistros()
        {
            txtCantidad.Text = cantidadRegistros.ToString();
        }

        private void tcboMateriales_SelectedIndexChanged(object sender, EventArgs e)
        {
            var materialSeleccionado = (Material)tcboMateriales.SelectedItem!;
            lista = repositorio!.Filtrar(materialSeleccionado);
            cantidadRegistros = repositorio!.GetCantidad(materialSeleccionado);
            MostrarDatosGrilla();
            MostrarCantidadRegistros();

        }
    }
}
