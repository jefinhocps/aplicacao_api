using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        // Evento de fechamento do formulário
        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit(); // Fecha a aplicação
        }

        // Evento do botão "Acessar"
        private void btnAcessar_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text; // Pega o texto do campo de login
            string senha = txtSenha.Text; // Pega o texto do campo de senha

            // Instancia a classe Usuario, sem passar login e senha no construtor
            Usuario usuario = new Usuario("localhost", "prod", "root", ""); // Passando os parâmetros da conexão com o banco

            // Valida o usuário
            bool usuarioValido = usuario.ValidarUsuario(login, senha);

            // Verifica se o login foi bem-sucedido
            if (usuarioValido)
            {
                // MessageBox.Show("Login Bem-sucedido!");
                // Você pode redirecionar para o próximo formulário ou ação
                FrmPrincipal principal = new FrmPrincipal();
                principal.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuário ou senha Inválidos!");
            }
        }
    }
}
