using PiramideRectangularFinalProg.Entidades;

namespace PiramideRectangularFinalProg.Windows
{
    public partial class frmPiramideAE : Form
    {
        private PiramideRegular? piramide;
        public frmPiramideAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (piramide != null)
            {
                txtLado.Text = piramide.LadoBase.ToString();
                txtAltura.Text = piramide.Altura.ToString();
                if (piramide.Material == Material.Plastico)
                {
                    rbtPlastico.Checked = true;
                }
                else if (piramide.Material == Material.Vidrio)
                {
                    rbtVidrio.Checked = true;
                }
                else
                {
                    rbtMadera.Checked = true;
                }
            }
        }
        public PiramideRegular? GetPiramide()
        {
            return piramide;
        }

        public void SetPiramide(PiramideRegular piramide)
        {
            this.piramide = piramide;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (piramide is null)
                {
                    piramide = new PiramideRegular();
                }
                piramide.LadoBase=int.Parse(txtLado.Text);
                piramide.Altura=int.Parse(txtAltura.Text);
                if (rbtMadera.Checked)
                {
                    piramide.Material = Material.Madera;
                }
                else if (rbtVidrio.Checked)
                {
                    piramide.Material = Material.Vidrio;
                }
                else
                {
                    piramide.Material = Material.Plastico;
                }
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (!int.TryParse(txtAltura.Text,out int altura) || altura <= 0)
            {
                valido = false;
                errorProvider1.SetError(txtAltura,"Altura no válida o mal ingresada");
            }
            if (!int.TryParse(txtLado.Text, out int lado) || lado <= 0)
            {
                valido = false;
                errorProvider1.SetError(txtAltura, "Lado no válido o mal ingresado");
            }
            return valido;
        }
    }
}
