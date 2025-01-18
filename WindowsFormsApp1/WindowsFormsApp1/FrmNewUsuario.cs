using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class FrmNewUsuario : Form
    {
        public FrmNewUsuario()
        {
            InitializeComponent();
        }

        private void FrmNewUsuario_Load(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario("localhost", "prod", "root", "");
            usuario.GetUsuario(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Método para cadastrar novo usuário
        public void CadastrarUsuario()
        {
            try
            {
                string nome_completo = txtNome.Text;
                string telefone = txtTelefone.Text;
                string endereco = txtEndereco.Text;
                string numero = txtNumero.Text;
                string email = txtEmail.Text;
                string login = txtLogin.Text;
                string senha = txtSenha.Text;
                string status = "A";

                // Insere os dados no banco
                string query = $"insert into usuario (nome_completo, telefone, endereco, numero, email, login, senha, status) values ('{nome_completo}','{telefone}','{endereco}',{numero},'{email}','{login}','{senha}', '{status}')";

                Conexao conexao = new Conexao("localhost", "prod", "root", "");
                conexao.ExecutarComando(query);

                MessageBox.Show("Usuário Adicionado com sucesso!");

                Usuario u = new Usuario("localhost", "prod", "root", "");
                u.GetUsuario(dataGridView1);

            }

            catch (Exception ex) { 
                MessageBox.Show("Erro ao inserir usuário no banco..." + ex.Message);
            }            

        }

        string connectionString = "Server=localhost;Database=prod;Uid=root;Pwd=;";

        public bool BloqueiaUsuario()
        {
            try
            {
                string login = txtLogin.Text;

                using (MySqlConnection conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();
                    string query = $"update usuario set status = 'B' where login = '{login}'";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);

                    cmd.Parameters.AddWithValue("@loginUsuario", login);
                    int result = cmd.ExecuteNonQuery();
                    MessageBox.Show("O usuário: " + login + ", foi bloqueado com sucesso!");

                    return result > 0;
                }
            }
            catch (Exception ex){
                MessageBox.Show("Erro ao Bloquear usuário" + ex.Message);
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CadastrarUsuario();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BloqueiaUsuario();
        }
    }
}
