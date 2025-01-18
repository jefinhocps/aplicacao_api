using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public class Usuario
    {
        private string connectionString;
        
        // Construtor para a classe Usuario, configurando a conexão com o banco
        public Usuario(string servidor, string banco, string usuario, string senha)
        {
            connectionString = $"Server={servidor};Database={banco};Uid={usuario};Pwd={senha};";
        }

        // Método para validar o usuário com login e senha
        public bool ValidarUsuario(string nomeUsuario, string senha)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Consulta para verificar o usuário no banco de dados
                    string query = "SELECT COUNT(*) FROM usuario WHERE login = @nomeUsuario AND senha = @senha AND status != 'B';";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nomeUsuario", nomeUsuario);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    // Executa a consulta e verifica se encontrou o usuário
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0; // Se count for maior que 0, o usuário existe
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao validar usuário: " + ex.Message);
                return false;
            }
        }

        // método para retornar a tabela de usuarios
        public void GetUsuario(DataGridView dataGridView) {
            try
            {

                // Criando a conexão
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Criando a consulta SQL
                    string consultaSQL = "SELECT nome_completo, telefone, email, login, status FROM usuario;";


                    // Executando a consulta e obtendo o leitor de dados
                    MySqlCommand cmd = new MySqlCommand(consultaSQL, conn);
                    MySqlDataReader leitor = cmd.ExecuteReader();

                    // Criando e populando o DataTable
                    DataTable dt = new DataTable();
                    dt.Load(leitor);

                    // Definindo o DataSource do DataGridView
                    dataGridView.DataSource = dt;

                    // Fechando o leitor após o uso
                    leitor.Close();
                }
            }
            catch (Exception ex)
            {
                // Exibindo uma mensagem de erro caso ocorra
                MessageBox.Show("Erro ao carregar os usuários do banco: " + ex.Message);
            }
        }

                

                

        }
    
}
