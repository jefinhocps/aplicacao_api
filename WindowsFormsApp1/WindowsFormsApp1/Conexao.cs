using System;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public class Conexao
    {
        private string connectionString;
        private MySqlConnection conexao;

        // Construtor que recebe a string de conexão
        public Conexao(string servidor, string banco, string usuario, string senha)
        {
            // String de conexão com o banco de dados
            connectionString = $"Server={servidor};Database={banco};Uid={usuario};Pwd={senha};";
            conexao = new MySqlConnection(connectionString);
        }

        // Método para abrir a conexão
        public void AbrirConexao()
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                {
                    conexao.Open();
                    Console.WriteLine("Conexão aberta com sucesso!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao abrir a conexão: " + ex.Message);
            }
        }

        // Método para fechar a conexão
        public void FecharConexao()
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Closed)
                {
                    conexao.Close();
                    Console.WriteLine("Conexão fechada com sucesso!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao fechar a conexão: " + ex.Message);
            }
        }

        // Método para executar comandos SQL que não retornam dados (como INSERT, UPDATE, DELETE)
        public void ExecutarComando(string comandoSQL)
        {
            try
            {
                AbrirConexao();

                MySqlCommand comando = new MySqlCommand(comandoSQL, conexao);
                comando.ExecuteNonQuery();
                Console.WriteLine("Comando executado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao executar comando: " + ex.Message);
            }
            finally
            {
                FecharConexao();
            }
        }

        // Método para executar consultas SQL que retornam dados (como SELECT)
        public MySqlDataReader ExecutarConsulta(string consultaSQL)
        {
            try
            {
                AbrirConexao();

                MySqlCommand comando = new MySqlCommand(consultaSQL, conexao);
                MySqlDataReader leitor = comando.ExecuteReader();

                return leitor; // Retorna o leitor para ser lido no formulário
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao executar consulta: " + ex.Message);
                return null;
            }
        }
    }
}
