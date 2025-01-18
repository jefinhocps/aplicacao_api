using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void entradaDeMateriaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEntrada frmEntrada = new FrmEntrada();

            frmEntrada.ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Encerrar a aplicação
            DialogResult dialog = new DialogResult();

            dialog = MessageBox.Show("Deseja mesmo encerrar o programa?", "Alert!", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void Form1_Load_2(object sender, EventArgs e)
        {

        }

        private void ferramentasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void novoUsuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNewUsuario novoUsuario = new FrmNewUsuario();
            novoUsuario.ShowDialog();
        }
    }
}
